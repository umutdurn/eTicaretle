using Core.Models;
using Core.Repositories;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Services;
using Service.Services.Calculate;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using Web.Models;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using Service.Services.Payment;
using Service.Services.Mail;
using IPara.DeveloperPortal.Core.Entity;
using Payment = Core.Models.Payment;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeColumnService _homeColumnService;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly ICargoService _cargoService;
        private readonly ICountryService _countryService;
        private readonly IService<City> _cityService;
        private readonly IService<District> _districtService;
        private readonly IService<Payment> _paymentService;
        private readonly IService<Installment> _installmentService;
        private readonly IService<OrderSituation> _orderSituationService;
        private readonly IBankTransferService _bankService;
        private readonly IOrderService _orderService;


        public HomeController(ILogger<HomeController> logger, IHomeColumnService columnService, IProductService productService, ICartService cartService, ICargoService cargoService, ICountryService countryService, IService<City> cityService, IService<District> districtService, IService<Payment> paymentService, IService<Installment> installmentService, IBankTransferService bankService, IOrderService orderService, IService<OrderSituation> orderSituationService)
        {
            _logger = logger;
            _homeColumnService = columnService;
            _productService = productService;
            _cartService = cartService;
            _cargoService = cargoService;
            _countryService = countryService;
            _cityService = cityService;
            _districtService = districtService;
            _paymentService = paymentService;
            _installmentService = installmentService;
            _bankService = bankService;
            _orderService = orderService;
            _orderSituationService = orderSituationService;
        }

        public IActionResult Index()
        {
            var homeColumn = _homeColumnService.GetAllHomecolumn();

            ViewBag.HomeColumn = homeColumn;

            return View();
        }

        [Route("urunler")]
        public IActionResult ProductList() {

            var products = _productService.GetAllProductsList();

            return View(products);
        
        }

        [Route("urun/{seoUrl}")]
        public IActionResult Product(string seoUrl)
        {

            var product = _productService.GetProductBySeoUrl(seoUrl);

            var randomProduct = _productService.RandomProductListTake5();

            ViewBag.RandomProduct = randomProduct;

            return View(product);

        }
        [HttpPost]
        public async Task<string> AddToBasket(string situation, int product, int piece) {

            try
            {
                var getProduct = await _productService.FirstOfDefaultAsync(x => x.Id == product);
                var ifCart = false;

                if (Request.Cookies["CookieId"] != null)
                {
                    var getCart = await _cartService.Where(x => x.CookieId == Request.Cookies["CookieId"].ToString());

                    if (getCart.Count() > 0)
                    {
                        ifCart = true;

                        foreach (var cart in getCart)
                        {
                            if (cart.Product.Id == getProduct.Id)
                            {
                                var getPiece = cart.Piece;
                                var totalPiece = piece + getPiece;

                                cart.Piece = totalPiece;

                                _cartService.Update(cart);

                                break;
                            }
                        }
                    }

                }

                if (!ifCart)
                {
                    var cookieId = Guid.NewGuid().ToString();

                    CookieOptions cookie = new CookieOptions();
                    cookie.Expires = DateTime.Now.AddMonths(1);
                    Response.Cookies.Append("CookieId", cookieId, cookie);

                    Cart newCart = new Cart();
                    newCart.Product = getProduct;
                    newCart.Piece = piece;
                    newCart.CookieId = cookieId;

                    await _cartService.AddAsync(newCart);
                }

                if (situation == "buyNow")
                {
                    return situation;
                }
                else
                {
                    return "1";
                }

                
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        
        }
        [Route("sepet")]
        public IActionResult Cart() {


            // Country 
            var country = _countryService.GetAllIncludeCountry();

            List<SelectListItem> countryList = new List<SelectListItem>();
            foreach (var item in country)
            {
                countryList.Add(new SelectListItem { Text = item.Title, Value = item.Id.ToString() });
            }

            ViewBag.Country = countryList;

            // Payment
            var payments = _paymentService.GetAll().ToList();

            ViewBag.Payments = payments;

            // City 
            var city = _cityService.GetAll().ToList();

            List<SelectListItem> cityList = new List<SelectListItem>();
            foreach (var item in city)
            {
                cityList.Add(new SelectListItem { Text = item.Title, Value = item.Id.ToString() });
            }

            ViewBag.City = cityList;

            // Bank Transfer
            var bankTransfer = _bankService.GetAllBankTransfer();

            List<SelectListItem> listBankTransfer = new List<SelectListItem>();

            foreach (var bank in bankTransfer)
            {
                listBankTransfer.Add(new SelectListItem { Text = bank.Bank.Name, Value = bank.Id.ToString() });
            }

            ViewBag.BankTransfer = listBankTransfer;

            return View();
        
        }

        [HttpPost]
        public string PieceCount() {

            if (Request.Cookies["CookieId"] != null)
            {
                var cartList = _cartService.GetAllCartInclude(Request.Cookies["CookieId"].ToString());

                int totalpiece = 0;

                foreach (var cart in cartList)
                {
                    totalpiece += cart.Piece;
                }

                return totalpiece.ToString();
            }
            else
            {
                return "0";
            }
        
        }

        [HttpPost]
        public IActionResult GetCartTable(string process, int piece, int cartId)
        {

            CalculateModel carts = new CalculateModel();

            if (Request.Cookies["CookieId"] != null)
            {
                var getCarts = _cartService.GetAllCartInclude(Request.Cookies["CookieId"].ToString()).ToList();

                if (process == "update")
                {
                    foreach (var item in getCarts)
                    {
                        if (item.Id == cartId)
                        {
                            item.Piece = piece;
                        }

                        _cartService.Update(item);
                    }
                }

                CalculateService calcService = new CalculateService();

                carts = calcService.CartCalculate(getCarts,null,null);
            }

            return PartialView("~/Views/Home/_PartialView/_CartTable.cshtml", carts);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveCart(int cartId) {

            CalculateModel carts = new CalculateModel();

            var removeCart = await _cartService.FirstOfDefaultAsync(x => x.Id == cartId);

            _cartService.Remove(removeCart);

            var getCarts = _cartService.GetAllCartInclude(Request.Cookies["CookieId"].ToString()).ToList();

            CalculateService calcService = new CalculateService();

            carts = calcService.CartCalculate(getCarts, null, null);

            return PartialView("~/Views/Home/_PartialView/_CartTable.cshtml", carts);
        }

        [HttpPost]
        public JsonResult Getcargos(int id) {

            var cargos = _cargoService.GetAllCargosForCountry(id);

            return Json(cargos);
        
        }
        [HttpPost]
        public JsonResult PayPrice(string coupon, string cargo, float paymentPrice) {

            CalculateModel carts = new CalculateModel();

            Cargo getCargo = null;

            if (!String.IsNullOrEmpty(cargo))
            {
                getCargo = _cargoService.GetSingleCargo(Convert.ToInt32(cargo));
            }

            var getCarts = _cartService.GetAllCartInclude(Request.Cookies["CookieId"].ToString()).ToList();

            CalculateService calcService = new CalculateService();

            carts = calcService.CartCalculate(getCarts, getCargo, paymentPrice);

            return Json(carts);

        }
        [HttpPost]
        public JsonResult GetDistrict(int id) {

            var districts = _districtService.Where(x => x.City.Id == id).Result;

            return Json(districts);

        }
        [HttpPost]
        public IActionResult GetInstallment(string coupon, string cargo, float paymentPrice)
        {

            CalculateModel carts = new CalculateModel();

            Cargo getCargo = null;

            if (!String.IsNullOrEmpty(cargo))
            {
                getCargo = _cargoService.GetSingleCargo(Convert.ToInt32(cargo));
            }

            var getCarts = _cartService.GetAllCartInclude(Request.Cookies["CookieId"].ToString()).ToList();

            CalculateService calcService = new CalculateService();

            carts = calcService.CartCalculate(getCarts, getCargo, paymentPrice);

            var getInstallment = _installmentService.GetAll().ToList();

            var calcInstallment = calcService.InstallmentCalculate(getInstallment, carts);

            return PartialView("~/Views/Home/_PartialView/_Installment.cshtml", calcInstallment);
        }

        [HttpPost]
        public async Task<JsonResult> GetBankTransfer(int id) {

            var bankTransfer = await _bankService.FirstOfDefaultAsync(x => x.Id == id);

            return Json(bankTransfer);

        }
        [HttpPost]
        public async Task<string> GetPaymentInformation(int id) {

            var payment = await _paymentService.FirstOfDefaultAsync(x => x.Id == id);

            return payment.Information;


        }
        [HttpPost]
        public async Task<string> CreateOrder(string coupon, string cargo, float paymentPrice, int paymentId)
        {
            CalculateModel carts = new CalculateModel();

            Cargo getCargo = null;

            if (!String.IsNullOrEmpty(cargo))
            {
                getCargo = _cargoService.GetSingleCargo(Convert.ToInt32(cargo));
            }

            var getCarts = _cartService.GetAllCartInclude(Request.Cookies["CookieId"].ToString()).ToList();

            CalculateService calcService = new CalculateService();

            carts = calcService.CartCalculate(getCarts, getCargo, paymentPrice);

            var city = await _cityService.FirstOfDefaultAsync(x => x.Id == Convert.ToInt32(Request.Form["City"].ToString()));
            var district = await _districtService.FirstOfDefaultAsync(x => x.Id == Convert.ToInt32(Request.Form["District"].ToString()));
            var orderSituation = await _orderSituationService.FirstOfDefaultAsync(x => x.Id == 1);
            var payment = await _paymentService.FirstOfDefaultAsync(x => x.Id == paymentId);
            var guid = Guid.NewGuid().ToString("N").Substring(0, 9);

            Order order = new Order();
            order.OrderNote = Request.Form["OrderNote"].ToString();
            order.GiftBox = Convert.ToBoolean(Request.Form["GiftBox"].ToString());
            order.GiftTextOne = Request.Form["giftNoteTop"].ToString();
            order.GiftTextTwo = Request.Form["giftNoteBottom"].ToString();
            order.OrderDate = DateTime.Now;
            order.Cargo = getCargo;
            order.OrderSituation = orderSituation;
            order.Payment = payment;
            order.OrderId = guid;
            order.TotalPrice = carts.GeneralTotal;
            order.Cart = getCarts;
            order.Name = Request.Form["Name"].ToString();
            order.Surname = Request.Form["Surname"].ToString();
            order.GSM = Request.Form["GSM"].ToString();
            order.Email = Request.Form["Email"].ToString();
            order.City = city;
            order.District = district;
            order.Adress = Request.Form["Adress"].ToString();

            if (Request.Form["PayAtDoor"].ToString() != "0")
            {
                order.PaymentAtDoorType = Request.Form["PayAtDoor"].ToString();
            }

            await _orderService.AddAsync(order);

            CookieOptions cookie = new CookieOptions();
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Append("CookieId", "", cookie);

            if (paymentId == 1) // Kredi Kartı
            {

                PaymentModel model = new PaymentModel();
                model.Total = carts.GeneralTotal;
                model.CardHolderName = Request.Form["CardHolderName"].ToString();
                model.CardNumber = Request.Form["CardNumber"].ToString().Replace(" ","");
                model.ExpirateDateMonth = Request.Form["CardExpireDateMonth"].ToString();
                model.ExpirateDateYear = Request.Form["CardExpireDateYear"].ToString();
                model.CardCVV2 = Request.Form["CardCVV2"].ToString();
                model.MerchantOrderId = guid;
                model.Installment = Convert.ToInt32(Request.Form["Installment"].ToString());
                model.IpAdress = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                model.OkUrl = $"{Request.Scheme}://{Request.Host}/odeme-onaylandi?nameSurname=" + order.Name + " " + order.Surname + "&orderNo=" + order.OrderId;
                model.FailUrl = $"{Request.Scheme}://{Request.Host}/odeme-hatali?orderNo=" + order.OrderId;

                var response = "";

                PaymentService paymentService = new PaymentService();

                if (model.Installment == 1)
                {
                    response = paymentService.CartPayment(model);
                }
                else
                {
                    List<IPara.DeveloperPortal.Core.Entity.Product> products = new List<IPara.DeveloperPortal.Core.Entity.Product>();

                    foreach (var cart in getCarts)
                    {
                        products.Add(new IPara.DeveloperPortal.Core.Entity.Product { Title = cart.Product.Title, Price = cart.Product.Price.ToString(), Quantity = cart.Piece, Code = "IPK" + cart.Product.Id });
                    }

                    response = paymentService.PaytrPayment(model, products, order);

                }

                return response;
            }
            else
            {
                return order.Id.ToString();
            }

            MailModel mailModel = new MailModel();
            mailModel.From = "info@ipeksalevi.com";
            mailModel.SendMail = order.Email;
            mailModel.Subject = "İpek Şal Evi | Yeni Sipariş";
            mailModel.Title = "İpek Şal Evi | Yeni Sipariş";
            mailModel.OrderNo = guid;

            MailService mailService = new MailService();

            try
            {
               
                mailService.OrderMail(mailModel);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "0";
                throw;
            }
        }
        [Route("odeme-onaylandi")]
        public async Task<IActionResult> ReturnApproval() {

            var order = await _orderService.FirstOfDefaultAsync(x => x.OrderId == Request.Query["orderNo"].ToString());
            order.PaymentResult = Request.Form["result"].ToString();

            _orderService.Update(order);

            return View();
        
        }
        [Route("odeme-hatali")]
        public async Task<IActionResult> ReturnError()
        {

            var order = await _orderService.FirstOfDefaultAsync(x => x.OrderId == Request.Query["orderNo"].ToString());

            order.PaymentErrorCode = Request.Form["errorCode"].ToString();
            order.PaymentErrorMessage = Request.Form["errorMessage"].ToString();
            order.PaymentResult = Request.Form["result"].ToString();

            _orderService.Update(order);

            return View();

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
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

        public HomeController(ILogger<HomeController> logger, IHomeColumnService columnService, IProductService productService, ICartService cartService, ICargoService cargoService, ICountryService countryService, IService<City> cityService, IService<District> districtService)
        {
            _logger = logger;
            _homeColumnService = columnService;
            _productService = productService;
            _cartService = cartService;
            _cargoService = cargoService;
            _countryService = countryService;
            _cityService = cityService;
            _districtService = districtService;
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

            // City 
            var city = _cityService.GetAll().ToList();

            List<SelectListItem> cityList = new List<SelectListItem>();
            foreach (var item in city)
            {
                cityList.Add(new SelectListItem { Text = item.Title, Value = item.Id.ToString() });
            }

            ViewBag.City = cityList;


            return View();
        
        }

        [HttpPost]
        public string PieceCount() {

            if (Request.Cookies["CookieId"] != null)
            {
                var cartList = _cartService.GetAll();

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

                carts = calcService.CartCalculate(getCarts,null);
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

            carts = calcService.CartCalculate(getCarts, null);

            return PartialView("~/Views/Home/_PartialView/_CartTable.cshtml", carts);
        }

        [HttpPost]
        public JsonResult Getcargos(int id) {

            var cargos = _cargoService.GetAllCargosForCountry(id);

            return Json(cargos);
        
        }
        [HttpPost]
        public JsonResult PayPrice(string coupon, string cargo) {

            CalculateModel carts = new CalculateModel();

            Cargo getCargo = null;

            if (!String.IsNullOrEmpty(cargo))
            {
                getCargo = _cargoService.GetSingleCargo(Convert.ToInt32(cargo));
            }

            var getCarts = _cartService.GetAllCartInclude(Request.Cookies["CookieId"].ToString()).ToList();

            CalculateService calcService = new CalculateService();

            carts = calcService.CartCalculate(getCarts, getCargo);

            return Json(carts);

        }
        [HttpPost]
        public JsonResult GetDistrict(int id) {

            var districts = _districtService.Where(x => x.City.Id == id).Result;

            return Json(districts);

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
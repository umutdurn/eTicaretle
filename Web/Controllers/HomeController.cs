using Core.Models;
using Core.Repositories;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
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

        public HomeController(ILogger<HomeController> logger, IHomeColumnService columnService, IProductService productService, ICartService cartService)
        {
            _logger = logger;
            _homeColumnService = columnService;
            _productService = productService;
            _cartService = cartService;
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

                carts = calcService.CartCalculate(getCarts);
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

            carts = calcService.CartCalculate(getCarts);

            return PartialView("~/Views/Home/_PartialView/_CartTable.cshtml", carts);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
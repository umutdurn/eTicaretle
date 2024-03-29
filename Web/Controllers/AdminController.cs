﻿using Core.Models;
using Core.Services;
using IPara.DeveloperPortal.Core.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace Web.Controllers
{
    public class AdminController : Controller
    {
        private IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private readonly IService<Colors> _colorService;
        private readonly IService<Galleries> _imageGallery;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IService<OrderSituation> _orderSituationService;
        private readonly IService<Coupons> _couponsService;
        private readonly IReturnOrderService _returnOrderService;
        private readonly IService<Comments> _commentsService;
        private readonly IService<Screen> _screenService;
        private readonly IColumnDetailService _columnDetailService;
        private readonly IHomeColumnService _homeColumnService;


        public AdminController(IWebHostEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, IService<Colors> colorService, IProductService productService, IService<Galleries> imageGallery, IOrderService orderService, IService<OrderSituation> orderSituationService, IReturnOrderService returnOrderService, IService<Coupons> couponsService, IService<Comments> commentsService, IHomeColumnService homeColumnService, IService<Screen> screenService, IColumnDetailService columnDetailService)
        {
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _colorService = colorService;
            _productService = productService;
            _imageGallery = imageGallery;
            _orderService = orderService;
            _orderSituationService = orderSituationService;
            _returnOrderService = returnOrderService;
            _couponsService = couponsService;
            _commentsService = commentsService;
            _homeColumnService = homeColumnService;
            _screenService = screenService;
            _columnDetailService = columnDetailService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddProducts()
        {
            _session.Remove("uploadImagePc");
            _session.Remove("uploadImagemobile");

            var colors = _colorService.GetAll();

            List<SelectListItem> colorsList = new List<SelectListItem>();

            foreach (var color in colors)
            {
                colorsList.Add(new SelectListItem { Text = color.Title, Value = color.Id.ToString() });
            }

            ViewBag.Colors = colorsList;

            return View();
        }
        public async Task<IActionResult> UpdateProduct(int id)
        {
            _session.Remove("uploadImagePc");
            _session.Remove("uploadImagemobile");

            var product = _productService.UpdateProductGetAllInclude(id);

            var colors = _colorService.GetAll();

            List<SelectListItem> colorsList = new List<SelectListItem>();

            foreach (var color in colors)
            {
                colorsList.Add(new SelectListItem { Text = color.Title, Value = color.Id.ToString() });
            }

            ViewBag.Colors = colorsList;

            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(Core.Models.Product product, IFormFile fileImage)
        {

            // Situation

            if (Request.Form["Situation"].ToString() == "on")
            {

                product.Situation = true;

            }

            string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "tema/images/upload");

            string imageTitle = "";
            if (fileImage != null)
            {

                imageTitle = Guid.NewGuid().ToString() + Path.GetExtension(fileImage.FileName);

                if (fileImage.Length > 0)
                {
                    string filePath = Path.Combine(uploads, imageTitle);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await fileImage.CopyToAsync(fileStream);
                    }

                    product.FeaturedImage = imageTitle;
                }

            }

            // Color
            var colors = Request.Form["Color"];

            List<Colors> productColors = new List<Colors>();

            foreach (var color in colors)
            {
                var EFColor = await _colorService.FirstOfDefaultAsync(x => x.Id == Convert.ToInt32(color));

                productColors.Add(EFColor);
            }

            // Images
            // PC

            List<Galleries> productGallery = new List<Galleries>();

            if (_session.GetString("uploadImagePc") != null)
            {
                string[] imagesPc = _session.GetString("uploadImagePc").Split(',');

                foreach (var image in imagesPc)
                {
                    productGallery.Add(new Galleries { Image = image, Mobile = false, Product = product });
                }
            }

            // Mobile

            if (_session.GetString("uploadImageMobile") != null)
            {
                string[] imagesMobile = _session.GetString("uploadImageMobile").Split(',');

                foreach (var image in imagesMobile)
                {
                    productGallery.Add(new Galleries { Image = image, Mobile = true, Product = product });
                }
            }

            product.Colors = productColors;
            product.Gallery = productGallery;

            await _productService.AddAsync(product);

            return RedirectToAction(nameof(UpdateProduct), new { id = product.Id });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(Core.Models.Product getProduct, IFormFile fileImage)
        {

            var product = _productService.UpdateProductGetAllInclude(Convert.ToInt32(Request.Form["Id"]));

            product.Title = getProduct.Title;
            product.Explanation = getProduct.Explanation;
            product.Price = getProduct.Price;
            product.DiscountPrice = getProduct.DiscountPrice;
            product.Stock = getProduct.Stock;
            product.Arrangement = getProduct.Arrangement;
            product.SeoTitle = getProduct.SeoTitle;
            product.SeoDescpription = getProduct.SeoDescpription;
            product.SeoUrl = getProduct.SeoUrl;

            // Situation

            if (Request.Form["Situation"].ToString() == "on")
            {

                product.Situation = true;

            }
            else
            {
                product.Situation = false;
            }

            string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "tema/images/upload");

            string imageTitle = "";
            if (fileImage != null)
            {

                imageTitle = Guid.NewGuid().ToString() + Path.GetExtension(fileImage.FileName);

                if (fileImage.Length > 0)
                {
                    string filePath = Path.Combine(uploads, imageTitle);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await fileImage.CopyToAsync(fileStream);
                    }

                    product.FeaturedImage = imageTitle;
                }

            }

            // Color
            var colors = Request.Form["Color"];

            List<Colors> productColors = product.Colors.ToList();
            List<Colors> formProductColors = new List<Colors>();

            foreach (var color in colors)
            {
                var EFColor = await _colorService.FirstOfDefaultAsync(x => x.Id == Convert.ToInt32(color));

                formProductColors.Add(EFColor);
            }

            List<Colors> tempProductColors = productColors.Except(formProductColors).ToList();
            List<Colors> tempFormProductColors = formProductColors.Except(productColors).ToList();

            // Remove Color
            foreach (var color in tempProductColors)
            {
                productColors.Remove(color);
            }

            // Add Color
            foreach (var color in tempFormProductColors)
            {
                productColors.Add(color);
            }

            // Images
            // PC

            List<Galleries> productGallery = product.Gallery.ToList();

            if (_session.GetString("uploadImagePc") != null)
            {
                string[] imagesPc = _session.GetString("uploadImagePc").Split(',');

                foreach (var image in imagesPc)
                {
                    productGallery.Add(new Galleries { Image = image, Mobile = false, Product = product });
                }
            }

            // Mobile

            if (_session.GetString("uploadImageMobile") != null)
            {
                string[] imagesMobile = _session.GetString("uploadImageMobile").Split(',');

                foreach (var image in imagesMobile)
                {
                    productGallery.Add(new Galleries { Image = image, Mobile = true, Product = product });
                }
            }

            product.Colors = productColors;
            product.Gallery = productGallery;

            _productService.Update(product);

            return RedirectToAction(nameof(UpdateProduct), new { id = product.Id });
        }
        public async Task<string> ImageUpload(int id)
        {
            string totalImagePc = "", totalImageMobile = "";

            string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "tema/images/upload");

            string fileName = "";

            foreach (FormFile file in Request.Form.Files)
            {
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                if (file.Length > 0)
                {
                    string filePath = Path.Combine(uploads, fileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }

            if (id == 0)
            {
                if (String.IsNullOrEmpty(totalImagePc))
                {
                    totalImagePc = fileName;
                }
                else
                {
                    totalImagePc = totalImagePc + "," + fileName;
                }

                _session.SetString("uploadImagePc", totalImagePc);
            }
            else
            {
                if (String.IsNullOrEmpty(totalImageMobile))
                {
                    totalImageMobile = fileName;
                }
                else
                {
                    totalImageMobile = totalImageMobile + "," + fileName;
                }

                _session.SetString("uploadImageMobile", totalImageMobile);
            }

            return fileName;

        }

        [HttpPost]
        public async Task<string> RemoveImage(int id) {

            string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "tema/images/upload/");

            var image = await _imageGallery.FirstOfDefaultAsync(x => x.Id == id);
            _imageGallery.Remove(image);

            System.IO.File.Delete(uploads + image.Image);

            return id.ToString();
        
        }

        public IActionResult ProductList() {

            var products = _productService.GetAll().ToList();

            return View(products);
        
        }
        public async Task<IActionResult> DeleteProduct(int id) { 
        
            var product = await _productService.FirstOfDefaultAsync(x => x.Id == id);

            _productService.Remove(product);

            return RedirectToAction(nameof(ProductList));
        
        }

        public IActionResult AllProductsChangePrice() {

            return View();
        
        }

        public IActionResult updateProductAllPrice() {

            var products = _productService.GetAll();

            foreach (var product in products)
            {

                product.Price = Convert.ToDecimal(Request.Form["Price"]);
                product.DiscountPrice = Convert.ToDecimal(Request.Form["discountPrice"]);

                _productService.Update(product);

            }

            return RedirectToAction(nameof(AllProductsChangePrice));
        
        }

        public IActionResult ListOrder() {

            var orders = _orderService.GetAllInclude();

            var orderSituation = _orderSituationService.GetAll();

            List<SelectListItem> orderSituationListItem = new List<SelectListItem>();

            foreach (var situation in orderSituation)
            {
                orderSituationListItem.Add(new SelectListItem { Text = situation.Name, Value = situation.Id.ToString() });
            }

            ViewBag.OrderSituation = orderSituationListItem;

            return View(orders);
        
        }

        public IActionResult ReturnExchangeRequest() {

            var returnOrders = _returnOrderService.GetAllInclude();

            return View(returnOrders);

        }

        public IActionResult ClosedReturnOrder(int id)
        {

            var ro = _returnOrderService.GetAllIncludeById(id);

            if (ro.Situation)
            {
                ro.Situation = false;
            }
            else
            {
                ro.Situation = true;
            }

            _returnOrderService.Update(ro);

            return RedirectToAction(nameof(ReturnExchangeRequest));


        }
        public IActionResult DeleteReturnOrder(int id)
        {

            var ro = _returnOrderService.GetAllIncludeById(id);

            _returnOrderService.Remove(ro);

            return RedirectToAction(nameof(ReturnExchangeRequest));


        }

        public IActionResult OrderDetail(int id)
        {

            var order = _orderService.GetAllIncludeId(id);

            return View(order);

        }

        public string SaveCargoCode(string id) {

            var order = _orderService.GetAllIncludeOrderId(id);

            order.CargoCode = Request.Form["CargoCode"].ToString();

            _orderService.Update(order);

            return "1";
        
        }

        public IActionResult AddCoupon() {

            return View();

        }
        public async Task<IActionResult> UpdateCoupon(int id)
        {

            var coupon = await _couponsService.GetByIdAsync(id);

            return View(coupon);

        }
        public async Task<IActionResult> AddCouponForm(Coupons coupon)
        {
            if (String.IsNullOrEmpty(coupon.Code))
            {
                coupon.Code = Guid.NewGuid().ToString("N").Substring(0, 6);
            }

            if (Request.Form["ForOnce"].ToString() == "on")
            {
                coupon.ForOnce = true;
            }

            await _couponsService.AddAsync(coupon);

            return RedirectToAction(nameof(UpdateCoupon), new { coupon.Id });

        }
        public async Task<IActionResult> UpdateCouponForm(Coupons coupon)
        {
            if (String.IsNullOrEmpty(coupon.Code))
            {
                coupon.Code = Guid.NewGuid().ToString("N").Substring(0, 6);
            }

            if (Request.Form["ForOnce"].ToString() == "on")
            {
                coupon.ForOnce = true;
            }

            _couponsService.Update(coupon);

            return RedirectToAction(nameof(UpdateCoupon), new { coupon.Id });

        }
        public ActionResult ListCoupon() {
        
            var coupons = _couponsService.GetAll().ToList();

            return View(coupons);
        
        }
        public async Task<ActionResult> DeleteCoupon(int id)
        {

            var coupon = await _couponsService.GetByIdAsync(id);

            _couponsService.Remove(coupon);

            return RedirectToAction(nameof(ListCoupon));

        }
        public IActionResult Comments() {

            var comments = _commentsService.GetAll().ToList();

            return View(comments);
        
        }

        public async Task<IActionResult> ApproveComment(int id)
        {

            var comment = await _commentsService.GetByIdAsync(id);

            if (comment.Situation)
            {
                comment.Situation = false;
            }
            else
            {
                comment.Situation = true;
            }

            _commentsService.Update(comment);

            return RedirectToAction(nameof(Comments));

        }

        public async Task<IActionResult> DeleteComment(int id) {

            var comment = await _commentsService.GetByIdAsync(id);

            _commentsService.Remove(comment);

            return RedirectToAction(nameof(Comments));

        }

        public IActionResult ColumnAdd() {

            var screens = _screenService.GetAll().ToList();

            List<SelectListItem> screenList = new List<SelectListItem>();

            foreach (var screen in screens)
            {
                screenList.Add(new SelectListItem { Text = screen.Name, Value = screen.Id.ToString() });
            }

            ViewBag.Screens = screenList;

            return View();
        
        }
        public async Task<IActionResult> ColumnUpdate(int id)
        {
            var column = _homeColumnService.GetIdHomecolumn(id);

            var screens = _screenService.GetAll().ToList();

            List<SelectListItem> screenList = new List<SelectListItem>();

            foreach (var screen in screens)
            {
                if (column.Screen.Id == screen.Id)
                {
                    screenList.Add(new SelectListItem { Text = screen.Name, Value = screen.Id.ToString(), Selected = true });
                }
                else
                {
                    screenList.Add(new SelectListItem { Text = screen.Name, Value = screen.Id.ToString() });
                }
                
            }

            ViewBag.Screens = screenList;

            return View(column);

        }
        public async Task<IActionResult> AddColumnForm(HomeColumn column) {

            var screen = await _screenService.GetByIdAsync(Convert.ToInt32(Request.Form["Screen"].ToString()));

            column.Screen = screen;

            await _homeColumnService.AddAsync(column);

            return RedirectToAction(nameof(ColumnUpdate), new { column.Id });
        
        }
        public async Task<IActionResult>  UpdateColumnForm(HomeColumn column)
        {

            var screen = await _screenService.GetByIdAsync(Convert.ToInt32(Request.Form["Screen"].ToString()));

            column.Screen = screen;

            _homeColumnService.Update(column);

            return RedirectToAction(nameof(ColumnUpdate), new { column.Id });

        }
        public IActionResult ListColumn() { 
        
            var column = _homeColumnService.GetAll().ToList();

            return View(column);
        
        }

        public IActionResult DeleteColumn(int id) { 
        
            var column = _homeColumnService.GetIdHomecolumn(id);

            _homeColumnService.Remove(column);

            return RedirectToAction(nameof(ListColumn));

        }

        public IActionResult AddColumnDetail() {

            var homecolums = _homeColumnService.GetAll();

            List<SelectListItem> homeList = new List<SelectListItem>();

            foreach (var block in homecolums)
            {
                homeList.Add(new SelectListItem { Text = block.ColumnName, Value = block.Id.ToString() });
            }

            ViewBag.homeList = homeList;

            return View();
        
        }
        public async Task<IActionResult> UpdateColumnDetail(int id)
        {

            var columnDetail = _columnDetailService.GetById(id);

            var homecolums = _homeColumnService.GetAll();

            List<SelectListItem> homeList = new List<SelectListItem>();

            foreach (var block in homecolums)
            {
                if (columnDetail.HomeColumn.Id == block.Id)
                {
                    homeList.Add(new SelectListItem { Text = block.ColumnName, Value = block.Id.ToString(), Selected = true });
                }
                else
                {
                    homeList.Add(new SelectListItem { Text = block.ColumnName, Value = block.Id.ToString() });
                }
                
            }

            ViewBag.homeList = homeList;

            return View(columnDetail);

        }

        public async Task<IActionResult> AddColumnDetailForm(ColumnDetail detail, IFormFile Background) {

            string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "tema/images/homepage");

            string imageTitle = "";
            if (Background != null)
            {

                imageTitle = Guid.NewGuid().ToString() + Path.GetExtension(Background.FileName);

                if (Background.Length > 0)
                {
                    string filePath = Path.Combine(uploads, imageTitle);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await Background.CopyToAsync(fileStream);
                    }

                    detail.Background = imageTitle;
                }

            }

            var block = _homeColumnService.GetIdHomecolumn(Convert.ToInt32(Request.Form["HomeColumn"].ToString()));

            detail.HomeColumn = block;

            await _columnDetailService.AddAsync(detail);

            return RedirectToAction(nameof(UpdateColumnDetail), new { detail.Id });

        }
        public async Task<IActionResult> UpdateColumnDetailForm(ColumnDetail detail, IFormFile Background)
        {

            string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "tema/images/homepage");

            string imageTitle = "";
            if (Background != null)
            {

                imageTitle = Guid.NewGuid().ToString() + Path.GetExtension(Background.FileName);

                if (Background.Length > 0)
                {
                    string filePath = Path.Combine(uploads, imageTitle);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await Background.CopyToAsync(fileStream);
                    }

                    detail.Background = imageTitle;
                }

            }

            var block = _homeColumnService.GetIdHomecolumn(Convert.ToInt32(Request.Form["HomeColumn"].ToString()));

            detail.HomeColumn = block;

            _columnDetailService.Update(detail);

            return RedirectToAction(nameof(UpdateColumnDetail), new { detail.Id });

        }

        public IActionResult ListColumnDetail() {

            var columnDetails = _columnDetailService.GetAll().ToList();

            return View(columnDetails);
        
        }

        public IActionResult DeleteColumnDetail(int id)
        {

            var columnDetail = _columnDetailService.GetById(id);

            _columnDetailService.Remove(columnDetail);

            return RedirectToAction(nameof(ListColumnDetail));

        }
    }
}

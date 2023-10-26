using Core.Models;
using Core.Services;
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

        public AdminController(IWebHostEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, IService<Colors> colorService, IProductService productService, IService<Galleries> imageGallery, IOrderService orderService, IService<OrderSituation> orderSituationService)
        {
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _colorService = colorService;
            _productService = productService;
            _imageGallery = imageGallery;
            _orderService = orderService;
            _orderSituationService = orderSituationService;
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
        public async Task<IActionResult> AddProduct(Product product, IFormFile fileImage)
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
        public async Task<IActionResult> UpdateProduct(Product getProduct, IFormFile fileImage)
        {

            var product = _productService.UpdateProductGetAllInclude(Convert.ToInt32(Request.Form["Id"]));

            product.Title = getProduct.Title;
            product.Explanation = getProduct.Explanation;
            product.Price = getProduct.Price;
            product.DiscountPrice = getProduct.DiscountPrice;
            product.Stock = getProduct.Stock;
            product.Order = getProduct.Order;
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
    }
}

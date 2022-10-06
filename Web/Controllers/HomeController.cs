using Core.Models;
using Core.Repositories;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IService<HomeColumn> _homeColumn;

        public HomeController(ILogger<HomeController> logger, IService<HomeColumn> homeColumn)
        {
            _logger = logger;
            _homeColumn = homeColumn;
        }

        public IActionResult Index()
        {
            var homeColumn = _homeColumn.GetAll().OrderBy(x => x.Order).ToList();

            ViewBag.HomeColumn = homeColumn;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
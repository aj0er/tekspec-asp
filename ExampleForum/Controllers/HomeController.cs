using ExampleForum.Models.Views;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExampleForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private const string SpaRoot = @"../Frontend/dist/frontend/spa";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/spa")]
        public IActionResult Spa()
        {
            var stream = System.IO.File.OpenRead(@"frontend-dist/spa/index.html");
            return new FileStreamResult(stream, "text/html");
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
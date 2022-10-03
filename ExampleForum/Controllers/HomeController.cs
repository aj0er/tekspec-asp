using ExampleForum.Models;
using ExampleForum.Models.Views;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExampleForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Example()
        {
            return View(new TestModel
            {
                Names = new List<string>{ "ok" }
            });
        }

        public IActionResult Bre()
        {
            return StatusCode(200, "ssss");
        }

        public IActionResult Files()
        {
            var stream = System.IO.File.OpenRead("Images/v60.jpg");
            return new FileStreamResult(stream, "image/jpeg");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
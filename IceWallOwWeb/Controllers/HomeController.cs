using IceWallOw.Application.Dto;
using IceWallOwWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IceWallOwWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {

            HttpClient client = new HttpClient();
            string queryString = "https://localhost:7053/api/Products";

            var response = await client.GetAsync(queryString);
            var statusCode = response.StatusCode;
            var body = await response.Content.ReadAsStringAsync();

            var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductDto>>(body);

            return View(list);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult ProductDesc()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }
        public IActionResult Chatbox()
        {
            return View();
        }
        public IActionResult Tickets()
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
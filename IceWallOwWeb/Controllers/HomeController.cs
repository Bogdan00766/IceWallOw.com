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

        public async Task<IActionResult> Index(string? name = null, string? location = null, int? distance = null, string? categoryName = null, float? priceMin = null, float? priceMax = null)//(float priceMin, float priceMax)
        {

            HttpClient client = new HttpClient();
            //$...../id={
            string queryString = "https://localhost:7053/api/Products?";
            if (name != null) queryString = queryString + $"name={name}&";
            if (location != null) queryString = queryString + $"location={location}&";
            if (distance != null) queryString = queryString + $"distance={distance}&";
            if (categoryName != null) queryString = queryString + $"categoryName={categoryName}&";
            if (priceMin != null) queryString = queryString + $"priceMin={priceMin}&";
            if (priceMax != null) queryString = queryString + $"priceMax={priceMax}&";

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
        public async Task<IActionResult> ProductDescAsync(int id)
        {
            HttpClient client = new HttpClient();
            string queryString = $"https://localhost:7053/api/Products/{id}";

            var response = await client.GetAsync(queryString);
            var statusCode = response.StatusCode;
            var body = await response.Content.ReadAsStringAsync();

            var product = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductDto>(body);

            return View(product);
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

        public IActionResult LoginPage()
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
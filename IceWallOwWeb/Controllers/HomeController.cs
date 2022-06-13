using IceWallOw.Application.Dto;
using IceWallOwWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Encodings.Web;
using System.Web;

namespace IceWallOwWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cock = Request.Cookies["GUID"];
            if (await IsLoggedAsync(cock))
            {
                ViewData["is_logged"] = true;
            }
            else
            {
                ViewData["is_logged"] = false;
            }
            await next();
        }

        public async Task<IActionResult> Index(string? name = null, string? location = null, int? distance = null, string? categoryName = null, float? priceMin = null, float? priceMax = null)//(float priceMin, float priceMax)
        {
            
            HttpClient client = new HttpClient();
            //$...../id={
            string queryString = "http://localhost:5000/api/Products?";
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
            string queryString = $"http://localhost:5000/api/Products/{id}";

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
        public async Task<IActionResult> RegisterPageAsync()
        {
            var cock = Request.Cookies["GUID"];
            if (await IsLoggedAsync(cock))
            {
                return Unauthorized("User is already logged");
            }

            HttpClient client = new HttpClient();


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterPageAsync(RegisterUserDto person)
        {
          

            HttpClient client = new HttpClient();
            string queryString = $"http://localhost:5000/Api/Users/Register";

            var json = System.Text.Json.JsonSerializer.Serialize(person);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(queryString, stringContent);
            var statusCode = response.StatusCode;
            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var user = System.Text.Json.JsonSerializer.Deserialize<UserDto>(body);

                return RedirectToAction("LoginPage", "Home");
            }
            else
            {
                ViewData["body_response"] = body;
                return View();
            }
        }

        public async Task<IActionResult> LoginPageAsync()
        {

            var cock = Request.Cookies["GUID"];
            if (await IsLoggedAsync(cock))
            {
                return Unauthorized("User is already logged");
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> LogoutPageAsync()
        {
            var cock = Request.Cookies["GUID"];
            if (!(await IsLoggedAsync(cock)))
            {
                return Unauthorized("User is not already logged");
            }

            string queryString = $"http://localhost:5000/api/Users/Logout";
            CookieContainer cookies = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler();

            handler.CookieContainer = cookies;

            HttpClient client = new HttpClient(handler);

            cookies.Add(new Uri("http://localhost:5000"), new Cookie("GUID", cock));
            var response = await client.GetAsync(queryString);
            var statusCode = response.StatusCode;
            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["body_response"] = body;
                return View();
            }


        }


            [HttpPost]
        public async Task<IActionResult> LoginPageAsync(LoginUserDto person)
        {
            string email = person.EMail;
            string password = person.Password;
            CookieContainer cookies = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler();

            handler.CookieContainer = cookies;
            
            HttpClient client = new HttpClient(handler);
            string queryString = $"http://localhost:5000/Api/Users/Login";

            var json = System.Text.Json.JsonSerializer.Serialize(person);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(queryString,stringContent);
            var statusCode = response.StatusCode;
            var body = await response.Content.ReadAsStringAsync();
            
            if(response.IsSuccessStatusCode)
            {
                var user = System.Text.Json.JsonSerializer.Deserialize<UserDto>(body);

                Uri uri = new Uri(queryString);
                var responseCookies = cookies.GetCookies(uri).Cast<Cookie>();
                HttpContext.Response.Cookies.Append("GUID", responseCookies.Where(x => x.Name=="GUID").FirstOrDefault().Value, new Microsoft.AspNetCore.Http.CookieOptions
                {
                    Expires = DateTime.Now.AddDays(1),
                });

                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewData["body_response"] = body;
                return View();
                //return BadRequest(body);
            }

            
        }

        async Task<bool> IsLoggedAsync(String guid)
        {
            string queryString = $"http://localhost:5000/api/Users/isLogged";
            CookieContainer cookies = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler();

            handler.CookieContainer = cookies;

            HttpClient client = new HttpClient(handler);

            cookies.Add(new Uri("http://localhost:5000"), new Cookie("GUID",guid));
            var response = await client.GetAsync(queryString);
            var statusCode = response.StatusCode;
            var body = await response.Content.ReadAsStringAsync();

            return bool.Parse(body);
        }

        public async Task<ActionResult> AddProductAsync()
        {
            var cock = Request.Cookies["GUID"];
            if (!(await IsLoggedAsync(cock)))
            {
                return Unauthorized("User is not already logged");
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddProductAsync(CreateProductDto product)
        {
            var cock = Request.Cookies["GUID"];
            if (!(await IsLoggedAsync(cock)))
            {
                return Unauthorized("User is not already logged");
            }


            string queryString = $"http://localhost:5000/api/Products";
            CookieContainer cookies = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler();

            handler.CookieContainer = cookies;

            var json = System.Text.Json.JsonSerializer.Serialize(product);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient(handler);

            cookies.Add(new Uri("http://localhost:5000"), new Cookie("GUID", cock));
            var response = await client.PostAsync(queryString, stringContent);
            var statusCode = response.StatusCode;
            var body = await response.Content.ReadAsStringAsync();



            if (response.IsSuccessStatusCode)
            {
                var item = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductDto>(body); ;

                //var str = "ProductDesc" + '?' + "id=" + item.Id;
                return RedirectToAction("ProductDesc", "Home", new {id = item.Id});
            }
            else
            {
                return BadRequest(body);
            }
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
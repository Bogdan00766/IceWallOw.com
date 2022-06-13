using IceWallOwWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace IceWallOwWeb.Controllers
{
    public class ChatController : Controller
    {
        private readonly ILogger<ChatController> _logger;
        public ChatController(ILogger<ChatController> logger)
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

        public IActionResult Socket(int ChatId)
        {
            if (Request.Cookies["GUID"] == null)
                return Unauthorized();
#pragma warning disable CS8604 // Possible null reference argument.
            Guid guid = Guid.Parse(Request.Cookies["GUID"]);
#pragma warning restore CS8604 // Possible null reference argument.
            return View(new ChatViewModel() { ChatId = ChatId, Guid = guid });
        }

        async Task<bool> IsLoggedAsync(String guid)
        {
            string queryString = $"http://localhost:5000/api/Users/isLogged";
            CookieContainer cookies = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler();

            handler.CookieContainer = cookies;

            HttpClient client = new HttpClient(handler);

            cookies.Add(new Uri("http://localhost:5000"), new Cookie("GUID", guid));
            var response = await client.GetAsync(queryString);
            var statusCode = response.StatusCode;
            var body = await response.Content.ReadAsStringAsync();

            return bool.Parse(body);
        }
    }
}

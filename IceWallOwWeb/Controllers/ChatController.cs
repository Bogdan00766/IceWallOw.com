using IceWallOwWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace IceWallOwWeb.Controllers
{
    public class ChatController : Controller
    {
        private readonly ILogger<ChatController> _logger;
        public ChatController(ILogger<ChatController> logger)
        {
            _logger = logger;
        }
        public IActionResult Socket(int ChatId)
        {
            if (Request.Cookies["GUID"] == null)
                return Unauthorized();
            #pragma warning disable CS8604 // Possible null reference argument.
            Guid guid = Guid.Parse(Request.Cookies["GUID"]);
            #pragma warning restore CS8604 // Possible null reference argument.

            return View(new ChatViewModel() { ChatId = ChatId });
        }
    }
}

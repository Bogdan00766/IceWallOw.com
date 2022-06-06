using Microsoft.AspNetCore.Mvc;

namespace IceWallOw.Api.Controllers
{
    public class WebSocketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

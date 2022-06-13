using Domain.IRepositories;
using IceWallOw.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IceWallOw.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ILogger<ChatController> _logger;
        private readonly IChatService _chatService;
        public ChatController(ILogger<ChatController> logger, IChatService chatService)
        {
            _logger = logger;
            _chatService = chatService;
        }
        [HttpGet("GetMessagesFromChat")]
        public async Task<IActionResult> GetMessagesFromChat(int chatId)
        {
            _logger.LogInformation("Download all messages from chat " + chatId);
            var chat = await _chatService.FindChatById(chatId);
            return Ok(_chatService.GetMessages(chat));
        }
    }
}

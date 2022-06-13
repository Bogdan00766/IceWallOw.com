using IceWallOw.Application.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using IceWallOw.Application.Interfaces;

namespace IceWallOw.Api.Controllers
{
    public class WebSocketController : ControllerBase, IDisposable
    {
        private readonly IChatService _chatService;
        protected class WebSocketMessage
        {
            public WebSocketReceiveResult ReceiveResult { get; }
            public MessageDto? Message { get; }
            public WebSocketMessage(WebSocketReceiveResult _receiveResult, byte[] message, IChatService chatService, ILogger<WebSocketController> logger)
            {
                ReceiveResult = _receiveResult;
                string result = Encoding.UTF8.GetString(message);
                MessageDto? mes = JsonConvert.DeserializeObject<MessageDto?>(result);
                if (mes != null)
                {
                    if (mes.Content == null || mes.Content == String.Empty || (mes.Owner == null && mes.OwnerGuid == null))
                        return;
                    Message = new MessageDto()
                    {
                        ChatId = mes.ChatId,
                        Content = mes.Content,
                        Date = DateTime.Now,
                        Id = 2,
                        Owner = (mes.Owner != null) ? mes.Owner : chatService.FindUserByGuid((Guid)mes.OwnerGuid)
                    };
                };
            }
            public override string ToString()
            {
                return (Message == null) ? String.Empty : Message.Content;
            }
        }
        private WebSocket? _webSocket;
        private ILogger<WebSocketController> _logger;
        public WebSocketController(ILogger<WebSocketController> logger, IChatService chatService)
        {
            _logger = logger;
            _chatService = chatService;
        }
        [HttpGet("/chat")]
        public async Task Get()
        {
            if(!HttpContext.WebSockets.IsWebSocketRequest)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }
            _webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await Echo();
        }
        private async Task<WebSocketMessage> Read()
        {
            if(_webSocket == null)
            {
                _logger.LogError("Null pointer for webSocket");
                throw new NullReferenceException("Null pointer for webSocket");
            }
            byte[] buffer = new byte[1024 * 4];
            var received = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            var message = new WebSocketMessage(received, buffer, _chatService, _logger);
            return message;
        }
        private async Task Write(WebSocketMessage webSocketMessage)
        {
            if (_webSocket == null)
            {
                _logger.LogError("Null pointer for webSocket");
                throw new NullReferenceException("Null pointer for webSocket");
            }
            var messageObject = JsonConvert.SerializeObject(webSocketMessage.Message);
            var message = Encoding.UTF8.GetBytes(messageObject);
            await _webSocket.SendAsync(
                new ArraySegment<byte>(message),
                webSocketMessage.ReceiveResult.MessageType,
                webSocketMessage.ReceiveResult.EndOfMessage,
                CancellationToken.None);
        }
        private async Task Echo()
        {
            WebSocketMessage received = await Read();
            if(received.Message != null)
                received.Message.Date = DateTime.Now;
            while(!received.ReceiveResult.CloseStatus.HasValue)
            {
                if(received.Message != null)
                    await Write(received);
                received = await Read();
            }
        }

        public void Dispose()
        {
            if (_webSocket == null)
            {
                _logger.LogError("Null pointer for webSocket");
                throw new NullReferenceException("Null pointer for webSocket");
            }
            _webSocket.CloseAsync(WebSocketCloseStatus.Empty, null, CancellationToken.None).Wait();
            _webSocket.Dispose();
        }
    }
}

using Confluent.Kafka;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IceWallOw.Application.Dto;
using IceWallOw.Application.Interfaces;

namespace IceWallOw.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ILogger<TicketsController> _logger;
        private readonly ITicketService _ticketService;
        private readonly IProducer<Null, int> _producer;
        private readonly IConsumer<Null, int> _consumer;

        public TicketsController(ILogger<TicketsController> logger, IProducer<Null, int> producer,
            IConsumer<Null, int> consumer, ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
            _producer = producer;
            _consumer = consumer;
        }
        [HttpPost("CreateTicket")]
        public async Task<IActionResult> CreateTicket()
        {
            var guid = Guid.Parse(Request.Headers["GUID"]);
            var user = _ticketService.FindUserByGuid(guid);
            _logger.LogInformation(0, "Creating token for " + user.Id);
            _logger.LogError(1, "Creating tokens not implemented");

            throw new NotImplementedException("Pobieranie ticketu nie zostalo zaimplementowane");

            var token = new TicketDto(0)
            {
                Chat = new ChatDto(1)
            };


            _logger.LogInformation(2, $"Received tokenId {token.Id} for clientId {user.Id}");
            _logger.LogInformation(3, $"Sending tokenId {token.Id} to broker");
            await _producer.ProduceAsync("Tickets", new Message<Null, int>()
            {
                Value = token.Id
            });
            _producer.Flush();
            return Ok(token);
        }

        [HttpPost("GetNewTicket")]
        public async Task<IActionResult> GetNewTicket()
        {
            _consumer.Subscribe("Tickets");
            var message = await Task.Run(() => _consumer.Consume(TimeSpan.FromSeconds(10)));
            if (message == null) return NoContent();
            var token = new TicketDto(message.Message.Value)
            {
                Chat = new ChatDto(0)
            };
            return Ok(token);
        }
    }
}

using Confluent.Kafka;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IceWallOw.Application.Dto;

namespace IceWallOw.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ILogger<TicketsController> _logger;
        private readonly IProducer<Null, int> _producer;
        private static readonly ProducerConfig config = new ProducerConfig()
        {
            BootstrapServers = "kafka:29092"
        };

        public TicketsController(ILogger<TicketsController> logger)
        {
            _logger = logger;
            _producer = new ProducerBuilder<Null, int>(config).Build();
        }
        [HttpPost("CreateTicket")]
        public async Task<IActionResult> CreateTicket()
        {
            string guid = Request.Headers["GUID"];

            throw new NotImplementedException("Pobieranie id nie zostało zaimplementowane");

            int clientId = 4;
            _logger.LogInformation(0, "Creating token for " + clientId);
            _logger.LogError(1, "Creating tokens not implemented");

            throw new NotImplementedException("Pobieranie czatu nie zostalo zaimplementowane");

            var token = new TicketDto(0)
            {
                Chat = new ChatDto(0)
            };


            _logger.LogInformation(2, $"Received tokenId {token.Id} for clientId {clientId}");
            _logger.LogInformation(3, $"Sending tokenId {token.Id} to broker");
            using(var producer = new ProducerBuilder<Null, int>(config).Build())
            await _producer.ProduceAsync("Tokens", new Message<Null, int>()
            {
                Value = token.Id
            });
            _producer.Flush();
            return Ok(token);
        }
    }
}

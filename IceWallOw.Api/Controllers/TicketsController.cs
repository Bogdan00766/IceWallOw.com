using Confluent.Kafka;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IceWallOw.Application.Dto;
using IceWallOw.Application.Interfaces;
using Domain.Models;

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
        [HttpGet("CreateTicket")]
        public async Task<IActionResult> CreateTicket(string title)
        {
            if (Request.Cookies["GUID"] == null)
                return Unauthorized();
            Guid guid = Guid.Parse(Request.Cookies["GUID"]);
            var user = _ticketService.FindUserByGuid(guid);
            if (user == null)
                return BadRequest();
            if (title.Contains('\'') || title.Contains('\"'))
                return BadRequest();
            _logger.LogInformation(0, "Creating ticket for " + user.Id);

            TicketDto ticket = _ticketService.NewTicket(new TicketDto { Title = title }, user);


            _logger.LogInformation(1, $"Received ticketId {ticket.Id} for clientId {user.Id}");
            _logger.LogInformation(2, $"Sending ticketId {ticket.Id} to broker");
            await _producer.ProduceAsync("Tickets", new Message<Null, int>()
            {
                Value = (int)ticket.Id
            });
            _producer.Flush();
            return Ok(ticket);
        }

        [HttpPost("GetNextTicket")]
        public async Task<IActionResult> GetNextTicket()
        {
            if (Request.Cookies["GUID"] == null)
                return Unauthorized();
            Guid guid = Guid.Parse(Request.Cookies["GUID"]);
            var user = _ticketService.FindUserByGuid(guid);
            if (user == null)
                return Unauthorized();
            var message = await Task.Run(() => _consumer.Consume(TimeSpan.FromSeconds(10)));
            if (message == null)
            {
                _logger.LogInformation(0, $"User {user.Id} tried to pull new ticket, but find none");
                return NoContent();
            }
            _logger.LogDebug("User " + user.Id);
            var ticket = await _ticketService.ClaimTicketById(message.Message.Value, user);
            if (ticket == null) return StatusCode(500);
            _logger.LogInformation(1, $"User {user.Id} pulled ticket {ticket.Id}");
            return Ok(ticket);
        }
    }
}

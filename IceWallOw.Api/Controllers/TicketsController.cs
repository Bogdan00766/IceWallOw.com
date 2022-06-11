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
            Guid guid;
            try
            {
                guid = Guid.Parse(Request.Headers["GUID"]);
            }
            catch (System.ArgumentNullException)
            {
                return Unauthorized("GUID cookie cannot null");
            }
            var user = _ticketService.FindUserByGuid(guid);
            if (user == null)
                return BadRequest();
            if (title.Contains('\'') || title.Contains('\"'))
                return BadRequest();
            _logger.LogInformation(0, "Creating ticket for " + user.Id);

            TicketDto ticket = _ticketService.NewTicket(new TicketDto { Title = title }, user);


            _logger.LogInformation(2, $"Received ticketId {ticket.Id} for clientId {user.Id}");
            _logger.LogInformation(3, $"Sending ticketId {ticket.Id} to broker");
            await _producer.ProduceAsync("Tickets", new Message<Null, int>()
            {
                Value = (int)ticket.Id
            });
            _producer.Flush();
            return Ok(ticket);
        }

        [HttpPost("GetNewTicket")]
        public async Task<IActionResult> GetNewTicket()
        {
            _consumer.Subscribe("Tickets");
            var message = await Task.Run(() => _consumer.Consume(TimeSpan.FromSeconds(10)));
            if (message == null) return NoContent();
            //var token = new TicketDto(message.Message.Value)
            //{
            //    Chat = new ChatDto(0)
            //};
            //return Ok(token);
            return StatusCode(499);
        }
    }
}

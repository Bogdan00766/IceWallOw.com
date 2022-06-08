using Confluent.Kafka;
using System.Collections;
using IceWallOw.Application.Classes;

namespace IceWallOwWeb.Support
{
    public class TicketConsumer : IEnumerable<Ticket>
    {
        private readonly IConsumer<Null, int> _consumer;
        public TicketConsumer()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "kafka:29092",
                GroupId = "Consumers",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _consumer = new ConsumerBuilder<Null, int>(config).Build();
            _consumer.Subscribe("Tokens");
        }

        public IEnumerator<Ticket> GetEnumerator()
        {
            return new TicketConsumerEnum(_consumer);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new TicketConsumerEnum(_consumer);
        }
    }
    internal class TicketConsumerEnum : IEnumerator<Ticket>
    {
        private readonly IConsumer<Null, int> _consumer;
        private Ticket _ticket = new Ticket(-1);

        public TicketConsumerEnum(IConsumer<Null, int> consumer)
        {
            _consumer = consumer;
        }

        public Ticket Current => _ticket;

        object IEnumerator.Current => _ticket;

        public void Dispose()
        {
            _consumer.Dispose();
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }
        public bool LoadNew()
        {
            var message = _consumer.Consume(TimeSpan.FromSeconds(4));
            if (message == null) return false;
            var ticket = new Ticket(message.Message.Value)
            {
                Chat = GetChat()
            };
            _ticket = ticket;
            return true;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
        private Chat GetChat()
        {
            throw new NotImplementedException();
        }
    }
}

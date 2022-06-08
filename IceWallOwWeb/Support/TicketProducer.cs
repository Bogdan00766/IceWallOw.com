using Confluent.Kafka;

namespace IceWallOwWeb.Support
{
    public class TicketProducer : IMessageProducer, IDisposable
    {
        private ILogger<TicketProducer> _logger;
        private readonly IProducer<Null, int> _producer;

        public TicketProducer(ILogger<TicketProducer> logger)
        {
            _logger = logger;
            var config = new ProducerConfig()
            {
                BootstrapServers = "kafka:29092"
            };
            _producer = new ProducerBuilder<Null, int>(config).Build();
        }
        public async Task CreateMessage(int clientId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Creating token for " + clientId);
            _logger.LogError("Creating tokens not implemented");
            throw new NotImplementedException("Creating tokens not implemented");
            int tokenId = 1;
            _logger.LogInformation($"Received tokenId {tokenId} for clientId {clientId}");
            _logger.LogInformation($"Sending tokenId {tokenId} to broker");
            await _producer.ProduceAsync("Tokens", new Message<Null, int>()
            {
                Value = clientId
            }, cancellationToken);
            _producer.Flush(cancellationToken);
        }

        public void Dispose()
        {
            _producer.Dispose();
        }
    }
}

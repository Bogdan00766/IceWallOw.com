using Confluent.Kafka;

namespace IceWallOwWeb.Support
{
    public class KafkaProducer : IDisposable, IMessageProducer
    {
        private readonly ILogger<Kafka> _logger;
        private IProducer<Null, int>? _producer;

        public KafkaProducer(ILogger<Kafka> logger) => _logger = logger;
        public async Task CreateToken(int clientId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Creating new token for clientId " + clientId);
            throw new NotImplementedException("Getting id of token not implemented yet");
            int tokenId = 1;
            _logger.LogInformation($"clientId {clientId} received support token {tokenId}");
            if (_producer == null)
            {
                var config = new ProducerConfig()
                {
                    BootstrapServers = "kafka:29092"
                };
                _producer = new ProducerBuilder<Null, int>(config).Build();
            }
            _logger.LogInformation("Sending token " + tokenId + "to broker");
            await _producer.ProduceAsync("Tickets", new Message<Null, int>()
            {
                Value = tokenId
            }, cancellationToken);
            _producer.Flush(cancellationToken);
        }
        public void Dispose()
        {
            if (_producer != null)
                _producer.Dispose();
        }
    }
}

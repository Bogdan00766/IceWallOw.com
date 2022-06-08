using Confluent.Kafka;

namespace IceWallOw.Api.Tokens
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
            _logger.LogInformation(0, "Creating token for " + clientId);
            _logger.LogError(1, "Creating tokens not implemented");

            /*
             * Tutaj zaimplementuj pobieranie tokenu!
             */

            throw new NotImplementedException("Creating tokens not implemented");
            int tokenId = 1;
            _logger.LogInformation(2, $"Received tokenId {tokenId} for clientId {clientId}");
            _logger.LogInformation(3, $"Sending tokenId {tokenId} to broker");
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

namespace IceWallOw.Api.Tokens
{
    public interface IMessageProducer
    {
        Task CreateMessage(int producerId, CancellationToken cancellationToken = default);
    }
}
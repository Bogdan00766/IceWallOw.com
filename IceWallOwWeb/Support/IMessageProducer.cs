namespace IceWallOwWeb.Support
{
    public interface IMessageProducer
    {
        Task CreateMessage(int producerId, CancellationToken cancellationToken = default);
    }
}
namespace IceWallOwWeb.Support
{
    internal interface IMessageProducer
    {
        public Task CreateToken(int clientId, CancellationToken cancellationToken = default);
    }
}
namespace IceWallOwWeb.Support
{
    public interface IMessager
    {
        Task CreateToken(int clientId, CancellationToken cancellationToken = default);
        void ReadToken();
    }
}
namespace API_Gateway.Services.Interface
{
    public interface IKeyVaultManager
    {
        public Task<string> GetSecret(string username);

    }
}

using API_Gateway.Services.Interface;
using Azure.Security.KeyVault.Secrets;
using System.Security.Cryptography;
using System.Text;

namespace API_Gateway.Services
{
    public class KeyVaultManager : IKeyVaultManager
    {
        private readonly SecretClient _secretClient;

        public KeyVaultManager(SecretClient secretClient)

        {
            _secretClient = secretClient;


        }

        public async Task<string> GetSecret(string username)

        {

            try

            {

                KeyVaultSecret keyValueSecret = await

                _secretClient.GetSecretAsync(username);

                return keyValueSecret.Value;

            }

            catch

            {

                throw;

            }





        }

        
    }
}

namespace TokenGenerator
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using IdentityModel.Client;

    internal class Program
    {
        private static void Main(string[] args)
        {
            GetToken().Wait();
        }

        private static async Task GetToken()
        {
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "hotelsweb",
                ClientSecret = "hotelswebsecret",
                Scope = "hotelsapi"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");
        }
    }
}

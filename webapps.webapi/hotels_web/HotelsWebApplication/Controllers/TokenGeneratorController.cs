namespace HotelsWebApplication.Controllers
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using IdentityModel.Client;
    using Microsoft.AspNetCore.Mvc;

    public class TokenGeneratorController : Controller
    {
        private readonly HttpClient client;

        public TokenGeneratorController(IHttpClientFactory factory)
        {
            this.client = factory.CreateClient();
        }

        public async Task<IActionResult> Index()
        {
            var disco = await this.client.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return this.NotFound();
            }

            // request token
            TokenResponse tokenResponse = await this.client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "hotelsweb",
                ClientSecret = "hotelswebsecret",
                Scope = "hotelsapi"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return this.NotFound();
            }

            Console.WriteLine(tokenResponse.Json);

            return this.View(tokenResponse);
        }
    }
}

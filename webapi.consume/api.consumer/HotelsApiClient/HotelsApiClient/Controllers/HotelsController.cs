namespace HotelsApiClient.Controllers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHttpClientFactory factory;

        public HotelsController(IHttpClientFactory factory)
        {
            this.factory = factory;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var client = this.factory.CreateClient("hotels");

            //client.BaseAddress = new Uri("http://localhost:5000/");
            //client.Timeout = TimeSpan.FromMinutes(1);
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync($"api/hotels/{id}");

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return this.NotFound();
                }

                //...
            }

            return this.Ok(await response.Content.ReadAsStringAsync());
        }
    }
}

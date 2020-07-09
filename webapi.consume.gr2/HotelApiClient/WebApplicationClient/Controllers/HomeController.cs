using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplicationClient.Controllers
{
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading;
    using Newtonsoft.Json;
    using JsonSerializer = System.Text.Json.JsonSerializer;

    [ApiController]
    [Route("home")]
    public class HomeController : ControllerBase
    {
        private readonly IHttpClientFactory factory;
        private readonly HotelClient hotelClient;

        public HomeController(IHttpClientFactory factory, HotelClient hotelClient)
        {
            this.factory = factory;
            this.hotelClient = hotelClient;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(int id)
        {
            //wrong
            //var client = new HttpClient();

            var client = this.factory.CreateClient("hotels-api");

            var response = await client.GetAsync($"api/hotels/{id}");
            //response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }
    }

    public class HotelClient
    {
        public HttpClient client { get; }

        public HotelClient(HttpClient c)
        {
            c.BaseAddress = new Uri("http://localhost:5005/");
            c.DefaultRequestHeaders.Accept.Clear();
            c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

           client = c;
        }

        public async Task<HotelModel> GetHotel(int id)
        {
            var response = await this.client.GetAsync($"api/hotels/{id}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<HotelModel>(result);
        }

        public async Task<HotelModel> CreateHotel(CreateHotelModel model)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await this.client.PostAsync("api/hotels", content);

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<HotelModel>(result);
        }

        public class CreateHotelModel
        {
            public string Name { get; set; }

            public string City { get; set; }
        }

        public class HotelModel
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string City { get; set; }
        }
    }
}

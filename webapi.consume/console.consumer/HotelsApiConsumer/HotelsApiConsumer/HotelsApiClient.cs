namespace HotelsApiConsumer
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Resources.HotelsApiConsumer.Resources;

    internal class HotelsApiClient
    {
        private readonly HttpClient client;

        public HotelsApiClient(HttpClient client)
        {
            this.client = client;

            this.client.BaseAddress = new Uri("http://localhost:5000/");
            this.client.Timeout = TimeSpan.FromSeconds(1);
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<HttpResponseMessage> GetHotel(int id)
        {
            HttpResponseMessage response = await this.client.GetAsync($"api/hotels/{id}");
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> GetHotelV2(int id)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"api/hotels/{id}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

            HttpResponseMessage response = await this.client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task DeleteHotel(int id)
        {
            HttpResponseMessage response = await this.client.DeleteAsync($"api/hotels/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<HttpResponseMessage> CreateHotel(CreateHotelResource hotel)
        {
            HttpContent content = new StringContent(hotel.ToJson(), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await this.client.PostAsync("api/hotels", content);
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}

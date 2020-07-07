namespace HotelsApiConsumer
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.JsonPatch.Operations;
    using Newtonsoft.Json;
    using Resources.HotelsApiConsumer.Resources;

    internal class HotelsApiClientV1
    {
        private readonly HttpClient client;

        public HotelsApiClientV1(HttpClient client)
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

        public async Task UpdateHotel(int id)
        {
            var doc = new JsonPatchDocument<UpdateHotelResource>();

            doc.Replace(u => u.City, "Other city name");

            doc.Remove(u => u.City);

            var message = JsonConvert.SerializeObject(doc);

            var response = await this.client.PatchAsync($"api/hotels/{id}", new StringContent(message, Encoding.UTF8, "application/json"));
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

        public async Task<HttpResponseMessage> CreateHotelV2(CreateHotelResource hotel)
        {
            var json = hotel.ToJson();

            var request = new HttpRequestMessage(HttpMethod.Post, "api/hotels");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            request.Content = new StringContent(json);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await this.client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}

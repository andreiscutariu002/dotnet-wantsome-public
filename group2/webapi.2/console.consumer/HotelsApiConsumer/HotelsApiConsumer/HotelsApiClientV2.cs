namespace HotelsApiConsumer
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Resources.HotelsApiConsumer.Resources;

    internal class HotelsApiClientV2
    {
        private readonly HttpClient client;

        public HotelsApiClientV2(HttpClient client)
        {
            this.client = client;

            this.client.BaseAddress = new Uri("http://localhost:5000/");
            this.client.Timeout = TimeSpan.FromMinutes(1);
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<HotelResource> GetHotel(int id)
        {
            using var response = await this.client.GetAsync($"api/hotels/{id}", HttpCompletionOption.ResponseHeadersRead);
            var stream = await response.Content.ReadAsStreamAsync();

            using var streamReader = new StreamReader(stream);
            using var jsonReader = new JsonTextReader(streamReader);
            var hotel = new JsonSerializer().Deserialize<HotelResource>(jsonReader);

            return hotel;
        }

        public async Task LongRunning(CancellationToken token)
        {
            try
            {
                var response = await this.client.PostAsync("api/hotels/long-running", new StringContent(""), token);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}

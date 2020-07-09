using System;

namespace HotelApiClient
{
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Text.Json.Serialization;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    class Program
    {
        static async Task Main(string[] args)
        {
            // single instance  
            var httpClient = new HttpClient();

            var hotelApiClient = new HotelsApiClient(httpClient);

            //var hotelResponse = await hotelApiClient.GetHotel(1);

            //var hotel = await hotelApiClient.CreateHotel(new HotelsApiClient.CreateHotelModel
            //    {City = "Iasi", Name = "123456"});

            //Console.WriteLine("Create hotel with id: " + hotel.Id);

            await hotelApiClient.LongRunning();
        }
    }

    public class HotelsApiClient
    {
        private readonly HttpClient client;

        public HotelsApiClient(HttpClient client)
        {
            this.client = client;

            this.client.BaseAddress = new Uri("http://localhost:5005/");
           
            //this.client.Timeout = TimeSpan.FromSeconds(1);
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public async Task<HotelModel> GetHotel(int id)
        {
            var response = await this.client.GetAsync($"api/hotels/{id}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<HotelModel>(result);
        }

        public async Task<HotelModel> GetHotelV2(int id)
        {
            var response = await this.client.GetAsync($"api/hotels/{id}");
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            using var streamReader = new StreamReader(stream);
            using var jsonReader = new JsonTextReader(streamReader);
            var hotel = new JsonSerializer().Deserialize<HotelModel>(jsonReader);
            return hotel;
        }

        public async Task<HotelModel> CreateHotel(CreateHotelModel model)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await this.client.PostAsync("api/hotels", content);

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<HotelModel>(result);
        }

        public async Task<string> LongRunning()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(5));
            var response = await this.client.GetAsync($"api/hotels/long-running", cancellationTokenSource.Token);
            var result = await response.Content.ReadAsStringAsync();
            return result;
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

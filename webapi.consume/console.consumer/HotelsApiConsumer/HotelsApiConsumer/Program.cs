namespace HotelsApiConsumer
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Resources.HotelsApiConsumer.Resources;

    internal class Program
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        private static void Main(string[] args)
        {
            RunAsync3().GetAwaiter().GetResult();
        }

        private static async Task RunAsync()
        {
            var client = new HotelsApiClientV1(HttpClient);

            var hotel = new CreateHotelResource
            {
                City = "buc",
                Name = "hotel15"
            };

            var response = await client.CreateHotelV2(hotel);

            var createdHotel = HotelResource.FromJson(await response.Content.ReadAsStringAsync());

            var responseMessage = await client.GetHotel(createdHotel.Id);
            var stringAsync = await responseMessage.Content.ReadAsStringAsync();
            var getHotelResource = JsonConvert.DeserializeObject<HotelResource>(stringAsync);

            Console.WriteLine(getHotelResource.Name);

            await client.UpdateHotel(createdHotel.Id);

            //await client.DeleteHotel(createdHotel.Id);
        }

        private static async Task RunAsync2()
        {
            var client = new HotelsApiClientV2(HttpClient);

            var hotel = await client.GetHotel(27);

            Console.WriteLine(hotel.Name);
        }

        private static async Task RunAsync3()
        {
            var client = new HotelsApiClientV2(HttpClient);

            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(5));

            await client.LongRunning(cancellationTokenSource.Token);
        }
    }
}

namespace HotelsApiConsumer
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Resources.HotelsApiConsumer.Resources;

    internal class Program
    {
        private static readonly HttpClient Client = new HttpClient();

        private static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }

        private static async Task RunAsync()
        {
            var client = new HotelsApiClient(Client);

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
    }
}

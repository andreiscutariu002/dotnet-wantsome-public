namespace HotelsApiConsumer
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
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
                Name = "hotel14"
            };

            var response = await client.CreateHotel(hotel);

            var createdHotel = HotelResource.FromJson(await response.Content.ReadAsStringAsync());

            var getHotelResponse = await client.GetHotel(createdHotel.Id);

            var getHotelResource = HotelResource.FromJson(await getHotelResponse.Content.ReadAsStringAsync());

            Console.WriteLine(getHotelResource.Name);

            await client.DeleteHotel(createdHotel.Id);
        }
    }
}

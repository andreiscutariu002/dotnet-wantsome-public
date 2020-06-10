namespace _17MigrateToAsync
{
    using System;
    using System.Threading.Tasks;

    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var service = new NewsSearchServiceV4();
            var response = await service.GetHtmlAsync("data");
            Console.WriteLine(response);
        }
    }
}

namespace _04AsyncAwait
{
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"[Main] Start on: [{Thread.CurrentThread.ManagedThreadId}]");
            Console.WriteLine();

            // here

            await Demo01.Run();

            //var service = new NewsSearchServiceV1();

            //var response2 = await service.GetHtmlV2Async("data");

            //var response3 = await service.GetHtmlV3Async("data");

            //Console.WriteLine(response3);

            Console.WriteLine();
            Console.WriteLine($"[Main] Finished on: [{Thread.CurrentThread.ManagedThreadId}]");
        }
    }

    internal class NewsSearchServiceV1
    {
        private readonly WebClient client;

        public NewsSearchServiceV1()
        {
            this.client = new WebClient();
        }

        public string GetHtml(string search)
        {
            string response = this.client.DownloadString($"https://www.digi24.ro/cautare?q={search}");
            return response;
        }

        public Task<string> GetHtmlAsync(string search)
        {
            return Task.Run(() => this.GetHtml(search));
        }

        public async Task<string> GetHtmlV2Async(string search)
        {
            Task<string> task = this.client.DownloadStringTaskAsync($"https://www.digi24.ro/cautare?q={search}");
            
            var s = await task;

            // response s

            return s;
        }

        public Task<string> GetHtmlV3Async(string search)
        {
            Task<string> task = this.client.DownloadStringTaskAsync($"https://www.digi24.ro/cautare?q={search}");

            return task;
        }
    }
}

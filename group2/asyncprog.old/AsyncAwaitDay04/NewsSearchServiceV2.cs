namespace AsyncAwaitDay04
{
    using System.Net;
    using System.Threading.Tasks;

    internal class NewsSearchServiceV2
    {
        private readonly WebClient client;

        public NewsSearchServiceV2()
        {
            this.client = new WebClient();
        }

        public Task<string> GetHtmlAsync(string search)
        {
            Task<string> response = this.client.DownloadStringTaskAsync($"https://www.digi24.ro/cautare?q={search}");

            // to do something with response

            return response;
        }
    }
}
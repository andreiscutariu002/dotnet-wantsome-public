namespace AsyncAwaitDay04
{
    using System.Net;
    using System.Threading.Tasks;

    internal class NewsSearchServiceV4
    {
        private readonly WebClient client;

        public NewsSearchServiceV4()
        {
            this.client = new WebClient();
        }

        public async Task<string> GetHtmlAsync(string search)
        {
            string response = await this.client.DownloadStringTaskAsync($"https://www.digi24.ro/cautare?q={search}");

            // we can do something 

            return response;
        }
    }
}
namespace _17MigrateToAsync
{
    using System.Net;
    using System.Threading.Tasks;

    public class NewsSearchServiceV4
    {
        private readonly WebClient client;

        public NewsSearchServiceV4()
        {
            this.client = new WebClient();
        }

        public string GetHtml(string search)
        {
            string response = this.client.DownloadString($"https://www.digi24.ro/cautare?q={search}");
            return response;
        }

        public async Task<string> GetHtmlAsync(string search)
        {
            string value = await this.client.DownloadStringTaskAsync($"https://www.digi24.ro/cautare?q={search}");
            return value;
        }
    }
}
namespace _17MigrateToAsync
{
    using System.Net;
    using System.Threading.Tasks;

    public class NewsSearchServiceV2
    {
        private readonly WebClient client;

        public NewsSearchServiceV2()
        {
            this.client = new WebClient();
        }

        public string GetHtml(string search)
        {
            var response = this.client.DownloadString($"https://www.digi24.ro/cautare?q={search}");
            return response;
        }

        public Task<string> GetHtmlAsync(string search)
        {
            var task = this.client.DownloadStringTaskAsync($"https://www.digi24.ro/cautare?q={search}");
            return task;
        }
    }
}

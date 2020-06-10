namespace _17MigrateToAsync
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;

    public class NewsSearchServiceV1
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

        public List<string> GetHtmlList(string search)
        {
            string response = this.client.DownloadString($"https://www.digi24.ro/cautare?q={search}");
            return new List<string>{response};
        }

        public async Task<List<string>> GetHtmlListAsync(string search)
        {
            string response = await this.client.DownloadStringTaskAsync($"https://www.digi24.ro/cautare?q={search}");
            string response2 = await this.client.DownloadStringTaskAsync($"https://www.digi24.ro/cautare?q={search}");
            string response3 = await this.client.DownloadStringTaskAsync($"https://www.digi24.ro/cautare?q={search}");

            return new List<string> { response , response2 , response3 };
        }

        public Task<List<string>> GetHtmlListAsync2(string search)
        {
            Task<List<string>> task = Task.Factory.StartNew(() =>
            {
                var html = this.GetHtml(search);
                return new List<string>() { html };
            });

            return task;
        }
    }
}
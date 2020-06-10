namespace _17MigrateToAsync
{
    using System.Net;
    using System.Threading.Tasks;

    public class NewsSearchServiceV3
    {
        private readonly WebClient client;

        public NewsSearchServiceV3()
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
            return Task.Factory.StartNew(() =>
            {
                var html = this.GetHtml(search);
                return html;
            });
        }
    }
}
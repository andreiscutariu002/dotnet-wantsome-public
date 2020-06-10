namespace AsyncAwaitDay04
{
    using System.Net;

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

            // we can do something 

            return response;
        }
    }
}
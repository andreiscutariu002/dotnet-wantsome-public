namespace WebApp.Controllers
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;

    [Route("deadlock/v3")]
    public class DeadlockV3Controller : ApiController
    {
        public async Task<string> Get()
        {
            string value = await GetJsonAsync(new Uri("https://jsonplaceholder.typicode.com/posts/1"));
            return value;
        }

        public static async Task<string> GetJsonAsync(Uri uri)
        {
            var client = new HttpClient();
            var value = await client.GetStringAsync(uri);
            return value;
        }
    }
}
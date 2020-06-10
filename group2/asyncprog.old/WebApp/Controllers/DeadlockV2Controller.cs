namespace WebApp.Controllers
{
    using System;
    using System.Net.Http;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Web.Http;

    [Route("deadlock/v2")]
    public class DeadlockV2Controller : ApiController
    {
        public string Get()
        {
            Task<string> task = GetJsonAsync(new Uri("https://jsonplaceholder.typicode.com/posts/1"));
            return task.Result;
        }

        public static async Task<string> GetJsonAsync(Uri uri)
        {
            var client = new HttpClient();
            var value = await client.GetStringAsync(uri).ConfigureAwait(false);
            return value;
        }
    }
}

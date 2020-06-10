namespace WebApp.Controllers
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;

    [Route("deadlock/v1")]
    public class DeadlockV1Controller : ApiController
    {
        //public string Get()
        //{
        //    Task<string> task = GetJsonAsync(new Uri("https://jsonplaceholder.typicode.com/posts/1"));

        //    return task.Result;
        //}

        public async Task<string> Get2()
        {
            var s = await GetJsonAsync(new Uri("https://jsonplaceholder.typicode.com/posts/1"));
            return s;
        }

        public static async Task<string> GetJsonAsync(Uri uri)
        {
            var client = new HttpClient();

            var value = await client.GetStringAsync(uri);

            return value;
        }
    }
}

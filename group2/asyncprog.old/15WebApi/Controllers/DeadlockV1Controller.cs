namespace WebApi.Controllers
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("deadlock/v1")]
    public class DeadlockV1Controller : ControllerBase
    {
        public string Get()
        {
            Task<string> task = GetJsonAsync(new Uri("https://jsonplaceholder.typicode.com/posts/1"));
            return task.Result;
        }

        public static async Task<string> GetJsonAsync(Uri uri)
        {
            var client = new HttpClient();
            var value = await client.GetStringAsync(uri);
            return value;
        }
    }
}
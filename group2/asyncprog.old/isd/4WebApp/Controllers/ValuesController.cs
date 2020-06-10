namespace _4WebApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;

    // ASP
    public class JsonLibrary
    {
        public static async Task<string> GetJsonAsync(Uri uri)
        {
            var client = new HttpClient(); // use pool

            var value = await client.GetStringAsync(uri).ConfigureAwait(false); // continuation will be blocked due to main thread block. we need to use ConfigureAwait with false

            // code 

            return value;
        }
    }

    public class DeadlockController : ApiController
    {
        public string Get()
        {
            Task<string> task = JsonLibrary.GetJsonAsync(new Uri("https://jsonplaceholder.typicode.com/posts/1"));

            var taskResult = task.Result; //block main thread

            // context 

            return taskResult;
        }
    }

    public class DeadlockV2Controller : ApiController
    {
        public async Task<string> Get()
        {
            string value = await JsonLibrary.GetJsonAsync(new Uri("https://jsonplaceholder.typicode.com/posts/1"));
            return value;
        }
    }

    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new[] {"value1", "value2"};
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}

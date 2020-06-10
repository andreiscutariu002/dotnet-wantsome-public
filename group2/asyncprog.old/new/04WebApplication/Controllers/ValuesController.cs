namespace _04WebApplication.Controllers
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;

    public class DownloadController : ApiController
    {
        // GET api/values
        [Route("api/download")]
        [HttpGet]
        public string Get(CancellationToken token)
        {
            var library = new Library();

            // ASP.NET context
            var task = library.GetJsonAsync(new Uri("https://jsonplaceholder.typicode.com/posts/1"), token);

            //wait here (task to be completed)
            var result = task.Result;

            // ASP.NET context
            var task2 = library.GetJsonAsync(new Uri("https://jsonplaceholder.typicode.com/posts/2"), token);

            var user = HttpContext.Current.User;

            //static instance - user

            return result;
        }

        // GET api/values
        [Route("api/download2")]
        [HttpGet]
        public async Task<string> GetAsync()
        {
            var library = new LibraryV2();

            var result = await library.GetJsonAsync(new Uri("https://jsonplaceholder.typicode.com/posts/1"));
            
            return result;
        }
    }

    public class Library
    {
        // task is returned
        public async Task<string> GetJsonAsync(Uri uri, CancellationToken token)
        {
            var client = new HttpClient();

            var value = await client.GetAsync(uri, token);

            // blocked [should on ASP.NET context thread]
            // will run on a different thread [still ASP.NET thread pool]
            return "";
        }
    }

    public class LibraryV2
    {
        // task is returned
        public async Task<string> GetJsonAsync(Uri uri)
        {
            var client = new HttpClient();

            var value = await client.GetStringAsync(uri);

            // blocked [should on ASP.NET context thread]
            // will run on a different thread [still ASP.NET thread pool]
            return value;
        }
    }
}

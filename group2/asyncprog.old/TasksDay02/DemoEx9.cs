namespace TasksDay02
{
    using System.Net.Http;
    using System.Threading.Tasks;

    internal class DemoEx9
    {
        internal static void Run()
        {
            //https://jsonplaceholder.typicode.com/posts
            //https://jsonplaceholder.typicode.com/comments?postId=1

            var httpClient = new HttpClient();

            var comments1 = httpClient.GetAsync("https://jsonplaceholder.typicode.com/comments?postId=1");
            var comments2 = httpClient.GetAsync("https://jsonplaceholder.typicode.com/comments?postId=1");
            var comments3 = httpClient.GetAsync("https://jsonplaceholder.typicode.com/comments?postId=1");
            var comments4 = httpClient.GetAsync("https://jsonplaceholder.typicode.com/comments?postId=1");

            Task.WaitAll(comments4, comments1, comments2, comments3);
        }
    }
}
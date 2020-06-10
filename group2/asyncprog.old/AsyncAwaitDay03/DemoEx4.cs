namespace AsyncAwaitDay03
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    internal class DemoEx4
    {
        internal static void Run()
        {
            RunAsync().Wait();
        }

        // Main thread - 10s
        private static void RunSync()
        {
            var r1 = GetPost(1); //1
            var r2 = GetPost(2); //2
            var r3 = GetPost(3); //3
            var r4 = GetPost(4); //4
            //10
        }

        // Main thread - 0.1s
        // Other thread - 10s
        private static async Task RunAsync()
        {
            var r1 = await GetPostAsync(1); //1
            var r2 = await GetPostAsync(2); //2
            var r3 = await GetPostAsync(3); //3
            var r4 = await GetPostAsync(4); //4
            //10
        }

        private static object GetPost(int p)
        {
            return p;
        }

        private static async Task RunInParallelAsync()
        {
            var t1 = GetPostAsync(1);
            var t2 = GetPostAsync(2);
            var t3 = GetPostAsync(3);
            var t4 = GetPostAsync(4);

            string[] response = await Task.WhenAll(t1, t2, t3, t4);

            foreach (var r in response)
            {
                Console.WriteLine(r);
            }
        }

        private static async Task<string> GetPostAsync(int id)
        {
            var client = new HttpClient();

            var response = await client.GetAsync($"https://jsonplaceholder.typicode.com/posts/{id}");
            var value = await response.Content.ReadAsStringAsync();

            await Task.Delay(TimeSpan.FromSeconds(id));

            return value;
        }
    }
}
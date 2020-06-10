namespace _04AsyncAwait
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class Demo01
    {
        public static async Task Run()
        {
            Console.WriteLine($"Running on: [{Thread.CurrentThread.ManagedThreadId}]");

            var httpClient = new HttpClient();

            // here static instance will be available 

            var response = await httpClient.GetAsync("https://www.google.com");
            
            // here static instance will not be available

            Console.WriteLine($"Running again on: [{Thread.CurrentThread.ManagedThreadId}]");
        }
    }
}
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace _4AsyncAwait
{
    using System.Net;
    using System.Threading;

    class Program
    {
        static async Task Main(string[] args)
        {
            await Demo01.Run2();
        }
    }

    public class Demo01
    {
        public static async Task Run()
        {
            Task<string> task1 = CreateTask(1); //1s
            Task<string> task2 = CreateTask(2); //2s
            Task<string> task3 = CreateTask(3); //3s
            Task<string> task4 = CreateTask(4); //4s

            string[] results = await Task.WhenAll(task1, task2, task3, task4); //4s

            Console.WriteLine("Finished");
        }

        //10s
        //thread is not blocked
        public static async Task Run1()
        {
            string r1 = await CreateTask(1); //1s
            string r2 = await CreateTask(2); //2s
            string r3 = await CreateTask(3); //3s
            string r4 = await CreateTask(4); //4s

            Console.WriteLine("Finished");
        }

        //10s
        public static async Task Run1B()
        {
            string r1 = CreateTask(1).Result; //1s
            string r2 = CreateTask(2).Result; //2s
            string r3 = CreateTask(3).Result; //3s
            string r4 = CreateTask(4).Result; //4s

            Console.WriteLine("Finished");
        }

        public static async Task Run2()
        {
            Task<string> task1 = CreateTask(1); //1s
            Task<string> task2 = CreateTask(2); //2s
            Task<string> task3 = CreateTask(3); //3s
            Task<string> task4 = CreateTask(4); //4s

            try
            {
                var result2 = await Task.WhenAll(task1, task2, task3, task4); //task1 return //1s
                //Task.WaitAll(task1, task2, task3, task4); // task 
            }
            catch (DivideByZeroException e)
            {
            }
            catch (InvalidOperationException e)
            {
            }
            catch (AggregateException e)
            {
            }
            catch (Exception e)
            {
            }

            Thread.Sleep(TimeSpan.FromSeconds(5));

            Console.WriteLine();
        }

        private static async Task<string> CreateTask(int id)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"https://jsonplaceholder.typicode.com/posts/{id}");
            //var value = await response.Content.ReadAsStringAsync();

            if (id == 2)
            {
                throw new InvalidOperationException();
            }

            await Task.Delay(TimeSpan.FromSeconds(id));

            if (id == 3)
            {
                throw new DivideByZeroException();
            }

            return id.ToString();
        }
    }

    public class Demo02
    {
        public static async Task Run()
        {
            var task = OperationAsync();
            try
            {
                // Run as task [no state machine]
                //OperationAsync().Wait();

                // Run with async await [with state machine]
                
                //await task;

                Operation2Async();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().FullName);
            }
        }

        // Use this when void return 
        public static async Task OperationAsync()
        {
            throw new InvalidOperationException("OperationAsync");
        }

        // Not used
        public static async void Operation2Async()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));

            throw new InvalidOperationException("Operation2Async");
        }
    }

    public class Demo03
    {
        public static async Task Run()
        {
        }

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
                return response;
            }

            public Task<string> GetHtmlV1Async(string search)
            {
                Task<string> response = this.client.DownloadStringTaskAsync($"https://www.digi24.ro/cautare?q={search}");
                return response;
            }

            public async Task<string> GetHtmlV1BAsync(string search)
            {
                string response = await this.client.DownloadStringTaskAsync($"https://www.digi24.ro/cautare?q={search}");
                
                return response;
            }

            public Task<string> GetHtmlV2Async(string search)
            {
                return Task.Factory.StartNew(() =>
                {
                    var html = this.GetHtml(search);
                    return html;
                });
            }
        }
    }
}

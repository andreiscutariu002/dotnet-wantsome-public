namespace AsyncAwait
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            Task task = AsyncAwaitExceptions.Run();
            task.Wait();
        }
    }

    public class AsyncAwaitExceptions
    {
        public static async Task Run()
        {
            Task task;
            try
            {
                //task = OperationTaskAsync();
                //await task;
                //task.Wait();

                //OperationTaskAsync2();

                OperationAsync2();
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.GetType().FullName);
            }
            catch (AggregateException e)
            {
                Console.WriteLine(e.GetType().FullName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().FullName);
            }

            Thread.Sleep(3000);
        }

        public static Task OperationTaskAsync()
        {
            var task = new Task(() =>
            {
                throw new InvalidOperationException("OperationAsync");
            });
            task.Start();
            return task;
        }

        public static void OperationTaskAsync2()
        {
            new Task(() =>
            {
                throw new InvalidOperationException("OperationAsync");
            }).Start();
        }

        public static async Task OperationAsync()
        {
            throw new InvalidOperationException("OperationAsync");
        }

        public static async void OperationAsync2()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));

            throw new InvalidOperationException("Operation2Async");
        }
    }

    public class AsyncAwaitInParallel
    {
        // 1 + 2 + 3 + 4 = 10
        public static async Task Run()
        {
            var v1 = await CreateTask(1); // 1
            var v2 = await CreateTask(2); // 2
            var v3 = await CreateTask(3); // 3
            var v4 = await CreateTask(4); // 4
        }

        // 4
        public static async Task Run2()
        {
            var t1 = CreateTask(1); // 1
            var t2 = CreateTask(2); // 2
            var t3 = CreateTask(3); // 3
            var t4 = CreateTask(4); // 4

            string[] results = await Task.WhenAll(t1, t2, t3, t4);
        }

        private static async Task<string> CreateTask(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"https://jsonplaceholder.typicode.com/posts/{id}");
            string value = await response.Content.ReadAsStringAsync();

            Thread.Sleep(TimeSpan.FromSeconds(id));

            return value;
        }
    }

    public class AsyncAwaitReturnDemos
    {
        void VoidMethod()
        {
        }

        // obligat return task
        Task VoidMethodAsync()
        {
            // return task

            return new Task(() => { });
        }

        async Task VoidMethodAsync2()
        {
            // return task
        }
    }
}

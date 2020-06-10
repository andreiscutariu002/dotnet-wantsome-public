namespace TplDay05
{
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    public class Demo01
    {
        public static void Run()
        {
            Task<int>[] tasks = new Task<int>[3];

            tasks[0] = Task.Run(() => {
                Console.WriteLine("1 - start");
                Thread.Sleep(1000);
                Console.WriteLine("1 - finish");
                return 1;
            });

            tasks[1] = Task.Run(() => {
                Console.WriteLine("2 - start");
                Thread.Sleep(1000);
                Console.WriteLine("2 - finish");
                return 2;
            });

            tasks[2] = Task.Run(() => {
                Console.WriteLine("3 - start");
                Thread.Sleep(5000);
                Console.WriteLine("3 - finish");
                return 3;
            });

            Task.WaitAll(tasks);

            Console.WriteLine("Press Enter to terminate! Tasks finished!");
            Console.ReadLine();
        }
    }

    internal class EapPatternsInATask
    {
        internal static void Run()
        {
            RunAsync().Wait();
        }

        internal static async Task RunAsync()
        {
            var instance = new EapPatternsInATask();
            var result = await instance.GetData("www.google.com", new CancellationToken());
            Console.WriteLine(result);
        }

        Task<string> GetData(string url, CancellationToken token)
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();

            WebClient client = new WebClient();

            client.DownloadStringCompleted += (sender, args) =>
            {
                if (args.Cancelled)
                {
                    tcs.TrySetCanceled();
                    return;
                }

                if (args.Error != null)
                {
                    tcs.TrySetException(args.Error);
                    return;
                }

                var result = args.Result;
                tcs.TrySetResult(result);
            };

            Uri address;
            try
            {
                address = new Uri(url);
            }
            catch (Exception e)
            {
                tcs.TrySetException(e);
                return tcs.Task;
            }

            client.DownloadStringAsync(address);

            return tcs.Task;
        }
    }
}
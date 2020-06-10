namespace _18TPL
{
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    internal class EapPatternInATask
    {
        internal static void Run()
        {
            RunAsync().Wait();
        }


        internal static async Task RunAsync()
        {
            var instance = new EapPatternInATask();
            var result = await instance.GetData("https://www.google.com", new CancellationToken());
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
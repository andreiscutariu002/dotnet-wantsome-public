namespace _05TplDemos
{
    using System;
    using System.Net;
    using System.Threading.Tasks;

    internal class Demo01
    {
        internal static async Task Run()
        {
            var result = await EapEx.Run("https://dev.azure.com/dotnet-workshops/Endava-AsyncAwait-2/");
            Console.WriteLine(result);
        }
    }

    internal class EapEx
    {
        public static Task<string> Run(string url)
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
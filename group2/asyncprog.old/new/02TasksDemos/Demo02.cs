namespace _02TasksDemos
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class Demo02
    {
        public static void Run()
        {
            Task<string> task = Task.Factory.StartNew<string>(() =>
            {
                using (var wc = new System.Net.WebClient())
                {
                    Thread.Sleep(TimeSpan.FromSeconds(1));

                    throw new Exception("EXC");

                    return wc.DownloadString("http://www.linqpad.net");
                }
            });

            //task.Wait();// blocking main

            //var result = task.Result;
            //var exc = task.Exception;

            Thread.Sleep(TimeSpan.FromSeconds(1));

            Thread.Sleep(TimeSpan.FromSeconds(1));
        }
    }
}
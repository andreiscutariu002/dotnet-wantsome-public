namespace _12Tasks
{
    using System;
    using System.Collections.Concurrent;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ParallelEx
    {
        public static void Run()
        {
            var data = new ConcurrentBag<string>();

            Parallel.Invoke(
                () => data.Add(new WebClient().DownloadString("https://www.sport.ro/stiri.html")),
                () => data.Add(new WebClient().DownloadString("https://www.sport.ro/sporturi.html"))
            );

            Console.WriteLine($"finish on main thread [thread id: {Thread.CurrentThread.ManagedThreadId}]");
        }
    }
}
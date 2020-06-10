namespace _01Threading
{
    using System;
    using System.Threading;

    internal class ForegroundBackground
    {
        public static void ThreadProc()
        {
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine("ThreadProc: {0}", i);

                // Yield the rest of the time slice.
                Thread.Sleep(TimeSpan.FromMilliseconds(1000));
            }
        }

        public static void Run()
        {
            var t = new Thread(ThreadProc)
            {
                IsBackground = true
            };

            t.Start();
        }
    }
}

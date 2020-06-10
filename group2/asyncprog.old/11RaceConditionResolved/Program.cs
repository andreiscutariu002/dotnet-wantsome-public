namespace _11RaceConditionSolved
{
    using System;
    using System.Diagnostics;
    using System.Threading;

    internal class Program
    {
        private static volatile int counter;
        private static readonly object CounterLock = new object();

        public static void Main(string[] args)
        {
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(new string('*', 60));

                StartCounterConcurrently();

                counter = 0;
            }
        }

        public static void StartCounterConcurrently()
        {
            var t1 = new Thread(() => Run(ConsoleColor.Green));
            var t2 = new Thread(() => Run(ConsoleColor.Yellow));

            var stopwatch = Stopwatch.StartNew();

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            stopwatch.Stop();

            Console.ResetColor();
            Console.WriteLine($"Elapsed time: {stopwatch.Elapsed.TotalMilliseconds} ms");
        }

        public static void Run(ConsoleColor foregroundColor)
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;

            while (counter < 10)
            {
                Thread.Sleep(10);

                lock (CounterLock)
                {
                    counter++;
                    Console.ForegroundColor = foregroundColor;
                    Console.WriteLine($"[Thread:{threadId}] - {counter}");
                }
            }
        }
    }
}

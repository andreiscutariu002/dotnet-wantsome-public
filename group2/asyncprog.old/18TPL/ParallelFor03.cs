namespace _18TPL
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ParallelFor03
    {
        internal static void Run()
        {
            DateTime startDateTime = DateTime.Now;

            var options = new ParallelOptions
            {
                MaxDegreeOfParallelism = 4
            };

            var number = 10;

            Parallel.For(0, number, options, (i, breakLoopState) =>
            {
                long total = 0;
                for (var idx = 1; idx < 100000000; idx++)
                {
                    total += idx;
                }

                Console.WriteLine("{0} - {1} - thread_id: {2}", i, total, Thread.CurrentThread.ManagedThreadId);
            });

            DateTime endDateTime = DateTime.Now;
            Console.WriteLine(@"Parallel For Loop Execution end at : {0}", endDateTime);

            TimeSpan span = endDateTime - startDateTime;
            int ms = (int) span.TotalMilliseconds;
            Console.WriteLine(@"Time Taken to Execute the Loop in ms {0}", ms);
        }
    }
}
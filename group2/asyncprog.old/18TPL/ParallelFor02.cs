namespace _18TPL
{
    using System;
    using System.Threading;

    internal class ParallelFor02
    {
        internal static void Run()
        {
            DateTime startDateTime = DateTime.Now;

            var number = 10;
            for (var i = 0; i < number; i++)
            {
                long total = 0;
                for (int idx = 1; idx < 100000000; idx++)
                {
                    total += idx;
                }

                Console.WriteLine("{0} - {1} - thread_id: {2}", i, total, Thread.CurrentThread.ManagedThreadId);
            }

            DateTime endDateTime = DateTime.Now;
            Console.WriteLine(@"Parallel For Loop Execution end at : {0}", endDateTime);

            TimeSpan span = endDateTime - startDateTime;
            int ms = (int)span.TotalMilliseconds;
            Console.WriteLine(@"Time Taken to Execute the Loop in ms {0}", ms);
        }

        static long DoSomeIndependentTask()
        {
            long total = 0;

            for (int i = 1; i < 100000000; i++)
            {
                total += i;
            }

            return total;
        }
    }
}
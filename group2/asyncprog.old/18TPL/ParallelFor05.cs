namespace _18TPL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;


    internal class ParallelFor05
    {
        internal static void Run2()
        {
            DateTime startDateTime = DateTime.Now;

            int[] nums = Enumerable.Range(0, 100000000).ToArray();

            long total = 0;

            var options = new ParallelOptions
            {
                MaxDegreeOfParallelism = 4
            };

            Func<long> localInit = () =>
            {
                //Console.WriteLine($"localInit call on thread = {Thread.CurrentThread.ManagedThreadId}");

                return 0;
            };

            Func<int, ParallelLoopState, long, long> body = (idx, parallelLoopState, subtotal) =>
            {
                //Console.WriteLine($"body call on thread = {Thread.CurrentThread.ManagedThreadId}, {subtotal} add with {nums[idx]} => {subtotal + nums[idx]}");

                subtotal += nums[idx];

                return subtotal;
            };

            Action<long> localFinally = localTotal =>
            {
                //Console.WriteLine($"localFinally call on thread = {Thread.CurrentThread.ManagedThreadId}. Local Result = {localTotal}");
                Interlocked.Add(ref total, localTotal);
            };

            Parallel.For(0, nums.Length, localInit, body, localFinally);

            DateTime endDateTime = DateTime.Now;

            TimeSpan span = endDateTime - startDateTime;
            int ms = (int)span.TotalMilliseconds;
            Console.WriteLine(@"Time in ms {0}", ms);

            Console.WriteLine($"Result {total}");

            // Time in ms 408
            // Result 4999999950000000
        }

        internal static void Run()
        {
            DateTime startDateTime = DateTime.Now;

            int[] nums = Enumerable.Range(0, 100000000).ToArray();

            long total = 0;
            var locker = new object();

            Parallel.For(0, nums.Length, i =>
            {
                lock (locker)
                {
                    total += i;
                }
            });

            DateTime endDateTime = DateTime.Now;

            TimeSpan span = endDateTime - startDateTime;
            int ms = (int)span.TotalMilliseconds;
            Console.WriteLine(@"Time in ms {0}", ms);

            Console.WriteLine($"Result {total}");

            //Time in ms 4398
            //Result 4999999950000000
        }
    }
}
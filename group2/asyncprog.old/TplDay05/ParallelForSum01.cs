namespace TplDay05
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ParallelForSum01
    {
        internal static void Run()
        {
            DateTime startDateTime = DateTime.Now;

            int[] nums = Enumerable.Range(0, 100000000).ToArray();

            long total = 0;
            var locker = new object();

            Parallel.For(0, nums.Length, i =>
            {
                //lock (locker)
                //{
                //    total += i;
                //}

                Interlocked.Add(ref total, i);
            });

            DateTime endDateTime = DateTime.Now;

            TimeSpan span = endDateTime - startDateTime;
            int ms = (int)span.TotalMilliseconds;
            Console.WriteLine(@"Time in ms {0}", ms);

            Console.WriteLine($"Result {total}");

            //lock
            //Time in ms 4343
            //Result 4999999950000000

            //Interlocked
            //Time in ms 2958
            //Result 4999999950000000
        }
    }
}
namespace TplDay05
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ParallelForLocalThreadVariables
    {
        internal static void Run()
        {
            DateTime startDateTime = DateTime.Now;

            int[] nums = Enumerable.Range(0, 10).ToArray();

            long total = 0;

            // is called when the iteration will start at thread level

            // is called iteration with iteration at thread level, current iteration index (idx), and thread local variable (subtotal) is passed 

            // last iteration at thread level. here we need to sync stuff

            Parallel.ForEach(nums, () => 0, (num, parallelLoopState, subtotal) =>
            {
                subtotal += num;
                return subtotal;
            }, localTotal =>
            {
                Interlocked.Add(ref total, localTotal);
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

            //Thread local
            //Time in ms 414
            //Result 4999999950000000
        }
    }
}
namespace _05TplDemos
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;


    internal class Demo06
    {
        internal static async Task Run()
        {
            var nums = Enumerable.Range(0, 100000000).ToArray();
            long total = 0;
            var o = new object();

            //Parallel.For(0, nums.Length, (indx) =>
            //{
            //    lock (o)
            //    {
            //        total += nums[indx];
            //    }
            //});

            var options = new ParallelOptions
            {
                MaxDegreeOfParallelism = 8
            };

            //4
            Func<long> start = () => { return 0; };

            //a lot of times
            Func<int, ParallelLoopState, long, long> middle = (idx, parallelLoopState, currentValue) =>
            {
                return currentValue + nums[idx];
            };

            //4
            Action<long> end = local => { Interlocked.Add(ref total, local); };

            Parallel.For(0, nums.Length, options, start, middle, end);

            Console.WriteLine(total);
        }
    }
}

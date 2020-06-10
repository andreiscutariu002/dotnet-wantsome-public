namespace _05TplDemos
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal class Demo09
    {
        public static async Task Run()
        {
            var nums = Enumerable.Range(1, 200);

            // Replace NotBuffered with AutoBuffered 
            // or FullyBuffered to compare behavior.
            var scanLines = from n in nums.AsParallel()
                    //.WithMergeOptions(ParallelMergeOptions.AutoBuffered)
                   // .WithMergeOptions(ParallelMergeOptions.NotBuffered)
                   .WithMergeOptions(ParallelMergeOptions.FullyBuffered)
                where n % 2 == 0
                select ExpensiveFunc(n);

            Stopwatch sw = Stopwatch.StartNew();
            foreach (var line in scanLines)
            {
                Console.WriteLine(line);
            }

            Console.WriteLine("Elapsed time: {0} ms. Press any key to exit.",
                sw.ElapsedMilliseconds);
            Console.ReadKey();
        }


        static string ExpensiveFunc(int i)
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(300));
            return $"{i} ****************************";
        }
    }
}
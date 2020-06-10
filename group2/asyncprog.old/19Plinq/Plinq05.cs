namespace _19Plinq
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;

    public class Plinq05
    {
        public static void Run()
        {
            var nums = Enumerable.Range(1, 100);

            // Replace NotBuffered with AutoBuffered 
            // or FullyBuffered to compare behavior.
            var scanLines = from n in nums.AsParallel()
                    .WithMergeOptions(ParallelMergeOptions.AutoBuffered)
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
            Thread.Sleep(TimeSpan.FromMilliseconds(200));
            return $"{i} ****************************";
        }
    }
}
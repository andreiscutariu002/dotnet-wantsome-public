namespace _05TplDemos
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    internal class Demo08
    {
        internal static async Task Run()
        {
            DateTime startDateTime = DateTime.Now;

            var source = Enumerable.Range(1, 100000000).ToList();

            // Opt in to PLINQ with AsParallel.
            var evenNums = source.AsParallel().WithExecutionMode(ParallelExecutionMode.ForceParallelism).Where(num => num % 2 == 0);

            Console.WriteLine("{0} even numbers out of {1} total",
                evenNums.Count(), source.Count());

            DateTime endDateTime = DateTime.Now;

            TimeSpan span = endDateTime - startDateTime;
            int ms = (int)span.TotalMilliseconds;
            Console.WriteLine(@"Time in ms {0}", ms);
        }
    }
}
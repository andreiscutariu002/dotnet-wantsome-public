namespace _05TplDemos
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class Demo04
    {
        internal static async Task Run()
        {
            var ct = new CancellationTokenSource().Token;
            var options = new ParallelOptions
            {
                MaxDegreeOfParallelism = 4,
            };

            var number = 10;

            Parallel.For(0, number, options, (i, loopstate) => {
                loopstate.Break();
                loopstate.Stop();

                long total = 0;
                for (int idx = 1; idx < 100000000; idx++)
                {
                    total += idx;
                }

                Console.WriteLine("{0} - {1} - thread_id: {2}", i, total, Thread.CurrentThread.ManagedThreadId);
            });
        }
    }
}
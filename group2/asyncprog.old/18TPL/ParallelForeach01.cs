namespace _18TPL
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ParallelForeach01
    {
        internal static void Run()
        {
            int[] nums = Enumerable.Range(0, 10000000).ToArray();
            long total = 0;

            Parallel.ForEach<int, long>(nums,   // source collection
                () => 0,                        // method to initialize the local variable
                (j, loop, subtotal) =>          // method invoked by the loop on each iteration
                {
                    subtotal += j;              // modify local variable [actual value is passed, not the index from the array]
                    return subtotal;            // value to be passed to next iteration
                },
                // method to be executed when each partition has completed.
                // finalResult is the final value of subtotal for a particular partition.
                (finalResult) => Interlocked.Add(ref total, finalResult)
            );

            Console.WriteLine("The total from Parallel.ForEach is {0:N0}", total);
        }
    }
}
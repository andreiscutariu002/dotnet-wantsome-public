namespace _18TPL
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ParallelFor06
    {
        internal static void Run()
        {
            int[] nums = Enumerable.Range(0, 10000000).ToArray();
            CancellationTokenSource cts = new CancellationTokenSource();

            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cts.Token,
                MaxDegreeOfParallelism = System.Environment.ProcessorCount
            };

            try
            {
                Parallel.ForEach(nums, options, (num) =>
                {
                    options.CancellationToken.ThrowIfCancellationRequested();
                });
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                cts.Dispose();
            }
        }
    }
}
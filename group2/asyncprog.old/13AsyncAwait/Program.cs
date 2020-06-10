namespace _13AsyncAwait
{
    using System;

    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine($"Program started at: {DateTime.Now:T}");
                //new AsyncAwait().Run().Wait();
                ParallelHttpCalls.Run1().Wait();
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e);
            }
            catch (AggregateException e)
            {
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Console.WriteLine($"Program finished at: {DateTime.Now:T}");
                Console.ReadKey();
            }
        }
    }
}

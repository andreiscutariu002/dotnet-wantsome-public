namespace _12TasksExceptions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class TaskSimpleExceptions
    {
        internal static void Run()
        {
            var x = 0;

            var calc = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Hello from child thread {Thread.CurrentThread.ManagedThreadId}");

                var y = 7 / x;
            });

            Console.WriteLine($"Hello from main thread {Thread.CurrentThread.ManagedThreadId}");

            try
            {
                calc.Wait();
            }
            catch (AggregateException aex)
            {
                Console.Write(aex.Message); // Attempted to divide by 0
            }
        }
    }
}

namespace TasksDay02
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class DemoEx3
    {
        internal static void Run()
        {
            var x = 0;

            Func<int> function = () =>
            {
                Console.WriteLine($"Hello from thread {Thread.CurrentThread.ManagedThreadId}");
                return 7 / x;
            };

            var task = Task.Factory.StartNew(function);

            Console.WriteLine($"Hello from thread {Thread.CurrentThread.ManagedThreadId}");

            try
            {
                Console.WriteLine(task.Result);
            }
            catch (AggregateException aex)
            {
                foreach (var e in aex.Flatten().InnerExceptions)
                {
                    Console.WriteLine("Exception: " + e.GetType().Name);
                }
            }
        }
    }
}
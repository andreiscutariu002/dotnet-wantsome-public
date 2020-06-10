namespace TplDay05
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ParallelInvoke01
    {
        internal static void Run()
        {
            Parallel.Invoke(
                SomeAction,     // Invoking Normal Method
                delegate        // Invoking an inline delegate 
                {
                    Console.WriteLine($"Method 2, Thread={Thread.CurrentThread.ManagedThreadId}");
                },
                () =>           // Invoking a lambda expression
                {
                    Console.WriteLine($"Method 3, Thread={Thread.CurrentThread.ManagedThreadId}");
                    Task.Delay(TimeSpan.FromSeconds(3)).Wait();
                }
            );

            Console.WriteLine("Press any key to exist.");
            Console.ReadKey();
        }

        private static void SomeAction()
        {
            Console.WriteLine($"Method 1, Thread={Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
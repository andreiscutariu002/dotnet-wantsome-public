namespace _12TasksExceptions
{
    using System;
    using System.Threading;

    internal class ThreadSimpleExceptions
    {
        internal static void Run()
        {
            var calc = new Thread(() =>
            {
                var x1 = 0;

                Console.WriteLine($"Hello from child thread {Thread.CurrentThread.ManagedThreadId}");

                var y1 = 7 / x1;
            });

            calc.Start();

            Console.WriteLine($"Hello from main thread {Thread.CurrentThread.ManagedThreadId}");

            try
            {
                calc.Join();
            }
            catch (Exception aex)
            {
                Console.Write(aex.Message); // Attempted to divide by 0
            }
        }
    }
}
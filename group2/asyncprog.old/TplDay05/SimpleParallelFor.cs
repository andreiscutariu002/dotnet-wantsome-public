namespace TplDay05
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class SimpleParallelFor
    {
        internal static void Run()
        {
            Console.WriteLine("C# For Loop");
            var number = 10;
            for (var count = 0; count < number; count++)
            {
                //Thread.CurrentThread.ManagedThreadId returns an integer that 
                //represents a unique identifier for the current managed thread.

                Console.WriteLine($"value of count = {count}, thread = {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            Console.WriteLine();
            Console.WriteLine("Parallel For Loop");
            Parallel.For(0, number, count =>
            {
                Console.WriteLine($"value of count = {count}, thread = {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(TimeSpan.FromSeconds(1));
            });
            Console.ReadLine();
        }
    }
}
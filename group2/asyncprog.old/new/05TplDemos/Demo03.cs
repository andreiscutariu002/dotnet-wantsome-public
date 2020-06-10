namespace _05TplDemos
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class Demo03
    {
        internal static async Task Run()
        {
            var number = 10;
          
            Console.WriteLine();
            Console.WriteLine("Parallel For Loop");
            Parallel.For(0, number, currentIdx =>
            {
                Console.WriteLine($"value of count = {currentIdx}, thread = {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(10);
            });
        }
    }
}
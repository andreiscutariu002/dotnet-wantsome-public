namespace AsyncAwaitDay03
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class DemoEx3
    {
        internal static void Run()
        {
            var task = CallCount();
            task.Wait();

            Console.WriteLine($"finish on main thread [thread id: {Thread.CurrentThread.ManagedThreadId}]");
        }

        private static async Task CallCount()
        {
            var task = new Task(Count);

            task.Start();

            Console.WriteLine($"just before await, [thread id: {Thread.CurrentThread.ManagedThreadId}]");

            await task;

            Console.WriteLine($"callCount completed, [thread id: {Thread.CurrentThread.ManagedThreadId}]");
        }

        private static void Count()
        {
            for (var i = 0; i < 5; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(2));

                Console.WriteLine($"count loop: {i}, [thread id: {Thread.CurrentThread.ManagedThreadId}]");
            }
        }
    }
}
namespace _03AsyncAwait
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class Demo03
    {
        public static void Run()
        {
            var task = CallCount();
            task.Wait();

            Console.WriteLine($"finish on main thread [thread id: {Thread.CurrentThread.ManagedThreadId}]");
        }

        private static async Task CallCount()
        {
            var task = new Task(Count);

            task.Start();

            for (var i = 0; i < 3; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine($"writing from callCount loop: {i}, [thread id: {Thread.CurrentThread.ManagedThreadId}]");
            }

            Console.WriteLine($"just before await, [thread id: {Thread.CurrentThread.ManagedThreadId}]");

            await task;

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"call in continuation. task completed, [thread id: {Thread.CurrentThread.ManagedThreadId}]");
                Thread.Sleep(1000);
            }

            Console.WriteLine($"call in continuation completed, [thread id: {Thread.CurrentThread.ManagedThreadId}]");
        }

        private static void Count()
        {
            for (var i = 0; i < 5; i++)
            {
                Thread.Sleep(2000);

                Console.WriteLine($"count loop: {i}, [thread id: {Thread.CurrentThread.ManagedThreadId}]");
            }
        }
    }
}
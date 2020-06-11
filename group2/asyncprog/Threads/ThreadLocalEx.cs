namespace Threads
{
    using System;
    using System.Threading;

    internal class ThreadLocalEx
    {
        [ThreadStatic] static int counter;

        public static void Run()
        {
            Thread t1 = new Thread(() =>
            {
                counter++;
                counter++;
                counter++;
                counter++;
                Console.WriteLine($"[tid: {Thread.CurrentThread.ManagedThreadId}] - {counter}");
            });

            Thread t2 = new Thread(() =>
            {
                counter--;
                counter--;
                Console.WriteLine($"[tid: {Thread.CurrentThread.ManagedThreadId}] - {counter}");
            });

            //start thread t1 and t2
            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            Console.WriteLine($"[tid: {Thread.CurrentThread.ManagedThreadId}] - {counter}");

            Console.WriteLine("Press enter to terminate!");
            Console.Read();
        }
    }
}

namespace Threads
{
    using System;
    using System.Threading;

    public static class JoinThread
    {
        public static void Run()
        {
            Thread t = new Thread(SomeMethod);
            Thread t2 = new Thread(SomeMethod);

            t.Start();
            t.Join();

            t2.Start();
            t2.Join();

            Console.WriteLine($"[tid: {Thread.CurrentThread.ManagedThreadId}] Finish work!");
            Console.ReadLine();
        }

        private static void SomeMethod()
        {
            Console.WriteLine($"[tid: {Thread.CurrentThread.ManagedThreadId}] Hello world from thread! [Start]");

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"[tid: {Thread.CurrentThread.ManagedThreadId}] step: {i}");

                // simulate some work
                Thread.Sleep(TimeSpan.FromMilliseconds(1000));
            }

            Console.WriteLine($"[tid: {Thread.CurrentThread.ManagedThreadId}] Hello world from thread! [End]");
        }
    }
}

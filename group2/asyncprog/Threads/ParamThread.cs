namespace Threads
{
    using System;
    using System.Threading;

    public static class ParamThread
    {
        public static void Run()
        {
            Thread t = new Thread(SomeMethod);
            t.Start(0);

            Thread t2 = new Thread(SomeMethod);
            t2.Start(10);

            Thread t3 = new Thread(SomeMethod);
            t3.Start(20);

            t.Join();
            t2.Join();
            t3.Join();

            Console.WriteLine($"[tid: {Thread.CurrentThread.ManagedThreadId}] Hello world from main thread!");
            Console.ReadLine();
        }

        private static void SomeMethod(object start)
        {
            int startVal = (int) start;

            Console.WriteLine($"[tid: {Thread.CurrentThread.ManagedThreadId}] Hello world from thread! [Start]");

            for (int i = startVal; i < startVal + 10; i++)
            {
                Console.WriteLine($"[tid: {Thread.CurrentThread.ManagedThreadId}] step: {i}");

                // simulate some work
                Thread.Sleep(TimeSpan.FromMilliseconds(1000));
            }

            Console.WriteLine($"[tid: {Thread.CurrentThread.ManagedThreadId}] Hello world from thread! [End]");
        }
    }
}

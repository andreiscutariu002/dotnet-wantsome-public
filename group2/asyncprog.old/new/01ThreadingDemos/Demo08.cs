namespace _01ThreadingDemos
{
    using System;
    using System.Threading;

    public class Demo07
    {
        static CountdownEvent _countdown = new CountdownEvent(4);

        public static void Run()
        {
            new Thread(SaySomething).Start("I am thread 1");
            new Thread(SaySomething).Start("I am thread 2");
            new Thread(SaySomething).Start("I am thread 3");

            _countdown.Wait();   // Blocks until Signal has been called 3 times
            Console.WriteLine("All threads have finished speaking!");
        }

        static void SaySomething(object thing)
        {
            Thread.Sleep(1000);
            Console.WriteLine(thing);
            _countdown.Signal();
        }
    }

    public class Demo08
    {
        private static readonly EventWaitHandle WaitHandle = new AutoResetEvent(false);

        public static void Run()
        {
            new Thread(Waiter).Start();
            Thread.Sleep(1000); // Pause for a second...
            WaitHandle.Set(); // Wake up the Waiter
        }

        private static void Waiter()
        {
            Console.WriteLine("Waiting...");

            WaitHandle.WaitOne(); // Wait for notification

            Console.WriteLine("Notified");
        }
    }
}
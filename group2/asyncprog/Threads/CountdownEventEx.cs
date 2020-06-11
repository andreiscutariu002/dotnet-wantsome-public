namespace Threads
{
    using System;
    using System.Threading;

    internal class CountdownEventEx
    {
        static CountdownEvent _countdown = new CountdownEvent(3);

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
            Thread.Sleep(4000);
            Console.WriteLine(thing);
            _countdown.Signal();
        }
    }
}

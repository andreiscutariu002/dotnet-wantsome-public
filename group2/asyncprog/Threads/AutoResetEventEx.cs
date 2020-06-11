namespace Threads
{
    using System;
    using System.Threading;

    internal class AutoResetEventEx
    {
        private static readonly EventWaitHandle WaitHandle = new AutoResetEvent(false);

        public static void Run()
        {
            new Thread(Waiter).Start();
            Thread.Sleep(3000); // Pause for a second...
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

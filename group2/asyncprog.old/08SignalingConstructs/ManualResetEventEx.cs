namespace _081SignalingConstructs
{
    using System;
    using System.Threading;

    internal class ManualResetEventEx
    {
        private static readonly EventWaitHandle WaitHandle = new ManualResetEvent(false);

        public static void Run()
        {
            new Thread(Waiter).Start();
            new Thread(Waiter).Start();
            new Thread(Waiter).Start();

            Thread.Sleep(1000); // Pause for a second...
            WaitHandle.Set(); // Wake up the Waiter
        }

        private static void Waiter()
        {
            Console.WriteLine("Waiting...");
            
            WaitHandle.WaitOne(); // Wait for notification

            Console.WriteLine("Notified");

            Thread.Sleep(1000);
        }
    }
}
namespace _081SignalingConstructs
{
    using System;
    using System.Threading;

    internal class AutoResetEventEx
    {
        private static readonly EventWaitHandle WaitHandle = new AutoResetEvent(false);

        public static void Run()
        {
            new Thread(Waiter).Start();
            
            new Thread(Waiter).Start();

            //new Thread(Waiter).Start();

            Thread.Sleep(1000); // Pause for a second...
            WaitHandle.Set(); // Wake up the Waiter
            WaitHandle.Set(); // Wake up the Waiter
            WaitHandle.Set(); // Wake up the Waiter
        }

        private static void Waiter()
        {
            Console.WriteLine($"Waiting... [{Thread.CurrentThread.ManagedThreadId}]");
            
            WaitHandle.WaitOne(); // Wait for notification
            
            Console.WriteLine($"Notified [{Thread.CurrentThread.ManagedThreadId}]");

            Thread.Sleep(1000);

            Console.WriteLine($"Finished [{Thread.CurrentThread.ManagedThreadId}]");
        }
    }
}
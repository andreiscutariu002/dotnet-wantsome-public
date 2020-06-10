namespace _02ThreadPool
{
    using System;
    using System.Threading;

    internal class Program
    {
        public static void Main()
        {
            // call QueueUserWorkItem to queue your work item
            ThreadPool.QueueUserWorkItem(SomeMethod, "Object info param");

            Console.WriteLine($"Hello from thread with id: {Thread.CurrentThread.ManagedThreadId}");

            Console.WriteLine("Press Enter to terminate!");
            Console.ReadLine();
        }

        //your custom method you want to run in another thread
        public static void SomeMethod(object stateInfo)
        {
            if (stateInfo == null)
            {
                Console.WriteLine("State info is null");
            }
            else
            {
                Console.WriteLine($"State is: {stateInfo}");
            }

            Console.WriteLine($"Hello from thread with id: {Thread.CurrentThread.ManagedThreadId}");

            // No state object was passed to QueueUserWorkItem, so stateInfo is null.
            Console.WriteLine("Hello World!");
        }
    }
}

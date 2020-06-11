namespace Threads
{
    using System;
    using System.Threading;

    public static class StartThread
    {
        public static void Run()
        {
            //initialize a thread class object 
            //and pass your custom method name to the constructor parameter
            Thread t = new Thread(SomeMethod);

            //start running your thread
            t.Start();

            //while thread is running in parallel 
            //you can carry out other operations here        

            Console.WriteLine($"[tid: {Thread.CurrentThread.ManagedThreadId}] Hello world from main thread!");
            Console.ReadLine();
        }

        // public delegate void ThreadStart()  
        // respect the delegate signature:
        // public delegate void ThreadStart()  
        private static void SomeMethod()
        {
            //your code here that you want to run parallel
            //most of the time it will be a CPU bound operation

            Console.WriteLine($"[tid: {Thread.CurrentThread.ManagedThreadId}] Hello world from thread!");
        }
    }
}

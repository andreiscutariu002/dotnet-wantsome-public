namespace _01Threading
{
    using System;
    using System.Threading;

    public class ThreadCreateEx
    {
        public static void Run()
        {
            //initialize a thread class object 
            //And pass your custom method name to the constructor parameter
            var t = new Thread(SomeMethod);

            //start running your thread
            t.Start();

            //while thread is running in parallel 
            //you can carry out other operations here        

            Console.WriteLine("Press Enter to terminate! From T_ID:" + Thread.CurrentThread.ManagedThreadId);

            //Console.ReadLine();
        }

        // respect the deletegate signature:
        // public delegate void ThreadStart()  
        private static void SomeMethod()
        {
            //your code here that you want to run parallel
            //most of the time it will be a CPU bound operation

            Console.WriteLine("S-> Hello World! From T_ID: " + Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(TimeSpan.FromSeconds(5));

            Console.WriteLine("-> Hello World! From T_ID: " + Thread.CurrentThread.ManagedThreadId);
        }
    }
}

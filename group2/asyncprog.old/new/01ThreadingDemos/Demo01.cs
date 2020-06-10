namespace _01ThreadingDemos
{
    using System;
    using System.Threading;

    public class Demo01
    {
        public static void Run()
        {
            //initialize a thread class object 
            //And pass your custom method name to the constructor parameter
            Thread t = new Thread(SomeMethod);

            //start running your thread
            t.Start();

            //wait thread to finish
            t.Join();

            //while thread is running in parallel 
            //you can carry out other operations here        

            Console.WriteLine($"[T_ID:{Thread.CurrentThread.ManagedThreadId}] Press Enter to terminate!");
            Console.ReadLine();
        }

        public static void SomeMethod()
        {
            FileHelper.ReadFile();
        }
    }
}
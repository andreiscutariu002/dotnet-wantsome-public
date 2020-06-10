namespace _01Threading
{
    using System;
    using System.Threading;

    public class StopThread
    {
        //set to volatile as its liable to change so we JIT to don't cache the value
        private static volatile bool cancel;

        public static void Run()
        {
            //initialize a thread class object 
            //And pass your custom method name to the constructor parameter

            var t = new Thread(Print);

            //start running your thread
            //don't forget to pass your parameter for the 
            //Speak method (ParameterizedThreadStart delegate) in Start method
            t.Start("Hello World!");

            //wait for 5 secs while Speak method print Hello World! for multiple times
            Thread.Sleep(5000);

            //t.Abort();

            //t.Interrupt();

            //signal thread to terminate
            cancel = true;

            //wait until CLR confirms that thread is shutdown
            t.Join();
        }

        private static void Print(object s)
        {
            while (!cancel)
            {
                var say = s as string;
                Console.WriteLine(say);
            }
        }
    }
}

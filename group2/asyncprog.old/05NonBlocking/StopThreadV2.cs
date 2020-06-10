namespace _05NonBlocking
{
    using System;
    using System.Threading;

    public class StopThreadV2
    {
        private static bool cancel;

        private static object locker = new object();

        public static void Run()
        {
            Console.WriteLine(DateTime.Now);
            //initialize a thread class object 
            //And pass your custom method name to the constructor parameter

            var t = new Thread(Go);

            //start running your thread
            //don't forget to pass your parameter for the 
            //Speak method (ParameterizedThreadStart delegate) in Start method
            t.Start();

            //wait for 5 secs while Speak method print Hello World! for multiple times
            Thread.Sleep(1000);

            //signal thread to terminate
            cancel = true;

            //wait until CLR confirms that thread is shutdown
            t.Join();
            Console.WriteLine(DateTime.Now);
        }

        private static void Go()
        {
            bool toggle = false;

            while (!cancel)
            {
                toggle = !toggle;
            }
        }
    }
}
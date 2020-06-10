namespace _06Monitors
{
    using System;
    using System.Threading;

    internal class DeadlockEx
    {
        public static void Run()
        {
            var locker1 = new object();
            var locker2 = new object();


            new Thread(() =>
            {
                Console.WriteLine("Child thread");

                lock (locker1) 
                {
                    Thread.Sleep(1000);

                    lock (locker2)
                    {
                        Console.WriteLine("Child thread lock ok");
                    }
                }
            }).Start();

            Console.WriteLine("Main thread");

            lock (locker2)
            {
                Thread.Sleep(1000);

                lock (locker1)
                {
                    Console.WriteLine("Main thread lock ok");
                }

                ;
            }
        }
    }
}

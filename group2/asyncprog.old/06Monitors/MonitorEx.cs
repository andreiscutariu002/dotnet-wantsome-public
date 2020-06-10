namespace _06Monitors
{
    using System;
    using System.Threading;

    internal class MonitorEx
    {
        private static int sum;
        private static readonly object Lock = new object();

        public static void Run()
        {
            //create thread t1 using anonymous method
            var t1 = new Thread(() =>
            {
                for (var i = 0; i < 10000000; i++)
                {
                    //acquire lock ownership
                    Monitor.Enter(Lock);

                    //increment sum value
                    sum++;

                    //release lock ownership
                    Monitor.Exit(Lock);
                }
            });

            //create thread t2 using anonymous method
            var t2 = new Thread(() =>
            {
                for (var i = 0; i < 10000000; i++)
                {
                    //acquire lock ownership
                    Monitor.Enter(Lock);

                    //increment sum value
                    sum++;

                    //release lock ownership
                    Monitor.Exit(Lock);
                }
            });


            //start thread t1 and t2
            t1.Start();
            t2.Start();

            //wait for thread t1 and t2 to finish their execution
            t1.Join();
            t2.Join();

            //write final sum on screen
            Console.WriteLine("sum: " + sum);

            Console.WriteLine("Press enter to terminate!");
            Console.ReadLine();
        }
    }
}

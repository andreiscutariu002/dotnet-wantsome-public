namespace Threads
{
    using System;
    using System.Threading;

    internal class RaceSimpleInterlocked
    {
        private static int sum;

        public static void Run()
        {
            Thread t1 = new Thread(() =>
            {
                for (int i = 0; i < 10000000; i++)
                {
                    Interlocked.Increment(ref sum);
                }
            });

            Thread t2 = new Thread(() =>
            {
                for (int i = 0; i < 10000000; i++)
                {
                    Interlocked.Increment(ref sum);
                }
            });

            //start thread t1 and t2
            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            //write final sum on screen
            Console.WriteLine("sum: " + sum);

            //sum: 10961442
            //sum: 10446924
            //sum: 11215789

            //sum: 20000000
            Console.WriteLine("Press enter to terminate!");
        }
    }
}

namespace _01ThreadingDemos
{
    using System;
    using System.Threading;

    public class Demo04
    {
        private static long sum;
        private static object _lock = new object();

        public static void Run()
        {
            //create thread t1 using anonymous method
            Thread t1 = new Thread(() => {
                long localSum = 0;

                for (int i = 0; i < 10000000; i++)
                {
                    localSum += i;
                }

                lock (_lock)
                {
                    sum += localSum;
                }
            });

            //create thread t2 using anonymous method
            Thread t2 = new Thread(() =>
            {
                long localSum = 0;
                for (int i = 10000000; i < 20000000; i++)
                {
                    localSum += i;
                }

                lock (_lock)
                {
                    sum += localSum;
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
            //199999990000000
            //138844983444094
            //199999990000000
            Console.WriteLine("Press enter to terminate!");
            Console.ReadLine();
        }
    }
}
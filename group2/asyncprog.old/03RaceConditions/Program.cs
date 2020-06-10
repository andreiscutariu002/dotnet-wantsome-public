namespace _03RaceConditions
{
    using System;
    using System.Threading;

    internal class Program
    {
        private static int sum;

        private static void Main(string[] args)
        {
            //create thread t1 using anonymous method
            var t1 = new Thread(() =>
            {
                for (var i = 0; i < 10000000; i++)
                    //increment sum value
                    sum++;
            });

            //create thread t2 using anonymous method
            var t2 = new Thread(() =>
            {
                for (var i = 0; i < 10000000; i++)
                    //increment sum value
                    sum++;
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

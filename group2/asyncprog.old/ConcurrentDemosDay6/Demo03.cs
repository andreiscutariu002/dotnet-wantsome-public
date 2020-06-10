namespace ConcurrentDemosDay6
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;

    internal class Demo03
    {
        public static void Run()
        {
            var bag = new ConcurrentBag<int>();

            var t1 = Task.Factory.StartNew(() =>
            {
                for (var i = 1; i < 10; ++i)
                {
                    bag.Add(i);
                    Thread.Sleep(200);
                }
            });


            var t3 = Task.Factory.StartNew(() =>
            {
                for (var i = 100; i < 110; ++i)
                {
                    bag.Add(i);
                    Thread.Sleep(200);
                }
            });

            var t2 = Task.Factory.StartNew(() =>
            {
                var i = 0;
                while (i != 4)
                {
                    foreach (var item in bag)
                    {
                        Console.Write(item + " ");
                        Thread.Sleep(200);
                    }

                    Console.WriteLine();

                    i++;
                    Thread.Sleep(200);
                }
            });

            Task.WaitAll(t1, t2, t3);
        }
    }
}
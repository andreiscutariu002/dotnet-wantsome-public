namespace _05TplDemos
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    internal class Demo05
    {
        internal static async Task Run()
        {
            object _lock = new object();
            DateTime startDateTime = DateTime.Now;

            long total = 0;

            Task t1 = Task.Run(() =>
            {
                long agg = 0;
                
                for (int i = 0; i < 25000000; i++)
                {
                    agg += i;
                }

                lock (_lock)
                {
                    total += agg;
                }
            });

            Task t2 = Task.Run(() =>
            {
                long agg = 0;
                for (int i = 25000000; i < 50000000; i++)
                {
                    agg += i;
                }

                lock (_lock)
                {
                    total += agg;
                }
            });

            Task t3 = Task.Run(() =>
            {
                long agg = 0;
                for (int i = 50000000; i < 75000000; i++)
                {
                    agg += i;
                }

                lock (_lock)
                {
                    total += agg;
                }
            });

            Task t4 = Task.Run(() =>
            {
                long agg = 0;
                for (int i = 75000000; i < 100000000; i++)
                {
                    agg += i;
                }

                lock (_lock)
                {
                    total += agg;
                }
            });

            Task.WaitAll(t1, t2, t3, t4);

            DateTime endDateTime = DateTime.Now;

            TimeSpan span = endDateTime - startDateTime;
            int ms = (int)span.TotalMilliseconds;
            Console.WriteLine(@"Time in ms {0}", ms);

            //Time in ms 4217
            //Result 4999999950000000

            //Time in ms 88
            //Result 4999999950000000

            Console.WriteLine($"Result {total}");
        }
    }
}
namespace _20ConcurrentDemos
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;

    public class CountdownEventDemo
    {
        public static void Run()
        {
            ConcurrentBag<int> bag = new ConcurrentBag<int>();
            CountdownEvent cde = new CountdownEvent(20);

            void Consumer()
            {
                var rand = new Random();
                
                while (true)
                {
                    int number = rand.Next();
                    
                    if (number % 2 == 0)
                    {
                        //Console.WriteLine($"Number {number} processed on thread: {Thread.CurrentThread.ManagedThreadId}");
                        bag.Add(number);
                        cde.Signal();
                    }
                }
            };

            var cts = new CancellationTokenSource();

            Task.Factory.StartNew(Consumer, cts.Token);
            Task.Factory.StartNew(Consumer, cts.Token);
            Task.Factory.StartNew(Consumer, cts.Token);
            Task.Factory.StartNew(Consumer, cts.Token);

            cde.Wait(cts.Token);
            cts.Cancel();

            foreach (var v in bag)
            {
                Console.WriteLine(v);
            }

            Console.WriteLine("Press any key to exit!");
            Console.ReadKey();
        }
    }
}
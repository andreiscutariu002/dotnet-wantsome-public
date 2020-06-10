namespace ConcurrentDemosDay6
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;

    internal class Demo07
    {
        public static void Run()
        {
            var coll = new BlockingCollection<int>();

            var publisher = Task.Factory.StartNew(() =>
            {
                for (var i = 0; i < 100; ++i)
                {
                    Thread.Sleep(1000);
                    coll.Add(i);
                    Console.WriteLine($"TID: {Thread.CurrentThread.ManagedThreadId} - Produced: {i}");
                }
            });

            var consumer = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (coll.TryTake(out var element))
                        Console.WriteLine($"TID: {Thread.CurrentThread.ManagedThreadId} - Element received: {element}");
                    else
                        Console.WriteLine($"TID: {Thread.CurrentThread.ManagedThreadId} - Not received");
                }
            });

            try
            {
                Task.WaitAll(publisher, consumer);
            }
            catch (AggregateException ex) // No exception
            {
                Console.WriteLine(ex.Flatten().Message);
            }
        }
    }
}
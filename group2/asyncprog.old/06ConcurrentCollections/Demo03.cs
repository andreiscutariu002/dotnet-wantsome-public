namespace _06ConcurrentCollections
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;

    internal class Demo03
    {
        public static async Task Run()
        {
            {
                var bCollection = new BlockingCollection<int>(new ConcurrentQueue<int>(), 10);

                var producerThread = Task.Factory.StartNew(() =>
                {
                    for (var i = 0; i < 10; ++i)
                    {
                        bCollection.Add(i);
                    }

                    bCollection.CompleteAdding();

                    Console.WriteLine("Finish to add elements");
                });

                var consumerThread = Task.Factory.StartNew(() =>
                {
                    while (!bCollection.IsCompleted)
                    {
                        var item = bCollection.Take();
                        Console.WriteLine(item);
                        Thread.Sleep(TimeSpan.FromSeconds(2));
                        Console.WriteLine($"Item processed {item}");
                    }
                });

                Task.WaitAll(producerThread, consumerThread);
            }
        }
    }
}
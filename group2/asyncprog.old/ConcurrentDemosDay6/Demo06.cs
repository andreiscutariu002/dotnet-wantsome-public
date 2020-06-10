namespace ConcurrentDemosDay6
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;

    internal class Demo06
    {
        public static void Run()
        {
            BlockingCollection<int> bCollection = new BlockingCollection<int>(boundedCapacity: 10);

            Task producerThread = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(5));

                for (int i = 0; i < 10; ++i)
                {
                    bCollection.Add(i);
                }

                bCollection.CompleteAdding();
            });

            Task consumerThread = Task.Factory.StartNew(() =>
            {
                while (!bCollection.IsCompleted)
                {
                    int item = bCollection.Take();
                    Console.WriteLine(item);
                }
            });

            Task.WaitAll(producerThread, consumerThread);
        }
    }
}
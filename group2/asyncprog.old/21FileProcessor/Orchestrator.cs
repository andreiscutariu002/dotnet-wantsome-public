namespace _21FileProcessor
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;

    public class Orchestrator
    {
        public void Run(string directoryPath)
        {
            var cts = new CancellationTokenSource();
            var queue = new BlockingCollection<string>();
            var aggregator = new ConcurrentDictionary<string, string>();

            var ce = new CountdownEvent(10);

            var publisher = new Publisher(queue, cts.Token);
            publisher.Start(directoryPath);

            var consumer = new Consumer(aggregator, queue, ce, cts.Token);
            Parallel.Invoke(() => consumer.Start(), () => consumer.Start(), () => consumer.Start(), () => consumer.Start());

            ce.Wait(cts.Token);

            foreach (var v in aggregator)
            {
                Console.WriteLine($"{v.Key} - {v.Value}");
            }
        }
    }
}
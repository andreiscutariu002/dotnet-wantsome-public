namespace _21FileProcessor
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Threading;

    public class Consumer
    {
        private readonly ConcurrentDictionary<string, string> aggregator;
        private readonly BlockingCollection<string> queue;
        private readonly CountdownEvent countdownEvent;
        private readonly CancellationToken token;

        public Consumer(ConcurrentDictionary<string, string> aggregator, BlockingCollection<string> queue,
            CountdownEvent countdownEvent,
            CancellationToken token)
        {
            this.aggregator = aggregator;
            this.queue = queue;
            this.countdownEvent = countdownEvent;
            this.token = token;
        }

        public void Start()
        {
            Console.WriteLine($"Starting a consumer on thread {Thread.CurrentThread.ManagedThreadId} ...");

            while (this.queue.TryTake(out var filePath, Timeout.OneMinute, this.token) && this.countdownEvent.CurrentCount > 0)
            {
                this.ProcessFile(filePath);
            }
        }

        private void ProcessFile(string filePath)
        {
            Console.WriteLine($"File {filePath} processed on thread: {Thread.CurrentThread.ManagedThreadId}");

            this.aggregator.TryAdd(filePath, File.ReadAllText(filePath));

            this.countdownEvent.Signal();
        }
    }
}
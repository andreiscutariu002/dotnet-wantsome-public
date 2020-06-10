namespace FileProcessor
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;

    public class Consumer
    {
        private readonly BlockingCollection<string> blockingCollection;
        private readonly CountdownEvent countdownEvent;
        private readonly CancellationToken token;

        private readonly ConcurrentDictionary<string, string> data;

        public Consumer(BlockingCollection<string> blockingCollection, CountdownEvent countdownEvent,
            CancellationToken token)
        {
            this.blockingCollection = blockingCollection;
            this.countdownEvent = countdownEvent;
            this.token = token;
            this.data = new ConcurrentDictionary<string, string>();
        }

        public void Start()
        {
            while (true)
            {
                var result = this.blockingCollection.TryTake(out var path, 1000000, this.token);

                if (result)
                {
                    try
                    {
                        var content = File.ReadAllText(path);
                        this.data.TryAdd(path, content);
                        this.countdownEvent.Signal();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Retry for {path} on TID {Thread.CurrentThread.ManagedThreadId}");
                        this.blockingCollection.Add(path, this.token);
                    }
                }
            }
        }

        public ConcurrentDictionary<string, string> GetData()
        {
            return this.data;
        }
    }
}
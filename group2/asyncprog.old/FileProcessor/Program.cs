namespace FileProcessor
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        static string Location = "c:\\Users\\ascutariu\\OneDrive - ENDAVA\\Desktop\\data\\";

        static void Main(string[] args)
        {
            var collection = new BlockingCollection<string>(4);
            var countdownEvent = new CountdownEvent(10);
            var cts = new CancellationTokenSource();

            var publisher = new Publisher(collection, cts.Token);

            publisher.Start(Location);

            var consumer = new Consumer(collection, countdownEvent, cts.Token);

            Task.Run(() => consumer.Start(), cts.Token);
            Task.Run(() => consumer.Start(), cts.Token);
            Task.Run(() => consumer.Start(), cts.Token);
            Task.Run(() => consumer.Start(), cts.Token);

            // Parallel.Invoke(consumer.Start, consumer.Start, consumer.Start, consumer.Start);

            countdownEvent.Wait(cts.Token);

            cts.Cancel();

            var data = consumer.GetData();

            Parallel.ForEach(data, e =>
            {
                Console.WriteLine($"{e.Key} - {e.Value}");
            });
        }
    }
}

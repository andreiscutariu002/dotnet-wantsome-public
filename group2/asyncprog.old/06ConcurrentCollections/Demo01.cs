namespace _06ConcurrentCollections
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;

    internal class Demo01
    {
        private static readonly ConcurrentStack<int> stack = new ConcurrentStack<int>();
        private static readonly int maxItems = 10;
        private static readonly AutoResetEvent itemAdded = new AutoResetEvent(false);
        private static readonly AutoResetEvent itemConsumed = new AutoResetEvent(false);

        public static async Task Run()
        {
            var producer = Task.Factory.StartNew(() =>
            {
                for (var i = 0; i < maxItems; ++i)
                {
                    stack.Push(i);
                    Console.WriteLine("Added: " + i);
                    itemAdded.Set();
                    itemConsumed.WaitOne();
                }
            });

            var consumer = Task.Factory.StartNew(() =>
            {
                int item;

                for (var i = 0; i < maxItems; ++i)
                {
                    itemAdded.WaitOne();

                    if (stack.TryPop(out item))
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(3));
                        Console.WriteLine("Consumed: " + item);
                    }

                    itemConsumed.Set();
                }
            });

            Task.WaitAll(producer, consumer);
            itemAdded.Close();
            itemConsumed.Close();
        }
    }
}
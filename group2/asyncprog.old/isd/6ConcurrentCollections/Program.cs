namespace _6ConcurrentCollections
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;
    using _20ConcurrentDemos;

    internal class Program
    {
        private static void Main(string[] args)
        {
            SemaphoreSlimDemo.Run();
        }
    }

    public class Demo01
    {
        private static readonly ConcurrentStack<int> sharedData = new ConcurrentStack<int>();

        private static readonly int maxItems = 10;

        private static readonly AutoResetEvent elemPublished = new AutoResetEvent(false);

        private static readonly AutoResetEvent elemConsumed = new AutoResetEvent(false);

        public static void Run()
        {
            var t1 = Task.Factory.StartNew(() =>
            {
                for (var i = 0; i < maxItems; ++i)
                {
                    Thread.Sleep(TimeSpan.FromMinutes(1));

                    sharedData.Push(i);
                    elemPublished.Set();
                    elemConsumed.WaitOne();
                }
            });

            var t2 = Task.Factory.StartNew(() =>
            {
                int item;

                for (var i = 0; i < maxItems; ++i)
                {
                    elemPublished.WaitOne();

                    if (sharedData.TryPop(out item)) Console.WriteLine(item);

                    elemConsumed.Set();
                }
            });

            Task.WaitAll(t1, t2);
            elemPublished.Close();
            elemConsumed.Close();
        }
    }

    public class Demo02
    {
        public static void Run()
        {
            var bCollection = new BlockingCollection<int>(2);

            bCollection.Add(1);
            bCollection.Add(2);

            if (bCollection.TryAdd(3, TimeSpan.FromSeconds(1)))
                Console.WriteLine("Item added");
            else
                Console.WriteLine("Item does not added");
        }
    }

    public class Demo03
    {
        public static void Run()
        {
            var bCollection = new BlockingCollection<int>(2);
            bCollection.Add(1);
            bCollection.Add(2);

            var item = bCollection.Take();
            item = bCollection.Take();

            if (bCollection.TryTake(out item, TimeSpan.FromSeconds(1)))
                Console.WriteLine(item);
            else
                Console.WriteLine("No item removed");
        }
    }

    public class Demo04
    {
        public static void Run()
        {
            var bCollection = new BlockingCollection<int>(10);

            var producerThread = Task.Factory.StartNew(() =>
            {
                for (var i = 0; i < 10; ++i) bCollection.Add(i);

                bCollection.CompleteAdding();

                Console.WriteLine("CompleteAdding");
            });

            var consumerThread = Task.Factory.StartNew(() =>
            {
                while (!bCollection.IsCompleted)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(2));
                    var item = bCollection.Take();
                    Console.WriteLine(item);
                }
            });

            Task.WaitAll(producerThread, consumerThread);
        }
    }

    internal class BarrierDemo
    {
        public static void Run()
        {
            var barrier = new Barrier(3, b =>
            {
                Console.WriteLine("Barrier method: phase={0}", b.CurrentPhaseNumber);

                if (b.CurrentPhaseNumber == 2)
                {
                    Console.WriteLine();
                    Console.WriteLine($"### Finished - Tid: {Thread.CurrentThread.ManagedThreadId}");
                }
            });

            void Action()
            {
                Console.WriteLine($"Before Phase 1 Tid: {Thread.CurrentThread.ManagedThreadId}");
                if (Thread.CurrentThread.ManagedThreadId == 4) Thread.Sleep(TimeSpan.FromSeconds(3));
                barrier.SignalAndWait();
                Console.WriteLine($"After Phase 1 Tid: {Thread.CurrentThread.ManagedThreadId}");

                Console.WriteLine($"Before Phase 2 Tid: {Thread.CurrentThread.ManagedThreadId}");
                if (Thread.CurrentThread.ManagedThreadId == 5) Thread.Sleep(TimeSpan.FromSeconds(3));
                barrier.SignalAndWait();
                Console.WriteLine($"After Phase 2 Tid: {Thread.CurrentThread.ManagedThreadId}");

                Console.WriteLine($"Before Phase 3 Tid: {Thread.CurrentThread.ManagedThreadId}");
                if (Thread.CurrentThread.ManagedThreadId == 1) Thread.Sleep(TimeSpan.FromSeconds(3));
                barrier.SignalAndWait();
                Console.WriteLine($"After Phase 3 Tid: {Thread.CurrentThread.ManagedThreadId}");
            }

            Parallel.Invoke(Action, Action, Action);

            barrier.Dispose();
        }
    }

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

    internal class SemaphoreSlimDemo
    {
        private static readonly SemaphoreSlim Sem = new SemaphoreSlim(3); // Capacity of 3

        public static void Run()
        {
            for (var i = 1; i <= 7; i++)
            {
                new Thread(Enter).Start(i);
            }
        }

        private static void Enter(object id)
        {
            Console.WriteLine(id + " wants to enter");

            Sem.Wait();

            Console.WriteLine(id + " is in!");

            Thread.Sleep(2000); // some operation

            Console.WriteLine(id + " is leaving");

            Sem.Release();
        }
    }
}

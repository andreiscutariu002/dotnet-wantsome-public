namespace ConcurrentDemosDay6
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;

    internal class Demo11
    {
        private static readonly SemaphoreSlim Sem = new SemaphoreSlim(3); // Capacity of 3

        public static void Run()
        {
            for (var i = 1; i <= 5; i++)
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

    internal class Demo01
    {
        public static void Run()
        {
            var coll = new ConcurrentStack<int>();

            AutoResetEvent autoEvent1 = new AutoResetEvent(false);

            var publisher = Task.Factory.StartNew(() =>
            {
                for (var i = 0; i < 100; ++i)
                {
                    Thread.Sleep(1000);
                    coll.Push(i);
                    autoEvent1.Set();
                    Console.WriteLine($"TID: {Thread.CurrentThread.ManagedThreadId} - Produced: {i}");
                }
            });

            var consumer = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    autoEvent1.WaitOne();

                    if (coll.TryPop(out var element))
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
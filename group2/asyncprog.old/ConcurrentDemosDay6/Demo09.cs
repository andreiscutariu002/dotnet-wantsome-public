namespace ConcurrentDemosDay6
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;

    internal class Demo10
    {
        public static void Run()
        {
            ConcurrentBag<int> bag = new ConcurrentBag<int>();
            CountdownEvent cde = new CountdownEvent(20);

            void Producer()
            {
                var rand = new Random();

                while (true)
                {
                    int number = rand.Next();

                    if (number % 2 == 0)
                    {
                        //Console.WriteLine($"Number {number} processed on thread: {Thread.CurrentThread.ManagedThreadId}");
                        bag.Add(number);
                        cde.Signal();
                    }
                }
            };

            var cts = new CancellationTokenSource();

            Task.Factory.StartNew(Producer, cts.Token);
            Task.Factory.StartNew(Producer, cts.Token);
            Task.Factory.StartNew(Producer, cts.Token);
            Task.Factory.StartNew(Producer, cts.Token);

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

    internal class Demo09
    {
        public static void Run()
        {
            Barrier barrier = new Barrier(3,
                b =>
                {
                    Console.WriteLine("Barrier method: phase={0}", b.CurrentPhaseNumber);
                });

            void Action()
            {
                Console.WriteLine($"Before Phase 1 Tid: {Thread.CurrentThread.ManagedThreadId}");
                //Some long running operation is happening
                barrier.SignalAndWait(); // during the post-phase action, count should be 4 and phase should be 0
                Console.WriteLine($"After Phase 1 Tid: {Thread.CurrentThread.ManagedThreadId}");

                Console.WriteLine($"Before Phase 2 Tid: {Thread.CurrentThread.ManagedThreadId}");
                //Some long running operation is happening
                barrier.SignalAndWait(); // during the post-phase action, count should be 8 and phase should be 1
                Console.WriteLine($"After Phase 2 Tid: {Thread.CurrentThread.ManagedThreadId}");

                Console.WriteLine($"Before Phase 3 Tid: {Thread.CurrentThread.ManagedThreadId}");
                if (Thread.CurrentThread.ManagedThreadId == 1)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(10));
                }

                barrier.SignalAndWait(); // during the post-phase action, count should be 8 and phase should be 1

                Console.WriteLine($"After Phase 3 Tid: {Thread.CurrentThread.ManagedThreadId}");

                Console.WriteLine($"Before Phase 4 Tid: {Thread.CurrentThread.ManagedThreadId}");
                //Some long running operation is happening
                barrier.SignalAndWait(); // during the post-phase action, count should be 4 and phase should be 0
                Console.WriteLine($"After Phase 4 Tid: {Thread.CurrentThread.ManagedThreadId}");
            }

            Parallel.Invoke(Action, Action, Action);
        }
    }
}
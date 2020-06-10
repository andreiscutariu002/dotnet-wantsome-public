namespace _06ConcurrentCollections
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;

    internal class Program
    {
        private static async Task Main(string[] args)
        {
            await Demo07.Run();
        }
    }

    internal class Demo06
    {
        public static async Task Run()
        {
            ConcurrentBag<int> concurrentBag = new ConcurrentBag<int>();
            CountdownEvent countdownEvent = new CountdownEvent(1000);

            void ActionMethod()
            {
                var rand = new Random();

                while (true)
                {
                    int number = rand.Next();

                    if (number % 2 == 0)
                    {
                        //Console.WriteLine($"Number {number} processed on thread: {Thread.CurrentThread.ManagedThreadId}");
                        concurrentBag.Add(number);
                        countdownEvent.Signal();
                    }
                }
            };

            var cancellationTokenSource = new CancellationTokenSource();

            Task.Factory.StartNew(ActionMethod, cancellationTokenSource.Token);
            Task.Factory.StartNew(ActionMethod, cancellationTokenSource.Token);
            Task.Factory.StartNew(ActionMethod, cancellationTokenSource.Token);
            Task.Factory.StartNew(ActionMethod, cancellationTokenSource.Token);
            Task.Factory.StartNew(ActionMethod, cancellationTokenSource.Token);
            Task.Factory.StartNew(ActionMethod, cancellationTokenSource.Token);
            Task.Factory.StartNew(ActionMethod, cancellationTokenSource.Token);
            Task.Factory.StartNew(ActionMethod, cancellationTokenSource.Token);

            countdownEvent.Wait(cancellationTokenSource.Token);

            cancellationTokenSource.Cancel();

            foreach (var v in concurrentBag)
            {
                Console.WriteLine(v);
            }

            Console.WriteLine("Press any key to exit!");
            Console.ReadKey();
        }
    }

    internal class Demo07
    {
        public static async Task Run()
        {
            Barrier barrier = new Barrier(3, b =>
            {
                Console.WriteLine("Barrier method: phase={0}", b.CurrentPhaseNumber);

                if (b.CurrentPhaseNumber == 2)
                {
                    Console.WriteLine();
                    Console.WriteLine($"### Finished.");
                    Console.WriteLine();
                }

                Console.WriteLine();
            });

            void Action()
            {
                Console.WriteLine($"Before Phase 1 Tid: {Thread.CurrentThread.ManagedThreadId}");
                if (Thread.CurrentThread.ManagedThreadId == 1)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                }

                barrier.SignalAndWait(); // during the post-phase action, count should be 4 and phase should be 0
                Console.WriteLine($"After Phase 1 Tid: {Thread.CurrentThread.ManagedThreadId}");

                Console.WriteLine($"Before Phase 2 Tid: {Thread.CurrentThread.ManagedThreadId}");
                if (Thread.CurrentThread.ManagedThreadId == 1)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                }

                barrier.SignalAndWait(); // during the post-phase action, count should be 8 and phase should be 1
                Console.WriteLine($"After Phase 2 Tid: {Thread.CurrentThread.ManagedThreadId}");

                Console.WriteLine($"Before Phase 3 Tid: {Thread.CurrentThread.ManagedThreadId}");
                if (Thread.CurrentThread.ManagedThreadId == 1)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                }

                barrier.SignalAndWait(); // during the post-phase action, count should be 8 and phase should be 1

                Console.WriteLine($"After Phase 3 Tid: {Thread.CurrentThread.ManagedThreadId}");
            }

            Parallel.Invoke(Action, Action, Action);

            // It's good form to Dispose() a barrier when you're done with it.
            barrier.Dispose();
        }
    }

    internal class Demo08
    {
        public static async Task Run()
        {
        }
    }
}
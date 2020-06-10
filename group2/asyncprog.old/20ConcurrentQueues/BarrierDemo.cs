namespace _20ConcurrentDemos
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class BarrierDemo
    {
        public static void Run()
        {
            Barrier barrier = new Barrier(3, b =>
            {
                Console.WriteLine("Barrier method: phase={0}", b.CurrentPhaseNumber);

                if (b.CurrentPhaseNumber == 2)
                {
                    Console.WriteLine();
                    Console.WriteLine($"### Finished - Tid: {Thread.CurrentThread.ManagedThreadId}");
                    Console.WriteLine();
                }
            });

            // barrier.AddParticipants(2);

            // barrier.RemoveParticipant();

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
}
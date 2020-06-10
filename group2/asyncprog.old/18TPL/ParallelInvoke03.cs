namespace _18TPL
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ParallelInvoke03
    {
        internal static void Run()
        {
            ParallelOptions parallelOptions = new ParallelOptions
            {
                MaxDegreeOfParallelism = 3
            };

            Parallel.Invoke(parallelOptions,
                () => DoSomeTask(1),
                () => DoSomeTask(2),
                () => DoSomeTask(3),
                () => DoSomeTask(4),
                () => DoSomeTask(5),
                () => DoSomeTask(6),
                () => DoSomeTask(7)
            );

            Console.WriteLine("Press any key to exist.");
            Console.ReadKey();
        }

        private static void DoSomeTask(int number)
        {
            Console.WriteLine($"DoSomeTask {number} started by Thread {Thread.CurrentThread.ManagedThreadId}");
            //Sleep for 5000 milliseconds
            Thread.Sleep(5000);
            Console.WriteLine($"DoSomeTask {number} completed by Thread {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
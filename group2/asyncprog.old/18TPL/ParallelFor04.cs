namespace _18TPL
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ParallelFor04
    {
        internal static void Run()
        {
            var number = 10000000;
            var options = new ParallelOptions
            {
                MaxDegreeOfParallelism = 4
            };

            var a = new int[20];

            Parallel.For(0, number, options, (i, breakLoopState) =>
            {
                a[Thread.CurrentThread.ManagedThreadId]++;

                if (a[Thread.CurrentThread.ManagedThreadId] == 10000)
                {
                    //Console.WriteLine("Stop - thread_id: {0}", Thread.CurrentThread.ManagedThreadId);
                    breakLoopState.Break();
                }

                //Console.WriteLine("{0} - {1} - thread_id: {2}", i, total, Thread.CurrentThread.ManagedThreadId);
            });

            for (int i = 0; i < a.Length; i++)
            {
                Console.WriteLine($"a[{i}]={a[i]}");
            }
        }
    }
}
namespace _20ConcurrentDemos
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ManualResetEventSlimDemo
    {
        public static void Run()
        {
            ManualResetEventSlim manualResetEventSlim1 = new ManualResetEventSlim(false);
            ManualResetEventSlim manualResetEventSlim2 = new ManualResetEventSlim(false);

            var task = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Before manualResetEventSlim_1!");
                manualResetEventSlim1.Wait();
                Console.WriteLine("After manualResetEventSlim_1!");

                Thread.Sleep(TimeSpan.FromSeconds(5));

                manualResetEventSlim2.Set();
            });

            Thread.Sleep(TimeSpan.FromSeconds(1));

            manualResetEventSlim1.Set();

            Console.WriteLine("Before manualResetEventSlim_2!");
            manualResetEventSlim2.Wait();
            Console.WriteLine("After manualResetEventSlim_2!");

            task.Wait();

            //manualResetEventSlim.Dispose();
        }
    }
}
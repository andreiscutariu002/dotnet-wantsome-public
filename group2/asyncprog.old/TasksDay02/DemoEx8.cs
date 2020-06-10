namespace TasksDay02
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class DemoEx8
    {
        internal static void Run()
        {
            Task<int[]> parent = new Task<int[]>(() =>
            {
                var results = new int[3];

                new Task(() => {
                        Thread.Sleep(TimeSpan.FromSeconds(4));
                        results[0] = 0;
                    },
                    TaskCreationOptions.AttachedToParent).Start();

                new Task(() => results[1] = 1,
                    TaskCreationOptions.AttachedToParent).Start();

                new Task(() => results[2] = 2,
                    TaskCreationOptions.AttachedToParent).Start();

                return results;
            });

            parent.Start();

            var finalTask = parent.ContinueWith(
                t =>
                {
                    foreach (int i in t.Result)
                        Console.WriteLine(i);
                });

            finalTask.Wait();
            Console.ReadLine();
        }
    }
}
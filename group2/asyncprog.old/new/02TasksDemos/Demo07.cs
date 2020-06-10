namespace _02TasksDemos
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class Demo07
    {
        public static void Run()
        {
            Task<int[]> parent = new Task<int[]>(() =>
            {
                var results = new int[3];

                new Task(() => {
                        Thread.Sleep(TimeSpan.FromSeconds(5));
                        results[0] = -1;
                    },
                    TaskCreationOptions.AttachedToParent).Start();

                new Task(() => results[1] = 1,
                    TaskCreationOptions.AttachedToParent).Start();

                new Task(() => results[2] = 2,
                    TaskCreationOptions.AttachedToParent).Start();

                return results;
            });

            parent.Start();
            parent.Wait();

            var finalTask = parent.ContinueWith(
                parentTask => {
                    foreach (int i in parentTask.Result)
                    {
                        Console.WriteLine(i);
                    }
                });

            finalTask.Wait();
            Console.ReadLine();
        }
    }
}
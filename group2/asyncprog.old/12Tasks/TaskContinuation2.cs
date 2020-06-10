namespace _12Tasks
{
    using System;
    using System.Threading.Tasks;

    internal class TaskContinuation2
    {
        internal static void Run()
        {
            Task task1 = Task.Factory.StartNew(() =>
            {
                return 20;
            }).ContinueWith(ant =>
            {
                Console.WriteLine(ant.Result);
            });

            task1.Wait();
        }
    }
}
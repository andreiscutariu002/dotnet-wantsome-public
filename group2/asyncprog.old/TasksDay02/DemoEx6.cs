namespace TasksDay02
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class DemoEx6
    {
        internal static void Run()
        {
            Task task1 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("task 1");
                Thread.Sleep(TimeSpan.FromSeconds(1));
            });

            Task task2 = task1.ContinueWith(ant => Console.WriteLine("task 2"));
            Task task3 = task2.ContinueWith(ant => Console.WriteLine("task 3"));

            Task task4 = Task.Factory.StartNew(() =>
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
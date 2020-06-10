using System;
using System.Threading.Tasks;

namespace _12Tasks
{
    internal class TaskContinuation
    {
        internal static void Run()
        {
            Task t = new Task(()=> {
            });

            t.Start();

            t.Wait();

            Task task1 = Task.Factory.StartNew(() => Console.WriteLine("task 1"));

            Task task2 = task1.ContinueWith(ant => Console.WriteLine("task 2"));

            task1.Wait();
        }
    }
}

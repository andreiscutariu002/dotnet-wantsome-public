namespace TasksDay02
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class DemoEx
    {
        internal static void Run()
        {
            Action someAction = () =>
            {
                //print task t thread id
                var threadId = Thread.CurrentThread.ManagedThreadId;
                Console.WriteLine("Task Current Thread Id:" + threadId);
            };

            var task = new Task(someAction);
            task.Start();

            // not to recommended to be used 
            task.Wait();

            Console.WriteLine("Task Main Thread Id:" + Thread.CurrentThread.ManagedThreadId);
        }
    }
}
namespace _12Tasks
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class TaskCreation
    {
        public static void Run()
        {
            Action someAction = () =>
            {
                //print task t thread id
                var threadId = Thread.CurrentThread.ManagedThreadId;
                Console.WriteLine("Task Current Thread Id:" + threadId);
            };

            var task = new Task(someAction);

            //task.Start();

            task.Wait();

            Console.WriteLine("Task Main Thread Id:" + Thread.CurrentThread.ManagedThreadId);
        }
    }
}

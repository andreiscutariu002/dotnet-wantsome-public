namespace TasksDay02
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class DemoEx2
    {
        internal static void Run()
        {
            var task = new Task<string>(() =>
            {
                //print task t thread id
                var threadId1 = Thread.CurrentThread.ManagedThreadId;
                Console.WriteLine("Task Current Thread Id:" + threadId1);
                Thread.Sleep(TimeSpan.FromSeconds(1));
                return "info from task";
            });

            task.Start();
            // not to recommended to be used 
            //task.Wait();

            Console.WriteLine(task.Result);

            Console.WriteLine("Task Main Thread Id:" + Thread.CurrentThread.ManagedThreadId);
        }
    }
}
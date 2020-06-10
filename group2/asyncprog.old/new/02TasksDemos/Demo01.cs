namespace _02TasksDemos
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class Demo01
    {
        public static void Run()
        {
            Action someAction = () =>
            {
                //print task t thread id
                var threadId = Thread.CurrentThread.ManagedThreadId;
                Thread.Sleep(TimeSpan.FromSeconds(1));
                Console.WriteLine("Task Current Thread Id:" + threadId);
            };

            var task = new Task(someAction);
            //task.Start();

            task.Wait();

            //var t = Task.Run(someAction);

            //t.Wait();

            Console.WriteLine("Task Main Thread Id:" + Thread.CurrentThread.ManagedThreadId);
        }
    }
}
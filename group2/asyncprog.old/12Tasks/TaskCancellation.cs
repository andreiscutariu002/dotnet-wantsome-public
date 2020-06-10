namespace _12Tasks
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class TaskCancellation
    {
        public static void Run()
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            var state = "";

            Action<object> someAction = (value) =>
            {
                while (true)
                {
                    //print task t thread id
                    var threadId = Thread.CurrentThread.ManagedThreadId;
                    Console.WriteLine("Task Loop Current Thread Id:" + threadId);

                    Task.Delay(TimeSpan.FromSeconds(1)).Wait();

                    token.ThrowIfCancellationRequested();
                    //if (token.IsCancellationRequested)
                    //{
                    //    break;
                    //}
                }
            };

            var t = new Task(someAction, state);

            //start task t execution
            t.Start();

            Console.WriteLine("Press enter terminate the task!");
            Console.ReadLine();

            tokenSource.Cancel();

            //wait for task t to complete its execution
            t.Wait();

            Console.WriteLine("Press enter terminate the process!");
            Console.ReadLine();
        }
    }
}

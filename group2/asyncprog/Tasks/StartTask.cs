namespace Tasks
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public static class StartTask
    {
        public static void Run()
        {
            Task t = new Task(SomeMethod);
            t.Start();
            t.Wait();

            Task t2 = Task.Run(SomeMethod);
            t2.Wait();

            Task t3 = Task.Factory.StartNew(SomeMethod);
            t3.Wait();

            TaskFactory tf = new TaskFactory(TaskCreationOptions.AttachedToParent,
                TaskContinuationOptions.ExecuteSynchronously);
            Task t4 = tf.StartNew(SomeMethod);
            t4.Wait();

            Console.WriteLine($"[tid: {Thread.CurrentThread.ManagedThreadId}] Hello world from main thread!");
            Console.ReadLine();
        }

        private static void SomeMethod()
        {
            Console.WriteLine($"[tid: {Thread.CurrentThread.ManagedThreadId}] Hello world from thread!");
        }
    }
    }

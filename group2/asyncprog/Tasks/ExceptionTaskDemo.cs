namespace Tasks
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public static class ExceptionTaskDemo
    {
        public static void Run()
        {
            Task<string> t = new Task<string>(SomeMethod);

            t.Start();

            try
            {
                t.Wait();
            }
            catch (AggregateException e)
            {
                foreach (var ex in e.Flatten().InnerExceptions)
                {
                    Console.WriteLine("Exception: " + ex.GetType().Name);
                }
            }

            var result = t.Result;
        }

        private static string SomeMethod()
        {
            Console.WriteLine($"[tid: {Thread.CurrentThread.ManagedThreadId}] Hello world from thread!");

            throw new InvalidOperationException("some exception");

            return "ceva";
        }
    }
}

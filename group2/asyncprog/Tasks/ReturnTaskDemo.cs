namespace Tasks
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public static class ReturnTaskDemo
    {
        public static void Run()
        {
            Task<string> t = new Task<string>(SomeMethod);

            t.Start();
            t.Wait();

            var result = t.Result;
        }

        private static string SomeMethod()
        {
            Console.WriteLine($"[tid: {Thread.CurrentThread.ManagedThreadId}] Hello world from thread!");

            return "ceva";
        }
    }
}

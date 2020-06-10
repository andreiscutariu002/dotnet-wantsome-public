namespace _03AsyncAwait
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public class Demo02
    {
        public static void Run()
        {
            RunAsync().Wait();
        }

        public static async Task RunAsync()
        {
            SomeAwaitableClass sa = new SomeAwaitableClass();

            int result = await sa;
        }
    }

    public class SomeAwaitableClass
    {
        public SomeAwaiter GetAwaiter()
        {
            Console.WriteLine("SomeAwaitableClass.GetAwaiter");
            return new SomeAwaiter();
        }
    }

    public class SomeAwaiter : INotifyCompletion
    {
        public bool IsCompleted
        {
            get
            {
                Console.WriteLine("SomeAwaiter.IsCompleted");
                return true;
            }
        }

        public void OnCompleted(Action continuation)
        {
            Console.WriteLine("SomeAwaiter.OnCompleted");
        }

        public int GetResult()
        {
            Console.WriteLine("SomeAwaiter.GetResult");
            return 0;
        }
    }
}
namespace _03AsyncAwaitStateMachine
{
    using System;
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
            int result = 2;

            SomeAwaitableClass operationAsync1 = new SomeAwaitableClass();

            result = await operationAsync1; //not blocking

            result = 10;

            SomeAwaitableClass operationAsync2 = new SomeAwaitableClass();

            result = await operationAsync2; //not blocking

            result = 20;

            SomeAwaitableClass operationAsync3 = new SomeAwaitableClass();

            result = await operationAsync3; //not blocking

            result = 30;

            Console.WriteLine(result);
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
            return 4;
        }
    }
}
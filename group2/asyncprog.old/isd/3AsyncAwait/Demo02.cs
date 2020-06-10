namespace _3AsyncAwait
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public class Demo02
    {
        public static async Task Run()
        {
            int number1 = 10;
            int number2 = 2;

            var sa = new SomeAwaitableClass();
            var sa2 = new SomeAwaitableClass();
            var sa3 = new SomeAwaitableClass();

            int res = await sa;

            number1 = 100;
            number2 = 20;

            int res2 = await sa2;

            int res3 = await sa3;

            number1 = 1000;
            number2 = 200;

            //
        }
    }

    public class SomeAwaitableClass
    {
        public SomeAwaiter GetAwaiter()
        {
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

        public int GetResult()
        {
            Console.WriteLine("SomeAwaiter.GetResult");
            return -1;
        }

        public void OnCompleted(Action continuation)
        {
            Console.WriteLine("SomeAwaiter.OnCompleted");
        }
    }
}
namespace _14Awaitables
{
    using System;
    using System.Runtime.CompilerServices;

    public class SomeAwaiter : INotifyCompletion
    {
        public bool IsCompleted
        {
            get
            {
                Console.WriteLine("SomeAwaiter.IsCompleted");
                return false;
            }
        }

        public void OnCompleted(Action continuation)
        {
            Console.WriteLine("SomeAwaiter.OnCompleted");
        }

        public int GetResult()
        {
            Console.WriteLine("SomeAwaiter.GetResult");

            return 10;
        }
    }
}
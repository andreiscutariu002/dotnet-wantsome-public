namespace AsyncAwaitDay03
{
    using System;
    using System.Runtime.CompilerServices;

    public class SomeAwaiter : INotifyCompletion
    {
        public bool IsCompleted => true;

        public void OnCompleted(Action continuation)
        {
            Console.WriteLine("SomeAwaiter.OnCompleted");
        }

        public int GetResult()
        {
            Console.WriteLine("SomeAwaiter.GetResult");

            return 2;
        }
    }
}
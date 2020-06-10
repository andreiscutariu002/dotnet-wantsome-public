namespace _14Awaitables
{
    using System;

    public class SomeAwaitableClass
    {
        public SomeAwaiter GetAwaiter()
        {
            Console.WriteLine("SomeAwaitableClass.GetAwaiter");
            return new SomeAwaiter();
        }
    }
}
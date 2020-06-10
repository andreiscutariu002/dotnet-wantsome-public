namespace AsyncAwaitDay03
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
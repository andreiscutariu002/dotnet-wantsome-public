namespace AsyncAwaitDay03
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    internal class DemoEx2
    {
        internal static void Run()
        {
            AwaitSomeAwaitableClass().Wait();
        }

        private static async Task AwaitSomeAwaitableClass()
        {
            var sa = new SomeAwaitableClass();

            var r = await sa;

            Console.WriteLine(r);
        }
    }
}
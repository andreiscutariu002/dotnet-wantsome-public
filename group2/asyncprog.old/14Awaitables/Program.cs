namespace _14Awaitables
{
    using System;
    using System.Threading.Tasks;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var a = AwaitSomeAwaitableClass();

            a.GetAwaiter().GetResult();
        }

        private static async Task AwaitSomeAwaitableClass()
        {
            var sac = new SomeAwaitableClass();

            var res = await sac;

            Console.WriteLine(res);
        }
    }
}

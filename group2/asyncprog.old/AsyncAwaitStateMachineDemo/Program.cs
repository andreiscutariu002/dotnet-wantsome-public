namespace AsyncAwaitStateMachineDemo
{
    using System;
    using System.Threading.Tasks;
    using AsyncAwaitDay03;

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
            await sac;
        }
    }
}

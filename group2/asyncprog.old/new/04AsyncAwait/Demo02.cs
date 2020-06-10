namespace _04AsyncAwait
{
    using System;
    using System.Threading.Tasks;

    public class Demo02
    {
        public static async Task Run()
        {
            var t = OperationAsync();
            try
            {
                Operation2Async();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.GetType().FullName);
                Console.WriteLine();
                Console.WriteLine(t.Exception.GetType().FullName);
            }

            await Task.Delay(TimeSpan.FromSeconds(3));
        }

        // Use this when void return 
        public static async Task OperationAsync()
        {
            throw new InvalidOperationException("OperationAsync");
        }

        // Not used
        public static async void Operation2Async()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));

            throw new InvalidOperationException("Operation2Async");
        }
    }
}
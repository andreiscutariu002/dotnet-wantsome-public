namespace _13AsyncAwait
{
    using System;
    using System.Threading.Tasks;

    public class AsyncAwaitException
    {
        public static async Task Run()
        {
            try
            {
                //OperationAsync().Wait();

                await OperationAsync();

                //await Operation2Async();

                Operation2Async();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().FullName);
            }
        }

        // Use this when void return 
        public static async Task OperationAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(0));

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
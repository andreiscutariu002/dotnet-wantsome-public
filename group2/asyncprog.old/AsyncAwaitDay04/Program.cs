using System;

namespace AsyncAwaitDay04
{
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            AsyncAwaitException.Run().Wait();
        }
    }

    public class AsyncAwaitException
    {
        public static async Task Run()
        {
            try
            {
                var task = OperationAsync();

                Operation2Async();

                Task.Delay(TimeSpan.FromSeconds(5)).Wait();
                Console.WriteLine("Press any key");
                Console.ReadKey();
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("InvalidOperationException catch");
                Console.WriteLine(e.GetType().FullName);
            }
            catch (AggregateException e)
            {
                Console.WriteLine("AggregateException catch");
                Console.WriteLine(e.GetType().FullName);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception catch");
                Console.WriteLine(e.GetType().FullName);
            }
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

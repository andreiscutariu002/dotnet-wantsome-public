namespace _03AsyncAwait
{
    using System;
    using System.Threading.Tasks;

    public class Demo04
    {
        public static void Run()
        {
            RunAsync().Wait();
        }

        public static async Task RunAsync()
        {
            //var result = await CreateStringAsync();

            //Console.WriteLine(result);

            var task = DisplayStringAsync();
            try
            {
                //task.Wait();
                await task;
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine(e);
                throw;
            }
            catch (AggregateException e)
            {
                Console.WriteLine(e);
                throw;
            }

            Console.ReadKey();
        }

        public static Task<string> CreateStringAsync(ref string a)
        {
            return Task.Run(() => { return "Some string"; });
        }

        public static Task DisplayStringAsync()
        {
            return Task.Run(() =>
            {
                throw new DivideByZeroException();

                Console.WriteLine("Some string 2");
            });
        }
    }
}
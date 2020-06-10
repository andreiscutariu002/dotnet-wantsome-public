using System;

namespace _3AsyncAwait
{
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        [ThreadStatic] static private int Value = 100;

        static async Task Main(string[] args)
        {
            int a = 20;
    
            Console.WriteLine($"Main TID: {Thread.CurrentThread.ManagedThreadId}: {Value}, a={a}");

            await RunAsync();

            Console.WriteLine($"Main TID: {Thread.CurrentThread.ManagedThreadId}: {Value}, a={a}");

            //await Demo03.Run();
        }

        private static Task RunAsync()
        {
            return Task.Run(() => { Console.WriteLine($"Task TID: {Thread.CurrentThread.ManagedThreadId}: {Value}"); });
        }
    }

    public class Demo01
    {
        public static void Run()
        {
        }

        public async Task<int> AdditionV1()
        {
            var a = this.SlowMethodOneAsync();
            var b = this.SlowMethodTwoAsync();

            //a.Wait(); // blocks main thread
            //b.Wait();

            var aResult = await a; // is not blocking main thread
            var bResult = await b;

            return aResult + bResult; 
            //return a.Result + b.Result; 
        }

        public int AdditionV2()
        {
            var a = this.SlowMethodOneAsync();
            var b = this.SlowMethodTwoAsync();

            a.Wait(); // blocks main thread
            b.Wait();

            return a.Result + b.Result; // blocks main thread
        }

        private Task<int> SlowMethodOneAsync()
        {
            return Task.Run(() => { return this.SlowMethodOne(); });
        }

        private Task<int> SlowMethodTwoAsync()
        {
            return Task.Run(() => { return this.SlowMethodTwo(); });
        }

        private int SlowMethodTwo()
        {
            throw new NotImplementedException();
        }

        private int SlowMethodOne()
        {
            throw new NotImplementedException();
        }
    }
}

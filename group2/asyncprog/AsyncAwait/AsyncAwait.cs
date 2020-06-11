namespace AsyncAwait
{
    using System;
    using System.Threading.Tasks;

    public class AsyncAwait
    {
        public async Task<int> Run()
        {
            var x = 200;

            var a = await this.SlowMethodOneTask();

            x = 250;

            var b = await this.SlowMethodTwoTask();

            x = 300;

            var c = await this.SlowMethodTwoTask();

            Console.WriteLine(x);

            return a + b;
        }

        private Task<int> SlowMethodOneTask()
        {
            return Task.Factory.StartNew(() => 1);
        }

        private Task<int> SlowMethodTwoTask()
        {
            return Task.Factory.StartNew(() => 2);
        }
    }
}

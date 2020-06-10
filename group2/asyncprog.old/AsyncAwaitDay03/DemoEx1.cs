namespace AsyncAwaitDay03
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class DemoEx1
    {
        public int Addition()
        {
            var a = this.SlowMethodOne();
            var b = this.SlowMethodTwo();

            return a + b;
        }

        public async Task<int> AdditionAsync()
        {
            int resAv2 = await this.SlowMethodOneAsync();
            int resBv2 = await this.SlowMethodOneAsync();

            return resAv2 + resBv2;
        }

        private Task<int> SlowMethodTwoAsync()
        {
            return Task.Run(this.SlowMethodTwo);
        }

        private Task<int> SlowMethodOneAsync()
        {
            return Task.Run(this.SlowMethodOne);
        }

        private int SlowMethodTwo()
        {
            Thread.Sleep(TimeSpan.FromSeconds(1));
            return 2;
        }

        private int SlowMethodOne()
        {
            Thread.Sleep(TimeSpan.FromSeconds(1));
            return 1;
        }

        internal static void Run()
        {
        }
    }
}
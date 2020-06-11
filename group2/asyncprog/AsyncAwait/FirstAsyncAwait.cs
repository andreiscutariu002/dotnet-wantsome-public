namespace AsyncAwait
{
    using System.Threading.Tasks;

    public class FirstAsyncAwait
    {
        public int Addition3()
        {
            var a = this.SlowMethodOneAsync();
            var b = this.SlowMethodTwoAsync();

            a.Wait();
            b.Wait();

            int ar = a.Result;
            int br = b.Result;

            return ar + br;
        }

        private Task<int> SlowMethodTwoAsync()
        {
            return Task.Run(() => this.SlowMethodTwo());
        }

        private Task<int> SlowMethodOneAsync()
        {
            return Task.Run(() => this.SlowMethodOne());
        }
        
        public async Task<int> Addition2()
        {
            var a = this.SlowMethodOneAsync();
            var b = this.SlowMethodTwoAsync();

            return await a + await b;
        }

        public int Addition()
        {
            var a = this.SlowMethodOne();
            var b = this.SlowMethodTwo();

            return a + b;
        }

        public int SlowMethodOne()
        {
            return 1;
        }

        public int SlowMethodTwo()
        {
            return 2;
        }
    }
}

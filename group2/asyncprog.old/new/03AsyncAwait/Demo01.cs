namespace _03AsyncAwait
{
    using System.Threading.Tasks;

    public class Demo01
    {
        public static void Run()
        {
        }
    }

    class MyClass01
    {
        public int Addition()
        {
            var a = this.SlowMethodOne();
            var b = this.SlowMethodTwo();

            return a + b;
        }

        private int SlowMethodTwo()
        {
            return 2;
        }

        private int SlowMethodOne()
        {
            return 3;
        }
    }

    class MyClass02
    {
        public int Addition()
        {
            Task<int> a = this.SlowMethodTwoAsync();
            Task<int> b = this.SlowMethodOneAsync();

            Task.WaitAll(a, b);

            return a.Result + b.Result; // blocking 
        }

        private Task<int> SlowMethodTwoAsync()
        {
            return Task.Run(() => SlowMethodTwo());
        }

        private Task<int> SlowMethodOneAsync()
        {
            return Task.Run(() => SlowMethodOne());
        }

        private int SlowMethodTwo()
        {
            return 2;
        }

        private int SlowMethodOne()
        {
            return 3;
        }
    }

    class MyClass03
    {
        public async Task<int> Addition()
        {
            Task<int> a = this.SlowMethodTwoAsync();
            Task<int> b = this.SlowMethodOneAsync();

            var resA = await a;
            var resB = await b;

            return resA + resB; // not blocking 
        }

        private Task<int> SlowMethodTwoAsync()
        {
            return Task.Run(() => SlowMethodTwo());
        }

        private Task<int> SlowMethodOneAsync()
        {
            return Task.Run(() => SlowMethodOne());
        }

        private int SlowMethodTwo()
        {
            return 2;
        }

        private int SlowMethodOne()
        {
            return 3;
        }
    }

    class MyClass04
    {
        public int AdditionOld()
        {
            var a = this.SlowMethodOne();
            var b = this.SlowMethodTwo();

            return a + b;
        }

        public async Task<int> Addition()
        {
            var a = await this.SlowMethodTwoAsync();
            var b = await this.SlowMethodOneAsync();

            return a + b; // not blocking 
        }

        private Task<int> SlowMethodTwoAsync()
        {
            return Task.Run(() => SlowMethodTwo());
        }

        private Task<int> SlowMethodOneAsync()
        {
            return Task.Run(() => SlowMethodOne());
        }

        private int SlowMethodTwo()
        {
            return 2;
        }

        private int SlowMethodOne()
        {
            return 3;
        }
    }
}
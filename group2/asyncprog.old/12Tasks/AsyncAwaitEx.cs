namespace _12Tasks
{
    using System;
    using System.Threading.Tasks;

    public class AsyncAwaitEx
    {
        public static void Run()
        {
        }

        public int Addition()
        {
            var a = this.SlowMethodOneAsync().Result;
            var b = this.SlowMethodTwoAsync().Result;

            return a + b;
        }

        public async Task<int> AdditionAsync()
        {
            var a = this.SlowMethodOneAsync();
            var b = this.SlowMethodTwoAsync();

            return await a + await b;
        }

        public int AdditionWithTasks()
        {
            var a = this.SlowMethodOneTask();
            var b = this.SlowMethodTwoTask();

            Task.WaitAll(a, b);

            return a.Result + b.Result;
        }


        private Task<int> SlowMethodTwoTask()
        {
            return Task.Factory.StartNew(() => { return this.SlowMethodTwo(); });
        }

        private Task<int> SlowMethodOneTask()
        {
            return Task.Factory.StartNew(() => { return this.SlowMethodOne(); });
        }

        //public async Task<int> AdditionAsync()
        //{
        //    var a = this.SlowMethodOneAsync();
        //    var b = this.SlowMethodTwoAsync();

        //    return await a + await b;
        //}

        private Task<int> SlowMethodTwoAsync()
        {
            throw new NotImplementedException();
        }

        private Task<int> SlowMethodOneAsync()
        {
            throw new NotImplementedException();
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

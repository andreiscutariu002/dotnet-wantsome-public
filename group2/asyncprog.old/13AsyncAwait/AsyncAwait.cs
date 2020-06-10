namespace _13AsyncAwait
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class AsyncAwait
    {
        public async Task<int> Run()
        {
            var a = await this.SlowMethodOneTask();
            Debug.WriteLine("After First Await");

            var b = await this.SlowMethodTwoTask();
            Debug.WriteLine("After Second Await");

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
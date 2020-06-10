namespace _13AsyncAwait
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class ParallelHttpCalls
    {
        public static async Task Run1()
        {
            var task1 = CreateTask(1);
            var task2 = CreateTask(2);
            var task3 = CreateTask(3);
            var task4 = CreateTask(4);

            var results = await Task.WhenAll(task1, task2, task3, task4);
            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }

        public static async Task Run2()
        {
            var task1 = CreateTask(1);
            var task2 = CreateTask(2);
            var task3 = CreateTask(3);
            var task4 = CreateTask(4);

            var result = await Task.WhenAny(task1, task2, task3, task4);
            Console.WriteLine(result);
        }

        public static async Task Run3()
        {
            var task1 = CreateTask(1);
            var task2 = CreateTask(2);
            var task3 = CreateTask(3);
            var task4 = CreateTask(4);

            var result = await Task.WhenAny(task1, task2, task3, task4);
            Console.WriteLine(result);
        }

        private static async Task<string> CreateTask(int id)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"https://jsonplaceholder.typicode.com/posts/{id}");
            var value = await response.Content.ReadAsStringAsync();

            await Task.Delay(TimeSpan.FromSeconds(id));

            return value;
        }
    }
}
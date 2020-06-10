namespace _05TplDemos
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    internal class Demo10
    {
        public static async Task Run()
        {
            Task<string> task1 = CreateTask(1); //1s
            Task<string> task2 = CreateTask(2); //2s
            Task<string> task3 = CreateTask(3); //3s
            Task<string> task4 = CreateTask(4); //4s
            Task<string> task5 = CreateTask(4); //4s
            Task<string> task6 = CreateTask(4); //4s
            Task<string> task7 = CreateTask(4); //4s
            Task<string> task8 = CreateTask(4); //4s

            string[] results = await Task.WhenAll(task1, task2, task3, task4, task5, task6, task7, task8); //4s
            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
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
namespace _14ReturnTypes
{
    using System.Threading.Tasks;

    class Program
    {
        static async Task Main(string[] args)
        {
            var app = new EmployeeApplication();
            await app.Run();
        }
    }

    // todo connect to sql server, user ADO net async methods
}

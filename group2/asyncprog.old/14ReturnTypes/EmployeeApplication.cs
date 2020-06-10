namespace _14ReturnTypes
{
    using System;
    using System.Threading.Tasks;

    public class EmployeeApplication
    {
        public async Task Run()
        {
            var service = new EmployeeService(new EmployeeRepository());

            var result = await service.GetCountPerDepartment(1);

            Console.WriteLine(result);
        }
    }
}
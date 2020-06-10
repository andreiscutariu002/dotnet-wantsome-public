namespace _14ReturnTypes
{
    using System.Threading.Tasks;

    public class EmployeeRepository
    {
        public Task<int> GetCountPerDepartment(int depId)
        {
            return Task.Factory.StartNew(() =>
            {
                return 10;
            });
        }

        public Task UpdateEmployeeDepartment(int id, int depId)
        {
            return Task.Factory.StartNew(() =>
            {
            });
        }
    }
}
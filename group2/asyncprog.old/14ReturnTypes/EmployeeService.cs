namespace _14ReturnTypes
{
    using System.Threading.Tasks;

    public class EmployeeService
    {
        private readonly EmployeeRepository repo;

        public EmployeeService(EmployeeRepository repo)
        {
            this.repo = repo;
        }

        public async Task<int> GetCountPerDepartment(int depId)
        {
            var result = await this.repo.GetCountPerDepartment(depId);
            return result;
        }

        public async Task<int> GetCountPerDepartment2(int depId)
        {
            var countPerDepartment = this.repo.GetCountPerDepartment(depId);
            await countPerDepartment;
            return countPerDepartment.Result;
        }
    }
}
namespace ConferencePlanner.Services
{
    using System.Threading.Tasks;

    public class SessionService
    {
        private readonly ISessionRepository repository;

        public SessionService(ISessionRepository repository)
        {
            this.repository = repository;
        }

        public async Task SomeMethodWithLogic(int id)
        {
            var session = await this.repository.Get(id);

            // dummy code, do something with session
        }
    }
}

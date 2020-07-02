namespace ConferencePlanner.Services
{
    using System.Threading.Tasks;
    using Entities;

    public interface ISessionRepository
    {
        Task<Session> Get(int id);

        Task<Session> Save(Session session);

        Task<Session> Remove(int id);
    }
}

namespace ConferencePlanner.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;

    public interface ISpeakerRepository
    {
        Task<IEnumerable<SpeakerResponseModel>> CountSessionPerSpeaker();

        Task<Speaker> Get(int id);

        Task<IEnumerable<Session>> GetAllSessions(int id);

        Task<int> Save(Speaker speaker);
    }

    public class SpeakerResponseModel
    {
        public int Id { get; set; }

        public int SessionsCount { get; set; }
    }
}

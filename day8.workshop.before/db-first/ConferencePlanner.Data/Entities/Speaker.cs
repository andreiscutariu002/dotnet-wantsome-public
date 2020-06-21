namespace ConferencePlanner.Data.Entities
{
    using System.Collections.Generic;

    public class Speaker : ConferencePlanner.Entities.Speaker
    {
        public virtual ICollection<SessionSpeaker> SessionSpeakers { get; set; } = new List<SessionSpeaker>();
    }
}

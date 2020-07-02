namespace ConferencePlanner.Data.Entities
{
    using System.Collections.Generic;

    public class Session : ConferencePlanner.Entities.Session
    {
        public virtual ICollection<SessionSpeaker> SessionSpeakers { get; set; }

        public virtual ICollection<SessionAttendee> SessionAttendees { get; set; }

        public Track Track { get; set; }
    }
}

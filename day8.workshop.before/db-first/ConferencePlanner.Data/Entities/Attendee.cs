namespace ConferencePlanner.Data.Entities
{
    using System.Collections.Generic;

    public class Attendee : ConferencePlanner.Entities.Attendee
    {
        public virtual ICollection<SessionAttendee> SessionsAttendees { get; set; }
    }
}

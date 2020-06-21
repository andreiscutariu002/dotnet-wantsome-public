namespace ConferencePlanner.Data.Entities
{
    using System.Collections.Generic;

    public class Track : ConferencePlanner.Entities.Track
    {
        public virtual ICollection<Session> Sessions { get; set; }
    }
}

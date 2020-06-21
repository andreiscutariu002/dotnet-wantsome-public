namespace ConferencePlanner.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Session
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(4000)]
        public virtual string Abstract { get; set; }

        public virtual DateTimeOffset? StartTime { get; set; }

        public virtual DateTimeOffset? EndTime { get; set; }

        public TimeSpan Duration => this.EndTime?.Subtract(this.StartTime ?? this.EndTime ?? DateTimeOffset.MinValue) ??
                                    TimeSpan.Zero;

        public int? TrackId { get; set; }
    }
}

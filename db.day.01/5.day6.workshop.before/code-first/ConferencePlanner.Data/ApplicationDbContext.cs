namespace ConferencePlanner.Data
{
    using System.Collections.Generic;
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<Track> Tracks { get; set; }

        public DbSet<Speaker> Speakers { get; set; }

        public DbSet<SessionSpeaker> SessionSpeaker { get; set; }

        public DbSet<Attendee> Attendees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ex1 - todo
            modelBuilder.Entity<Attendee>(a => a.HasIndex(e => e.UserName).IsUnique());

            modelBuilder.Entity<SessionAttendee>()
                .HasKey(ss => new { ss.SessionId, ss.AttendeeId });

            modelBuilder.Entity<SessionSpeaker>()
                .HasKey(ss => new { ss.SessionId, ss.SpeakerId });

            // Ex2 here
            modelBuilder.Entity<Track>().HasData(new List<Track>()
            {
                new Track {Id = 10,Name = "C#"},
                new Track {Id = 11,Name = "PHP"},
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.; Integrated Security=True; Initial Catalog=ConferencePlanner;");
            }
        }
    }
}
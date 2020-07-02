namespace ConferencePlanner.Data
{
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
            // Unique UserName Attendee
            // Ex1 - todo

            // Many-to-many: Session <-> Attendee
            // Ex1 - todo

            // Many-to-many: Speaker <-> Session
            // Ex1 - todo        
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("TODO - Write Connection String");
            }
        }
    }
}

namespace Hotels.Data
{
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Room> Rooms { get; set; }
    }
}

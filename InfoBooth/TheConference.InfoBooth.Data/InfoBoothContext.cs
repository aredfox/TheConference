using Microsoft.EntityFrameworkCore;
using TheConference.InfoBooth.Core.Model;

namespace TheConference.InfoBooth.Data
{
    public class InfoBoothContext : DbContext
    {
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Room> Rooms { get; set; }

        public InfoBoothContext() : base() { 
        }

        public InfoBoothContext(DbContextOptions<InfoBoothContext> infoBoothContextOptions) 
            : base(infoBoothContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {            
        }
    }
}

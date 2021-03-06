﻿using System.Linq;
using Microsoft.EntityFrameworkCore;
using TheConference.InfoBooth.Core;
using TheConference.InfoBooth.Core.Model;
using TheConference.InfoBooth.Core.Sessions.Models;
using TheConference.InfoBooth.Core.Speakers;
using TheConference.InfoBooth.Core.Speakers.Models;

namespace TheConference.InfoBooth.Data {
    public class InfoBoothContext : DbContext, IInfoBoothContext {
        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<SpeakersPerSession> SpeakersPerSession { get; set; }

        public InfoBoothContext() { }

        public InfoBoothContext(DbContextOptions<InfoBoothContext> infoBoothContextOptions)
            : base(infoBoothContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<SpeakersPerSession>(cfg => {
                cfg.HasKey(e => new { e.SessionId, e.SpeakerId });
                cfg.HasOne(e => e.Session)
                   .WithMany(e => e.SessionsPerSpeaker)
                   .HasForeignKey(e => e.SessionId);
                cfg.HasOne(e => e.Speaker)
                   .WithMany(e => e.SpeakersPerSession)
                   .HasForeignKey(e => e.SpeakerId);
            });
            modelBuilder.Entity<Speaker>(cfg => {
                cfg.Ignore(p => p.Sessions);
            });
            modelBuilder.Entity<Session>(cfg => {
                cfg.Ignore(p => p.Speakers);
            });
        }
    }
}

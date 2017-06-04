using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TheConference.InfoBooth.Data;
using TheConference.InfoBooth.Core.Model;

namespace TheConference.InfoBooth.Data.Migrations
{
    [DbContext(typeof(InfoBoothContext))]
    partial class InfoBoothContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TheConference.InfoBooth.Core.Model.Attendee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Company");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Attendees");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Attendee");
                });

            modelBuilder.Entity("TheConference.InfoBooth.Core.Model.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime>("End");

                    b.Property<Guid?>("RoomId");

                    b.Property<DateTime>("Start");

                    b.Property<string>("Title");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Events");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Event");
                });

            modelBuilder.Entity("TheConference.InfoBooth.Core.Model.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("TheConference.InfoBooth.Core.Model.SpeakersPerSession", b =>
                {
                    b.Property<Guid>("SessionId");

                    b.Property<Guid>("SpeakerId");

                    b.HasKey("SessionId", "SpeakerId");

                    b.HasIndex("SpeakerId");

                    b.ToTable("SpeakersPerSession");
                });

            modelBuilder.Entity("TheConference.InfoBooth.Core.Model.Track", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("TheConference.InfoBooth.Core.Model.Speaker", b =>
                {
                    b.HasBaseType("TheConference.InfoBooth.Core.Model.Attendee");

                    b.Property<string>("Biography");

                    b.ToTable("Speaker");

                    b.HasDiscriminator().HasValue("Speaker");
                });

            modelBuilder.Entity("TheConference.InfoBooth.Core.Model.Session", b =>
                {
                    b.HasBaseType("TheConference.InfoBooth.Core.Model.Event");

                    b.Property<Guid?>("AttendeeId");

                    b.Property<string>("Description");

                    b.Property<int>("Level");

                    b.Property<Guid?>("TrackId");

                    b.HasIndex("AttendeeId");

                    b.HasIndex("TrackId");

                    b.ToTable("Session");

                    b.HasDiscriminator().HasValue("Session");
                });

            modelBuilder.Entity("TheConference.InfoBooth.Core.Model.Event", b =>
                {
                    b.HasOne("TheConference.InfoBooth.Core.Model.Room", "Room")
                        .WithMany("Events")
                        .HasForeignKey("RoomId");
                });

            modelBuilder.Entity("TheConference.InfoBooth.Core.Model.SpeakersPerSession", b =>
                {
                    b.HasOne("TheConference.InfoBooth.Core.Model.Session", "Session")
                        .WithMany("SessionsPerSpeaker")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TheConference.InfoBooth.Core.Model.Speaker", "Speaker")
                        .WithMany("SpeakersPerSession")
                        .HasForeignKey("SpeakerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TheConference.InfoBooth.Core.Model.Session", b =>
                {
                    b.HasOne("TheConference.InfoBooth.Core.Model.Attendee")
                        .WithMany("MarkedSessions")
                        .HasForeignKey("AttendeeId");

                    b.HasOne("TheConference.InfoBooth.Core.Model.Track", "Track")
                        .WithMany("Sessions")
                        .HasForeignKey("TrackId");
                });
        }
    }
}

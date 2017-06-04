using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TheConference.InfoBooth.Data;

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

            modelBuilder.Entity("TheConference.InfoBooth.Core.Model.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<Guid?>("RoomId");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Events");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Event");
                });

            modelBuilder.Entity("TheConference.InfoBooth.Core.Model.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("TheConference.InfoBooth.Core.Model.Speaker", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("Speakers");
                });

            modelBuilder.Entity("TheConference.InfoBooth.Core.Model.Track", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("TheConference.InfoBooth.Core.Model.Session", b =>
                {
                    b.HasBaseType("TheConference.InfoBooth.Core.Model.Event");


                    b.ToTable("Session");

                    b.HasDiscriminator().HasValue("Session");
                });

            modelBuilder.Entity("TheConference.InfoBooth.Core.Model.Event", b =>
                {
                    b.HasOne("TheConference.InfoBooth.Core.Model.Room")
                        .WithMany("Events")
                        .HasForeignKey("RoomId");
                });
        }
    }
}

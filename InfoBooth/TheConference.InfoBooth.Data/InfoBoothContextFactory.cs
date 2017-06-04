using System;
using System.Collections.Generic;
using TheConference.InfoBooth.Core.Model;
using TheConference.Shared.Infrastructure.Data.EFCore;

namespace TheConference.InfoBooth.Data
{
    public class InfoBoothContextFactory : DefaultDbContextFactory<InfoBoothContext>
    {
        protected override void Seed()
        {
            var dbFactory = new InfoBoothContextFactory();
            using (var db = dbFactory.Create())
            {
                Console.WriteLine("Seeding...");                

                var rooms = new List<Room> {
                    Room.Create("1.A"), Room.Create("1.B"), Room.Create("1.C"),
                    Room.Create("2.A"), Room.Create("2.B"), Room.Create("3.A"),
                };
                db.AddRange(rooms);
                Console.WriteLine("Adding rooms");

                db.SaveChanges();
                Console.WriteLine("Saved changes");
            }
        }
    }
}

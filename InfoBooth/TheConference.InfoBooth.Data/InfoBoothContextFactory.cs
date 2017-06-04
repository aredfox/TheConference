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
                Console.WriteLine("Start seeding...");

                Console.WriteLine(" - Start adding rooms...");
                var rooms = new List<Room> {
                    Room.Create("1A"), Room.Create("1B"), Room.Create("1C"),
                    Room.Create("2A"), Room.Create("2B"), Room.Create("3A"),
                };
                db.AddRange(rooms);
                Console.WriteLine("   Added rooms.");

                Console.WriteLine(" - Start adding attendees...");
                var attendees = new List<Attendee>() {
                    Attendee.Create("James", "Smith", "Apple Inc."),
                    Attendee.Create("John", "Wilson", "Sony"),
                    Attendee.Create("Robert", "Lam", "Toshiba"),
                    Attendee.Create("Michael", "Williams", "Apple Inc."),
                    Attendee.Create("Mary", "Brown", "Apple Inc."),
                    Attendee.Create("William", "Jones", "Apple Inc."),
                    Attendee.Create("David", "Miller"),
                    Attendee.Create("Patricia", "Davis", "Sony"),
                    Attendee.Create("Jennifer", "Garcia", "Intel"),
                    Attendee.Create("Richard", "Rodriguez", "Sony"),
                    Attendee.Create("Charles", "Martinez"),
                    Attendee.Create("Joseph", "Anderson", "HP Inc."),
                    Attendee.Create("Thomas", "Moore", "LG Electronics"),
                    Attendee.Create("Daniel", "Thompson", "Microsoft"),
                    Attendee.Create("Linda", "Clark"),
                    Attendee.Create("Barbara", "Taylor", "Microsoft"),
                    Attendee.Create("Michael", "King", "Apple Inc."),
                    Attendee.Create("Richard", "Scott", "Apple Inc."),
                    Attendee.Create("Lisa", "Green", "Dell technologies"),
                    Attendee.Create("Donald", "Evans", "IBM"),
                    Attendee.Create("John", "Carter", "Apple Inc.")
                };
                db.AddRange(attendees);
                Console.WriteLine("   Added attendees.");

                Console.WriteLine(" - Start adding speakers...");
                var speakers = new List<Speaker>() {
                    Speaker.Create(Attendee.Create("Scott", "Hanselman", "Microsoft"), "My name is Scott Hanselman. I'm a programmer, teacher, and speaker. I work out of my home office in Portland, Oregon for the Web Platform Team at Microsoft, but this blog, its content and opinions are my own. I blog about technology, culture, gadgets, diversity, code, the web, where we're going and where we've been. I'm excited about community, social equity, media, entrepreneurship and above all, the open web."),
                    Speaker.Create(Attendee.Create("Jimmy", "Bogard", "Los Techies"), "I'm the chief architect at Headspring in Austin, TX. I focus on DDD, distributed systems, and any other acronym-centric design/architecture/methodology. I created AutoMapper and am a co-author of the ASP.NET MVC in Action books."),
                    Speaker.Create(Attendee.Create("Jon", "Skeet", "Google"), " Jon Skeet, a Software Engineer at Google and long-time Stack Overflow contributor. Creator of Noda Time, a better date/time API for .NET, Jon is the author of the book, C# in Depth, and he writes regularly about coding on his blog."),
                    Speaker.Create(Attendee.Create("Todd", "Motto", "Ultimate Angular"), "I’m Todd, a 26 year old front-end engineer from England, UK. I run Ultimate Angular (which just won “Best Angular product for Education” award!), teaching developers and teams how to become Angular experts through online courses."),
                    Speaker.Create(Attendee.Create("Scott", "Allen", "Ode To Code LLC"), "I write software and consult through OdeToCode LLC. I have 25+ years of commercial software development experience across a wide range of technologies. I’ve successfully delivered software products for embedded, Windows, and web platforms. I’ve developed web services for Fortune 500 companies and firmware for startups.")                    
                };
                db.AddRange(speakers);
                Console.WriteLine("   Added speakers.");

                Console.WriteLine(" - Saving changes...");
                db.SaveChanges();
                Console.WriteLine("   Saved changes.");

                Console.WriteLine("Seeding done.");
            }
        }
    }
}

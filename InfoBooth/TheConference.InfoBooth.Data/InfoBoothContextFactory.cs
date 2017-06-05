using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TheConference.InfoBooth.Core.Model;
using TheConference.InfoBooth.Core.Sessions.Models;
using TheConference.InfoBooth.Core.Speakers;
using TheConference.InfoBooth.Core.Speakers.Models;
using TheConference.Shared.Infrastructure.Data.EFCore;

namespace TheConference.InfoBooth.Data {
    public class InfoBoothContextFactory : DefaultDbContextFactory<InfoBoothContext> {
        private void SaveChanges(DbContext db) {
            Console.WriteLine(" - Saving changes...");
            db.SaveChanges();
            Console.WriteLine("   Saved changes.");
        }

        private void ListDataFromInfoBoothPerspective(InfoBoothContext db) {
            Console.Clear();

            Console.WriteLine("SESSIONS: ");
            foreach (var session in db.Sessions.Include(e => e.Room).Include(e => e.Track).Include(e => e.SessionsPerSpeaker).ThenInclude(e => e.Speaker).OrderBy(s => s.Start).ToList()) {
                Console.WriteLine($"{session.Title.ToUpper()}");
                Console.WriteLine($"  ABOUT: {session.Description}");
                Console.WriteLine($"  BY: {String.Join(", ", session.Speakers.OrderBy(s => s.LastName).Select(s => s.FullName))}");
                Console.WriteLine($"@@ [ {session.Level.ToString()} | {session.Duration} | {session.Room.Name} ]");
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("ATTENDEES: ");
            Console.WriteLine(String.Join(", ", db.Attendees.ToList().Select(s => s.FullName)));

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("SPEAKERS: ");
            Console.WriteLine(String.Join(", ", db.Speakers.ToList().Select(s => s.FullName)));

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("ATTENDEES minus SPEAKERS: ");
            Console.WriteLine(String.Join(", ", db.Attendees.Where(a => a.GetType() != typeof(Speaker)).ToList().Select(s => s.FullName)));

            Console.ReadKey();
        }

        protected override void Seed() {
            var dbFactory = new InfoBoothContextFactory();
            using (var db = dbFactory.Create()) {
                Console.WriteLine("Start seeding...");

                if (db.Rooms.Any() || db.Tracks.Any() || db.Events.Any() || db.Attendees.Any()) {
                    Console.WriteLine("  Data present in db, quitting...");
                    ListDataFromInfoBoothPerspective(db);
                    return;
                }

                Console.WriteLine(" - Start adding rooms...");
                var rooms = new List<Room> {
                    Room.Create("1A"), Room.Create("1B"), Room.Create("1C"),
                    Room.Create("2A"), Room.Create("2B"), Room.Create("3A"),
                };
                db.AddRange(rooms);
                Console.WriteLine("   Added rooms.");

                Console.WriteLine(" - Start adding tracks...");
                var tracks = new List<Track> {
                    Track.Create("Developer", "Developers, developers, developers!"),
                    Track.Create("DevOps", "It's sysadmin day again!")
                };
                db.AddRange(tracks);
                Console.WriteLine("   Added tracks.");

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

                db.SaveChanges();

                Console.WriteLine(" - Start adding sessions...");
                var sessions = new List<Session>() { };
                var speakersPerSession = new List<SpeakersPerSession>() { };

                var session1 =
                    Session.Create(Event.Create(
                            EventType.Talk, "JavaScript Patterns for 2017",
                            start: new DateTime(2017, 06, 01, 10, 15, 00),
                            end: new DateTime(2017, 06, 01, 11, 15, 00),
                            room: db.Rooms.Where(r => r.Name == "1A").FirstOrDefault()
                        ),
                        track: db.Tracks.Where(t => t.Name == "Developer").FirstOrDefault(),
                        level: SessionLevel.Introductionary,
                        description: "The JavaScript language and ecosystem have seen dramatic changes in the last 2 years. In this sessions we'll look at patterns for organizing code using modules, talk about some of the pros and cons of new language features, and look at the current state of build tools and build patterns."
                    );
                sessions.Add(session1);
                speakersPerSession.Add(SpeakersPerSession.Create(db.Speakers.FirstOrDefault(s => s.LastName == "Allen"), session1));

                var session2 =
                    Session.Create(Event.Create(
                            EventType.Keynote, "If I knew then what I know now.",
                            start: new DateTime(2017, 06, 01, 09, 00, 00),
                            end: new DateTime(2017, 06, 01, 10, 00, 00),
                            room: db.Rooms.Where(r => r.Name == "3A").FirstOrDefault()
                        ),
                        track: db.Tracks.Where(t => t.Name == "Developer").FirstOrDefault(),
                        level: SessionLevel.Introductionary,
                        description: "I've been coding for money for 25 years. I recently met a programmer who’s been coding for over 50. They asked me how they could become a web developer. Here’s what I told them."
                    );
                sessions.Add(session2);
                speakersPerSession.Add(SpeakersPerSession.Create(db.Speakers.FirstOrDefault(s => s.LastName == "Hanselman"), session2));

                var session3 =
                    Session.Create(Event.Create(
                            EventType.Talk, "SOLID Architecture in Slices not Layers",
                            start: new DateTime(2017, 06, 01, 10, 15, 00),
                            end: new DateTime(2017, 06, 01, 11, 15, 00),
                            room: db.Rooms.Where(r => r.Name == "2A").FirstOrDefault()
                        ),
                        track: db.Tracks.Where(t => t.Name == "Developer").FirstOrDefault(),
                        level: SessionLevel.Intermediate,
                        description: "For too long we've lived under the tyranny of n-tier architectures. Building systems with complicated abstractions, needless indirection and more mocks in our tests than a comedy special. But there is a better way - thinking in terms of architectures of vertical slices instead horizontal layers. Once we embrace slices over layers, we open ourselves to a new, simpler architecture, changing how we build, organize and deploy systems."
                    );
                sessions.Add(session3);
                speakersPerSession.Add(SpeakersPerSession.Create(db.Speakers.FirstOrDefault(s => s.LastName == "Bogard"), session3));

                var session4 =
                    Session.Create(Event.Create(
                            EventType.QuestionAndAnswer, "Head to Head: Scott Allen and Jon Skeet with Scott Hanselman",
                            start: new DateTime(2017, 06, 01, 11, 45, 00),
                            end: new DateTime(2017, 06, 01, 12, 45, 00),
                            room: db.Rooms.Where(r => r.Name == "1A").FirstOrDefault()
                        ),
                        track: db.Tracks.Where(t => t.Name == "Developer").FirstOrDefault(),
                        level: SessionLevel.Introductionary,
                        description: "We’re back with another Stack Overflow Question and Answer session - this time with K. Scott Allen going up against Jon Skeet. In this session, Scott Hanselman will select five questions from Stack Overflow pertaining to .NET and will send these questions to Scott and Jon a week before the talk."
                    );
                sessions.Add(session4);
                speakersPerSession.AddRange(SpeakersPerSession.Create(new List<Speaker> { db.Speakers.FirstOrDefault(s => s.LastName == "Skeet"), db.Speakers.FirstOrDefault(s => s.LastName == "Allen"), db.Speakers.FirstOrDefault(s => s.LastName == "Hanselman") }, session4));

                var session5 =
                    Session.Create(Event.Create(
                            EventType.Talk, "Domain Driven Design: The Good Parts",
                            start: new DateTime(2017, 06, 01, 11, 45, 00),
                            end: new DateTime(2017, 06, 01, 12, 45, 00),
                            room: db.Rooms.Where(r => r.Name == "2A").FirstOrDefault()
                        ),
                        track: db.Tracks.Where(t => t.Name == "Developer").FirstOrDefault(),
                        level: SessionLevel.Expert,
                        description: "The greenfield project started out so promising. Instead of devolving into big ball of mud, the team decided to apply domain-driven design principles. Ubiquitous language, proper boundaries, encapsulation, it all made sense."
                    );
                sessions.Add(session5);
                speakersPerSession.Add(SpeakersPerSession.Create(db.Speakers.FirstOrDefault(s => s.LastName == "Bogard"), session5));

                db.AddRange(sessions);
                db.AddRange(speakersPerSession);
                Console.WriteLine("   Added sessions.");

                SaveChanges(db);

                Console.WriteLine("Seeding done.");

                ListDataFromInfoBoothPerspective(db);
            }
        }
    }
}

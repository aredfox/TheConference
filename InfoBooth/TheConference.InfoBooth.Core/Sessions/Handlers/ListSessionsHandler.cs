using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TheConference.InfoBooth.Core.Sessions.Handlers {
    public class ListSessionsHandler : IRequestHandler<ListSessionsQuery, ListSessionsResponse> {
        private readonly IInfoBoothContext _db;

        public ListSessionsHandler(IInfoBoothContext db) {
            _db = db;
        }

        public ListSessionsResponse Handle(ListSessionsQuery message) {
            var manyToMany = _db.SpeakersPerSession
                .Include(sps => sps.Speaker)
                .Include(sps => sps.Session).ThenInclude(s => s.Room)
                .GroupBy(s => s.Session)
                .ToList();

            var sessions = new List<ListSessionsResponseItem>();
            foreach (var session in manyToMany) {
                var sessionItem = new ListSessionsResponseItem();
                sessionItem.Title = session.Key.Title;
                sessionItem.Description = session.Key.Description;
                sessionItem.Speakers = session.Key.Speakers.Select(spk => spk.FullName).ToList();
                sessionItem.Duration = session.Key.Duration;
                sessionItem.Start = session.Key.Start;
                sessionItem.End = session.Key.End;
                sessionItem.RoomName = session.Key.Room.Name;
                sessionItem.Slug = session.Key.Slug;
                sessions.Add(sessionItem);
            }

            /*var sessions = manyToMany
                .Select(s => new ListSessionsResponseItem {
                    Title = s.Key.Title,
                    Description = s.Key.Description,
                    Speakers = s.Key.Speakers.Select(spk => spk.FullName).ToList(),
                    Duration = s.Key.Duration,
                    Start = s.Key.Start,
                    End = s.Key.End,
                    RoomName = s.Key.Room.Name,
                    Slug = s.Key.Slug
                })
                .ToList();*/

            return new ListSessionsResponse {
                Sessions = sessions
            };
        }
    }

    public class ListSessionsResponse {
        public IEnumerable<ListSessionsResponseItem> Sessions { get; set; }
    }

    public class ListSessionsResponseItem {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Speakers { get; set; }
        public string RoomName { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Slug { get; set; }
    }

    public class ListSessionsQuery : IRequest<ListSessionsResponse> { }
}

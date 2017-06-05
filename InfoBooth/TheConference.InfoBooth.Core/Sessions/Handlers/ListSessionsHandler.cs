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
            var sessions = _db
                .Sessions
                .Include(e => e.SessionsPerSpeaker).ThenInclude(e => e.Speaker)
                .Select(s => new ListSessionsResponseItem {
                    Title = s.Title,
                    Description = s.Description,
                    Speakers = s.Speakers.Select(spk => spk.FullName).ToList(),
                    Duration = s.Duration,
                    Start = s.Start,
                    End = s.End,
                    RoomName = s.Room.Name,
                    Slug = s.Slug
                })
                .ToList();

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

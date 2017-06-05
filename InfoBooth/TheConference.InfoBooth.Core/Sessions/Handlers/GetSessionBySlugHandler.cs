using System;
using System.Linq;
using MediatR;

namespace TheConference.InfoBooth.Core.Sessions.Handlers {
    public class GetSessionBySlugHandler : IRequestHandler<GetSessionBySlugQuery, GetSessionBySlugResponse> {
        private readonly IInfoBoothContext _db;

        public GetSessionBySlugHandler(IInfoBoothContext db) {
            _db = db;
        }

        public GetSessionBySlugResponse Handle(GetSessionBySlugQuery message) {
            var session = _db
                .Sessions
                .Where(s => s.Slug == message.Slug.ToLower().Trim())
                .Select(s => new {
                    s.Title,
                    s.Description,
                    s.Duration,
                    s.Start,
                    s.End,
                    RoomName = s.Room.Name
                })
                .FirstOrDefault();

            return new GetSessionBySlugResponse {
                Title = session.Title,
                Description = session.Description,
                Duration = session.Duration,
                Start = session.Start,
                End = session.End,
                RoomName = session.RoomName
            };
        }
    }

    public class GetSessionBySlugResponse {
        public string Title { get; set; }
        public string Description { get; set; }
        public string RoomName { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    public class GetSessionBySlugQuery : IRequest<GetSessionBySlugResponse> {
        public GetSessionBySlugQuery(string slug) {
            Slug = slug;
        }

        public string Slug { get; set; }
    }
}

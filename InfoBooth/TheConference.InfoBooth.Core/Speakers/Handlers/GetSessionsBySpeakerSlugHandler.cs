using System.Linq;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace TheConference.InfoBooth.Core.Speakers.Handlers {
    public class GetSessionsBySpeakerSlugHandler : IRequestHandler<GetSessionsBySpeakerSlugQuery, GetSessionsBySpeakerSlugResponse> {
        private readonly IInfoBoothContext _db;

        public GetSessionsBySpeakerSlugHandler(IInfoBoothContext db) {
            _db = db;
        }

        public GetSessionsBySpeakerSlugResponse Handle(GetSessionsBySpeakerSlugQuery message) {
            var sessions = _db
                .Sessions
                .Include(s => s.SessionsPerSpeaker).ThenInclude(s => s.Session)
                .Include(s => s.SessionsPerSpeaker).ThenInclude(s => s.Speaker)
                .Where(s => s.Speakers.Select(t => t.Slug).Contains(message.Slug.ToLower().Trim()))
                .Select(s => new ListSessionsResponseItem {
                    Title = s.Title,
                    Description = s.Description,
                    Duration = s.Duration,
                    Start = s.Start,
                    End = s.End,
                    RoomName = s.Room.Name,
                    Slug = s.Slug
                })
                .ToList();

            return new GetSessionsBySpeakerSlugResponse {
                Sessions = sessions
            };
        }
    }

    public class GetSessionsBySpeakerSlugResponse {
        public IEnumerable<ListSessionsResponseItem> Sessions { get; set; }
    }

    public class ListSessionsResponseItem {
        public string Title { get; set; }
        public string Description { get; set; }
        public string RoomName { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Slug { get; set; }
    }

    public class GetSessionsBySpeakerSlugQuery : IRequest<GetSessionsBySpeakerSlugResponse> {
        public GetSessionsBySpeakerSlugQuery(string slug) {
            Slug = slug;
        }

        public string Slug { get; set; }
    }
}

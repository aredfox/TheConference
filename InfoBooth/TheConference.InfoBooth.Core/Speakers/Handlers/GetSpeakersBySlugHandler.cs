using System.Linq;
using MediatR;

namespace TheConference.InfoBooth.Core.Speakers.Handlers {
    public class GetSpeakerBySlugHandler : IRequestHandler<GetSpeakerBySlugQuery, GetSpeakerBySlugResponse> {
        private readonly IInfoBoothContext _db;

        public GetSpeakerBySlugHandler(IInfoBoothContext db) {
            _db = db;
        }

        public GetSpeakerBySlugResponse Handle(GetSpeakerBySlugQuery message) {
            var speaker = _db
                .Speakers
                .Where(s => s.Slug == message.Slug.ToLower().Trim())
                .Select(s => new {
                    s.FirstName,
                    s.LastName,
                    s.Biography,
                    s.Slug
                })
                .FirstOrDefault();

            return new GetSpeakerBySlugResponse {
                FirstName = speaker.FirstName,
                LastName = speaker.LastName,
                Biography = speaker.Biography,
                Slug = speaker.Slug
            };
        }
    }

    public class GetSpeakerBySlugResponse {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; }
        public string Slug { get; set; }
    }

    public class GetSpeakerBySlugQuery : IRequest<GetSpeakerBySlugResponse> {
        public GetSpeakerBySlugQuery(string slug) {
            Slug = slug;
        }

        public string Slug { get; set; }
    }
}

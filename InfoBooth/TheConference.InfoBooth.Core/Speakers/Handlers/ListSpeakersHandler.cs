using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace TheConference.InfoBooth.Core.Speakers.Handlers {
    public class ListSpeakersHandler : IRequestHandler<ListSpeakersQuery, ListSpeakersResponse> {
        private readonly IInfoBoothContext _db;

        public ListSpeakersHandler(IInfoBoothContext db) {
            _db = db;
        }

        public ListSpeakersResponse Handle(ListSpeakersQuery message) {
            var speakers = _db
                .Speakers
                .Select(s => new ListSpeakersResponseItem {
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Biography = s.Biography,
                    Slug = s.Slug
                })
                .ToList();

            return new ListSpeakersResponse {
                Speakers = speakers
            };
        }
    }

    public class ListSpeakersResponse {
        public IEnumerable<ListSpeakersResponseItem> Speakers { get; set; }
    }

    public class ListSpeakersResponseItem {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; }
        public string Slug { get; set; }
    }

    public class ListSpeakersQuery : IRequest<ListSpeakersResponse> { }
}

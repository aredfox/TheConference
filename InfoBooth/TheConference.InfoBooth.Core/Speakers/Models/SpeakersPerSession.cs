using System;
using System.Collections.Generic;
using System.Linq;
using TheConference.InfoBooth.Core.Sessions.Models;

namespace TheConference.InfoBooth.Core.Speakers.Models {
    public class SpeakersPerSession {
        private SpeakersPerSession() { }

        public Guid SpeakerId { get; private set; }
        public Speaker Speaker { get; private set; }
        public Guid SessionId { get; private set; }
        public Session Session { get; private set; }

        internal static IEnumerable<SpeakersPerSession> Create(IEnumerable<Speaker> speakers, Session session) {
            if (speakers == null) {
                throw new ArgumentNullException(nameof(speakers));
            }
            if (session == null) {
                throw new ArgumentNullException(nameof(session));
            }

            return speakers.Select(speaker => new SpeakersPerSession {
                Speaker = speaker,
                Session = session
            });
        }
        internal static SpeakersPerSession Create(Speaker speaker, Session session) {
            if (speaker == null) {
                throw new ArgumentNullException(nameof(speaker));
            }

            return Create(new List<Speaker> { speaker }, session).First();
        }
    }
}

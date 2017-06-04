using System.Collections.Generic;
using System.Linq;

namespace TheConference.InfoBooth.Core.Model
{
    public class Session : Event
    {
        public string Description { get; private set; }
        public Track Track { get; private set; }
        public SessionLevel Level { get; private set; }

        public IEnumerable<SpeakersPerSession> SessionsPerSpeaker { get; private set; }
        public IEnumerable<Session> Speakers => SessionsPerSpeaker.Select(e => e.Session).AsEnumerable();
    }
}
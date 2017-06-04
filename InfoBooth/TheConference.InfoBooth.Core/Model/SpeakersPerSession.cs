using System;

namespace TheConference.InfoBooth.Core.Model
{
    public class SpeakersPerSession
    {
        public Guid SpeakerId { get; private set; }
        public Speaker Speaker { get; private set; }
        public Guid SessionId { get; private set; }
        public Session Session { get; private set; }
    }
}

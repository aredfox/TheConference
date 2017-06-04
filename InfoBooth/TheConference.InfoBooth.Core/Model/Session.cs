using System.Collections.Generic;

namespace TheConference.InfoBooth.Core.Model
{
    public class Session : Event
    {
        public string Description { get; }
        public Track Track { get; }
        public SessionLevel Level { get; }

        public IEnumerable<Speaker> Speakers { get; }        
    }
}
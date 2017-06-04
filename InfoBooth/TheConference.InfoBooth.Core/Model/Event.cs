using System;
using TheConference.Shared.Infrastructure.Data.EFCore;

namespace TheConference.InfoBooth.Core.Model
{
    public class Event : Entity<Guid>
    {
        public string Title { get; }
        public EventType Type { get; }
        public DateTime Start { get; }
        public DateTime End { get; }

        public Room Room { get; }
    }
}

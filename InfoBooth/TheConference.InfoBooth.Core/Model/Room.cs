using System;
using System.Collections.Generic;
using TheConference.Shared.Infrastructure.Data.EFCore;

namespace TheConference.InfoBooth.Core.Model
{
    public class Room : Entity<Guid>
    {
        public string Name { get; }
        public IEnumerable<Event> Events { get; }
    }
}

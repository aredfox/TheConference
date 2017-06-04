using System;
using System.Collections.Generic;
using TheConference.Shared.Infrastructure.Data.EFCore;

namespace TheConference.InfoBooth.Core.Model
{
    public class Room : Entity<Guid>
    {
        private Room() { }

        public string Name { get; private set; }                
        public IEnumerable<Event> Events { get; private set; }
    }
}

using System;
using System.Collections.Generic;
using TheConference.Shared.Infrastructure.Data.EFCore;

namespace TheConference.InfoBooth.Core.Model
{
    public class Room : Entity<Guid>
    {
        private Room() {
            Events = new List<Event>();
        }

        public string Name { get; private set; }                
        public IEnumerable<Event> Events { get; private set; }

        public static Room Create(string name, IEnumerable<Event> events = null) {
            if(String.IsNullOrWhiteSpace(name)) {
                throw new ArgumentNullException(nameof(name));
            }

            return new Room {
                Name = name,
                Events = events
            };
        }
    }
}

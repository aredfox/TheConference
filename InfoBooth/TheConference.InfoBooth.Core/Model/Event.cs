using System;
using System.ComponentModel.DataAnnotations;
using TheConference.Shared.Infrastructure.Data.EFCore;

namespace TheConference.InfoBooth.Core.Model
{
    public class Event : Entity<Guid>
    {        
        protected Event() { }

        public string Title { get; protected set; }
        public EventType Type { get; protected set; }
        public DateTime Start { get; protected set; }
        public DateTime End { get; protected set; }
        public TimeSpan Duration => (End - Start).Duration();
        public Room Room { get; protected set; }

        internal static Event Create(EventType type, string title, DateTime start, DateTime end, Room room) {
            if(String.IsNullOrWhiteSpace(title)){
                throw new ArgumentNullException(nameof(title));
            }
            if (start == default(DateTime)) {
                throw new ArgumentNullException(nameof(start));
            }
            if (end == default(DateTime)) {
                throw new ArgumentNullException(nameof(end));
            }
            if (room == null) {
                throw new ArgumentNullException(nameof(room));
            }

            return new Event {
                Type = type,
                Title = title,
                Start = start,
                End = end,
                Room = room
            };
        }
    }
}

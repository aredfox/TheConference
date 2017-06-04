using System;
using System.ComponentModel.DataAnnotations;
using TheConference.Shared.Infrastructure.Data.EFCore;

namespace TheConference.InfoBooth.Core.Model
{
    public class Event : Entity<Guid>
    {        
        protected Event() { }

        public string Title { get; private set; }        
        public EventType Type { get; private set; }        
        public DateTime Start { get; private set; }        
        public DateTime End { get; private set; }        
        public Room Room { get; private set; }
    }
}

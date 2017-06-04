using System;
using System.Collections.Generic;
using TheConference.Shared.Infrastructure.Data.EFCore;

namespace TheConference.InfoBooth.Core.Model
{
    public class Track : Entity<Guid>
    {
        private Track() { }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public IEnumerable<Session> Sessions { get; private set; }
    }
}

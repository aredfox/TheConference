using System;
using System.Collections.Generic;
using TheConference.Shared.Infrastructure.Data.EFCore;

namespace TheConference.InfoBooth.Core.Model
{
    public class Speaker : Entity<Guid>
    {
        public string FullName { get; }
        public IEnumerable<Session> Sessions { get; }
    }
}

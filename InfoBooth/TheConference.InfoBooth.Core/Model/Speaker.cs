using System;
using System.Collections.Generic;
using System.Linq;
using TheConference.Shared.Infrastructure.Data.EFCore;

namespace TheConference.InfoBooth.Core.Model
{
    public class Speaker : Entity<Guid>
    {
        public string FullName { get; private set; }
        public IEnumerable<SpeakersPerSession> SpeakersPerSession { get; private set; }
        public IEnumerable<Session> Sessions => SpeakersPerSession.Select(e => e.Session).AsEnumerable();
    }
}

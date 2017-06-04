using System;
using TheConference.Shared.Infrastructure.Data.EFCore;

namespace TheConference.InfoBooth.Core.Model
{
    public class Track : Entity<Guid>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
    }
}

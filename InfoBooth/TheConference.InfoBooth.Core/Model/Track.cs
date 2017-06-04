using System;
using TheConference.Shared.Infrastructure.Data.EFCore;

namespace TheConference.InfoBooth.Core.Model
{
    public class Track : Entity<Guid>
    {
        public string Name { get; }
        public string Description { get; }
    }
}

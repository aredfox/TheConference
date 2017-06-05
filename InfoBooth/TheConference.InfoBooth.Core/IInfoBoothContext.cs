using Microsoft.EntityFrameworkCore;
using TheConference.InfoBooth.Core.Sessions.Models;
using TheConference.InfoBooth.Core.Speakers.Models;

namespace TheConference.InfoBooth.Core {
    public interface IInfoBoothContext {
        DbSet<Session> Sessions { get; }
        DbSet<Speaker> Speakers { get; }
    }
}

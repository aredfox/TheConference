using Microsoft.EntityFrameworkCore;
using TheConference.InfoBooth.Core.Sessions.Models;
using TheConference.InfoBooth.Core.Speakers.Models;

namespace TheConference.InfoBooth.Core {
    public interface IInfoBoothContext {
        DbSet<Speaker> Speakers { get; set; }
        DbSet<Session> Sessions { get; set; }
        DbSet<SpeakersPerSession> SpeakersPerSession { get; set; }
    }
}

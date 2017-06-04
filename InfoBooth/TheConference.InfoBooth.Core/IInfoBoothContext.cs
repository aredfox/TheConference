using System.Linq;
using TheConference.InfoBooth.Core.Sessions.Models;

namespace TheConference.InfoBooth.Core
{
    public interface IInfoBoothContext
    {
        IQueryable<Session> Sessions { get; }
    }
}

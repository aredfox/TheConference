using System;
using System.Collections.Generic;
using TheConference.InfoBooth.Core.Sessions.Models;
using TheConference.Shared.Infrastructure.Data.EFCore;

namespace TheConference.InfoBooth.Core.Model
{
    public class Track : Entity<Guid>
    {
        private Track() { }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public IEnumerable<Session> Sessions { get; private set; }

        internal static Track Create(string name, string description, IEnumerable<Session> sessions = null) {
            if(String.IsNullOrWhiteSpace(name)){
                throw new ArgumentNullException(nameof(name));
            }
            if (String.IsNullOrWhiteSpace(description)) {
                throw new ArgumentNullException(nameof(description));
            }

            return new Track {
                Name = name,
                Description = description,
                Sessions = sessions ?? new List<Session>()
            };
        }
    }
}

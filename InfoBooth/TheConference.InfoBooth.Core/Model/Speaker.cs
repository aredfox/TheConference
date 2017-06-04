using System;
using System.Collections.Generic;
using System.Linq;

namespace TheConference.InfoBooth.Core.Model
{
    public class Speaker : Attendee
    {
        private Speaker() : base() { }
        
        public string Biography { get; private set; }
        public IEnumerable<SpeakersPerSession> SpeakersPerSession { get; private set; }
        public IEnumerable<Session> Sessions => SpeakersPerSession.Select(e => e.Session).AsEnumerable();

        public Attendee AsAttendee() {
            return this;
        }

        internal static Speaker Create(Attendee attendee, string biography)
        {
            if(attendee == null) {
                throw new ArgumentNullException(nameof(attendee));
            }            
            if (String.IsNullOrWhiteSpace(biography)) {
                throw new ArgumentNullException(nameof(biography));
            }            

            return new Speaker {
                FirstName = attendee.FirstName,
                LastName = attendee.LastName,
                Company = attendee.Company,
                MarkedSessions = attendee.MarkedSessions ?? new List<Session>(),
                Biography = biography                
            };
        }
    }
}

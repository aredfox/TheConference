using System;
using System.Collections.Generic;
using System.Linq;
using TheConference.InfoBooth.Core.Sessions.Models;
using TheConference.Shared.Infrastructure.Data.EFCore;

namespace TheConference.InfoBooth.Core.Model {
    public class Attendee : Entity<Guid> {
        private readonly HashSet<Session> _markedSessions = new HashSet<Session>();

        protected Attendee() {
            MarkedSessions = new List<Session>();
        }

        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Company { get; protected set; }
        public bool HasCompany => String.IsNullOrWhiteSpace(Company);
        public IEnumerable<Session> MarkedSessions {
            get => _markedSessions.AsEnumerable();
            protected set {
                _markedSessions.Clear();
                value.ToList().ForEach(s => _markedSessions.Add(s));
            }
        }

        public void MarkSession(Session session) {
            _markedSessions.Add(session);
        }
        public void UnmarkSession(Session session) {
            UnmarkSession(session.Id);
        }
        public void UnmarkSession(Guid sessionId) {
            _markedSessions.RemoveWhere(s => s.Id == sessionId);
        }

        internal static Attendee Create(string firstName, string lastName, string company = null, IEnumerable<Session> markedSessions = null) {
            if (String.IsNullOrWhiteSpace(firstName)) {
                throw new ArgumentNullException(nameof(firstName));
            }
            if (String.IsNullOrWhiteSpace(lastName)) {
                throw new ArgumentNullException(nameof(lastName));
            }

            return new Attendee {
                FirstName = firstName,
                LastName = lastName,
                Company = company,
                MarkedSessions = markedSessions ?? new List<Session>()
            };
        }
    }
}

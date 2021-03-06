﻿using System;
using System.Collections.Generic;
using System.Linq;
using TheConference.InfoBooth.Core.Model;
using TheConference.InfoBooth.Core.Speakers;
using TheConference.InfoBooth.Core.Speakers.Models;

namespace TheConference.InfoBooth.Core.Sessions.Models {
    public class Session : Event {
        private Session() : base() { }

        public string Description { get; private set; }
        public Track Track { get; private set; }
        public SessionLevel Level { get; private set; }
        public IEnumerable<SpeakersPerSession> SessionsPerSpeaker { get; private set; }
        public IEnumerable<Speaker> Speakers => SessionsPerSpeaker.Select(e => e.Speaker).AsEnumerable();

        public Event AsEvent() {
            return this;
        }

        internal static Session Create(Event @event, Track track, SessionLevel level, string description) {
            if (@event == null) {
                throw new ArgumentNullException(nameof(@event));
            }
            if (track == null) {
                throw new ArgumentNullException(nameof(track));
            }
            if (String.IsNullOrWhiteSpace(description)) {
                throw new ArgumentNullException(nameof(description));
            }

            return new Session {
                Type = @event.Type,
                Title = @event.Title,
                Start = @event.Start,
                End = @event.End,
                Room = @event.Room,
                Description = description,
                Track = track,
                Level = level
            };
        }
    }
}
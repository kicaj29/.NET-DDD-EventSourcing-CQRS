﻿using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cafe.Domain
{
    public class InMemoryEventStore : IEventStore
    {
        private class Stream
        {
            public List<dynamic> Events;
        }

        private ConcurrentDictionary<Guid, Stream> store =
            new ConcurrentDictionary<Guid, Stream>();

        public IEnumerable<dynamic> LoadEventsFor<TAggregate>(Guid id)
        {
            // Get the current event stream; note that we never mutate the
            // Events array so it's safe to return the real thing.
            Stream s;
            if (store.TryGetValue(id, out s))
                return s.Events;
            else
                return new List<dynamic>();
        }

        public void SaveEventsFor<TAggregate>(Guid aggregateId, int eventsLoaded, IEnumerable<dynamic> newEvents)
        {
            // Get or create stream.
            var s = store.GetOrAdd(aggregateId, _ => new Stream());

            // We'll use a lock-free algorithm for the update.
            while (true)
            {
                // Read the current event list.
                var eventList = s.Events;

                // Ensure no events persisted since us.
                var prevEvents = eventList == null ? 0 : eventList.Count;
                if (prevEvents != eventsLoaded)
                    throw new Exception("Concurrency conflict; cannot persist these events");

                // Create a new event list with existing ones plus our new
                // ones (making new important for lock free algorithm!)

                var newEventList = new List<dynamic>();
                if (eventList != null)
                { newEventList.AddRange(eventList); }

                //var newEventList = eventList == null
                //    ? new List<dynamic>()
                //    : new List<dynamic> {eventList};

                newEventList.AddRange(newEvents);

                // Try to put the new event list in place atomically.
                if (Interlocked.CompareExchange(ref s.Events, newEventList, eventList) == eventList)
                    break;
            }
        }

        private Guid GetAggregateIdFromEvent(object e)
        {
            var idField = e.GetType().GetField("Id");
            if (idField == null)
                throw new Exception("Event type " + e.GetType().Name + " is missing an Id field");
            return (Guid)idField.GetValue(e);
        }
    }
}

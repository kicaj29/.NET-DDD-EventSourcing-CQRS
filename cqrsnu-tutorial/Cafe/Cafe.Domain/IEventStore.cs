using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Domain
{
    public interface IEventStore
    {
        IEnumerable<dynamic> LoadEventsFor<TAggregate>(Guid id);
        void SaveEventsFor<TAggregate>(Guid id, int eventsLoaded, IEnumerable<dynamic> newEvents);
    }
}

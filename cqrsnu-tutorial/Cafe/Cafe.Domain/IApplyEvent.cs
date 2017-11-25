using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Domain
{
    /// <summary>
    /// Implemented by an aggregate once for each event type it can apply.
    /// The aggregate plays an important role in turning that event history into a representation of current state.
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public interface IApplyEvent<TEvent>
    {
        void Apply(TEvent e);
    }
}

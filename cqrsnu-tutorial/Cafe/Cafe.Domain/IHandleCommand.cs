using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Domain
{
    public interface IHandleCommand<TCommand>
    {
        IEnumerable<dynamic> Handle(TCommand c);
    }
}

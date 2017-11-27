using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.ReadModel
{
    public interface IOpenTabQueries
    {
        List<int> ActiveTableNumbers();
        OpenTabs.TabInvoice InvoiceForTable(int table);
        Guid TabIdForTable(int table);
        OpenTabs.TabStatus TabForTable(int table);
        Dictionary<int, List<OpenTabs.TabItem>> TodoListForWaiter(string waiter);
    }
}

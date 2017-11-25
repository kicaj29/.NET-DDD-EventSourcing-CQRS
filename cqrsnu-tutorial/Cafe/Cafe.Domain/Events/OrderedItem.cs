using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Domain.Events
{
    public class OrderedItem
    {
        public int MenuNumber;
        public string Description;
        public bool IsDrink;
        public decimal Price;
    }
}

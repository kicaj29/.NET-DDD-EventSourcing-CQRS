﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Domain.Events
{
    public class DrinksServed
    {
        public Guid Id;
        public List<int> MenuNumbers;
    }
}

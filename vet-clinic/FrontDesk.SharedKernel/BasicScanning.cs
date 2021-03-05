using FrontDesk.SharedKernel.Interfaces;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontDesk.SharedKernel
{
    public class BasicScanning: Registry
    {
        public BasicScanning()
        {
            Scan(_ =>
            {
                _.AssemblyContainingType(typeof(IHandle<>));
            });
        }
    }
}

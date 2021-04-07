using FrontDesk.SharedKernel.Interfaces;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FrontDesk.SharedKernel
{
    public class BasicScanning: Registry
    {
        public BasicScanning()
        {
            Scan(scan =>
            {
                scan.AssembliesFromApplicationBaseDirectory();
                scan.ConnectImplementationsToTypesClosing(typeof(IHandle<>));
            });
            
        }
    }
}

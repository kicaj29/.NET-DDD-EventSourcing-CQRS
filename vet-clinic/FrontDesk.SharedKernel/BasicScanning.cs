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
                //_.AssemblyContainingType(typeof(IHandle<>));
                //scan.Assembly("AppointmentScheduling.Core");
                //scan.AssembliesFromApplicationBaseDirectory();
                Debug.WriteLine("Current dir:" + AppDomain.CurrentDomain.BaseDirectory);

                scan.Assembly(Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + ".\\AppointmentScheduling.Core.dll"));

                scan.AddAllTypesOf(typeof(IHandle<>)).NameBy( x => x.FullName);
                scan.WithDefaultConventions();
            });
            
        }
    }
}

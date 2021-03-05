using AppointmentScheduling.Core.Model.ApplicationEvents;
using FrontDesk.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduling.Core.Services
{
    public class RelayAppointmentScheduledService: IHandle<AppointmentScheduledEvent>
    {
        public void Handle(AppointmentScheduledEvent appointmentScheduledEvent)
        {
            // TBD
        }
    }
}

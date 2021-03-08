using AppointmentScheduling.Core.Model.ApplicationEvents;
using FrontDesk.SharedKernel;
using FrontDesk.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduling.Core.Services
{
    public class EmailConfirmationHandler: IHandle<AppointmentConfirmedEvent>
    {
        public void Handle(AppointmentConfirmedEvent appointmentConfirmedEvent)
        {
            // TBD
        }
    }
}

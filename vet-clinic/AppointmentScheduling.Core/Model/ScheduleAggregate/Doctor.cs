using FrontDesk.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduling.Core.Model.ScheduleAggregate
{
    public class Doctor : Entity<int>
    {
        public virtual string Name { get; set; }

        public Doctor(int id)
            : base(id)
        {
        }

        private Doctor()
        {

        }
    }
}

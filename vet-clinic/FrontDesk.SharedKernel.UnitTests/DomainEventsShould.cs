using AppointmentScheduling.Core.Model.ApplicationEvents;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontDesk.SharedKernel.UnitTests
{
    [TestFixture]
    public class DomainEventsShould
    {
        /// <summary>
        /// Test for handling application event (not domain event).
        /// </summary>
        [Test]
        public void CallEmailConfirmationServiceForAppointmentConfirmedEvent()
        {
            // Arrange
            var e = new AppointmentConfirmedEvent();
            e.AppointmentId = Guid.NewGuid();
            // TODO: add mock for EmailConfirmationService


            // Act
            DomainEvents.Raise(e);

            // Assert
    }
    }
}

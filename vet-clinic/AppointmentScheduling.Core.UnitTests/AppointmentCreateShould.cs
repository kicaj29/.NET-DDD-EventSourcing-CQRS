using AppointmentScheduling.Core.Model.ScheduleAggregate;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduling.Core.UnitTests
{
    [TestFixture]
    public class AppointmentCreateShould
    {
        private int invalidId = 0;
        private int testPatientId = 123;
        private int testClientId = 456;
        private int testRoomId = 567;
        private int testAppointmentTypeId = 1;
        private int testDoctorId = 2;
        private Guid testScheduleId = Guid.Empty;
        private DateTime testStartTime = new DateTime(2014, 7, 1, 9, 0, 0);
        private DateTime testEndTime = new DateTime(2014, 7, 1, 9, 30, 0);
        private string testTitle = "(WE) Darwin - Steve Smith";

        [Test]
        public void ThrowExceptionGivenInvalidClientId()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Appointment.Create(testScheduleId, invalidId, testPatientId, testRoomId, testStartTime, testEndTime,
                testAppointmentTypeId, null, testTitle);
            });

            Assert.AreEqual(exception.ParamName, "clientId");
        }

        [Test]
        public void ThrowExceptionGivenInvalidPatientId()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Appointment.Create(testScheduleId, testClientId, invalidId, testRoomId, testStartTime, testEndTime,
                testAppointmentTypeId, null, testTitle));

            Assert.AreEqual(exception.ParamName, "patientId");
        }

        [Test]
        public void ThrowExceptionGivenInvalidRoomId()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Appointment.Create(testScheduleId, testClientId, testPatientId, invalidId, testStartTime, testEndTime,
                testAppointmentTypeId, null, testTitle));

            Assert.AreEqual(exception.ParamName, "roomId");
        }

        [Test]
        public void ThrowExceptionGivenInvalidAppointmentTypeId()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Appointment.Create(testScheduleId, testClientId, testPatientId, testRoomId, testStartTime, testEndTime,
                invalidId, null, testTitle));

            Assert.AreEqual(exception.ParamName, "appointmentTypeId");
        }

        [Test]
        public void ThrowExceptionGivenInvalidTitle()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Appointment.Create(testScheduleId, testClientId, testPatientId, testRoomId, testStartTime, testEndTime,
                testAppointmentTypeId, null, String.Empty));

            Assert.AreEqual(exception.ParamName, "title");
        }

        [Test]
        public void RelateToAClientAndPatient()
        {
            var appointment = Appointment.Create(testScheduleId, testClientId, testPatientId, testRoomId, testStartTime, testEndTime, testAppointmentTypeId, testDoctorId, testTitle);

            Assert.AreEqual(testPatientId, appointment.PatientId);
            Assert.AreEqual(testClientId, appointment.ClientId);
            Assert.AreEqual(testRoomId, appointment.RoomId);
            Assert.AreEqual(testAppointmentTypeId, appointment.AppointmentTypeId);
            Assert.AreEqual(testTitle, appointment.Title);
            Assert.AreEqual(testDoctorId, appointment.DoctorId);
        }

        [Test]
        public void SetDoctorIdToDefaultValueGivenNull()
        {
            var appointment = Appointment.Create(testScheduleId, testClientId, testPatientId, testRoomId, testStartTime, testEndTime, testAppointmentTypeId, null, testTitle);

            Assert.AreEqual(1, appointment.DoctorId);
        }

    }
}

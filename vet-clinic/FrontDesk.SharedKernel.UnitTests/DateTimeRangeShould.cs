using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontDesk.SharedKernel.UnitTests
{
    [TestFixture]
    class DateTimeRangeShould
    {
        private DateTime _testStartDate = new DateTime(2014, 1, 1, 9, 0, 0);

        [Test]
        public void ThrowArgumentExceptionIfStartDateEqualsEndDate()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { new DateTimeRange(_testStartDate, _testStartDate); });
        }
    }
}

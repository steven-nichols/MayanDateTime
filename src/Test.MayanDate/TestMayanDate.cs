using NUnit.Framework;
using MayanDate;
using System;

namespace Test.MayanDate
{
    [TestFixture]
    public class TestMayanDate
    {
        [Test]
        public void MayanDateTime_FirstDay_ZeroDaysSinceCreation()
        {
            var date = new MayanDateTime(0, 0, 0, 0, 0);

            Assert.AreEqual(0, date.DaysSinceCreation);
        }

        [Test]
        public void MayanDateTime_NegativeBaktun_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MayanDateTime(-1, 0, 0, 0, 0));
        }

        [Test]
        public void MayanDateTime_TooLargeBaktun_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MayanDateTime(21, 0, 0, 0, 0));
        }

        [Test]
        public void MayanDateTime_NegativeKaktun_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MayanDateTime(0, -1, 0, 0, 0));
        }

        [Test]
        public void MayanDateTime_TooLargeKaktun_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MayanDateTime(0, 21, 0, 0, 0));
        }

        [Test]
        public void MayanDateTime_NegativeTun_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MayanDateTime(0, 0, -1, 0, 0));
        }

        [Test]
        public void MayanDateTime_TooLargeTun_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MayanDateTime(0, 0, 21, 0, 0));
        }
        
        [Test]
        public void MayanDateTime_NegativeWinal_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MayanDateTime(0, 0, 0, -1, 0));
        }

        [Test]
        public void MayanDateTime_TooLargeWinal_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MayanDateTime(0, 0, 0, 18, 0));
        }

        [Test]
        public void MayanDateTime_NegativeKin_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MayanDateTime(0, 0, 0, 0, -1));
        }

        [Test]
        public void MayanDateTime_TooLargeKin_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MayanDateTime(0, 0, 0, 0, 21));
        }

        [Test]
        public void Parse_ValidLongDate_SetsDigits()
        {
            var date = MayanDateTime.Parse("1.2.3.4.5");

            Assert.AreEqual(1, date.Baktun);
            Assert.AreEqual(2, date.Katun);
            Assert.AreEqual(3, date.Tun);
            Assert.AreEqual(4, date.Winal);
            Assert.AreEqual(5, date.Kin);
        }

        [Test]
        public void Parse_ValidLongDatePlusTzolkinHaab_SetsDigits()
        {
            var date = MayanDateTime.Parse("1.2.3.4.5 4 Ajaw 8 Kumk'u");

            Assert.AreEqual(1, date.Baktun);
            Assert.AreEqual(2, date.Katun);
            Assert.AreEqual(3, date.Tun);
            Assert.AreEqual(4, date.Winal);
            Assert.AreEqual(5, date.Kin);
        }

        [TestCase("0.0.0.0.0", 4, MayanDateTime.TzolkinDayNames.Ajaw)]
        [TestCase("13.0.0.0.0", 4, MayanDateTime.TzolkinDayNames.Ajaw)]
        [TestCase("9.9.2.4.8", 5, MayanDateTime.TzolkinDayNames.Lamat)]
        [TestCase("9.12.2.0.16", 5, MayanDateTime.TzolkinDayNames.Kib)]
        [TestCase("10.2.9.1.9", 9, MayanDateTime.TzolkinDayNames.Muluk)]
        public void TestTzolkinDate(string longDate, int expectedNumeral, MayanDateTime.TzolkinDayNames expectedDay)
        {
            var date = MayanDateTime.Parse(longDate);

            Assert.AreEqual(expectedNumeral, date.TzolkinNumber, "Numeral doesn't match");
            Assert.AreEqual(expectedDay, date.TzolkinDayName, "Day doesn't match");
        }

        [TestCase("0.0.0.0.0", 8, MayanDateTime.HaabMonthNames.Kumku)]
        [TestCase("13.0.0.0.0", 3, MayanDateTime.HaabMonthNames.Kankin)]
        [TestCase("9.9.2.4.8", 1, MayanDateTime.HaabMonthNames.Mol)]
        [TestCase("9.12.2.0.16", 14, MayanDateTime.HaabMonthNames.Yaxkin)]
        [TestCase("10.2.9.1.9", 7, MayanDateTime.HaabMonthNames.Sak)]
        public void TestHaabDate(string longDate, int expectedDay, MayanDateTime.HaabMonthNames expectedMonth)
        {
            var date = MayanDateTime.Parse(longDate);

            Assert.AreEqual(expectedDay, date.HaabDay, "Day doesn't match");
            Assert.AreEqual(expectedMonth, date.HaabMonthName, "Month doesn't match");
        }


        [TestCase("9.9.2.4.8", "0615-07-27")]
        [TestCase("10.2.9.1.9", "0878-07-28")]
        [TestCase("11.16.0.0.0", "1539-11-12")]
        [TestCase("12.19.19.17.19", "2012-12-20")]
        public void ToGregorianDate(string longDate, string expectedGregorianDate)
        {
            var date = MayanDateTime.Parse(longDate).ToDateTime();
            var gregorianDate = string.Format("{0:0000}-{1:00}-{2:00}", date.Year, date.Month, date.Day);

            Assert.AreEqual(expectedGregorianDate, gregorianDate);
        }

        [TestCase("0001-01-01", "7.17.18.13.3")]
        [TestCase("0615-07-27", "9.9.2.4.8")]
        [TestCase("0878-07-28", "10.2.9.1.9")]
        [TestCase("1539-11-12", "11.16.0.0.0")]
        [TestCase("2012-12-20", "12.19.19.17.19")]
        public void FromGregorianDate(string gregorianDate, string expectedLongDate)
        {
            var date = new MayanDateTime(DateTime.Parse(gregorianDate));
            var longDate = string.Format("{0}.{1}.{2}.{3}.{4}", date.Baktun, date.Katun, date.Tun, date.Winal, date.Kin);

            Assert.AreEqual(longDate, expectedLongDate);
        }

    }
}

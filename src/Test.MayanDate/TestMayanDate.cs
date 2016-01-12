#region License
/**
 * Copyright (c) Steven Nichols
 * All rights reserved. 
 *
 * MIT License
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this
 * software and associated documentation files (the "Software"), to deal in the Software 
 * without restriction, including without limitation the rights to use, copy, modify, merge,
 * publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
 * to whom the Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or
 * substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
 * PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
 * FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
 * OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 * DEALINGS IN THE SOFTWARE.
 */
#endregion

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
        public void MayanDateTime_LargestDate_NoErrors()
        {
            var date = new MayanDateTime(19, 19, 19, 17, 19);
            
            Assert.AreEqual(19, date.Baktun);
            Assert.AreEqual(19, date.Katun);
            Assert.AreEqual(19, date.Tun);
            Assert.AreEqual(17, date.Winal);
            Assert.AreEqual(19, date.Kin);
        }

        [Test]
        public void AddDays_1Day_IncrementsDateBy1Day()
        {
            var originalDate = new MayanDateTime(13, 0, 0, 0, 0);
            var newDate = originalDate.AddDays(1);

            Assert.AreEqual(originalDate.DaysSinceCreation + 1, newDate.DaysSinceCreation);
        }

        [Test]
        public void AddDays_Negative1Day_DecrementsDateBy1Day()
        {
            var originalDate = new MayanDateTime(13, 0, 0, 0, 0);
            var newDate = originalDate.AddDays(-1);

            Assert.AreEqual(originalDate.DaysSinceCreation - 1, newDate.DaysSinceCreation);
        }

        [Test]
        public void MayanDateTime_AddDayToLargestDate_ThrowsOutOfRangeException()
        {
            var date = new MayanDateTime(19, 19, 19, 17, 19);

            Assert.Throws<ArgumentOutOfRangeException>(() => date.AddDays(1));
        }

        [Test]
        public void MayanDateTime_SubtractDayFromSmallestDate_ThrowsOutOfRangeException()
        {
            var date = new MayanDateTime(0, 0, 0, 0, 0);

            Assert.Throws<ArgumentOutOfRangeException>(() => date.AddDays(-1));
        }

        [Test]
        public void CompareTo_LaterDate_ReturnsLessThanZero()
        {
            var pakalsBirthday = new MayanDateTime(9, 8, 9, 13, 0);
            var pakalsDeath = new MayanDateTime(9, 12, 11, 5, 18);

            var result = pakalsBirthday.CompareTo(pakalsDeath);

            Assert.Less(result, 0);
        }

        [Test]
        public void CompareTo_EarlierDate_ReturnsGreaterThanZero()
        {
            var pakalsBirthday = new MayanDateTime(9, 8, 9, 13, 0);
            var pakalsDeath = new MayanDateTime(9, 12, 11, 5, 18);

            var result = pakalsDeath.CompareTo(pakalsBirthday);

            Assert.Greater(result, 0);
        }

        [Test]
        public void CompareTo_SameDate_ReturnsZero()
        {
            var date1 = new MayanDateTime(9, 8, 9, 13, 0);
            var date2 = new MayanDateTime(9, 8, 9, 13, 0);

            var result = date1.CompareTo(date2);

            Assert.AreEqual(0, result);
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
        [TestCase("9.8.9.13.0", 8, MayanDateTime.TzolkinDayNames.Ajaw)]
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
        [TestCase("9.8.9.13.0", 13, MayanDateTime.HaabMonthNames.Pop)]
        [TestCase("9.9.2.4.8", 1, MayanDateTime.HaabMonthNames.Mol)]
        [TestCase("9.12.2.0.16", 14, MayanDateTime.HaabMonthNames.Yaxkin)]
        [TestCase("10.2.9.1.9", 7, MayanDateTime.HaabMonthNames.Sak)]
        public void TestHaabDate(string longDate, int expectedDay, MayanDateTime.HaabMonthNames expectedMonth)
        {
            var date = MayanDateTime.Parse(longDate);

            Assert.AreEqual(expectedDay, date.HaabDay, "Day doesn't match");
            Assert.AreEqual(expectedMonth, date.HaabMonthName, "Month doesn't match");
        }

        [TestCase("9.8.9.13.0", "0603-03-24")]
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
        [TestCase("0603-03-24", "9.8.9.13.0")] 
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

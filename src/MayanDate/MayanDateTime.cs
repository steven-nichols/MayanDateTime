using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MayanDate
{
    /// <summary>
    /// Used to compute dates in the ancient Mayan calendar system. Dates can be tracked in the 5 digit
    /// Long Count system, the 260-day Tzolk'in cycle and/or the 365-day Haab' cycle.
    /// </summary>
    public class MayanDateTime
    {
        #region Static Constants

        // Long count constants
        public static int DAYS_IN_BAKTUN = 144000;
        public static int DAYS_IN_KATUN = 7200;
        public static int DAYS_IN_TUN = 360;
        public static int DAYS_IN_WINAL = 20;

        // Tzolk'in constants
        public static int NUMERALS_IN_TZOLKIN_CYCLE = 13;
        public static int DAYS_IN_TZOLKIN_CYCLE = 20;

        // Haab' constants
        public static int DAYS_IN_HAAB_YEAR = 365;
        public static int DAYS_IN_HAAB_MONTH = 20;

        /// <summary>
        /// The reference date used to calibrate the Mayan long count date to the Gregorian calendar.
        /// The standard correlation (GMT) assumes that 11.16.0.0.0 corresponds to Nov 12, 1539.
        /// </summary>
        public static class MayanGregorianCalibrationDate
        {
            public static DateTime Gregorian = new DateTime(1539, 11, 12);
            public static MayanDateTime Mayan = new MayanDateTime(11, 16, 0, 0, 0);
        }

        /// <summary>
        /// Represents the current date, expressed as a MayanDateTime instance.
        /// </summary>
        public static MayanDateTime Now = new MayanDateTime(DateTime.Now);

        #endregion

        #region Enums

        /// <summary>
        /// The twenty named days of the Tzolk'in cycle.
        /// </summary>
        public enum TzolkinDayNames
        {
            /// <summary>
            /// Imix' is the 1st named day.
            /// </summary>
            [Display(Name = "Imix'")]
            Imix = 1,

            /// <summary>
            /// Ik' is the 2nd named day.
            /// </summary>
            [Display(Name = "Ik")]
            Ik,

            /// <summary>
            /// Ak'b'al is the 3rd named day.
            /// </summary>
            [Display(Name = "Ak'b'al")]
            Akbal,

            /// <summary>
            /// K'an is the 4th named day.
            /// </summary>
            [Display(Name = "K'an")]
            Kan,

            /// <summary>
            /// Chikchan is the 5th named day.
            /// </summary>
            [Display(Name = "Chikchan")]
            Chikchan,

            /// <summary>
            /// Kimi is the 6th named day.
            /// </summary>
            [Display(Name = "Kimi")]
            Kimi,

            /// <summary>
            /// Manik' is the 7th named day.
            /// </summary>
            [Display(Name = "Manik'")]
            Manik,

            /// <summary>
            /// Lamat is the 8th named day.
            /// </summary>
            [Display(Name = "Lamat")]
            Lamat,

            /// <summary>
            /// Muluk is the 9th named day.
            /// </summary>
            [Display(Name = "Muluk")]
            Muluk,

            /// <summary>
            /// Ok is the 10th named day.
            /// </summary>
            [Display(Name = "Ok")]
            Ok,

            /// <summary>
            /// Chuwen is the 11th named day.
            /// </summary>
            [Display(Name = "Chuwen")]
            Chuwen,

            /// <summary>
            /// Eb' is the 12th named day.
            /// </summary>
            [Display(Name = "Eb'")]
            Eb,

            /// <summary>
            /// B'en is the 13th named day.
            /// </summary>
            [Display(Name = "B'en")]
            Ben,

            /// <summary>
            /// Ix is the 14th named day.
            /// </summary>
            [Display(Name = "Ix")]
            Ix,

            /// <summary>
            /// Men is the 15th named day.
            /// </summary>
            [Display(Name = "Men")]
            Men,

            /// <summary>
            /// Kib' is the 16th named day.
            /// </summary>
            [Display(Name = "Kib'")]
            Kib,

            /// <summary>
            /// Kab'an is the 17th named day.
            /// </summary>
            [Display(Name = "Kab'an")]
            Kaban,

            /// <summary>
            /// Etz'nab' is the 18th named day.
            /// </summary>
            [Display(Name = "Etz'nab")]
            Etznab,

            /// <summary>
            /// Kawak is the 19th named day.
            /// </summary>
            [Display(Name = "Kawak")]
            Kawak,

            /// <summary>
            /// Ajaw is the 20th named day.
            /// </summary>
            [Display(Name = "Ajaw")]
            Ajaw
        };

        /// <summary>
        /// The 18 months of the Habb' cycle.
        /// </summary>
        public enum HaabMonthNames
        {
            /// <summary>
            /// Pop is the 1st month
            /// </summary>
            [Display(Name = "Pop")]
            Pop = 1,

            /// <summary>
            /// Wo' is the 2nd month
            /// </summary>
            [Display(Name = "Wo'")]
            Wo,

            /// <summary>
            /// Sip is the 3rd month
            /// </summary>
            [Display(Name = "Sip")]
            Sip,

            /// <summary>
            /// Sotz' is the 4th month
            /// </summary>
            [Display(Name = "Sotz'")]
            Sotz,

            /// <summary>
            /// Sek is the 5th month
            /// </summary>
            [Display(Name = "Sek")]
            Sek,

            /// <summary>
            /// Xul is the 6th month
            /// </summary>
            [Display(Name = "Xul")]
            Xul,

            /// <summary>
            /// Yaxk'in' is the 7th month
            /// </summary>
            [Display(Name = "Yaxk'in'")]
            Yaxkin,

            /// <summary>
            /// Mol is the 8th month
            /// </summary>
            [Display(Name = "Mol")]
            Mol,

            /// <summary>
            /// Ch'en is the 9th month
            /// </summary>
            [Display(Name = "Ch'en")]
            Chen,

            /// <summary>
            /// Yax is the 10th month
            /// </summary>
            [Display(Name = "Yax")]
            Yax,

            /// <summary>
            /// Sak' is the 11th month
            /// </summary>
            [Display(Name = "Sak'")]
            Sak,

            /// <summary>
            /// Keh is the 12th month
            /// </summary>
            [Display(Name = "Keh")]
            Keh,

            /// <summary>
            /// Mak is the 13th month
            /// </summary>
            [Display(Name = "Mak")]
            Mak,

            /// <summary>
            /// K'ank'in' is the 14th month
            /// </summary>
            [Display(Name = "K'ank'in'")]
            Kankin,

            /// <summary>
            /// Muwan' is the 15th month
            /// </summary>
            [Display(Name = "Muwan'")]
            Muwan,

            /// <summary>
            /// Pax is the 16th month
            /// </summary>
            [Display(Name = "Pax")]
            Pax,

            /// <summary>
            /// K'ayab is the 17th month
            /// </summary>
            [Display(Name = "K'ayab")]
            Kayab,

            /// <summary>
            /// Kumk'u is the 18th month
            /// </summary>
            [Display(Name = "Kumk'u")]
            Kumku,

            /// <summary>
            /// Wayeb' is the period of five nameless days at the end of the year.
            /// </summary>
            [Display(Name = "Wayeb'")]
            Wayeb
        };

        #endregion

        #region Public Properties
        
        /// <summary>
        /// The b'ak'tun is the first digit of the long count date.
        /// Range 1 to 20.
        /// </summary>
        public int Baktun { get; protected set; }

        /// <summary>
        /// The k'atun is the second digit of the long count date.
        /// Range 1 to 20.
        /// </summary>
        public int Katun { get; protected set; }

        /// <summary>
        /// The tun is the third digit of the long count date.
        /// Range 1 to 20.
        /// </summary>
        public int Tun { get; protected set; }

        /// <summary>
        /// The winal is the fourth digit of the long count date.
        /// Range 1 to 18.
        /// </summary>
        public int Winal { get; protected set; }

        /// <summary>
        /// The k'in is the fifth digit of the long count date.
        /// </summary>
        public int Kin { get; protected set; }


        /// <summary>
        /// The numerical portion of Tzolk'in date. 
        /// Range 1 to 13.
        /// </summary>
        public int TzolkinNumber { get; protected set; }

        /// <summary>
        /// The sequence number of the Tzolk'in day. 
        /// Range 1 to 20.
        /// </summary>
        public int TzolkinDay { get; protected set; }

        /// <summary>
        /// Returns the TzolkinDayName enum value corresponding to the Tzolk'in Day.
        /// </summary>
        public TzolkinDayNames TzolkinDayName
        {
            get
            {
                return (TzolkinDayNames)TzolkinDay;
            }
        }


        /// <summary>
        /// The sequence number of the Haab' day.
        /// Range 1 to 20.
        /// </summary>
        public int HaabDay { get; protected set; }

        /// <summary>
        /// The sequence number of the Haab' month.
        /// Range 1 to 19 (18 months + the 5 "nameless" Wayeb' days).
        /// </summary>
        public int HaabMonth { get; protected set; }

        /// <summary>
        /// The HaabMonthNames enum value corresponding to the Haab' month.
        /// </summary>
        public HaabMonthNames HaabMonthName
        {
            get
            {
                return (HaabMonthNames)HaabMonth;
            }
        }

        /// <summary>
        /// Returns the number of days since 0.0.0.0.0, 4 Ajaw 8 Kumk'u (August 11, 3114 BCE).
        /// </summary>
        public int DaysSinceCreation { get; protected set; }

        #endregion

        /// <summary>
        /// Converts the string representation of a date and time to its MayanDateTime equivalent.
        /// </summary>
        /// <param name="s">A string that contains a date to convert.</param>
        /// <returns></returns>
        public static MayanDateTime Parse(string s)
        {
            var longCountRegex = new Regex(@"(\d{1,2})\.(\d{1,2})\.(\d{1,2})\.(\d{1,2})\.(\d{1,2})");
            var match = longCountRegex.Match(s);

            return new MayanDateTime(int.Parse(match.Groups[1].Value),
                                     int.Parse(match.Groups[2].Value),
                                     int.Parse(match.Groups[3].Value),
                                     int.Parse(match.Groups[4].Value),
                                     int.Parse(match.Groups[5].Value));
        }

        /// <summary>
        /// Initializes a new instance of the MayanDateTime structure to the 
        /// specified Mayan Long Count date.
        /// </summary>
        /// <param name="baktun">The B'ak'tun (0 through 19)</param>
        /// <param name="katun">The K'atun (0 through 19)</param>
        /// <param name="tun">The Tun (0 through 19)</param>
        /// <param name="winal">The Winal (0 through 17)</param>
        /// <param name="kin">The Kin (0 through 19)</param>
        public MayanDateTime(int baktun, int katun, int tun, int winal, int kin)
        {
            Init(baktun, katun, tun, winal, kin);
        }

        /// <summary>
        /// Initializes a new instance of the MayanDateTime structure to the 
        /// specified DateTime.
        /// </summary>
        public MayanDateTime(DateTime dateTime)
        {
            var diff = dateTime - MayanGregorianCalibrationDate.Gregorian;
            var remainingDays = (int)diff.TotalDays + MayanGregorianCalibrationDate.Mayan.DaysSinceCreation;

            var baktun = (remainingDays / DAYS_IN_BAKTUN);
            remainingDays %= DAYS_IN_BAKTUN;

            var katun = (remainingDays / DAYS_IN_KATUN);
            remainingDays %= DAYS_IN_KATUN;

            var tun = (remainingDays / DAYS_IN_TUN);
            remainingDays %= DAYS_IN_TUN;

            var winal = (remainingDays / DAYS_IN_WINAL);
            remainingDays %= DAYS_IN_WINAL;

            var kin = remainingDays;

            Init(baktun, katun, tun, winal, kin);
        }

        /// <summary>
        /// Initializes the public properties.
        /// </summary>
        private void Init(int baktun, int katun, int tun, int winal, int kin)
        {
            if (baktun < 0 || baktun > 19)
            {
                throw new ArgumentOutOfRangeException(nameof(baktun));
            }
            if (katun < 0 || katun > 19)
            {
                throw new ArgumentOutOfRangeException(nameof(katun));
            }
            if (tun < 0 || tun > 19)
            {
                throw new ArgumentOutOfRangeException(nameof(tun));
            }
            if (winal < 0 || winal > 17)
            {
                throw new ArgumentOutOfRangeException(nameof(winal));
            }
            if (kin < 0 || kin > 19)
            {
                throw new ArgumentOutOfRangeException(nameof(kin));
            }

            Baktun = baktun;
            Katun = katun;
            Tun = tun;
            Winal = winal;
            Kin = kin;

            DaysSinceCreation = Baktun * DAYS_IN_BAKTUN + Katun * DAYS_IN_KATUN + Tun * DAYS_IN_TUN + Winal * DAYS_IN_WINAL + Kin;

            CalculateTzolkinDate(DaysSinceCreation);
            CalculateHaabDate(DaysSinceCreation);
        }

        /// <summary>
        /// Computes the Tzolk'in date which is the combination of one of twenty named 
        /// days and one of 13 numbers which produce 260 (20 x 13) unique days.
        /// </summary>
        protected void CalculateTzolkinDate(int daysSinceCreation)
        {
            //***
            // Tzolk'in date is counted forward from the day of creation which was 4 Ajaw.
            //***
            TzolkinNumber = (4 + daysSinceCreation) % NUMERALS_IN_TZOLKIN_CYCLE;
            
            //***
            // The calculation is performed using modulo arithmetic (0-19) which is then
            // incremented by 1 to change the range to 1-20. Counting starts from 4 Ajaw
            // which is the last named day of the cycle.
            //***
            TzolkinDay = ((19 + (daysSinceCreation % DAYS_IN_TZOLKIN_CYCLE)) % DAYS_IN_TZOLKIN_CYCLE) + 1;
        }

        /// <summary>
        /// Computes the Haab' date which consists of 18 months of 20 days each plus
        /// a period of 5 "nameless" days to produce a 365 day long year.
        /// </summary>
        /// <param name="daysSinceCreation">the number of days since 0.0.0.0.0 4 Ajaw 8 Kumk'u</param>
        protected void CalculateHaabDate(int daysSinceCreation)
        {
            //***
            // Haab' starts from 8 Kumk'u which is the 9th day of the 18th month. 
            // There are 17 days to the start of the next year.
            //***
            var daySinceNewYear = (DaysSinceCreation + DAYS_IN_HAAB_YEAR - 17) % DAYS_IN_HAAB_YEAR;
            HaabDay = (daySinceNewYear % DAYS_IN_HAAB_MONTH);
            HaabMonth = (daySinceNewYear / DAYS_IN_HAAB_MONTH) + 1;
        }

        /// <summary>
        /// Converts this MayanDateTime to a DateTime object. DateTime doesn't support dates before 1 AD
        /// so it will throw an ArgumentOutOfRangeException for dates before 7.17.18.13.3.
        /// </summary>
        public DateTime ToDateTime()
        {
            var daysSinceJan1st1CE = this.DaysSinceCreation - MayanGregorianCalibrationDate.Mayan.DaysSinceCreation;
            return MayanGregorianCalibrationDate.Gregorian.AddDays(daysSinceJan1st1CE);
        }

        /// <summary>
        /// Returns a string representation of the long count, Tzolk'in and Haab' values.
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0}.{1}.{2}.{3}.{4}, {5} {6} {7} {8}",
                Baktun, Katun, Tun, Winal, Kin,
                TzolkinNumber, ((TzolkinDayNames)TzolkinDay).GetAttribute<DisplayAttribute>().Name,
                HaabDay, ((HaabMonthNames)HaabMonth).GetAttribute<DisplayAttribute>().Name);
        }
    }
}
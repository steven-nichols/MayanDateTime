# MayanDateTime
MayanDateTime is a C# library for working with dates in the Maya calendar system.

## The Maya calendar
The Maya calendar system consists of two different calendars, the Calendar Round and the Long Count. The Calendar Round is a combination of a 260-day count called the Tzolk'in and a 365-day approximate solar year called the Haab'  which together uniquely identify dates during a 52-year cycle. The Long Count tracks longer periods of time by counting the number of days since the mythological creation of the world using a five digit, base-20 system.

The ancient Mayan inscriptions recorded dates using both the 5-digit Long Count and the two character tzolk'in followed by the two character haab'. For example, temple inscriptions at Palenque record the date of Pakal's accession to the throne as 9.8.9.13.0, 8 Ajaw 13 Pop (March 24, 603 CE in the Gregorian calendar).

## Correlation between Mayan and Western calendars
Mayan dates are converted to the proleptic Gregorian calendar using the standard Goodman-Martinez-Thompson (GMT) correlation, which is accepted by the great majority of Maya researchers.

## Using MayanDateTime
A MayanDateTime object can be initialized from a 5-digit Long Count. The Tzolk'in and Haab' values from the Calendar Round are not required, and in fact, can be calculated solely from the long count:

```c#
var pakalsBirthday = new MayanDateTime(9, 8, 9, 13, 0);
Console.WriteLine(pakalsBirthday);
// Displays "9.8.9.13.0, 8 Ajaw 13 Pop"

var calendarRound = string.Format(
      "{0} {1}, {2} {3}",
      pakalsBirthday.TzolkinNumber,
      pakalsBirthday.TzolkinDayName,
      pakalsBirthday.HaabDay,
      pakalsBirthday.HaabMonthName);
Console.WriteLine(calendarRound);
// Displays "8 Ajaw, 13 Pop"

```

MayanDateTime objects can be converted to their DateTime equivalents:

```c#
var pakalsBirthday = MayanDateTime.Parse("9.8.9.13.0");
Console.WriteLine(pakalsBirthday.ToDateTime());
// Displays "3/24/0603 12:00:00 AM"
```

and vice versa:

```c#
var pakalsDeath = new MayanDateTime(new DateTime("08/29/683"));
Console.WriteLine(pakalsDeath);
// Displays "9.12.11.5.18, 6 Etz'nab 11 Yax"

```

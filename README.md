# ![](logo.png) MayanDateTime
MayanDateTime is a C# library for working with dates in the Maya calendar system.

## The Maya calendar
The Maya calendar system consists of two different calendars, the Calendar Round and the Long Count. The Calendar Round is a combination of a 260-day count called the Tzolk'in and a 365-day approximate solar year called the Haab'  which together uniquely identify dates during a 52-year cycle. The Long Count tracks longer periods of time by counting the number of days since the mythological creation of the world using a five digit, modified base-20 system.

The ancient Mayan inscriptions recorded dates using both the 5-digit Long Count and the two character tzolk'in followed by the two character haab'. For example, temple inscriptions at Palenque record the date of Pakal's accession to the throne as 9.8.9.13.0, 8 Ajaw 13 Pop (March 24, 603 CE in the Gregorian calendar).

## Installing
Prebuilt DLL binaries can be found on the [releases](https://github.com/steven-nichols/MayanDateTime/releases) page. You can also use [NuGet](https://www.nuget.org) to install the latest version:
```
Install-Package MayanDateTime
```

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

### Custom Date Format Strings
The display of the Maya date can be completely customzied by passing a format string to the 
MayanDateTime.ToString() method. The below example shows how to display only the Tzolk'in
day number and day name for a particular date:

```c#
var pakalsDeath = new MayanDateTime(9, 12, 11, 5, 18);
Console.WriteLine(pakalsDeath.ToString("%t %D"));
// Displays "6 Etz'nab"
```

Specifiers in the format string are replaced with their computed value. Each specifier begins with the percent (`%`) symbol. The following table lists the valid date format specifiers.

| Specifier | Description                                   | Example                           |
|-----------|-----------------------------------------------|-----------------------------------|
| `%L`      | Standard long count format                    | "9.12.11.5.18, 6 Etz'nab 11 Yax" -> `9.12.11.5.18` |
| `%c`      | Total day count since creation   | MayanDateTime(0, 0, 0, 1, 5) -> `25` (1 * 20 + 5 = 25)|
| `%i`      | First (smallest) digit of the long count (k'in)| "9.12.11.5.18, 6 Etz'nab 11 Yax" -> `18` |
| `%w`      | Second digit of the long count (winal)        | "9.12.11.5.18, 6 Etz'nab 11 Yax" -> `5` |
| `%u`      | Third digit of the long count (tun)           | "9.12.11.5.18, 6 Etz'nab 11 Yax" -> `11` |
| `%k`      | Fourth digit of the long count (k'atun)       | "9.12.11.5.18, 6 Etz'nab 11 Yax" -> `12` |
| `%b`      | Fifth (largest) digit of the long count (b'ak'tun)| "9.12.11.5.18, 6 Etz'nab 11 Yax" -> `9` |
| `%t`      | Tzolkin digit                                 | "9.12.11.5.18, 6 Etz'nab 11 Yax" -> `6`|
| `%T`      | Tzolkin day number                            | "9.12.11.5.18, 6 Etz'nab 11 Yax" -> `18` (Etz'nab is the 18th named day)|
| `%D`      | Tzolkin day name                              | "9.12.11.5.18, 6 Etz'nab 11 Yax" -> `Etz'nab` |
| `%h`      | Haab digit                                    | "9.12.11.5.18, 6 Etz'nab 11 Yax" -> `11`|
| `%H`      | Haab month number                             | "9.12.11.5.18, 6 Etz'nab 11 Yax" -> `10` (Yax is the 10th month)|
| `%M`      | Haab month name                               | "9.12.11.5.18, 6 Etz'nab 11 Yax" -> `Yax` |
| `%%`      | Escape character                              | "%% %%b" => `% %b` |

### Date Range
MayanDateTime supports dates between `0.0.0.0.0` (August 11, 3114 BCE) and `19.19.19.17.19` (October 12, 4772 CE). However, the C# DateTime class cannot support dates before 1 CE. As a result,  `.ToDateTime()` will throw an ArgumentOutOfRangeException for dates before `7.17.18.13.3`.

### Note on Epoch boundary
For historical reasons, the Maya recorded the mythological date of creation as `13.0.0.0.0, 4 Awaj 8 Kumk'u`. This date marks the beginning of the new Epoch and was of such importance to the Maya that they dropped out of their usual base-20 system and restarted the Ba'kun (the first digit of the Long Count) at 13 instead of the usual 19.

Unfortunately this creates an ambiguity since the date `13.0.0.0.0` also corresponds to December 21, 2012, an arguably much more common date for this library. As a compromise, the mythological date of creation is re-interpreted as `0.0.0.0.0`.

| MayanDateTime | Traditional Mayan date          | Gregorian equivalent |
| ------------- | ------------------------------- | -------------------- |
| `0.0.0.0.0`   | `13.0.0.0.0, 4 Awaj 8 Kumk'u`   | August 11, 3114 BCE  |
| `13.0.0.0.0`  | `13.0.0.0.0, 4 Awaj 3 K'ank'in` | December 21, 2012 CE |


### Correlation between Mayan and Western calendars
Mayan dates are converted to the [proleptic Gregorian calendar](https://en.wikipedia.org/wiki/Proleptic_Gregorian_calendar) using the standard Goodman-Martinez-Thompson (GMT) correlation, which is accepted by the great majority of Maya researchers. GMT identifies the long date `11.16.0.0.0` with November 12, 1539 CE (Gregorian), which then puts the mythological date of creation at August 11, 3114 BCE.

## License
[MIT License](LICENSE)

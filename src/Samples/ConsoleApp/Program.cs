using System;
using MayanDate;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Enter a date (Mayan or Gregorian): ");
                var input = Console.ReadLine();
                Console.WriteLine();

                DateTime gregorian;
                MayanDateTime mayan;

                var success = DateTime.TryParse(input, out gregorian);
                if (success)
                {
                    mayan = new MayanDateTime(gregorian);
                    Console.WriteLine(mayan);
                }
                else
                {
                    mayan = MayanDateTime.Parse(input);
                    Console.WriteLine(mayan);
                    Console.WriteLine("Day count: {0}", mayan.DaysSinceCreation);

                    try
                    {
                        Console.WriteLine(mayan.ToDateTime().ToLongDateString());
                    }
                    catch
                    {
                        // Can't be converted to DateTime. Just ignore.
                    }
                }

                Console.WriteLine();
            }
        }
    }
}

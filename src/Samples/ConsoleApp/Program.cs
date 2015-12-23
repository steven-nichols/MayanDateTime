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
                Console.Write("Enter a date: ");
                var input = Console.ReadLine();

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
                    Console.WriteLine(mayan.ToDateTime());
                }
            }
        }
    }
}

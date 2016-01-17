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

using System;
using MayanDate;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime gregorian;
            MayanDateTime mayan;

            while (true)
            {
                Console.Write("Enter a date (Mayan or Gregorian): ");
                var input = Console.ReadLine();
                Console.WriteLine();

                if (DateTime.TryParse(input, out gregorian))
                {
                    mayan = new MayanDateTime(gregorian);
                    Console.WriteLine(mayan);
                }
                else if (MayanDateTime.TryParse(input, out mayan))
                {
                    Console.WriteLine(mayan);
                    try
                    {
                        Console.WriteLine(mayan.ToDateTime().ToLongDateString());
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("Built-in DateTime doesn't support dates before 1 AD.");
                    }
                }
                else
                {
                    Console.WriteLine("Unknown input.");
                    Console.WriteLine("Try a Mayan date like \"9.8.9.13.0\" or a Gregorian date like \"8/29/683\"");
                }

                Console.WriteLine();
            }
        }
    }
}

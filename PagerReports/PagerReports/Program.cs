using System;
using System.Collections.Generic;
using System.IO;

namespace PagerReports
{
    class Program
    {
        static void Main(string[] args)
        {

            string yearOfReport;
            string monthOfReport;
            string dayOfReport;
            string pagerNumber;
            string pathToLog;
            string pathToSave;
            string[] lines;
            List<string> foundLines = new List<string>();
            bool exitState = false;
            string exitInput;

            while (exitState == false)
            {

                Console.WriteLine("**Note that the report with be for the day prior to the entered date. Enter date with no leading zeros.**");
                Console.Write("Please enter month: ");
                monthOfReport = Console.ReadLine();
                Console.Write("Please enter day: ");
                dayOfReport = Console.ReadLine();
                Console.Write("Please enter year: ");
                yearOfReport = Console.ReadLine();
                Console.Write("Please enter pager number: ");
                pagerNumber = Console.ReadLine();

                pathToLog = @"\\pagegate2\PageGateData\Logs\" + monthOfReport + "-" + dayOfReport + "-" + yearOfReport + @"\PageGate.log";
                Console.WriteLine("Filepath: " + pathToLog);
                int dayBack = Convert.ToInt32(dayOfReport) - 1;
                pathToSave = @"\\fileserver001\Shared\Depts\Information Services\Dept Private\Network\Pager_Reports\Pager_" + pagerNumber + "_" + monthOfReport + "-" + dayBack + "-" + yearOfReport + ".txt";
                Console.WriteLine("Saving to: " + pathToSave);

                if (File.Exists(pathToLog))
                { 

                    lines = File.ReadAllLines(pathToLog);

                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].Contains("Recipient: " + pagerNumber) || lines[i].Contains("To:" + pagerNumber))
                        {
                            foundLines.Add(lines[i]);
                        }
                    }

                    Console.WriteLine("Line matches for pager " + pagerNumber + ": " + foundLines.Count);
                    File.WriteAllLines(pathToSave, foundLines);
                    Console.WriteLine("Log file written to " + pathToSave);
                }
                else
                {
                    Console.WriteLine("\n!!File does not exist or information was entered incorrectly!!");
                }

                Console.Write("\nRun additional reports? (y/n): ");
                exitInput = Console.ReadLine();

                if (exitInput == "y" || exitInput == "Y" || exitInput == "yes" || exitInput == "Yes")
                {
                    exitState = false;
                }
                else
                {
                    exitState = true;
                }
            }
        }
    }
}

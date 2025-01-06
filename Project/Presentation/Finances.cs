using System;
using System.ComponentModel.Design;
using System.Globalization;
//finance@test.nl
//TestFinance1
static class Finances
{
    public static FinanceLogic financeLogic = new();
    // total profit made (day/week/month/year/total)
    // total reservations made (day/week/month/year/total)
    public static void Finance()
    {

        string[] optionsFinance = {
            "See Profit Information",
            "See Reservation Information",
            "Quit"
        };
        bool finance = true;
        while (finance)
        {
            int selectedIndexFinance = 0;
            Console.Clear();
            selectedIndexFinance = HelperPresentation.ChooseOption("What would you like to see", optionsFinance, selectedIndexFinance);

            switch (selectedIndexFinance)
            {
                // profit
                case 0:
                    {
                        bool timeFrame1 = true;
                        while (timeFrame1)
                        {
                            System.Console.WriteLine("In what timeframe would you like to see the profits made? (day/week/month/year/total/(Q))");

                            string choice2 = Console.ReadLine().ToLower();
                            if (choice2 == "day")
                            {
                                System.Console.WriteLine("enter date: (mm/dd/yyyy)");
                                DateTime date = DisplayCalendarFinance();

                                System.Console.WriteLine($"Total profits made on {date.ToShortDateString()}: {financeLogic.ProfitsDay(date)}");
                                System.Console.WriteLine("[enter]");
                                Console.ReadLine();
                                timeFrame1 = false;
                            }
                            else if (choice2 == "week")
                            {
                                System.Console.WriteLine("What is the date of the week you want to calculate? (mm/dd/yyyy)");
                                DateTime date = DisplayCalendarFinance();

                                DateTime startOfWeek = date.AddDays(-(int)date.DayOfWeek + (int)DayOfWeek.Monday);

                                // Calculate end of the week (Sunday)
                                DateTime endOfWeek = startOfWeek.AddDays(6);
                                System.Console.WriteLine($"Total profits from {startOfWeek.ToShortDateString()} to {endOfWeek.ToShortDateString()}: {financeLogic.ProfitsWeek(startOfWeek, endOfWeek)}");
                                System.Console.WriteLine("[enter]");
                                Console.ReadLine();
                                timeFrame1 = false;
                            }
                            else if (choice2 == "month")
                            {
                                System.Console.WriteLine("enter month: (e.g 1)");
                                string Month = Console.ReadLine();
                                System.Console.WriteLine("enter year: ");
                                string Year = Console.ReadLine();
                                string date = $"{Month}/01/{Year}";

                                if (DateTime.TryParse(date, out DateTime real_date))
                                {
                                    System.Console.WriteLine($"Total profits made in the {real_date.Date.Month} Month of {real_date.Date.Year}: {financeLogic.ProfitsMonth(real_date)}");
                                    System.Console.WriteLine("[enter]");
                                    Console.ReadLine();
                                    timeFrame1 = false;
                                }
                                else
                                {
                                    System.Console.WriteLine("Invalid input enter valid date");
                                    System.Console.WriteLine("[enter]");
                                    Console.ReadLine();
                                }
                            }

                            else if (choice2 == "year")
                            {

                                System.Console.WriteLine("enter Year: ");
                                string Year = Console.ReadLine();
                                string date = $"01/01/{Year}";
                                if (DateTime.TryParse(date, out DateTime real_date))
                                {
                                    System.Console.WriteLine($"Total profits made in the year {real_date.Date.Year}: {financeLogic.ProfitsYear(real_date)}");
                                    System.Console.WriteLine("[enter]");
                                    Console.ReadLine();
                                    timeFrame1 = false;
                                }
                                else
                                {
                                    System.Console.WriteLine("Invalid input enter valid date");
                                    System.Console.WriteLine("[enter]");
                                    Console.ReadLine();
                                }
                            }
                            else if (choice2 == "total")
                            {
                                System.Console.WriteLine($"Total profits made: {financeLogic.TotalProfits()}");
                                System.Console.WriteLine("[enter]");
                                System.Console.ReadLine();
                                timeFrame1 = false;
                            }

                            else if (choice2 == "q")
                            {
                                timeFrame1 = false;
                            }
                            else
                            {
                                System.Console.WriteLine("Invalid input enter (day/week/month/year/total/ Q )");
                                System.Console.WriteLine("[enter]");
                                Console.ReadLine();
                            }
                        }
                    }
                    break;
                // reservation
                case 1:
                    {
                        bool timeFrame2 = true;
                        while (timeFrame2)
                        {
                            System.Console.WriteLine("In what timeframe would you like to see the reservations made? (day/week/month/year/total/ Q )");
                            string choice2 = Console.ReadLine().ToLower();
                            if (choice2 == "day")
                            {
                                System.Console.WriteLine("enter date: (mm/dd/yyyy)");
                                DateTime date = DisplayCalendarFinance();

                                System.Console.WriteLine($"Total reservations made on {date.ToShortDateString()}: {financeLogic.TotalReservationsDay(date)}");
                                System.Console.WriteLine("[enter]");
                                Console.ReadLine();
                                timeFrame2 = false;

                            }
                            else if (choice2 == "week")
                            {
                                System.Console.WriteLine("What is the date of the week you want to calculate? (mm/dd/yyyy)");
                                DateTime date = DisplayCalendarFinance();


                                DateTime startOfWeek = date.AddDays(-(int)date.DayOfWeek + (int)DayOfWeek.Monday);

                                // Calculate end of the week (Sunday)
                                DateTime endOfWeek = startOfWeek.AddDays(6);
                                System.Console.WriteLine($"Total reservations from {startOfWeek.ToShortDateString()} to {endOfWeek.ToShortDateString()}: {financeLogic.ReservationsWeek(startOfWeek, endOfWeek)}");
                                System.Console.WriteLine("[enter]");
                                Console.ReadLine();
                                timeFrame2 = false;

                            }
                            else if (choice2 == "month")
                            {
                                System.Console.WriteLine("enter month: (e.g 1)");
                                string Month = Console.ReadLine();
                                System.Console.WriteLine("enter year: ");
                                string Year = Console.ReadLine();
                                string date = $"{Month}/01/{Year}";
                                if (DateTime.TryParse(date, out DateTime real_date))
                                {
                                    System.Console.WriteLine($"Total reservations made in the {real_date.Date.Month} Month of {real_date.Date.Year}: {financeLogic.TotalReservationsMonth(real_date)}");
                                    System.Console.WriteLine("[enter]");
                                    Console.ReadLine();
                                    timeFrame2 = false;
                                }
                                else
                                {
                                    System.Console.WriteLine("Invalid input enter valid date");
                                    System.Console.WriteLine("[enter]");
                                    Console.ReadLine();
                                }
                            }

                            else if (choice2 == "year")
                            {

                                System.Console.WriteLine("enter Year: ");
                                string Year = Console.ReadLine();
                                string date = $"01/01/{Year}";
                                if (DateTime.TryParse(date, out DateTime real_date))
                                {
                                    System.Console.WriteLine($"Total reservations made in the year {real_date.Date.Year}: {financeLogic.TotalReservationsYear(real_date)}");
                                    System.Console.WriteLine("[enter]");
                                    Console.ReadLine();
                                    timeFrame2 = false;
                                }
                                else
                                {
                                    System.Console.WriteLine("Invalid input enter valid date");
                                    System.Console.WriteLine("[enter]");
                                    Console.ReadLine();
                                }
                            }
                            else if (choice2 == "total")
                            {
                                System.Console.WriteLine($"Total reservations made: {financeLogic.TotalReservations()}");
                                System.Console.WriteLine("[enter]");
                                System.Console.ReadLine();
                                timeFrame2 = false;
                            }
                            else if (choice2 == "q")
                            {
                                timeFrame2 = false;
                            }
                            else
                            {
                                System.Console.WriteLine("Invalid input enter (day/week/month/year/total)");
                                System.Console.WriteLine("[enter]");
                                Console.ReadLine();
                            }
                        }
                    }
                    break;
                // quit
                case 2:
                    return;
            }
        }
    }

    // shows calender when user hits enter datetime gets returned
    public static DateTime DisplayCalendarFinance()
    {
        DateTime currentDate = DateTime.Now;
        int selectedDay = currentDate.Day;
        int selectedMonth = currentDate.Month;
        int selectedYear = currentDate.Year;
        bool running = true;
        while (running)
        {
            Console.Clear();
            PrintCalendar(selectedYear, selectedMonth, selectedDay);

            Console.WriteLine("\nUse Arrow Keys to navigate. Press Enter to select a date. Press Esc to exit.");
            ConsoleKey key = Console.ReadKey(true).Key;

            // Handle navigation
            if (key == ConsoleKey.LeftArrow) selectedDay--;
            if (key == ConsoleKey.RightArrow) selectedDay++;
            if (key == ConsoleKey.UpArrow) selectedDay -= 7;
            if (key == ConsoleKey.DownArrow) selectedDay += 7;

            // Handle Enter and Esc keys
            if (key == ConsoleKey.Enter)
                return new DateTime(selectedYear, selectedMonth, selectedDay); // Return selected date
            // Return a default value to indicate cancellation

            // Adjust month and year if out of range
            int daysInMonth = DateTime.DaysInMonth(selectedYear, selectedMonth);
            if (selectedDay < 1)
            {
                selectedMonth--;
                if (selectedMonth < 1)
                {
                    selectedMonth = 12;
                    selectedYear--;
                }
                selectedDay = DateTime.DaysInMonth(selectedYear, selectedMonth);
            }
            else if (selectedDay > daysInMonth)
            {
                selectedMonth++;
                if (selectedMonth > 12)
                {
                    selectedMonth = 1;
                    selectedYear++;
                }
                selectedDay = 1;
            }

        }
        return default;
    }
    // works in combination with display calender
    private static void PrintCalendar(int year, int month, int selectedDay)
    {
        DateTime firstDayOfMonth = new DateTime(year, month, 1);
        int daysInMonth = DateTime.DaysInMonth(year, month);

        // Print month and year
        Console.WriteLine($"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)} {year}");
        Console.WriteLine("Su Mo Tu We Th Fr Sa");

        // Print leading spaces for the first week
        int startDayOfWeek = (int)firstDayOfMonth.DayOfWeek;
        for (int i = 0; i < startDayOfWeek; i++)
        {
            Console.Write("   ");
        }

        // Print days of the month
        for (int day = 1; day <= daysInMonth; day++)
        {
            if (day == selectedDay)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"{day,2} ");
                Console.ResetColor();
            }
            else
            {
                Console.Write($"{day,2} ");
            }

            // Break line at the end of the week
            if ((startDayOfWeek + day) % 7 == 0)
            {
                Console.WriteLine();
            }
        }
        Console.WriteLine();
    }
}

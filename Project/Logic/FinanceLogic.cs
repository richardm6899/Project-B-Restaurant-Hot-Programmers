using System;
using System.Globalization;
public class FinanceLogic
{
    public List<AccountModel> _accounts;
    public List<ReceiptModel> _receipts;
    public List<ReservationModel> _reservations;
    public static int Revenue = 0;
    public FinanceLogic()
    {
        _accounts = AccountsAccess.LoadAll();
        _receipts = ReceiptAccess.LoadAllReceipts();
        _reservations = ReservationAccess.LoadAllReservations();
    }
    
    // public static DateTime DisplayCalendar()
    // {
    //     DateTime currentDate = DateTime.Now;
    //     int selectedDay = currentDate.Day;
    //     int selectedMonth = currentDate.Month;
    //     int selectedYear = currentDate.Year;
    //     bool running = true;
    //     while (running)
    //     {
    //         Console.Clear();
    //         PrintCalendar(selectedYear, selectedMonth, selectedDay);

    //         Console.WriteLine("\nUse Arrow Keys to navigate. Press Enter to select a date. Press Esc to exit.");
    //         ConsoleKey key = Console.ReadKey(true).Key;

    //         // Handle navigation
    //         if (key == ConsoleKey.LeftArrow) selectedDay--;
    //         if (key == ConsoleKey.RightArrow) selectedDay++;
    //         if (key == ConsoleKey.UpArrow) selectedDay -= 7;
    //         if (key == ConsoleKey.DownArrow) selectedDay += 7;

    //         // Handle Enter and Esc keys
    //         if (key == ConsoleKey.Enter)
    //             return new DateTime(selectedYear, selectedMonth, selectedDay); // Return selected date
    //         // Return a default value to indicate cancellation

    //         // Adjust month and year if out of range
    //         int daysInMonth = DateTime.DaysInMonth(selectedYear, selectedMonth);
    //         if (selectedDay < 1)
    //         {
    //             selectedMonth--;
    //             if (selectedMonth < 1)
    //             {
    //                 selectedMonth = 12;
    //                 selectedYear--;
    //             }
    //             selectedDay = DateTime.DaysInMonth(selectedYear, selectedMonth);
    //         }
    //         else if (selectedDay > daysInMonth)
    //         {
    //             selectedMonth++;
    //             if (selectedMonth > 12)
    //             {
    //                 selectedMonth = 1;
    //                 selectedYear++;
    //             }
    //             selectedDay = 1;
    //         }
            
    //     }
    //     return default;
    // }

    // private static void PrintCalendar(int year, int month, int selectedDay)
    // {
    //     DateTime firstDayOfMonth = new DateTime(year, month, 1);
    //     int daysInMonth = DateTime.DaysInMonth(year, month);

    //     // Print month and year
    //     Console.WriteLine($"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)} {year}");
    //     Console.WriteLine("Su Mo Tu We Th Fr Sa");

    //     // Print leading spaces for the first week
    //     int startDayOfWeek = (int)firstDayOfMonth.DayOfWeek;
    //     for (int i = 0; i < startDayOfWeek; i++)
    //     {
    //         Console.Write("   ");
    //     }

    //     // Print days of the month
    //     for (int day = 1; day <= daysInMonth; day++)
    //     {
    //         if (day == selectedDay)
    //         {
    //             Console.ForegroundColor = ConsoleColor.DarkYellow;
    //             Console.Write($"{day,2} ");
    //             Console.ResetColor();
    //         }
    //         else
    //         {
    //             Console.Write($"{day,2} ");
    //         }

    //         // Break line at the end of the week
    //         if ((startDayOfWeek + day) % 7 == 0)
    //         {
    //             Console.WriteLine();
    //         }
    //     }
    //     Console.WriteLine();
    // }
  
    public void AddToRevenue(int cost)
    {
        Revenue += cost;
    }
    public int ProfitsDay(DateTime date)
    {
        int total = 0;
        foreach (var receipt in _receipts)
        {
            if (receipt.Status != "Canceled")
            {
                if (date == receipt.Date)

                {

                    total += receipt.Cost;
                }
            }


        }
        return total;
    }
    public int ProfitsMonth(DateTime date)
    {
        int total = 0;
        foreach (var receipt in _receipts)
        {
            if (receipt.Status != "Canceled")
            {
                if (date.Month == receipt.Date.Month && date.Year == receipt.Date.Year)

                {

                    total += receipt.Cost;
                }
            }


        }
        return total;
    }
    public int ProfitsWeek(DateTime startofweek, DateTime endofweek)
    {
        int total = 0;
        foreach (var receipt in _receipts)
        {
            if (receipt.Status != "Canceled")
            {
                if (receipt.Date >= startofweek && receipt.Date <= endofweek)

                {

                    total += receipt.Cost;
                }
            }


        }
        return total;
    }
    public int ProfitsYear(DateTime date)
    {
        int total = 0;
        foreach (var receipt in _receipts)
        {
            if (receipt.Status != "Canceled")
            {
                if (date.Year == receipt.Date.Year)

                {

                    total += receipt.Cost;
                }
            }


        }
        return total;
    }
    public int TotalProfits()
    {
        int total = 0;
        foreach (var receipt in _receipts)
        {
            if (receipt.Status != "Canceled")
            {

                total += receipt.Cost;

            }


        }
        return total;
    }
    public int TotalReservationsDay(DateTime date)
    {

        int total = 0;
        foreach (var reservation in _reservations)
        {
            if (reservation.Status != "Canceled")
            {
                if (date == reservation.Date)

                {

                    total++;
                }
            }


        }
        return total;

    }
    public int TotalReservationsMonth(DateTime date)
    {

        int total = 0;
        foreach (var reservation in _reservations)
        {
            if (reservation.Status != "Canceled")
            {
                if (date.Month == reservation.Date.Month && date.Year == reservation.Date.Year)
                {

                    total++;
                }
            }



        }
        return total;

    }
    public int TotalReservationsYear(DateTime date)
    {

        int total = 0;
        foreach (var reservation in _reservations)
        {
            if (reservation.Status != "Canceled")
            {
                if (date.Year == reservation.Date.Year)
                {

                    total++;
                }
            }



        }
        return total;

    }
    public int TotalReservations()
    {

        int total = 0;
        foreach (var reservation in _reservations)
        {
            if (reservation.Status != "Canceled")
            {



                total++;

            }



        }
        return total;

    }
    public int ReservationsWeek(DateTime startofweek, DateTime endofweek)
    {
        int total = 0;
        foreach (var receipt in _receipts)
        {
            if (receipt.Status != "Canceled")
            {
                if (receipt.Date >= startofweek && receipt.Date <= endofweek)

                {

                    total++;
                }
            }


        }
        return total;


    }
}
// -------------FinancialMenu--------------
/*
            Console.WriteLine("Welcome to the financial menu.");
            System.Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Welcome back " + acc.FullName);
            System.Console.WriteLine("-----------------------------------------");
            System.Console.WriteLine("Enter 1 to see all reservations.");
            System.Console.WriteLine("Enter 2 to see the restaurant info.");
            System.Console.WriteLine("Enter 3 to see your accounts data.");
            System.Console.WriteLine("Enter 4 to look at finances.");
            System.Console.WriteLine("Enter 5 to log out");




*/ 
// ----------- finances-----------------------
/*


            System.Console.WriteLine("Enter 1 to see profit information");
            System.Console.WriteLine("Enter 2 to see reservation information");
            System.Console.WriteLine("Enter 3 to Quit");
            System.Console.WriteLine("----------------------------------------");


if (choice == "1")
            {
                bool timeframe1 = true;
                while (timeframe1)
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
                        timeframe1 = false;



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
                        timeframe1 = false;
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
                            timeframe1 = false;
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
                            timeframe1 = false;
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
                        timeframe1 = false;
                    }


                    else if (choice2 == "q")
                    {
                        timeframe1 = false;
                    }
                    else
                    {
                        System.Console.WriteLine("Invalid input enter (day/week/month/year/total/ Q )");
                        System.Console.WriteLine("[enter]");
                        Console.ReadLine();
                    }
                }
            }
            else if (choice == "2")
            {
                bool timeframe2 = true;
                while (timeframe2)
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
                        timeframe2 = false;



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
                        timeframe2 = false;

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
                            timeframe2 = false;
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
                            timeframe2 = false;
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
                        timeframe2 = false;
                    }
                    else if (choice2 == "q")
                    {
                        timeframe2 = false;
                    }
                    else
                    {
                        System.Console.WriteLine("Invalid input enter (day/week/month/year/total)");
                        System.Console.WriteLine("[enter]");
                        Console.ReadLine();
                    }
                }
            }
            else if (choice == "3")
            {
                finance = false;
            }
            else
            {
                System.Console.WriteLine("invalid input");
                System.Console.WriteLine("[enter]");
                Console.ReadLine();
            }
        }
*/ 
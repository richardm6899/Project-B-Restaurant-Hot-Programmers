
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;


// order of working
// user gets asked for how many people user wants to reserve
// user gets asked if they want hotseats or regular seats
// how do you check if tables are next to each other
// user gets asked timeslot
//user gets shown  calender
// if not hot seat: user gets option to choose table.
// If there are no available hot seats, user gets notified that there are no available hot seats for the amount of people and can choose again.
// user gets asked confirmation
static class Reservation
{
    static private ReservationLogic reservationlogic = new();
    static private RestaurantLogic restaurantLogic = new();

    // displays table restaurant

    public static void MakeReservation(string name, int clientID, string number, string email)
    {

        bool running = true;
        while (running)
        {
            // user has to enter yes or no to go further
            bool Client_answer = HelperPresentation.YesOrNo("Hello would you like to make a reservation?");

            if (Client_answer)
            {

                string[] HotOrReg = ["Regular", "HotSeat", "Quit"];
                string HotOrRegChoice = Choice("Do you want to reserve a Hotseat or a Regular Seat\nHotSeat cost 10 euro extra.\nA Hotseat wil give you a seat at the edge of the restaurant.\n----------------------------------------", HotOrReg);

                if (HotOrRegChoice == "HotSeat")
                {
                    // go trough process of as a hotseat
                    HotSeatReservation.HotSeat(name, clientID, number, email);

                    break;
                }

                else if (HotOrRegChoice == "Regular")
                {
                    RegularReservation.Regular(name, clientID, number, email);
                    break;
                }
                else if (HotOrRegChoice == "Quit")
                {

                    System.Console.WriteLine("Goodbye...");
                    System.Console.WriteLine("[enter]");
                    System.Console.ReadLine();
                    Console.Clear();
                    break;
                }
            }
            else if (Client_answer == false)
            {
                System.Console.WriteLine("Goodbye....");
                System.Console.WriteLine("[enter]");
                System.Console.ReadLine();
                Console.Clear();
                break;

            }
        }
    }

    public static void ResOrderFood(List<int> TableID, string name, int clientID, int HowMany, DateTime Date, string typeofreservation, string TimeSlot, string number, string email, int cost)
    {
        string foodorder = "Would you like to order food in advance?";
        System.Console.WriteLine("-----------------------------------------");
        System.Console.WriteLine($"{foodorder}\n");
        bool orderfood = HelperPresentation.YesOrNo(foodorder);

        if (typeofreservation == "Regular")
        {
            if (orderfood)
            {
                var (foodCart, allergies) = FoodOrderMenu.OrderFood();
                ReservationModel Reservation = reservationlogic.Create_reservation(TableID[0], name, clientID, HowMany, Date, typeofreservation, TimeSlot, true);

                ReceiptModel receipt = reservationlogic.CreateReceipt(Reservation, cost, number, email, foodCart, []);

                System.Console.WriteLine();
                System.Console.WriteLine("This is your receipt for now: ");
                System.Console.WriteLine(reservationlogic.DisplayReceipt(receipt, allergies));
                System.Console.WriteLine("Reservation created");
                System.Console.WriteLine("[enter]");
                Console.ReadLine();

                reservationlogic.AvailableTables.Clear();
                Console.Clear();

            }
            else
            {
                ReservationModel Reservation = reservationlogic.Create_reservation(TableID[0], name, clientID, HowMany, Date, typeofreservation, TimeSlot, false);

                ReceiptModel receipt = reservationlogic.CreateReceipt(Reservation, cost, number, email, [], []);
                System.Console.WriteLine();
                System.Console.WriteLine("This is your receipt for now: ");

                System.Console.WriteLine(reservationlogic.DisplayReceipt(receipt, []));
                System.Console.WriteLine("reservation created");
                System.Console.WriteLine("[enter]");
                Console.ReadLine();
                reservationlogic.AvailableTables.Clear();
                Console.Clear();

            }
        }
        else if (typeofreservation == "HotSeat")
        {


            if (orderfood)
            {
                var (foodCart, allergies) = FoodOrderMenu.OrderFood();
                List<ReservationModel> Reservation = reservationlogic.Create_reservationHotSeat(TableID, name, clientID, HowMany, Date, typeofreservation, TimeSlot, true);

                ReceiptModel receipt = reservationlogic.CreateReceiptHotSeat(Reservation, cost, number, email, foodCart);

                System.Console.WriteLine();
                System.Console.WriteLine("This is your receipt for now: ");
                System.Console.WriteLine(reservationlogic.DisplayReceipt(receipt, allergies));
                System.Console.WriteLine("Reservation created");
                System.Console.WriteLine("[enter]");
                Console.ReadLine();

                reservationlogic.AvailableTables.Clear();
                Console.Clear();


            }
            else
            {
                List<ReservationModel> Reservation = reservationlogic.Create_reservationHotSeat(TableID, name, clientID, HowMany, Date, typeofreservation, TimeSlot, false);

                ReceiptModel receipt = reservationlogic.CreateReceiptHotSeat(Reservation, cost, number, email, []);
                System.Console.WriteLine();
                System.Console.WriteLine("This is your receipt for now: ");

                System.Console.WriteLine(reservationlogic.DisplayReceipt(receipt, []));
                System.Console.WriteLine("reservation created");
                System.Console.WriteLine("[enter]");
                Console.ReadLine();
                reservationlogic.AvailableTables.Clear();
                Console.Clear();

            }
        }
    }
    public static void CancelReservation(int clientID)
    {
        bool cancelreservation = true;
        while (cancelreservation)
        {
            System.Console.WriteLine("Would you like to cancel your reservation? (Y/N)");
            string choice = Console.ReadLine().ToUpper();

            if (choice == "Y")
            {

                if (reservationlogic.DisplayReservations(clientID) != "")
                {
                    bool ReservationIDCheck = true;
                    while (ReservationIDCheck)
                    {
                        System.Console.WriteLine("Which reservation would you like to cancel?\n");
                        System.Console.WriteLine(reservationlogic.DisplayReservations(clientID));
                        System.Console.WriteLine("enter ID: ");
                        string str_id = Console.ReadLine();
                        {
                            if (int.TryParse(str_id, out int reservationid))
                            {
                                // can only choose id if in reserved by person
                                if (reservationlogic.DisplayReservationByID(reservationid) != null && reservationlogic.IsReservationInAccount(clientID, reservationid).Contains(reservationid))
                                {

                                    ReservationIDCheck = false;
                                    bool confirmation = true;
                                    while (confirmation)
                                    {

                                        System.Console.WriteLine(reservationlogic.DisplayReservationByID(reservationid));
                                        System.Console.WriteLine("Are You sure you want to cancel this reservation? (Y/N)");

                                        string choice2 = Console.ReadLine().ToUpper();
                                        if (choice2 == "Y")
                                        {
                                            Console.Clear();
                                            ReservationModel reservation = reservationlogic.GetReservationById(reservationid);
                                            System.Console.WriteLine(reservationlogic.DisplayReservationByID(reservation.Id) + " has been canceled.\n");
                                            reservationlogic.RemoveReservationByID(reservationid);

                                            System.Console.WriteLine("[enter]");
                                            Console.ReadLine();
                                            Console.Clear();

                                            confirmation = false;
                                        }
                                        else if (choice2 == "N")
                                        {
                                            confirmation = false;

                                        }

                                    }
                                }
                                else
                                {
                                    System.Console.WriteLine("Invalid id given");
                                    System.Console.WriteLine("[enter]");
                                    Console.ReadLine();
                                }
                            }

                            else
                            {
                                System.Console.WriteLine("invalid input");
                                System.Console.WriteLine("[enter]");
                                System.Console.ReadLine();

                            }
                        }
                    }
                }
                else
                {
                    System.Console.WriteLine("You have no available reservations to cancel");
                    System.Console.WriteLine("[enter]");
                    System.Console.ReadLine();
                    cancelreservation = false;
                }

            }
            else if (choice == "N")
            {
                System.Console.WriteLine("Goodbye....");
                System.Console.WriteLine("[enter]");
                System.Console.ReadLine();
                cancelreservation = false;
            }
            else
            {
                System.Console.WriteLine("Please fill in (Y/N)");
                System.Console.WriteLine("[enter]");
                System.Console.ReadLine();
            }
        }
    }

    public static void AdminCancelReservation()
    {
        bool cancelreservation = true;
        while (cancelreservation)
        {
            if (HelperPresentation.YesOrNo("Would you like to cancel a reservation? (Y/N)"))
            {
                List<string> ongoingReservations = reservationlogic.DisplayAllOngoingReservations();
                foreach (string reservation in ongoingReservations)
                {
                    System.Console.WriteLine(reservation);
                }

                bool Canceling = true;
                while (Canceling)
                {
                    // make this so when an incorrect input is entered ask again
                    // make an escape
                    System.Console.WriteLine("ID to cancel: ");
                    int cancelID = Convert.ToInt32(Console.ReadLine());

                    ReservationModel toCancelReservation = reservationlogic.GetReservationById(cancelID);
                    bool cancelReservation = HelperPresentation.YesOrNo($"Are you sure you want to cancel this reservation:\nId: {toCancelReservation.Id}\nName: {toCancelReservation.Name}\nTotal people: {toCancelReservation.HowMany}\nDate: {toCancelReservation.Date.ToShortDateString()} {toCancelReservation.TimeSlot}\nType of reservation: {toCancelReservation.TypeOfReservation}");
                    if (cancelReservation)
                    {
                        reservationlogic.RemoveReservationByID(cancelID);
                    }
                    Canceling = false;

                }
            }
            else
            {
                System.Console.WriteLine("Goodbye....");
                System.Console.WriteLine("[enter]");
                System.Console.ReadLine();
                cancelreservation = false;
            }
        }
    }

    public static string AdminShowReservations()
    {
        List<string> ongoingReservations = reservationlogic.DisplayAllOngoingReservations();
        string reservation_string = "";
        foreach (string reservation in ongoingReservations)
        {
            reservation_string += reservation;
        }
        return reservation_string;
    }


    // Admin closes restaurant, removes reservation by date from user and refunds them
    public static void AdminCloseDay()
    {
        System.Console.Write("Enter date (mm/dd/yyyy): ");
        string dateInput = Console.ReadLine();
        DateTime date;
        // System.Console.Write("Enter table ID:");
        // int tableID = Convert.ToInt32(Console.ReadLine());
        // string typeofreservation = reservationlogic.TypeOfReservation(tableID);
        if (DateTime.TryParse(dateInput, out date))
        {
            reservationlogic.RemoveReservationsByDate(date);
            System.Console.WriteLine("Reservations for the specified date have been canceled.");
            // Users that had a reservation on that day get a refund. (paid out of financials)
            foreach (var reservation in reservationlogic._reservations)
            {

                if (reservation.TypeOfReservation == "HotSeat")
                {
                    FinanceLogic.SubtractFromRevenue(60);
                    Console.WriteLine($"{reservation.TypeOfReservation} seat refunded:\nSubtracted from Revenue: 60$)");
                }
                else if (reservation.TypeOfReservation == "Regular")
                {
                    FinanceLogic.SubtractFromRevenue(50);
                    Console.WriteLine($"{reservation.TypeOfReservation} seat refunded:\nSubtracted from Revenue: 50$");
                }
                else if (reservation.TypeOfReservation is null)
                {
                    Console.WriteLine("No reservation to be funded");
                }
            }
        }
        else
        {
            System.Console.WriteLine("Invalid date format. Please try again.");
        }
    }


    // displays calender for make reservation
    private static void PrintCalendar(int year, int month, int selectedDay, List<DateTime> maxCapDays, List<DateTime> maxTimeSlot)
    {

        DateTime firstDayOfMonth = new DateTime(year, month, 1);
        int daysInMonth = DateTime.DaysInMonth(year, month);

        System.Console.WriteLine("------------------------------------------------------");

        System.Console.Write("On what day would you like to reserve a table? ");
        Console.ForegroundColor = ConsoleColor.Green;

        System.Console.WriteLine(@"'You can only book 3 months in advanced'");

        Console.ForegroundColor = ConsoleColor.Red;
        System.Console.WriteLine("Red: no availability for combination of amount of people and timeslot.");


        Console.ForegroundColor = ConsoleColor.DarkGray;
        System.Console.WriteLine("Gray: Restaurant is closed for the day");

        Console.ResetColor();
        System.Console.WriteLine("--------------------------------------------------");
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
            // if day is on max capacity or day is closed then makes it red
            if (day == selectedDay)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"{day,2} ");
                Console.ResetColor();
            }

            else if (maxTimeSlot.Contains(Convert.ToDateTime($"{year}/{month}/{day}")) || maxCapDays.Contains(Convert.ToDateTime($"{year}/{month}/{day}")))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{day,2} ");
                Console.ResetColor();
            }


            else if (restaurantLogic.closed_Day(Convert.ToDateTime($"{year}/{month}/{day}")))
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
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
    public static string DisplayCalendarReservation(string timeslotid, int amount, string typeofreservation)
    {

        List<DateTime> maxCapDays = reservationlogic.MaxCapDays();
        // also days where timeslot and the amount of people chosen are same make darkgrey
        List<DateTime> maxTimeslot = typeofreservation == "Regular" ? reservationlogic.MaxTimeSlotRegularSeat(timeslotid, amount) : reservationlogic.MaxTimeSlotHotSeat(timeslotid, amount);
        // date init
        DateTime currentDate = DateTime.Now;
        int selectedDay = currentDate.Day;
        int selectedMonth = currentDate.Month;
        int selectedYear = currentDate.Year;
        bool running = true;

        while (running)
        {
            // days where it should display red
            Console.Clear();
            PrintCalendar(selectedYear, selectedMonth, selectedDay, maxCapDays, maxTimeslot);

            Console.WriteLine("\nUse Arrow Keys to navigate. Press Enter to select a date. Press Esc to exit.");
            ConsoleKey key = Console.ReadKey(true).Key;

            // Handle navigation
            if (key == ConsoleKey.LeftArrow) selectedDay--;
            if (key == ConsoleKey.RightArrow) selectedDay++;
            if (key == ConsoleKey.UpArrow) selectedDay -= 7;
            if (key == ConsoleKey.DownArrow) selectedDay += 7;

            // Handle Enter and Esc keys
            if (key == ConsoleKey.Enter)
            {
                //if date is fully booked return default // might want to expand further // think i want to return string so that it doens't get changed much
                string returnDate = $"{selectedYear}, {selectedMonth}, {selectedDay}";


                // if date is in max capacity days, or timeslot is fully booked return that date is fully booked
                if (maxCapDays.Contains(Convert.ToDateTime(returnDate)) || maxTimeslot.Contains(Convert.ToDateTime(returnDate)))
                {
                    return $"{returnDate} is fully booked.";
                }
                else if (restaurantLogic.closed_Day(Convert.ToDateTime(returnDate)))
                {
                    return "Sorry, We are closed on this day";
                }


                else
                {
                    return returnDate;
                }

            }
            if (key == ConsoleKey.Escape)
            {
                return "20";
            }// Return selected date
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
        return "";
    }
    public static bool DisplayChosenSeats(List<int> chosenseats)
    {
        // Initialize restaurant layout using a List of Lists
        var RestaurantLayout = new List<List<string>>
    {
        new() {"[  K  ]","[  K  ]","[  K  ]","[R:1  ]","[R:2  ]","[  T  ]","[  T  ]","[H:16 ]","[H:17 ]"},
        new() {"[R:3  ]","[R:4  ]","[R:5  ]","[R:6  ]","[R:7  ]","[     ]","[     ]","[R:14 ]","[H:18 ]",},
        new() {"[R:8  ]","[R:9  ]","[R:10 ]","[     ]","[     ]","[     ]","[     ]","[R:15 ]","[H:19 ]" },
        new() {"[R:11 ]","[R:12 ]","[R:13 ]","[  E  ]","[  E  ]","[H:23 ]","[H:22 ]","[H:21 ]","[H:20 ]" }
    };

        bool running = true;
        while (running)
        {
            // Console.Clear();

            // Render the layout
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            System.Console.WriteLine("You will be seated at the yellow seat(s)");
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("[X] These Tables are already booked for this timeslot");
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine("---------------------------------------------------------");
            for (int row = 0; row < RestaurantLayout.Count; row++)
            {
                for (int col = 0; col < RestaurantLayout[row].Count; col++)
                {
                    string table = RestaurantLayout[row][col];

                    if (table.Contains("H"))
                    {
                        string id = table.Split(":")[1].Trim(']').Trim();
                        if (chosenseats.Contains(Convert.ToInt32(id)))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            table = "[  X  ]";
                        }
                        Console.Write(table);
                    }
                    else if (table.Contains("R"))
                    {
                        string id = table.Split(":")[1].Trim(']').Trim(); ;
                        if (chosenseats.Contains(Convert.ToInt32(id)))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            table = "[  X  ]";
                        }
                        Console.Write(table);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write(table);
                    }

                    Console.ResetColor();
                    Console.Write(" "); // Add spacing between items
                }
                Console.WriteLine(); // Move to the next row
            }

            // Instructions
            Console.WriteLine("\n-------------------------------------------------------");
            System.Console.WriteLine("Hit Enter if you want to confirm your seat, hit Escape to choose an other preferences");

            // Read key input
            var key = Console.ReadKey(true).Key;


            // Exit on Esc
            if (key == ConsoleKey.Escape)
                return false;
            if (key == ConsoleKey.Enter)
            {
                return true;
            }

        }
        return true;
    }
    public static string Choice(string prompt, string[] opt)
    {
        int selectedIndex = 0;  // Start with "Yes" as the default selection
        string[] options = opt;

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"{prompt}\n");  // Display the prompt (e.g., "Are you sure?")

            // show options
            for (int i = 0; i < options.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green; // Highlight current selection
                    Console.WriteLine($"> {options[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {options[i]}");
                }
            }

            //get user input
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            ConsoleKey key = keyInfo.Key;

            if (key == ConsoleKey.UpArrow)
            {
                // Go up
                selectedIndex = (selectedIndex == 0) ? options.Length - 1 : selectedIndex - 1;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                // Go down
                selectedIndex = (selectedIndex == options.Length - 1) ? 0 : selectedIndex + 1;
            }
            else if (key == ConsoleKey.Enter)
            {
                // Return true for Yes, false for No
                return options[selectedIndex];
            }
        }
    }

    public static void DisplayOptions(string[] options, int selectedIndex)
    {
        for (int i = 0; i < options.Length; i++)
        {
            if (i == selectedIndex)
            {
                Console.ForegroundColor = ConsoleColor.Green; // Highlight the selected option
                Console.WriteLine($"> {options[i]}");
                Console.ResetColor();
            }
            else
            {
                Console.ResetColor();
                Console.WriteLine($"  {options[i]}");
            }
        }
    }
    public static int DisplayRestaurant()
    {
        // Initialize restaurant layout using a List of Lists
        var RestaurantLayout = new List<List<string>>
    {
        new() {"[  K  ]","[  K  ]","[  K  ]","[R:1  ]","[R:2  ]","[  T  ]","[  T  ]","[H:16 ]","[H:17 ]"},
        new() {"[R:3  ]","[R:4  ]","[R:5  ]","[R:6  ]","[R:7  ]","[     ]","[     ]","[R:14 ]","[H:18 ]",},
        new() {"[R:8  ]","[R:9  ]","[R:10 ]","[     ]","[     ]","[     ]","[     ]","[R:15 ]","[H:19 ]" },
        new() {"[R:11 ]","[R:12 ]","[R:13 ]","[  E  ]","[  E  ]","[H:23 ]","[H:22 ]","[H:21 ]","[H:20 ]" }
    };

        List<string> availableTableIDs = new List<string>() { };
        foreach (var table in reservationlogic.AvailableTables)
        {
            availableTableIDs.Add(Convert.ToString(table.Id));
        }

        int currentRow = 0;
        int currentCol = 0;
        bool running = true;
        while (running)
        {
            Console.Clear();

            // Render the layout
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("(R)egular seats have a 50€ deposist.");
            Console.ForegroundColor = ConsoleColor.Magenta;
            System.Console.WriteLine("(H)otSeats Cost an extra 10 € on top of your 50 € deposit");
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("[X] These Tables are already booked for this timeslot");
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine("---------------------------------------------------------");
            for (int row = 0; row < RestaurantLayout.Count; row++)
            {
                for (int col = 0; col < RestaurantLayout[row].Count; col++)
                {
                    string table = RestaurantLayout[row][col];
                    bool isCursorPosition = row == currentRow && col == currentCol;


                    if (isCursorPosition)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write("[  X  ]");
                    }
                    else if (table.Contains("H"))
                    {
                        string id = table.Split(":")[1].Trim(']').Trim();

                        Console.ForegroundColor = ConsoleColor.Red;
                        table = "[  X  ]";

                        Console.Write(table);
                    }
                    else if (table.Contains("R"))
                    {
                        string id = table.Split(":")[1].Trim(']').Trim(); ;
                        if (availableTableIDs.Contains(id))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            table = "[  X  ]";
                        }
                        Console.Write(table);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write(table);
                    }

                    Console.ResetColor();
                    Console.Write(" "); // Add spacing between items
                }
                Console.WriteLine(); // Move to the next row
            }

            // Instructions
            Console.WriteLine("\n-------------------------------------------------------");
            Console.WriteLine("\nUse arrow keys to move, Enter to book, and Esc to exit.");

            // Read key input
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow && currentRow > 0) currentRow--;
            if (key == ConsoleKey.DownArrow && currentRow < RestaurantLayout.Count - 1) currentRow++;
            if (key == ConsoleKey.LeftArrow && currentCol > 0) currentCol--;
            if (key == ConsoleKey.RightArrow && currentCol < RestaurantLayout[currentRow].Count - 1) currentCol++;

            // Booking a table
            if (key == ConsoleKey.Enter)
            {
                string selectedTable = RestaurantLayout[currentRow][currentCol];

                if (selectedTable.Contains("R"))
                {
                    string id = selectedTable.Split(":")[1].Trim(']').Trim();
                    if (availableTableIDs.Contains(id))
                    {
                        RestaurantLayout[currentRow][currentCol] = "[  X  ]";
                        availableTableIDs.Remove(id);
                        return Convert.ToInt32(id);
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid table chosen.");
                        Console.ReadKey(true);
                    }
                }
            }
            // Exit on Esc
            if (key == ConsoleKey.Escape)
                return 0;

        }
        return 0;
    }

    // reservation structure 
    // how many check -----------------
    public static int HowManyCheck(string typeofreservation)
    {
        string[] nums = [];
        if (typeofreservation == "Regular")
        {
            nums = ["1", "2", "3", "4", "5", "6", "Quit"];
        }
        else if (typeofreservation == "HotSeat")
        {
            nums = ["1", "2", "3", "4", "5", "6","7","8", "Quit"];
        }
        string HowManycheck = Reservation.Choice("\nFor how many people? We have a max of 6 per table.\n----------------------------------------", nums);
        if (int.TryParse(HowManycheck, out int HowMany))
        {

            System.Console.WriteLine($"{HowMany} person(s) to be seated");
            System.Console.WriteLine("[enter]");
            Console.ReadLine();
            Console.Clear();
            return HowMany;


        }
        else if (HowManycheck == "Quit")
        {
            System.Console.WriteLine("Goodbye....");
            System.Console.WriteLine("[enter]");
            Console.ReadLine();
            Console.Clear();
            return 0;
        }
        return 0;

    }
    public static string TimeSlot()
    {
        string[] timeslots = ["Lunch (12:00 - 14:00)", "Dinner 1 (17:00 - 19:00)", "Dinner 2 (19:00 - 21:00)", "Dinner 3 (21:00 - 23:00)", "Return", "Quit"];
        string TimeSlot = Reservation.Choice("\nWhat TimeSlot would you prefer: \n----------------------------------------\nPress 'Return' to go back to choosing amount of people.\n", timeslots);

        System.Console.WriteLine("----------------------------------------");
        if (TimeSlot == "Return")
        {
            return "Return";
        }
        else if (TimeSlot == "Quit")
        {
            System.Console.WriteLine("Goodbye....");
            System.Console.WriteLine("[enter]");
            Console.ReadLine();
            Console.Clear();
            return "Quit";
        }
        else
        {
            //    check if valid time slot
            System.Console.WriteLine($"Time slot {TimeSlot} chosen.");
            System.Console.WriteLine("[enter]");
            System.Console.ReadLine();
            Console.Clear();

            return TimeSlot;

        }
    }
    public static DateTime ChooseDate(string TimeSlot, int HowMany)
    {
        // calender gets shown with all available dates 
        bool DateCheck = true;
        while (DateCheck)
        {
            System.Console.WriteLine(@"'You can only book 3 months in advanced'");
            // check if valid date

            string UncheckedDate = Reservation.DisplayCalendarReservation(TimeSlot, HowMany, "Regular");
            if (DateTime.TryParse(UncheckedDate, out DateTime Date))
            {
                //checks if user filled in date not before today and not farther than 3 months in the future
                // can be more personalised in terms of what the user filled in wrong by making returns numbers
                if (reservationlogic.IsValidDate(Date))
                {

                    System.Console.WriteLine(Date + "chosen");
                    System.Console.WriteLine("[enter]");
                    System.Console.ReadLine();
                    return Date;


                }
                else
                {
                    System.Console.WriteLine("Invalid date entered. Try again");
                    System.Console.WriteLine("[enter]");
                    System.Console.ReadLine();
                }
            }
            // if it is equal to 20 means that they want to go back to timeslot chocie
            else if (int.TryParse(UncheckedDate, out int num) && num == 20)
            {
                System.Console.WriteLine("Going back to timeslot choice");
                System.Console.WriteLine("[enter]");
                System.Console.ReadLine();
                Console.Clear();
                return default;
            }
            else
            {
                System.Console.WriteLine(UncheckedDate);
                System.Console.WriteLine("[enter]");
                System.Console.ReadLine();
            }
        }
        return default;
    }
    public static void AvailableTablesHotSeat(DateTime Date, string TimeSlot)
    {
        reservationlogic.AvailableTables.Clear();
        reservationlogic.AddHotSeatsAvailableTables();

        reservationlogic.CheckDateHotSeat(Date, TimeSlot);
    }
    public static void AvailableTablesRegular(DateTime Date, string TimeSlot, int HowMany)
    {
        reservationlogic.AvailableTables.Clear();
        reservationlogic.AddRegularSeats(HowMany);
        reservationlogic.CheckDate(Date, TimeSlot);
    }
    
}
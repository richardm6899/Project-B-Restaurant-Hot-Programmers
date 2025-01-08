
using System.ComponentModel.DataAnnotations;
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
    static private ReservationAccess reservationAccess = new(); 
    static private RestaurantAccess restaurantAccess = new();
    static private TableAccess tableAccess = new();
    static private ReservationLogic reservationlogic = new();
    static private RestaurantLogic restaurantLogic = new();
    static private AccountsLogic accountsLogic = new();

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
                string HotOrRegChoice = Choice("Do you want to reserve a Hotseat or a Regular Seat\nHotSeat cost 10 euro extra.\nA Hotseat wil give you a seat near the kitchen.\n----------------------------------------", HotOrReg);

                if (HotOrRegChoice == "HotSeat")
                {
                    // go trough process of as a hotseat
                    HotSeat(name, clientID, number, email);

                    break;
                }

                else if (HotOrRegChoice == "Regular")
                {
                    Regular(name, clientID, number, email);
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
    private static void Regular(string name, int clientID, string number, string email)
    {
        int cost = 50;
        string typeofreservation;
        DateTime Date = default;
        string TimeSlot = "";
        int HowMany = 0;
        int progress = 0;
        bool running = true;
        while (running)
        {
            // user gets asked with how many people are and check is done
            if (progress == 0)
            {
                HowMany = Reservation.HowManyCheck("Regular");
                if (HowMany > 0)
                {
                    progress = 20;
                }
                else if (HowMany == 0)
                {

                    break;
                }


            }


            // user gets to choose timeslot of reservation
            else if (progress == 20)
            {
                TimeSlot = Reservation.TimeSlot();
                System.Console.WriteLine("----------------------------------------");
                if (TimeSlot == "Return")
                {
                    progress = 0;
                }
                else if (TimeSlot == "Quit")
                {
                    break;
                }
                else
                {
                    progress = 40;
                }
            }


            else if (progress == 40)
            {
                Date = Reservation.ChooseDate(TimeSlot, HowMany, "Regular", false);
                if (Date != default)
                {
                    progress = 60;
                }
                else
                {
                    progress = 20;
                }

            }

            // available table check
            // happens down here to not override other methods (DisplayCalendarReservation)

            else if (progress == 60)
            {
                AvailableTablesRegular(Date, TimeSlot, HowMany);
                // User gets shown restaurant and is shown what tables 
                System.Console.WriteLine("Where would you like to sit.\nChoose the table id of the table.");

                List<int> TableID = [Reservation.DisplayRestaurant(Date, TimeSlot, HowMany)];



                if (TableID[0] != 0)
                {

                    bool TableChoice = DisplayChosenSeats(TableID);
                    System.Console.WriteLine("-----------------------------------------");
                    System.Console.WriteLine(reservationlogic.displayTable(TableID[0]));


                    if (TableChoice)
                    {
                        Reservation.ResOrderFood(TableID, name, clientID, HowMany, Date, "Regular", TimeSlot, number, email, cost);

                        ReservationLogic.AvailableTables.Clear();
                        Console.Clear();
                        break;



                    }


                }
                else
                {
                    progress = 40;
                }

            }


        }

    }

    private static void HotSeat(string name, int clientID, string number, string email)
    {
        restaurantAccess.LoadAll();
        tableAccess.LoadAll();
        reservationlogic.AddHotSeatsAvailableTables();
        bool running = true;
        int progress = 0;
        int cost = 60;

        DateTime Date = default;
        string TimeSlot = "";
        int HowMany = 0;
        // user gets to choose timeslot of reservation
        // user gets asked with how many people are and check is done
        while (running)
        {
            if (progress == 0)
            {
                HowMany = Reservation.HowManyCheck("HotSeat");
                if (HowMany > 0)
                {
                    progress = 20;
                }
                else if (HowMany == 0)
                {

                    break;
                }
            }
            // user gets to choose timeslot of reservation
            else if (progress == 20)
            {
                TimeSlot = Reservation.TimeSlot();
                System.Console.WriteLine("----------------------------------------");
                if (TimeSlot == "Return")
                {
                    progress = 0;
                }
                else if (TimeSlot == "Quit")
                {
                    break;
                }
                else
                {
                    progress = 40;
                }
            }
            else if (progress == 40)
            {
                Date = Reservation.ChooseDate(TimeSlot, HowMany, "HotSeat", false);
                if (Date != default)
                {
                    progress = 60;
                }
                else
                {
                    progress = 20;
                }

            }

            else if (progress == 60)
            {

                AvailableTablesHotSeat(Date, TimeSlot);

                List<int> tableIDs = ReservationLogic.ChooseSeats(HowMany);
                bool TableChoice = DisplayChosenSeats(tableIDs);
                if (TableChoice)
                {
                    Reservation.ResOrderFood(tableIDs, name, clientID, HowMany, Date, "HotSeat", TimeSlot, number, email, cost);

                    ReservationLogic.AvailableTables.Clear();
                    Console.Clear();
                    break;
                }
                else if (TableChoice == false)
                {

                    progress = 40;
                }
            }
        }
    }
    private static void ResOrderFood(List<int> TableID, string name, int clientID, int HowMany, DateTime Date, string typeofreservation, string TimeSlot, string number, string email, int cost)
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

                ReservationLogic.AvailableTables.Clear();
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
                ReservationLogic.AvailableTables.Clear();
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

                ReservationLogic.AvailableTables.Clear();
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
                ReservationLogic.AvailableTables.Clear();
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

    // could be put somehwere else
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


    // Reservation Calender/Restaurant Display-------------------
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
    // calender logic

    private static string DisplayCalendarReservation(string timeslotid, int amount, string typeofreservation, bool modify)
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

                else if (Convert.ToDateTime(returnDate) < DateTime.Today.AddHours(48) && modify)
                {
                    return "You can only modify a reservation for 48 hours in advance";
                }
                else
                {
                    return returnDate;
                }

            }
            if (key == ConsoleKey.Escape)
            {
                return "Return";
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

    private static string Choice(string prompt, string[] opt)
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

    private static void DisplayOptions(string[] options, int selectedIndex)
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
    private static int DisplayRestaurant(DateTime Date, string timeslot, int amount)
    {
        // Initialize restaurant layout using a List of Lists
        var RestaurantLayout = new List<List<string>>
    {
        new() {"[  K  ]","[  K  ]","[  K  ]","[  K  ]","[  K  ]","[  K  ]","[  K  ]","[H:23 ]","[  T  ]"},
        new() {"[H:16 ]","[H:17 ]","[H:18 ]","[H:19 ]","[H:20 ]","[H:21 ]","[H:22 ]","[R:14 ]","[     ]",},
        new() {"[     ]","[     ]","[     ]","[     ]","[     ]","[     ]","[     ]","[R:15 ]","[     ]" },
        new() {"[R:1  ]","[R:2  ]","[R:3  ]","[R:4  ]","[     ]","[R:9  ]","[     ]","[     ]","[     ]" },
        new() {"[R:5  ]","[R:6  ]","[R:7  ]","[R:8  ]","[  E  ]","[R:10 ]","[R:11 ]","[R:12 ]","[R:13 ]" }
    };


        List<string> availableTableIDs = new List<string>() { };
        foreach (var table in ReservationLogic.AvailableTables)
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
            System.Console.WriteLine("[X] These Tables are not available for this timeslot");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Amount of people [{amount}]");
            Console.WriteLine($"Timeslot [{timeslot}]");
            Console.WriteLine($"Date [{Date.ToShortDateString()}]");
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
    private static bool DisplayChosenSeats(List<int> chosenseats)
    {
        // Initialize restaurant layout using a List of Lists
        var RestaurantLayout = new List<List<string>>
    {
        new() {"[  K  ]","[  K  ]","[  K  ]","[  K  ]","[  K  ]","[  K  ]","[  K  ]","[H:23 ]","[  T  ]"},
        new() {"[H:16 ]","[H:17 ]","[H:18 ]","[H:19 ]","[H:20 ]","[H:21 ]","[H:22 ]","[R:14 ]","[     ]",},
        new() {"[     ]","[     ]","[     ]","[     ]","[     ]","[     ]","[     ]","[R:15 ]","[     ]" },
        new() {"[R:1  ]","[R:2  ]","[R:3  ]","[R:4  ]","[     ]","[R:9  ]","[     ]","[     ]","[     ]" },
        new() {"[R:5  ]","[R:6  ]","[R:7  ]","[R:8  ]","[  E  ]","[R:10 ]","[R:11 ]","[R:12 ]","[R:13 ]" }
    };

        bool running = true;
        while (running)
        {
            Console.Clear();

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

    // reservation steps
    // how many check -----------------
    private static int HowManyCheck(string typeofreservation)
    {
        string[] nums = [];
        if (typeofreservation == "Regular")
        {
            nums = ["1", "2", "3", "4", "5", "6", "Quit"];
        }
        else if (typeofreservation == "HotSeat")
        {
            nums = ["1", "2", "3", "4", "5", "6", "7", "8", "Quit"];
        }
        string HowManycheck = Choice($"\nFor how many people? We have a max of {nums.Count()-1} per table.\n----------------------------------------", nums);
        if (int.TryParse(HowManycheck, out int HowMany))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine($"{HowMany} person(s) to be seated");
            Console.ResetColor();
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
    private static string TimeSlot()
    {
        string[] timeslots = ["Lunch (12:00 - 14:00)", "Dinner 1 (17:00 - 19:00)", "Dinner 2 (19:00 - 21:00)", "Dinner 3 (21:00 - 23:00)", "Return", "Quit"];
        string TimeSlot = Choice("\nWhat TimeSlot would you prefer: \n----------------------------------------\nPress 'Return' to go back to choosing amount of people.\n", timeslots);

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
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine($"Time slot {TimeSlot} chosen.");
            Console.ResetColor();
            System.Console.WriteLine("[enter]");
            System.Console.ReadLine();
            Console.Clear();

            return TimeSlot;

        }
    }
    private static DateTime ChooseDate(string TimeSlot, int HowMany, string type, bool modify)
    {
        // calender gets shown with all available dates 
        bool DateCheck = true;
        while (DateCheck)
        {

            string UncheckedDate = DisplayCalendarReservation(TimeSlot, HowMany, type, modify);
            if (DateTime.TryParse(UncheckedDate, out DateTime Date))
            {
                //checks if user filled in date not before today and not farther than 3 months in the future
                // can be more personalised in terms of what the user filled in wrong by making returns numbers
                if (reservationlogic.IsValidDate(Date))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine(Date.ToShortDateString() + " chosen");
                    Console.ResetColor();
                    System.Console.WriteLine("[enter]");
                    System.Console.ReadLine();
                    return Date;


                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("Invalid date entered. Try again");
                    Console.ResetColor();
                    System.Console.WriteLine("[enter]");
                    System.Console.ReadLine();
                }
            }
            // if it is equal to 20 means that they want to go back to timeslot chocie
            else if (UncheckedDate == "Return" && modify == false)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                System.Console.WriteLine("Going back to timeslot choice");
                Console.ResetColor();
                System.Console.WriteLine("[enter]");
                System.Console.ReadLine();
                Console.Clear();
                return default;
            }
            else if (UncheckedDate == "Return" && modify == true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine(UncheckedDate);
                Console.ResetColor();
                System.Console.WriteLine("[enter]");
                System.Console.ReadLine();
                return default;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine(UncheckedDate);
                Console.ResetColor();
                System.Console.WriteLine("[enter]");
                System.Console.ReadLine();

            }
        }
        return default;
    }

    private static int AvailableTablesHotSeat(DateTime Date, string TimeSlot)
    {
        ReservationLogic.AvailableTables.Clear();
        reservationlogic.AddHotSeatsAvailableTables();
        reservationlogic.CheckDateHotSeat(Date, TimeSlot);
        return ReservationLogic.AvailableTables.Count;

    }


    private static int AvailableTablesRegular(DateTime Date, string TimeSlot, int HowMany)
    {
        ReservationLogic.AvailableTables.Clear();
        reservationlogic.AddRegularSeats(HowMany);
        reservationlogic.CheckDate(Date, TimeSlot);
        return ReservationLogic.AvailableTables.Count;
    }
    // modify reservations ----------------------
    // // order: 
    // // amount of people
    // // timeslot 
    // // date (calender)
    // // choose table
    // // confirmation -- show receipt and write to all jsons

    // // hotseat possibilities 
    // // only order for hotseat if type is hot seat and only check for regular seat if reservation was for regular seat
    // // check if there are any tables available in both hot seat and regular seats 
    // // make calender show different colors if only hotseat or regular seats are available on this day 

    // // user can choose which reservation they want to modify
    // // user can choose to modify Amount of people
    // // if amount is bigger than max capacity of table then check if the there any other tables available
    // // if yes then show new table of reservation
    // // if no then user gets asked to enter another date
    // // table is chosen for them 

    // // user can choose to modify TimeSlot
    // // user can choose to modify the timeslot
    // // if no available tables
    // // user will get asked to enter


    // // ideas
    // // make every part of making a reservation a method so that it can be easily reusable 
    // // every modification needs a certain combination 
    // // put the functions in the right order get the right outcome.
    // // ask floor to make a function that displays all reservations and details and returns an id of the reservation that i want to modify


    public static void ModifyReservation(int clientID)
    {

        bool running = true;
        while (running)
        {
            string[] choicearr1 = { "Yes", "No" };
            string choice = Choice("Would you like to modify your reservation", choicearr1);
            if (choice == "Yes")
            {

                List<ReservationModel> reservations = reservationlogic.ModifyableReservations(clientID);
                if (reservations.Count > 0)
                {

                    List<string> ReservationsInformation = new();
                    foreach (ReservationModel res in reservations)
                    {
                        ReservationsInformation.Add($"Reservation ID: {res.Id}\nHow many People: {res.HowMany}\nDate: {res.Date}\nTime slot: {res.TimeSlot}\nStatus: {res.Status}\n");
                    }
                    int IndexSelectedReservation = HelperPresentation.ChooseItem("Select reservation for more information:", ReservationsInformation, 0);

                    ReservationModel reservation = reservations[IndexSelectedReservation];

                    System.Console.WriteLine("Selected Reservation: ");
                    System.Console.WriteLine($@"Reservation ID: {reservation.Id}");


                    if (reservation != null && reservations.Select(res => res.Id).Contains(reservation.Id))
                    {
                        List<TableModel> tables = reservationlogic.GetTablesByReservation(reservation);
                        string[] choiceArr = { "Amount of people", "Timeslot", "Date", "Food and drinks", "Return" };
                        string choice1 = Choice("What would you like to modify of this reservation", choiceArr);

                        if (choice1 == "Amount of people")
                        {
                            ModifyAmountOfPeople(reservation, tables);
                        }
                        else if (choice1 == "Timeslot")
                        {
                            ModifyTimeslot(reservation);
                        }
                        else if (choice1 == "Date")
                        {
                            ModifyDate(reservation);
                        }
                        else if (choice1 == "Food and drinks")
                        {
                            ModifyFoodAndDrinks(reservation);
                        }
                    }

                }
                else
                {
                    System.Console.WriteLine("There are no reservations to modify.");
                    System.Console.WriteLine("[enter]");
                    Console.ReadLine();
                    break;
                }
            }
            else if (choice == "No")
            {
                System.Console.WriteLine("Goodbye...");
                System.Console.WriteLine("[enter]");
                System.Console.ReadLine();
                break;
            }
        }
    }

    private static void ModifyAmountOfPeople(ReservationModel reservation, List<TableModel> tables)
    {
        int progress = 0;
        bool modify = true;
        string timeSlot = "";
        DateTime Date = default;
        string type = "";
        int amount = 0;
        int minCapacity;
        int maxCapacity;
        while (modify)
        {
            if (progress == 0)
            {
                System.Console.WriteLine("Choose the amount of people you want to sit with.");
                string[] amountarr = { "1", "2", "3", "4", "5", "6", "7", "8", "Quit" };
                string newAmount = Choice("Choose new amount: ", amountarr);
                if (int.TryParse(newAmount, out amount))
                {
                    progress = 10;

                }
                else if (newAmount == "Quit")
                {
                    System.Console.WriteLine("Goodbye....");
                    System.Console.WriteLine("[enter]");
                    System.Console.ReadLine();
                    break;
                }

            }
            else if (progress == 10)
            {
                // if you have a hotseat and go to 1 gonna be bad cause table max min cap == 1
                if (reservation.TypeOfReservation == "HotSeat")
                {
                    minCapacity = reservation.HowMany;
                    maxCapacity = reservation.HowMany;
                }
                else
                {
                    minCapacity = tables[0].MinCapacity;
                    maxCapacity = tables[0].MaxCapacity;
                }
                if (amount < minCapacity || amount > maxCapacity)
                {
                    if (amount < 7) // hotseats only  if above 6
                    {
                        System.Console.WriteLine("Looks like you're going to need  choose another timeslot ,date and seat for your reservation");
                        string[] TypeOfReservation = ["Regular", "HotSeat", "Return", "Cancel"];
                        type = Choice("What type of reservation would you like to make", TypeOfReservation);
                        if (type == "Return")
                        {
                            progress = 0;
                        }
                        else if (type == "Cancel")
                        {
                            break;
                        }
                        else
                        {
                            progress = 20;
                            Console.ForegroundColor = ConsoleColor.Green;
                            System.Console.WriteLine($"{type} reservation chosen");
                            Console.ResetColor();
                            System.Console.WriteLine("[enter]");
                            System.Console.ReadLine();

                        }
                    }
                    else if (amount > 6)
                    {


                        type = "HotSeat";
                        progress = 20;
                        Console.ForegroundColor = ConsoleColor.Green;
                        System.Console.WriteLine($"{type} reservation chosen");
                        Console.ResetColor();
                        System.Console.WriteLine("[enter]");
                        System.Console.ReadLine();


                    }


                }
                else if (amount == reservation.HowMany)
                {
                    System.Console.WriteLine("Amount of people is the same as the current reservation.");
                    System.Console.WriteLine("[enter]");
                    System.Console.ReadLine();
                    progress = 0;

                }
                else
                {
                    reservationlogic.ModifyReservation(reservation, amount);
                    Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine("Amount of people Modified");
                    Console.ResetColor();
                    System.Console.WriteLine("[enter]");
                    List<(FoodMenuModel, int)> foodcart = reservationlogic.GetReceiptById(reservation.Id).OrderedFood ?? [];//receipts 
                    List<string> Allergies = accountsLogic.GetById(reservation.ClientID).Allergies ?? [];
                    ReceiptModel receipt = reservationlogic.ModifyReceipt(reservation, foodcart);
                    System.Console.WriteLine(reservationlogic.DisplayReceipt(receipt, [Allergies]));
                    System.Console.ReadLine();
                    Console.Clear();
                    return;
                }
            }
            // // order: 
            // // timeslot
            else if (progress == 20)
            {
                timeSlot = Reservation.TimeSlot();
                System.Console.WriteLine("----------------------------------------");

                if (timeSlot == "Return")
                {
                    progress = 10;
                }
                else if (timeSlot == "Quit")
                {
                    break;
                }
                else
                {
                    progress = 30;
                }

            }
            // // date (calender)
            else if (progress == 30)
            {
                // i want all the tables to be removed from the tables and then added again when done with this part

                reservationlogic.TemporarilyUnassignTable(reservation.Id);
                Date = Reservation.ChooseDate(timeSlot, amount, type, true);

                if (Date != default)
                {
                    progress = 40;

                }
                else if (Date == default)
                {
                    progress = 20;
                    reservationlogic.AssignTables(reservation.TableID, reservation);
                }


            }
            else if (progress == 40)
            {
                // // choose table
                if (type == "Regular")
                {
                    int madereservation = RegularModifyReservation(reservation, Date, timeSlot, amount, type, reservation.FoodOrdered);
                    if (50 == madereservation)
                    {
                        // print reservation and make sure all receipt is modified aswell


                        break;

                    }
                    else if (30 == madereservation)
                    {
                        progress = 30;
                    }
                    // modify recept or just make another one
                }
                else if (type == "HotSeat")
                {
                    int madereservation = HotSeatModifyReservation(reservation, Date, timeSlot, amount, type, reservation.FoodOrdered);
                    if (50 == madereservation)
                    {

                        // print reservation and make sure all receipt is modified aswell
                        break;

                    }
                    else if (30 == madereservation)
                    {
                        progress = 30;
                    }
                }

            }

        }
    }



    private static void ModifyTimeslot(ReservationModel reservation)
    {
        int progress = 20;
        bool modify = true;
        string timeSlot = "";
        DateTime Date = default;
        string type = reservation.TypeOfReservation;
        int amount = reservation.HowMany;
        while (modify)
        {

            // // order: 
            // // timeslot
            if (progress == 20)
            {
                timeSlot = Reservation.TimeSlot();
                if (timeSlot == "Return")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("Cant go back");
                    Console.ResetColor();
                    System.Console.WriteLine("[enter]");
                    Console.ReadLine();
                }
                else if (timeSlot == "Quit")
                {
                    break;
                }
                else if (timeSlot == reservation.TimeSlot)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("You have already chosen this timeslot");
                    Console.ResetColor();
                    System.Console.WriteLine("[enter]");
                    Console.ReadLine();

                }
                else
                {
                    progress = 30;
                }

            }
            // // date (calender)
            else if (progress == 30)
            {
                reservationlogic.TemporarilyUnassignTable(reservation.Id);
                Date = Reservation.ChooseDate(timeSlot, amount, type, true);

                if (Date != default)
                {
                    progress = 40;

                }
                else if (Date == default)
                {
                    progress = 20;
                    reservationlogic.AssignTables(reservation.TableID, reservation);
                }


            }
            else if (progress == 40)
            {
                // // choose table
                if (type == "Regular")
                {
                    int madereservation = RegularModifyReservation(reservation, Date, timeSlot, amount, type, reservation.FoodOrdered);
                    if (50 == madereservation)
                    {
                        // print reservation and make sure all receipt is modified aswell

                        break;
                    }
                    else if (30 == madereservation)
                    {
                        progress = 30;
                    }
                    // modify recept or just make another one
                }
                else if (type == "HotSeat")
                {
                    int madereservation = HotSeatModifyReservation(reservation, Date, timeSlot, amount, type, reservation.FoodOrdered);
                    if (50 == madereservation)
                    {
                        // print reservation and make sure all receipt is modified aswell
                        break;

                    }
                    else if (30 == madereservation)
                    {
                        progress = 30;
                    }
                }

            }
            // // confirmation -- show receipt and write to all json
        }
    }


    private static void ModifyDate(ReservationModel reservation)
    {
        int progress = 30;
        bool modify = true;
        string timeSlot = reservation.TimeSlot;
        DateTime Date = default;
        string type = reservation.TypeOfReservation;
        int amount = reservation.HowMany;
        while (modify)
        {

            // // date (calender)
            if (progress == 30)
            {

                reservationlogic.TemporarilyUnassignTable(reservation.Id);
                Date = Reservation.ChooseDate(timeSlot, amount, type, true);

                if (Date != default)
                {
                    progress = 40;

                }
                else if (Date == default)
                {
                    break;
                }


            }
            else if (progress == 40)
            {
                // // choose table
                if (type == "Regular")
                {
                    int madereservation = RegularModifyReservation(reservation, Date,timeSlot, amount, type, reservation.FoodOrdered);
                    if (50 == madereservation)
                    {
                        // print reservation and make sure all receipt is modified aswell

                        break;
                    }
                    else if (30 == madereservation)
                    {
                        progress = 30;
                    }
                    // modify recept or just make another one
                }
                else if (type == "HotSeat")
                {
                    int madereservation = HotSeatModifyReservation(reservation, Date, timeSlot, amount, type, reservation.FoodOrdered);
                    if (50 == madereservation)
                    {
                        // print reservation and make sure all receipt is modified aswell
                        break;

                    }
                    else if (30 == madereservation)
                    {
                        progress = 30;
                    }
                }

            }
            // // confirmation -- show receipt and write to all json
        }
    }

    private static void ModifyFoodAndDrinks(ReservationModel reservation)
    {
        // Implement logic to modify food and drinks

        bool modify = true;
        while (modify)
        {
            if (reservation.FoodOrdered)
            {

                List<(FoodMenuModel, int)> foodcart = reservationlogic.GetReceiptById(reservation.Id).OrderedFood ?? [];//receipts 
                List<string> Allergies = accountsLogic.GetById(reservation.ClientID).Allergies ?? [];
                var (foodCart, allergies) = FoodOrderMenu.ModifyFood(foodcart, [Allergies]);
                // // choose table
                System.Console.WriteLine("Reservation Modified");
                System.Console.WriteLine("-----------------------------------------");
                System.Console.WriteLine("This is your receipt");

                ReceiptModel receipt = reservationlogic.ModifyReceipt(reservation, foodCart);
                System.Console.WriteLine(reservationlogic.DisplayReceipt(receipt, allergies));
                System.Console.WriteLine("[enter]");
                Console.ReadLine();
                break;


            }
            else
            {

                string[] yn = { "Yes", "No" };
                string choice = Choice("You had no food ordered.\nWould you like to add food to your reservation", yn);
                if (choice == "Yes")    
                {
                    reservation.FoodOrdered = true;
                    reservationAccess.WriteAll(reservationlogic._reservations);

                }
                else
                {
                    System.Console.WriteLine("Goodbye....");
                    System.Console.WriteLine("[enter]");
                    System.Console.ReadLine();
                    break;
                }
            }
        }
    }

    private static int RegularModifyReservation(ReservationModel reservation, DateTime Date, string TimeSlot, int HowMany, string type, bool foodOrdered)
    {

        AvailableTablesRegular(Date, TimeSlot, HowMany);
        // User gets shown restaurant and is shown what tables 
        System.Console.WriteLine("Where would you like to sit.\nChoose the table id of the table.");

        List<int> TableID = [DisplayRestaurant(Date, TimeSlot, HowMany)];


        if (TableID[0] > 0)
        {

            bool TableChoice = DisplayChosenSeats(TableID);
            System.Console.WriteLine("-----------------------------------------");
            System.Console.WriteLine(reservationlogic.displayTable(TableID[0]));


            if (TableChoice)
            {
                reservationlogic.ModifyReservation(reservation, TableID, HowMany, Date, TimeSlot, type, foodOrdered);
                reservationlogic.ModifyReservationTable(reservation, TableID);
                System.Console.WriteLine("Reservation Modified");
                System.Console.WriteLine("-----------------------------------------");
                System.Console.WriteLine("This is your receipt");
                List<(FoodMenuModel, int)> foodcart = reservationlogic.GetReceiptById(reservation.Id).OrderedFood ?? [];//receipts 
                List<string> Allergies = accountsLogic.GetById(reservation.ClientID).Allergies ?? [];
                ReceiptModel receipt = reservationlogic.ModifyReceipt(reservation, foodcart);
                System.Console.WriteLine(reservationlogic.DisplayReceipt(receipt, [Allergies]));
                System.Console.WriteLine("[enter]");
                Console.ReadLine();
                ReservationLogic.AvailableTables.Clear();
                return 50;

            }
            else if (TableChoice == false)
            {
                return 30;
            }

        }
        return 30;
    }
    private static int HotSeatModifyReservation(ReservationModel reservation, DateTime Date, string TimeSlot, int HowMany, string type, bool foodOrdered)
    {
        AvailableTablesHotSeat(Date, TimeSlot);

        List<int> tableIDs = ReservationLogic.ChooseSeats(HowMany);
        bool TableChoice = DisplayChosenSeats(tableIDs);
        if (TableChoice)
        {

            reservationlogic.ModifyReservation(reservation, tableIDs, HowMany, Date, TimeSlot, type, foodOrdered);
            reservationlogic.ModifyReservationTable(reservation, tableIDs);
            System.Console.WriteLine("Reservation Modified");
            System.Console.WriteLine("-----------------------------------------");
            System.Console.WriteLine("This is your receipt");

            List<(FoodMenuModel, int)> foodcart = reservationlogic.GetReceiptById(reservation.Id).OrderedFood ?? [];//receipts 
            List<string> Allergies = accountsLogic.GetById(reservation.ClientID).Allergies ?? [];
            ReceiptModel receipt = reservationlogic.ModifyReceipt(reservation, foodcart);
            System.Console.WriteLine(reservationlogic.DisplayReceipt(receipt, [Allergies]));

            System.Console.WriteLine("[enter]");
            Console.ReadLine();
            ReservationLogic.AvailableTables.Clear();

            return 50;



        }
        else if (TableChoice == false)
        {
            return 30;
        }

        return 30;
    }




}


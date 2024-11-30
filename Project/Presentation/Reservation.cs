
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;

static class Reservation
{
    static private ReservationLogic reservationlogic = new();
    static private RestaurantLogic restaurantLogic = new();
    // displays table restaurant

    public static void MakeReservation(string name, int clientID, string number, string email)
    {
        int cost = 50;
        string typeofreservation;
        bool reservation = true;
        while (reservation)
        {
            bool Client_answer = ChoicesLogic.YesOrNo("Hello would you like to make a reservation?");

            if (Client_answer)
            {
                bool dateCheck = true;
                bool howmanyCheck = true;
                bool tableIDcheck = true;
                DateTime Date = default;
                string TimeSlot = "";
                int HowMany = 0;
                while (howmanyCheck)
                {
                    System.Console.WriteLine("\nFor how many people? We can seat a maximum of 6 people at one table");

                    string howMany = Console.ReadLine();
                    if (int.TryParse(howMany, out int HowManycheck))
                    {
                        if (HowManycheck >= 1 && HowManycheck <= 6)
                        {
                            HowMany += HowManycheck;
                            // available tables check
                            reservationlogic.CheckMin_MaxCapacity(HowMany);
                            howmanyCheck = false;
                        }
                        else if (HowManycheck < 1 || HowManycheck > 6)
                        {
                            System.Console.WriteLine("no table found with amount of people. Enter again...");
                            System.Console.WriteLine("[enter]");
                            System.Console.ReadLine();

                        }
                    }
                    else
                    {
                        System.Console.WriteLine("invalid input please fill in digit");
                        System.Console.WriteLine("[enter]");
                        System.Console.ReadLine();
                    }

                }

                while (dateCheck)
                {
                    System.Console.WriteLine("------------------------------------------------------");
                    System.Console.Write("On what day would you like to reserve a table? ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine("(mm/dd/yyyy))");
                    Console.ResetColor();
                    System.Console.WriteLine(@"'You can only book 3 months in advanced'");
                    // check if valid date
                    string UncheckedDate = Console.ReadLine();
                    if (DateTime.TryParse(UncheckedDate, out Date))
                    {
                        //checks if user filled in date not before today and not farther than 3 months in the future
                        // can be more personalised in terms of what the user filled in wrong by making returns numbers
                        if (reservationlogic.IsValidDate(Date))
                        {
                            if (restaurantLogic.closed_Day(Date))
                            {
                                Console.WriteLine("Sorry, the restaurant is closed that day.");
                                Console.WriteLine($"The next open day is: {restaurantLogic.next_Open_Day(Date)}");
                            }
                            else
                            {
                                dateCheck = false;
                            }
                            bool timeslotbool = true;
                            while (timeslotbool)
                            {
                                System.Console.WriteLine("In what timeslot would you like to book your reservation: \n1. 12:00 - 14:00\n2. 17:00 - 19:00\n3. 19:00 - 21:00\n4. 21:00 - 23:00\nChoose id:");
                                string timeslotIdcheck = Console.ReadLine();
                                TimeSlot = ReservationLogic.TimSlotChooser(timeslotIdcheck);
                                if (TimeSlot != null)
                                {
                                    //    check if valid time slot
                                    System.Console.WriteLine($"Time slot {TimeSlot} chosen.");
                                    System.Console.WriteLine("[enter]");
                                    System.Console.ReadLine();
                                    timeslotbool = false;

                                }
                                else
                                {
                                    System.Console.WriteLine("Invalid ID entered. Try again");
                                    System.Console.WriteLine("[enter]");
                                    System.Console.ReadLine();
                                }
                            }
                        }
                        else
                        {
                            System.Console.WriteLine("Invalid date entered. Try again");
                            System.Console.WriteLine("[enter]");
                            System.Console.ReadLine();
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("Invalid date entered. Try again");
                        System.Console.WriteLine("[enter]");
                        System.Console.ReadLine();
                    }
                }

                // check if not table is not already booked at same day/time
                reservationlogic.CheckDate(Date, TimeSlot);
                // show available tables
                if (reservationlogic.AvailableTables.Count == 0)
                {
                    System.Console.WriteLine("There are no available tables for your preferences.");
                    System.Console.WriteLine("[enter]");
                    Console.ReadLine();
                    break;
                }

                while (tableIDcheck)
                {



                    // choose tables check
                    System.Console.WriteLine("Where would you like to sit.\nChoose the table id of the table.");
                    bool TableIdValid = false;
                    int TableID = reservationlogic.DisplayRestaurant();
                    if (TableID == 0)
                    {
                        break;
                    }
                    typeofreservation = reservationlogic.TypeOfReservation(TableID);

                    // check given id is one of available tables id
                    foreach (var table in reservationlogic.AvailableTables)
                    {
                        if (table.Id == TableID)
                        {

                            bool hotSeatConfirmation = true;

                            if (typeofreservation == "HotSeat")
                            {
                                while (hotSeatConfirmation)
                                {
                                    Console.WriteLine("Are you sure you want to sit a HotSeat? (Y/N)");
                                    Console.WriteLine("It cost € 10 extra");
                                    string choiceHotSeat = Console.ReadLine().ToUpper();
                                    if (choiceHotSeat == "Y")
                                    {
                                        hotSeatConfirmation = false;
                                        TableIdValid = true;
                                        cost = 60;
                                        Console.WriteLine("HotSeat chosen");
                                        Console.WriteLine("[enter]");
                                        Console.ReadLine();
                                    }
                                    else if (choiceHotSeat == "N")
                                    {
                                        hotSeatConfirmation = false;
                                        Console.WriteLine("going back.....");
                                        Console.WriteLine("[enter]");
                                        Console.ReadLine();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input1");
                                        Console.WriteLine("[enter]");
                                        Console.ReadLine();
                                    }
                                }
                            }
                            else
                            {
                                TableIdValid = true;
                            }
                        }

                    }


                    if (TableIdValid)
                    {
                        System.Console.WriteLine("-----------------------------------------");
                        System.Console.WriteLine("Do you want to sit a this table?(Y/N)\n");
                        System.Console.WriteLine(reservationlogic.displayTable(TableID)); ;

                        string confirmation = Console.ReadLine().ToUpper();
                        if (confirmation == "Y")
                        {



                            ReservationModel Reservation = reservationlogic.Create_reservation(TableID, name, clientID, HowMany, Date, typeofreservation, TimeSlot);

                            ReceiptModel receipt = reservationlogic.CreateReceipt(Reservation, cost, number, email);


                            System.Console.WriteLine();
                            System.Console.WriteLine("This is your receipt for now: ");

                            System.Console.WriteLine(reservationlogic.DisplayReceipt(receipt));
                            System.Console.WriteLine("reservation created");
                            System.Console.WriteLine("[enter]");
                            Console.ReadLine();
                            reservation = false;
                            tableIDcheck = false;
                            reservationlogic.AvailableTables.Clear();
                            Console.Clear();

                        }
                        else
                        {
                            cost = 50;
                        }
                    }



                }
            }
            else if (Client_answer == false)
            {
                System.Console.WriteLine("Goodbye....");
                System.Console.WriteLine("[enter]");
                System.Console.ReadLine();
                reservation = false;
            }
            else
            {
                System.Console.WriteLine("invalid choice please choose (Y/N).");
                System.Console.WriteLine("[enter]");
                System.Console.ReadLine();
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
            System.Console.WriteLine("Would you like to cancel a reservation? (Y/N)");
            string choice = Console.ReadLine().ToUpper();
            if (choice == "Y")
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

                    reservationlogic.RemoveReservationByID(cancelID);
                    Canceling = false;
                }
            }
            else if (choice == "N")
            {
                System.Console.WriteLine("Goodbye....");
                System.Console.WriteLine("[enter]");
                System.Console.ReadLine();
                cancelreservation = false;
            }
            else System.Console.WriteLine("Incorrect input");
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
}
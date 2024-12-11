
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;


// order of working
// user gets asked for how many people user wants to reserve
// user gets asked if they want hotseats or regular seats
// how do you check if tables are next to each other
// 
// user gets asked timeslot
//user gets shown  calender
// if not hot seat: user gets option to choose table.
// If there are no available hot seats, user gets notified that there are no available hot seats for the amount of people and can choose again.
// user gets asked confirmation
static class Reservation
{
    static private ReservationLogic reservationlogic = new();

    // displays table restaurant

    public static void MakeReservation(string name, int clientID, string number, string email)
    {
        int cost = 50;
        string typeofreservation;
        bool reservation = true;
        while (reservation)
        {
            // user has to enter yes or no to go further
            bool Client_answer = ChoicesLogic.YesOrNo("Hello would you like to make a reservation?");

            if (Client_answer)
            {
                bool dateCheck = true;
                bool howmanyCheck = true;
                bool tableIDcheck = true;
                DateTime Date = default;
                string TimeSlot = "";
                int HowMany = 0;

                // user gets asked with how many people are and check is done
                while (howmanyCheck)
                {
                    System.Console.WriteLine("\nFor how many people? We can seat a maximum of 6 people at one table");

                    string howMany = Console.ReadLine();
                    if (int.TryParse(howMany, out int HowManycheck))
                    {
                        if (HowManycheck >= 1 && HowManycheck <= 6)
                        {
                            HowMany += HowManycheck;


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
                bool timeslotbool = true;
                // user gets to choose timeslot of reservation
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


                while (dateCheck)
                {
                    // calender gets shown with all available dates 
                    System.Console.WriteLine(@"'You can only book 3 months in advanced'");
                    // check if valid date
                    string UncheckedDate = reservationlogic.DisplayCalendarReservation(TimeSlot, HowMany);
                    if (DateTime.TryParse(UncheckedDate, out Date))
                    {
                        //checks if user filled in date not before today and not farther than 3 months in the future
                        // can be more personalised in terms of what the user filled in wrong by making returns numbers
                        if (reservationlogic.IsValidDate(Date))
                        {
                            // if (restaurantLogic.closed_Day(Date))
                            // {
                            //     Console.WriteLine("Sorry, the restaurant is closed that day.");
                            //     Console.WriteLine($"The next open day is: {restaurantLogic.next_Open_Day(Date)}");
                            // }
                            // else
                            // {
                            dateCheck = false;
                            // }
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
                        System.Console.WriteLine(UncheckedDate);
                        System.Console.WriteLine("[enter]");
                        System.Console.ReadLine();
                    }
                }

                // available table check
                // happens down here to not override other methods (DisplayCalendarReservation)
                reservationlogic.CheckMin_MaxCapacity(HowMany);
                reservationlogic.CheckDate(Date, TimeSlot);

                while (tableIDcheck)
                {
                    // User gets shown restaurant and is shown what tables 
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
                            string foodorder = "Would you like to order food in advance?";
                            System.Console.WriteLine("-----------------------------------------");
                            System.Console.WriteLine($"{foodorder}\n");
                            bool orderfood = ChoicesLogic.YesOrNo(foodorder);

                            if (orderfood)
                            {
                                var (foodCart, allergies) = FoodOrderMenu.OrderFood();
                                ReservationModel Reservation = reservationlogic.Create_reservation(TableID, name, clientID, HowMany, Date, typeofreservation, TimeSlot, true);

                                ReceiptModel receipt = reservationlogic.CreateReceipt(Reservation, cost, number, email, foodCart);

                                System.Console.WriteLine();
                                System.Console.WriteLine("This is your receipt for now: ");
                                System.Console.WriteLine(reservationlogic.DisplayReceipt(receipt, allergies));
                                System.Console.WriteLine("Reservation created");
                                System.Console.WriteLine("[enter]");
                                Console.ReadLine();

                                reservation = false;
                                tableIDcheck = false;
                                reservationlogic.AvailableTables.Clear();
                                Console.Clear();
                            
                        
                            }
                            else
                            {
                                ReservationModel Reservation = reservationlogic.Create_reservation(TableID, name, clientID, HowMany, Date, typeofreservation, TimeSlot, false);

                                ReceiptModel receipt = reservationlogic.CreateReceipt(Reservation, cost, number, email, []);
                                System.Console.WriteLine();
                                System.Console.WriteLine("This is your receipt for now: ");

                                System.Console.WriteLine(reservationlogic.DisplayReceipt(receipt, []));
                                System.Console.WriteLine("reservation created");
                                System.Console.WriteLine("[enter]");
                                Console.ReadLine();
                                reservation = false;
                                tableIDcheck = false;
                                reservationlogic.AvailableTables.Clear();
                                Console.Clear();

                            }

                        }
                        else
                        {
                            cost = 50;
                        }
                    }



                }
            }
            else
            {
                System.Console.WriteLine("Goodbye....");
                System.Console.WriteLine("[enter]");
                System.Console.ReadLine();
                reservation = false;
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
            if (ChoicesLogic.YesOrNo("Would you like to cancel a reservation? (Y/N)"))
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
                    bool cancelReservation = ChoicesLogic.YesOrNo($"Are you sure you want to cancel this reservation:\nId: {toCancelReservation.Id}\nName: {toCancelReservation.Name}\nTotal people: {toCancelReservation.HowMany}\nDate: {toCancelReservation.Date.ToShortDateString()} {toCancelReservation.TimeSlot}\nType of reservation: {toCancelReservation.TypeOfReservation}");
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
}

using System.Runtime.Intrinsics.Arm;

static class Reservation
{
    static private ReservationLogic reservationlogic = new();
    // displays table restaurant

    public static void MakeReservation(string name, int clientID)
    {

        bool reservation = true;
        while (reservation)
        {
            System.Console.WriteLine("Hello would you like to make a reservation ? (Y/N) ");
            string choice = Console.ReadLine().ToUpper();

            if (choice == "Y")
            {
                bool dateCheck = true;
                bool howmanyCheck = true;
                bool tableIDcheck = true;
                DateTime Date = default;
                int HowMany = 0;
                while (howmanyCheck)
                {
                    System.Console.WriteLine("For how many people?");

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
                    System.Console.WriteLine("On what day would you like to reserve a table ? (mm/dd/yyyy)");
                    System.Console.WriteLine(@"'You can only book 3 months in advanced'");
                    // check if valid date
                    string UncheckedDate = Console.ReadLine();
                    if (DateTime.TryParse(UncheckedDate, out Date))
                    {
                        //checks if user filled in date not before today and not farther than 3 months in the future
                        // can be more personalised in terms of what the user filled in wrong by making returns numbers
                        if (reservationlogic.IsValidDate(Date))
                        {
                            reservationlogic.IsValidDate(Date);
                            dateCheck = false;
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
                reservationlogic.CheckDate(Date);
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

                    System.Console.WriteLine("These are the available tables:");
                    System.Console.WriteLine(reservationlogic.PrintAvailableTables()); ;

                    // choose tables check
                    System.Console.WriteLine("Where would you like to sit.\nChoose the id of the table.");
                    bool TableIdValid = false;
                    string tableID = Console.ReadLine();
                    if (int.TryParse(tableID, out int TableID))
                    {
                        // check given id is one of available tables id
                        foreach (var table in reservationlogic.AvailableTables)
                        {
                            if (table.Id == TableID)
                            {
                                TableIdValid = true;
                            }

                        }

                        if (TableIdValid)
                        {

                            System.Console.WriteLine("Do you want to sit a this table?(Y/N)");
                            System.Console.WriteLine(reservationlogic.displayAvailableTable(TableID)); ;

                            string confirmation = Console.ReadLine().ToUpper();
                            if (confirmation == "Y")
                            {
                                ReservationModel Reservation = reservationlogic.Create_reservation(TableID, name, clientID, HowMany, Date);
                                TableAccess.LoadAllTables();
                                ReservationAccess.LoadAllReservations();
                                System.Console.WriteLine(reservationlogic.DisplayReservation(Reservation.Id));
                                System.Console.WriteLine();
                                System.Console.WriteLine("reservation created");
                                System.Console.WriteLine("[enter]");
                                Console.ReadLine();
                                reservation = false;
                                tableIDcheck = false;
                            }
                        }
                        else
                        {
                            System.Console.WriteLine("Table ID is not valid choice.");
                            System.Console.WriteLine("[enter]");
                            System.Console.ReadLine();
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
            else if (choice == "N")
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
                        System.Console.WriteLine(reservationlogic.DisplayReservations(clientID)); ;
                        System.Console.WriteLine("enter ID: ");
                        string str_id = Console.ReadLine();
                        {
                            if (int.TryParse(str_id, out int reservationid))
                            {
                                // can only choose id if in reserved by person
                                if (reservationlogic.DisplayReservation(reservationid) != null && reservationlogic.IsReservationInAccount(clientID, reservationid).Contains(reservationid))
                                {

                                    ReservationIDCheck = false;
                                    bool confirmation = true;
                                    while (confirmation)
                                    {

                                        System.Console.WriteLine(reservationlogic.DisplayReservation(reservationid));
                                        System.Console.WriteLine("Are You sure you want to cancel this reservation? (Y/N)");

                                        string choice2 = Console.ReadLine().ToUpper();
                                        if (choice2 == "Y")
                                        {
                                            Console.Clear();
                                            ReservationModel reservation = reservationlogic.GetReservationById(reservationid);
                                            System.Console.WriteLine(reservationlogic.DisplayReservation(reservation.Id) + " has been canceled.\n");
                                            reservationlogic.RemoveReservationByID(reservationid);
                                            TableAccess.LoadAllTables();
                                            ReservationAccess.LoadAllReservations();
                                            System.Console.WriteLine("[enter]");
                                            Console.ReadLine();

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
}
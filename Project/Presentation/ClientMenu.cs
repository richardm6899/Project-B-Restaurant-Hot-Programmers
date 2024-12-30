class ClientMenu
{
    static private ReservationLogic reservationLogic = new ReservationLogic();
    static private AccountsLogic accountsLogic = new();
    public static void Start(AccountModel acc, AccountsLogic accountsLogic)
    {
        string[] options =
        {
            "Make a Reservation.",
            "Modify a Reservation.",
            "Cancel a reservation.",
            "See your Reservations.",
            "See the food menu.",
            "See the Restaurant info.",
            "See your accounts data.",
            "Modify your accounts data.",
            "Check your Messages.",
            "Deactivate or Delete your account.",
            "Log out.",
        };
        bool clientMenu = true;
        while (clientMenu)
        {
            int selectedIndex = 0;
            Console.Clear();
            string mainPrompt = @$"Welcome to the client menu.
-----------------------------------------
Welcome back {acc.FullName}
-----------------------------------------";
            selectedIndex = HelperPresentation.ChooseOption(mainPrompt, options, selectedIndex);

            switch (selectedIndex)
            {
                // make reservation
                //  public static void MakeReservation(string name, int clientID, string number, string email)
                case 0:
                    MakeReservation(acc);
                    break;

                case 1:
                    // modify reservation
                    ModifyReservation(acc);
                    break;
                // cancel reservation
                case 2:

                    CancelReservation(acc);
                    break;
                // see accounts reservation
                case 3:
                    SeeReservations(acc);
                    break;

                //  see the food menu
                case 4:
                    FoodMenuDisplay.StartFoodMenu();
                    break;

                // see restaurant info
                case 5:
                    RestaurantInfo.Start();
                    break;

                //  show account data
                case 6:
                    SeeData(acc);
                    break;
                // log out

                case 7:
                    ModifyData.Start(acc);
                    break;
                case 8:
                    Messages(acc);
                    break;


                case 9:
                    DeleteDeactivate(acc);
                    break;
                case 10:
                    acc = null;
                    clientMenu = false;
                    Menu.Start();
                    break;

                default:
                    System.Console.WriteLine("Invalid input");
                    Start(acc, accountsLogic);
                    break;
            }
        }
    }


    // case 0 make reservation
    private static void MakeReservation(AccountModel acc)
    {
        System.Console.WriteLine("Make reservation:");
        Reservation.MakeReservation(acc.FullName, acc.Id, acc.PhoneNumber, acc.EmailAddress);
    }
    private static void ModifyReservation(AccountModel acc)
    {
        System.Console.WriteLine("Modify reservation:");
        Reservation.ModifyReservation(acc.Id);
    }

    // case 1 cancel reservation
    private static void CancelReservation(AccountModel acc)
    {
        System.Console.WriteLine("Cancel reservation");
        List<ReservationModel> toCheckOngoingReservations = reservationLogic.AllOngoingReservationsByID(acc.Id);
        if (toCheckOngoingReservations.Count() >= 0)
        {
            Reservation.CancelReservation(acc.Id);
        }
        else System.Console.WriteLine("You have no reservations on this account.");

    }

    // case 2
    private static void SeeReservations(AccountModel acc)
    {
        string[] options =
        {
            "All reservations.",
            "All ongoing reservations.",
            "All past reservations.",
            "All canceled reservations.",
            "Search reservation by date",
            "Select reservation for more information",
            "Return",

        };

        bool seeingReservations = true;

        while (seeingReservations)
        {
            int selectedIndex = 0;
            Console.Clear();

            selectedIndex = HelperPresentation.ChooseOption("How would you like to see your reservations?", options, selectedIndex);

            switch (selectedIndex)
            {
                case 0:

                    List<ReservationModel> Reservations = reservationLogic.DisplayAllReservationsByStatusAndID(acc.Id, null);
                    foreach (ReservationModel reservation in Reservations)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("---------------------------------------------------");
                        Console.ResetColor();
                        System.Console.WriteLine($"Name: {reservation.Name}\nTable Id: {reservation.TableID[0]}\nAmount of people: {reservation.HowMany}\nDate: {reservation.Date.ToShortDateString()} {reservation.TimeSlot}\nStatus: {reservation.Status}\nType: {reservation.TypeOfReservation}");
                    }
                    Console.ReadKey();
                    break;

                case 1:
                    List<ReservationModel> ongoingReservations = reservationLogic.DisplayAllReservationsByStatusAndID(acc.Id, "Ongoing");
                    if (ongoingReservations.Count() <= 0)
                    {
                        System.Console.WriteLine("You have no ongoing reservations.");
                        Console.ReadKey();
                        break;
                    }
                    foreach (ReservationModel reservation in ongoingReservations)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        System.Console.WriteLine("---------------------------------------------------");
                        Console.ResetColor();
                        System.Console.WriteLine($"Name: {reservation.Name}\nTable Id: {reservation.TableID[0]}\nAmount of people: {reservation.HowMany}\nDate: {reservation.Date.ToShortDateString()} {reservation.TimeSlot}\nType: {reservation.TypeOfReservation}");
                    }
                    Console.ReadKey();
                    break;

                case 2:
                    List<ReservationModel> pastReservations = reservationLogic.DisplayAllReservationsByStatusAndID(acc.Id, "Past");
                    if (pastReservations.Count() <= 0)
                    {
                        System.Console.WriteLine("You have no past reservations.");
                        Console.ReadKey();
                        break;
                    }
                    foreach (ReservationModel reservation in pastReservations)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        System.Console.WriteLine("---------------------------------------------------");
                        Console.ResetColor();
                        System.Console.WriteLine($"Name: {reservation.Name}\nTable Id: {reservation.TableID[0]}\nAmount of people: {reservation.HowMany}\nDate: {reservation.Date.ToShortDateString()} {reservation.TimeSlot}\nType: {reservation.TypeOfReservation}");
                    }
                    Console.ReadKey();
                    break;

                case 3:
                    List<ReservationModel> canceledReservations = reservationLogic.DisplayAllReservationsByStatusAndID(acc.Id, "Canceled");
                    if (canceledReservations.Count() <= 0)
                    {
                        System.Console.WriteLine("You have no canceled reservations.");
                        Console.ReadKey();
                        break;
                    }
                    foreach (ReservationModel reservation in canceledReservations)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        System.Console.WriteLine("---------------------------------------------------");
                        Console.ResetColor();
                        System.Console.WriteLine($"Name: {reservation.Name}\nTable Id: {reservation.TableID[0]}\nAmount of people: {reservation.HowMany}\nDate: {reservation.Date.ToShortDateString()} {reservation.TimeSlot}\nType: {reservation.TypeOfReservation}");
                    }
                    Console.ReadKey();
                    break;

                case 4:
                    bool client_searchesDate = true;
                    do
                    {
                        System.Console.WriteLine("Enter date to look up dd/mm/yyyy");
                        string? UncheckedDate = Console.ReadLine();

                        if (DateTime.TryParse(UncheckedDate, out DateTime date))
                        {
                            List<ReservationModel> dateReservations = reservationLogic.DisplayAllReservationsByDateAndID(acc.Id, date);
                            foreach (ReservationModel reservation in dateReservations)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                System.Console.WriteLine("---------------------------------------------------");
                                Console.ResetColor();
                                System.Console.WriteLine($"Name: {reservation.Name}\nTable Id: {reservation.TableID[0]}\nAmount of people: {reservation.HowMany}\nDate: {reservation.Date.ToShortDateString()} {reservation.TimeSlot}\nType: {reservation.TypeOfReservation}");
                            }
                            if (dateReservations.Count() == 0)
                            {
                                System.Console.WriteLine("No reservations found on this day.");
                            }
                            Console.ReadKey();
                            client_searchesDate = false;
                        }
                        else
                        {
                            System.Console.WriteLine("Invalid input, please try again");
                        }
                    } while (client_searchesDate);
                    break;

                case 5:
                    int reservationSelectedIndex = 0;
                    List<ReservationModel> allReservations = reservationLogic.DisplayAllReservationsByStatusAndID(acc.Id, null);
                    List<string> ReservationsInformation = new();
                    foreach (ReservationModel reservation in allReservations)
                    {
                        ReservationsInformation.Add($"Reservation ID: {reservation.Id}\nHow many People: {reservation.HowMany}\nDate: {reservation.Date}\nTime slot: {reservation.TimeSlot}\nStatus: {reservation.Status}\n");
                    }
                    int IndexSelectedReservation = HelperPresentation.ChooseItem("Select reservation for more information:", ReservationsInformation, reservationSelectedIndex);

                    ReservationModel selectedReservation = allReservations[IndexSelectedReservation];

                    System.Console.WriteLine("Selected Reservation: ");
                    System.Console.WriteLine($@"Reservation ID: {selectedReservation.Id},");
                    System.Console.Write("Table Id/Ids: ");
                    foreach (int ids in selectedReservation.TableID)
                    {
                        System.Console.Write($"{ids}, ");
                    }
                    System.Console.WriteLine();
                    System.Console.WriteLine($@"Name: {selectedReservation.Name},
ClientID: {selectedReservation.ClientID},
How many People: {selectedReservation.HowMany},
Date: {HelperPresentation.DateTimeToReadableDate(selectedReservation.Date)},
Time slot: {selectedReservation.TimeSlot},
Status: {selectedReservation.Status},
Type of Reservation: {selectedReservation.TypeOfReservation},");
                    if (selectedReservation.FoodOrdered)
                    {
                        System.Console.WriteLine("Has food been ordered: yes");
                    }
                    else System.Console.WriteLine("Has food been ordered: no");

                    Console.ReadKey();
                    break;

                case 6:
                    return;

                default:
                    System.Console.WriteLine("Invalid input");
                    Start(acc, accountsLogic);
                    break;
            }
        }
    }

    // case 5
    private static void SeeData(AccountModel acc)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        System.Console.WriteLine("Your accounts data:");
        Console.ResetColor();

        System.Console.WriteLine(@$"Name: {acc.FullName}
Birthdate: {HelperPresentation.DateTimeToReadableDate(acc.Birthdate)}
Email: {acc.EmailAddress}
Phone Number: {acc.PhoneNumber}
Password: {acc.Password}");
        if (acc.Allergies.Count() != 0)
        {
            System.Console.WriteLine("Allergies: ");
            foreach (string allergies in acc.Allergies)
            {
                System.Console.WriteLine(allergies);
            }
        }
        else System.Console.WriteLine("No allergies given");

        System.Console.WriteLine("Press [enter] to continue.");
        Console.ReadKey();
    }

    // case 7
    private static void Messages(AccountModel acc)
    {
        Console.Clear();
        System.Console.WriteLine("Messages:");
        if (MessageLogic.Inbox(acc.Id))
        {
            Console.WriteLine("You have 1 new message");
            Console.ReadLine();
            Console.WriteLine("[enter]");
            // foreach (var reservation in reservationLogic._reservations)
            // {
            Console.WriteLine($"Your reservation has been canceled");
            Console.ReadLine();
            Console.WriteLine("[enter]");
            Console.Clear();
            // }
        }
        else
        {
            Console.WriteLine("You have no new messages");
            Console.ReadLine();
            Console.WriteLine("[enter]");
            Console.Clear();
        }
    }


    // case 8
    private static void DeleteDeactivate(AccountModel acc)
    {
        Console.Clear();
        bool deactivateDeletingAccount = true;
        while (deactivateDeletingAccount)
        {
            System.Console.WriteLine("Enter 1 to deactivate account.");
            System.Console.WriteLine("Enter 2 to delete account.");
            System.Console.WriteLine("Enter 3 to return.");
            string? userDeleteDeactivate = Console.ReadLine();
            switch (userDeleteDeactivate)
            {
                case "1":
                    bool userDeactivate = HelperPresentation.YesOrNo("Are you sure you want to deactivate your account?");
                    if (userDeactivate)
                    {
                        System.Console.WriteLine("Please re-enter your password.");
                        string? passToCheck = Console.ReadLine();
                        if (accountsLogic.ReCheckPassWord(acc, passToCheck))
                        {
                            accountsLogic.deactivateAccount(acc.Id);
                            System.Console.WriteLine("Account has been deactivated. You will be returned to the main menu.");
                            Console.ReadKey();
                            acc = null;
                            Menu.Start();
                        }
                        System.Console.WriteLine("Incorrect password.");
                        deactivateDeletingAccount = false;

                    };
                    break;

                case "2":
                    bool userDelete = HelperPresentation.YesOrNo("Are you sure you want to delete your account?");
                    if (userDelete)
                    {
                        System.Console.WriteLine("Please re-enter your password.");
                        string passToCheck = Console.ReadLine();
                        if (accountsLogic.ReCheckPassWord(acc, passToCheck))
                        {
                            accountsLogic.deleteAccount(acc.Id);
                            System.Console.WriteLine("Account has been deleted. You will be returned to the main menu.");
                            Console.ReadKey();
                            acc = null;
                            Menu.Start();
                        }
                        System.Console.WriteLine("Incorrect password.");
                        deactivateDeletingAccount = false;
                    };
                    break;

                case "3":
                    deactivateDeletingAccount = false;
                    break;

                default:
                    System.Console.WriteLine("Invalid input.");
                    break;
            }
        }
    }

}
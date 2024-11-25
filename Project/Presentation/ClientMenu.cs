class ClientMenu
{
    static private ReservationLogic reservationLogic = new ReservationLogic();
    public static void Start(AccountModel acc, AccountsLogic accountsLogic)
    {
        bool clientmenu = true;
        while (clientmenu)
        {
            System.Console.WriteLine("Welcome to the client menu.");
            System.Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Welcome back " + acc.FullName);
            System.Console.WriteLine("-----------------------------------------");
            System.Console.WriteLine("Enter 1 to make a reservation.");
            System.Console.WriteLine("Enter 2 to cancel a reservation.");
            System.Console.WriteLine("Enter 3 to see your reservations.");
            System.Console.WriteLine("Enter 4 to see the food menu.");
            System.Console.WriteLine("Enter 5 to see the restaurant info.");
            System.Console.WriteLine("Enter 6 to see your accounts data.");
            System.Console.WriteLine("Enter 7 to modify your data.");
            System.Console.WriteLine("Enter 8 to log out");

            string user_logged_in_answer = System.Console.ReadLine();
            switch (user_logged_in_answer)
            {
                // make reservation
                //  public static void MakeReservation(string name, int clientID, string number, string email)
                case "1":
                    System.Console.WriteLine("Make reservation:");
                    Reservation.MakeReservation(acc.FullName, acc.Id, acc.PhoneNumber, acc.EmailAddress);


                    break;
                // cancel reservation
                case "2":
                    System.Console.WriteLine("Cancel reservation");
                    Reservation.CancelReservation(acc.Id);


                    break;
                // see accounts reservation
                case "3":
                    System.Console.WriteLine("Your reservations: ");
                    System.Console.WriteLine("1. All reservations.");
                    System.Console.WriteLine("2. All ongoing reservations.");
                    System.Console.WriteLine("3. All past reservations");
                    System.Console.WriteLine("4. All canceled reservations");
                    System.Console.WriteLine("5. Search reservation by date.");
                    System.Console.WriteLine("6. Return.");
                    string user_reservation_answer = Console.ReadLine();
                    switch (user_reservation_answer)
                    {
                        case "1":
                            List<ReservationModel> Reservations = reservationLogic.DisplayAllReservationsByClientID(acc.Id);
                            foreach (ReservationModel reservation in Reservations)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                System.Console.WriteLine("---------------------------------------------------");
                                Console.ResetColor();
                                System.Console.WriteLine($"Name: {reservation.Name}\nTable Id: {reservation.TableID}\nAmount of people: {reservation.HowMany}\nDate: {reservation.Date.ToShortDateString()} {reservation.TimeSlot}\nStatus: {reservation.Status}");
                            }
                            Console.ReadKey();
                            break;

                        case "2":
                            List<ReservationModel> ongoingReservations = reservationLogic.DisplayAllReservationsByStatusAndID(acc.Id, "Ongoing");
                            foreach (ReservationModel reservation in ongoingReservations)
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                System.Console.WriteLine("---------------------------------------------------");
                                Console.ResetColor();
                                System.Console.WriteLine($"Name: {reservation.Name}\nTable Id: {reservation.TableID}\nAmount of people: {reservation.HowMany}\nDate: {reservation.Date.ToShortDateString()} {reservation.TimeSlot}");
                            }
                            Console.ReadKey();
                            break;

                        case "3":
                            List<ReservationModel> pastReservations = reservationLogic.DisplayAllReservationsByStatusAndID(acc.Id, "Past");
                            foreach (ReservationModel reservation in pastReservations)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                System.Console.WriteLine("---------------------------------------------------");
                                Console.ResetColor();
                                System.Console.WriteLine($"Name: {reservation.Name}\nTable Id: {reservation.TableID}\nAmount of people: {reservation.HowMany}\nDate: {reservation.Date.ToShortDateString()} {reservation.TimeSlot}");
                            }
                            Console.ReadKey();
                            break;

                        case "4":
                            List<ReservationModel> canceledReservations = reservationLogic.DisplayAllReservationsByStatusAndID(acc.Id, "Canceled");
                            foreach (ReservationModel reservation in canceledReservations)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                System.Console.WriteLine("---------------------------------------------------");
                                Console.ResetColor();
                                System.Console.WriteLine($"Name: {reservation.Name}\nTable Id: {reservation.TableID}\nAmount of people: {reservation.HowMany}\nDate: {reservation.Date.ToShortDateString()} {reservation.TimeSlot}");
                            }
                            Console.ReadKey();
                            break;

                        case "5":
                            System.Console.WriteLine("Enter date to look up dd/mm/yyyy");
                            string UncheckedDate = Console.ReadLine();
                            if (DateTime.TryParse(UncheckedDate, out DateTime date))
                            {
                                List<ReservationModel> dateReservations = reservationLogic.DisplayAllReservationsByDateAndID(acc.Id, date);
                                foreach (ReservationModel reservation in dateReservations)
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    System.Console.WriteLine("---------------------------------------------------");
                                    Console.ResetColor();
                                    System.Console.WriteLine($"Name: {reservation.Name}\nTable Id: {reservation.TableID}\nAmount of people: {reservation.HowMany}\nDate: {reservation.Date.ToShortDateString()} {reservation.TimeSlot}");
                                }
                                Console.ReadKey();
                            }
                            break;

                        case "6":
                            break;

                        default:
                            System.Console.WriteLine("Invalid input");
                            Start(acc, accountsLogic);
                            break;

                    }
                    break;
                //  see the food menu
                case "4":
                    FoodMenuDisplay.StartFoodMenu(acc.Allergies);

                    break;
                // see restaurant info
                case "5":
                    RestaurantInfo.Start();

                    break;
                //  show account data
                case "6":
                    System.Console.WriteLine("Your accounts data: ");
                    // full name
                    System.Console.WriteLine("Name: " + acc.FullName);
                    // age
                    System.Console.WriteLine("Age:" + acc.Age);
                    // email
                    System.Console.WriteLine("Email: " + acc.EmailAddress);
                    // phone numb
                    System.Console.WriteLine("Phone number: " + acc.PhoneNumber);
                    // pass
                    System.Console.WriteLine("Password: " + acc.Password);
                    // allergies
                    if (acc.Allergies.Count() != 0)
                    {
                        System.Console.WriteLine("Allergies: ");
                        foreach (string allergies in acc.Allergies)
                        {
                            System.Console.WriteLine(allergies);
                        }
                    }
                    else System.Console.WriteLine("No allergies given");
                    Console.ReadLine();

                    break;
                // log out
                case "7":
                    ModifyData.Start(acc, accountsLogic);
                    break;
                case "8":
                    acc = null;
                    clientmenu = false;
                    Menu.Start();
                    break;
                default:
                    System.Console.WriteLine("Invalid input");
                    Start(acc, accountsLogic);
                    break;
            }
        }
    }

}
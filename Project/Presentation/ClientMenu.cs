class ClientMenu
{
    static private ReservationLogic reservationLogic = new ReservationLogic();
    public static void Start(AccountModel acc)
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
        System.Console.WriteLine("Enter 7 to log out");

        string user_logged_in_answer = System.Console.ReadLine();
        switch (user_logged_in_answer)
        {
            // make reservation
            case "1":
                System.Console.WriteLine("Make reservation:");
                Reservation.MakeReservation(acc.FullName, acc.Id, acc.PhoneNumber, acc.EmailAddress);
                ReservationAccess.WriteAllReservations(reservationLogic._reservations);
                TableAccess.WriteAllTables(reservationLogic._tables);
                Start(acc);
                break;
            // cancel reservation
            case "2":
                System.Console.WriteLine("Cancel reservation");
                Reservation.CancelReservation(acc.Id);
                ReservationAccess.WriteAllReservations(reservationLogic._reservations);
                TableAccess.WriteAllTables(reservationLogic._tables);
                Start(acc);
                break;
            // see accounts reservation
            case "3":
                System.Console.WriteLine("Your reservations: ");
                System.Console.WriteLine(reservationLogic.DisplayReservations(acc.Id));
                Console.ReadLine();
                Start(acc);
                break;
            //  see the food menu
            case "4":
                FoodMenuDisplay.StartFoodMenu(acc.Allergies);
                Start(acc);
                break;
            // see restaurant info
            case "5":
                RestaurantInfo.Start();
                Start(acc);
                break;
            //  show account data
            case "6":
                System.Console.WriteLine("Your accounts data: ");
                // full name
                System.Console.WriteLine("Name: " + acc.FullName);
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
                Start(acc);
                break;
            // log out
            case "7":
                acc = null;
                Menu.Start();
                break;
            default:
                System.Console.WriteLine("Invalid input");
                Start(acc);
                break;
        }
    }

}
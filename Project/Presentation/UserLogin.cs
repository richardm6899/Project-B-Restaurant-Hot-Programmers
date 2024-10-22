static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();

    static private AccountModel acc = null;

    static private ReservationLogic reservationLogic = new ReservationLogic();


    public static void Start()
    {
        // check if not logged in yet, if not logged in log in
        if (acc == null)
        {
            Console.WriteLine("Welcome to the login tab");
            Console.WriteLine("Please enter your email address");
            string email = Console.ReadLine();
            Console.WriteLine("Please enter your password");
            string password = Console.ReadLine();
            acc = accountsLogic.CheckLogin(email, password);

            // check if account exists
            if(acc == null)
            {
                Console.WriteLine("No account found with that email or password, try again!");
                Menu.Start();
            }
        }

        // if logged in show this
        if (acc != null)
        {
            Console.WriteLine("Welcome back " + acc.FullName);
            System.Console.WriteLine("Enter 1 to make a reservation.");
            System.Console.WriteLine("Enter 2 to cancel a reservation.");
            System.Console.WriteLine("Enter 3 to see your reservations.");
            System.Console.WriteLine("Enter 4 to see the food menu.");
            System.Console.WriteLine("Enter 5 to see the restaurant info.");
            System.Console.WriteLine("Enter 6 to see your accounts data.");
            System.Console.WriteLine("Enter 6 to log out");

            string user_logged_in_answer = System.Console.ReadLine();
            switch (user_logged_in_answer)
            {
                // make reservation
                case "1":
                    System.Console.WriteLine("Make reservation:");
                    Reservation.MakeReservation(acc.FullName, acc.Id);
                    Start();
                    break;
                // cancel reservation
                case "2":
                    System.Console.WriteLine("Cancel reservation");
                    System.Console.WriteLine("Not implemented yet");
                    Start();
                    break;
                // see accounts reservation
                case "3":
                    System.Console.WriteLine("Your reservations: ");
                    reservationLogic.DisplayReservations(acc.Id);
                    Console.ReadLine();
                    Start();
                    break;
                //  see the food menu
                case "4":
                    FoodMenuDisplay.Start();
                    Start();
                    break;
                // see restaurant info
                case "5":
                    RestaurantInfo.Start();
                    Start();
                    break;
                case "6":
                    System.Console.WriteLine("Your accounts data: ");
                    System.Console.WriteLine("Not implemented yet.");
                    Start();
                    break;
                // log out
                case "7":
                    acc = null;
                    Menu.Start();
                    break;
                default:
                    System.Console.WriteLine("Invalid input");
                    Start();
                    break;
            }

            //Write some code to go back to the menu
            //Menu.Start();
        }

     }
}
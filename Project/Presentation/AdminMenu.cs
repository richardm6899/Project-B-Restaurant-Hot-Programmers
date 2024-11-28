class AdminMenu
{
    static private ReservationLogic reservationLogic = new ReservationLogic();
    public static void Start(AccountModel acc)
    {
        bool adminMenu = true;
        while (adminMenu)
        {

            Console.WriteLine("Welcome to the admin menu.");
            System.Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Welcome back " + acc.FullName);
            System.Console.WriteLine("-----------------------------------------");
            System.Console.WriteLine("Enter 1 to make a reservation.");
            System.Console.WriteLine("Enter 2 to cancel a reservation.");
            System.Console.WriteLine("Enter 3 to see all reservations and to close day.");
            System.Console.WriteLine("Enter 4 to see the food menu.");
            System.Console.WriteLine("Enter 5 to edit food menu.");
            System.Console.WriteLine("Enter 6 to see the drinks menu");
            System.Console.WriteLine("Enter 7 to see the restaurant info.");
            System.Console.WriteLine("Enter 8 to see your accounts data.");
            System.Console.WriteLine("Enter 9 to look at finances.");
            System.Console.WriteLine("Enter 10 to make en account");
            System.Console.WriteLine("Enter 11 to log out");


            string user_logged_in_answer = System.Console.ReadLine();
            switch (user_logged_in_answer)
            {


                // make reservation
                case "1":
                    System.Console.WriteLine("Make reservation:");
                    // when you want to make a reservation as admin you have to ask all info of the person that wants to make said reservation
                    // don't add the info of the admin to the reservation.
                    AdminMakeReservation.Start(acc);
                    System.Console.WriteLine("Not implemented yet");

                    break;

                // cancel reservation
                case "2":
                    System.Console.WriteLine("Cancel reservation");
                    Reservation.AdminCancelReservation();
                    // admin can cancel all reservations



                    break;

                // see all reservations
                case "3":
                    string reservationsForDay = Reservation.AdminShowReservations();
                    Console.WriteLine(Reservation.AdminShowReservations());
                    System.Console.WriteLine("[enter]");
                    if (reservationsForDay != "")
                    {
                        System.Console.WriteLine("The following reservations are scheduled:");
                        System.Console.WriteLine(reservationsForDay);
                        System.Console.WriteLine("Are you sure you want to close the restaurant for a day and cancel one of the reservations? (Y/N)");
                    }
                    else
                    {
                        System.Console.WriteLine("There are no reservations scheduled for this day.");
                        System.Console.WriteLine("Are you sure you want to close the restaurant for the day? (Y/N)");
                    }
                    string confirmation = Console.ReadLine().ToUpper();
                    if (confirmation == "Y")
                    {
                        // Cancel all reservations for the day
                        Reservation.AdminCloseDay();
                        // Refund users
                        // foreach (var reservation in reservationLogic._reservations)
                        // {
                        //      if (reservation.Date.Date == date.Date)
                        //     {
                        //         // Refund logic here
                        //         // FinanceLogic.AddToRevenue(reservation.Cost);
                        //         // FinanceLogic.Refund(reservation.ClientID, reservation.Cost);
                        //     }
                        // }
                        System.Console.WriteLine("Restaurant closed for the day. Reservations canceled and users refunded.");
                    }
                    else
                    {
                        System.Console.WriteLine("Closure cancelled.");
                    }
                    System.Console.ReadLine();
                    break;
                //  see the food menu
                case "4":
                    // add question with what allergies to look at if admin wants to look at allergies
                    FoodMenuDisplay.Start();

                    break;

                // edit food menu
                case "5":
                    System.Console.WriteLine("Editing the food menu.");
                    FoodMenuDisplay.EditFoodMenuMenu();
                    System.Console.WriteLine("[enter]");
                    System.Console.ReadLine();

                    break;

                // see drinks
                case "6":
                    DrinkMenuDisplay.Start();

                    break;

                // see restaurant info
                case "7":
                    RestaurantInfo.Start();

                    break;

                //  show account data
                case "8":
                    System.Console.WriteLine("Your accounts data: ");
                    // full name
                    System.Console.WriteLine("Name: " + acc.FullName);
                    // email
                    System.Console.WriteLine("Email: " + acc.EmailAddress);
                    // phone numb
                    System.Console.WriteLine("Phone number: " + acc.PhoneNumber);
                    // pass
                    System.Console.WriteLine("Password: " + acc.Password);
                    Console.ReadLine();

                    break;

                // look at finances
                case "9":
                    System.Console.WriteLine("Look at restaurant finances.");
                    Finances.Finance();

                    break;

                // make an account
                case "10":
                    System.Console.WriteLine("Create an account:");
                    System.Console.WriteLine("Not implemented yet.");
                    System.Console.WriteLine("[enter]");
                    System.Console.ReadLine();
                    // AdminLogic newAdmin = new("Jane Doe", "admin@test2.nl", "TestAdmin2", "123456789", 23);
                    // newAdmin.CreateAdmin();
                    break;

                // log out
                case "11":
                    acc = null;
                    Menu.Start();
                    break;
                default:
                    System.Console.WriteLine("Invalid input");
                    System.Console.WriteLine("[enter]");
                    Console.ReadLine();
                    break;

            }
        }
    }
}
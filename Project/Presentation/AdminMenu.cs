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
            System.Console.WriteLine("Enter 3 to see all reservations.");
            System.Console.WriteLine("Enter 4 to see the food menu.");
            System.Console.WriteLine("Enter 5 to edit food menu.");
            System.Console.WriteLine("Enter 6 to see the restaurant info.");
            System.Console.WriteLine("Enter 7 to see your accounts data.");
            System.Console.WriteLine("Enter 8 to look at finances.");
            System.Console.WriteLine("Enter 9 to make en account");
            System.Console.WriteLine("Enter 10 to log out");


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
                    System.Console.WriteLine("All reservations: ");
                    List<string> reservations = reservationLogic.DisplayAllReservationList();
                    foreach (string Reservation in reservations)
                    {
                        System.Console.WriteLine(Reservation);
                    }
                    System.Console.WriteLine("[enter]");
                    System.Console.ReadLine();
                    break;

                //  see the food menu
                case "4":
                    // add question with what allergies to look at if admin wants to look at allergies
                    FoodMenuDisplay.StartFoodMenu(acc.Allergies);

                    break;

                // edit food menu
                case "5":
                    System.Console.WriteLine("Editing the food menu.");
                    System.Console.WriteLine("Not implemented yet.");
                    System.Console.WriteLine("[enter]");
                    System.Console.ReadLine();

                    break;

                // see restaurant info
                case "6":
                    RestaurantInfo.Start();

                    break;

                //  show account data
                case "7":
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
                case "8":
                    System.Console.WriteLine("Look at restaurant finances.");
                    System.Console.WriteLine("not implemented yet");
                    System.Console.WriteLine("[enter]");
                    System.Console.ReadLine();
                    break;

                // make an account
                case "9":
                    System.Console.WriteLine("Create an account:");
                    System.Console.WriteLine("Not implemented yet.");
                    System.Console.WriteLine("[enter]");
                    System.Console.ReadLine();
                    // AdminLogic newAdmin = new("Jane Doe", "admin@test2.nl", "TestAdmin2", "123456789", 23);
                    // newAdmin.CreateAdmin();
                    break;

                // log out
                case "10":
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
}
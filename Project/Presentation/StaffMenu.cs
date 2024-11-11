class StaffMenu
{
    public static void Start(AccountModel acc)
    {
        Console.WriteLine("Welcome to the staff menu.");
        System.Console.WriteLine("-----------------------------------------");
        Console.WriteLine("Welcome back " + acc.FullName);
        System.Console.WriteLine("-----------------------------------------");
        System.Console.WriteLine("Enter 1 to make a reservation.");
        System.Console.WriteLine("Enter 2 to cancel a reservation.");
        System.Console.WriteLine("Enter 3 to find a reservation");
        System.Console.WriteLine("Enter 4 to see all reservations.");
        System.Console.WriteLine("Enter 5 to see the food menu.");
        System.Console.WriteLine("Enter 6 to see the restaurant info.");
        System.Console.WriteLine("Enter 7 to see your accounts data.");
        System.Console.WriteLine("Enter 8 to log out");


        string user_logged_in_answer = System.Console.ReadLine();
        switch (user_logged_in_answer)
        {
            // make reservation
            case "1":
                System.Console.WriteLine("Make reservation:");
                // when you want to make a reservation as staff you have to ask all info of the person that wants to make said reservation
                // don't add the info of the staff to the reservation.
                System.Console.WriteLine("Not implemented yet");
                Start(acc);
                break;

            // cancel reservation
            case "2":
                System.Console.WriteLine("Cancel reservation");
                // staff can cancel all reservations
                System.Console.WriteLine("Not implemented yet");
                Start(acc);
                break;

            // find a reservation
            case "3":
                System.Console.WriteLine("Find reservation.");
                // can be found by name or maybe reservation id
                System.Console.WriteLine("Not implemented yet.");
                break;

            // see all reservations
            case "4":
                System.Console.WriteLine("All reservations: ");
                System.Console.WriteLine("Not implemented yet");
                Start(acc);
                break;

            //  see the food menu
            case "5":
                // add question with what allergies to look at if admin wants to look at allergies
                FoodMenuDisplay.StartFoodMenu(acc.Allergies);
                Start(acc);
                break;

            // see restaurant info
            case "6":
                RestaurantInfo.Start();
                Start(acc);
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
                Start(acc);
                break;

            // log out
            case "8":
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
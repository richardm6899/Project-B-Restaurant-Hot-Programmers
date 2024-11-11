class FinancialMenu
{
    public static void Start(AccountModel acc)
    {
        Console.WriteLine("Welcome to the financial menu.");
        System.Console.WriteLine("-----------------------------------------");
        Console.WriteLine("Welcome back " + acc.FullName);
        System.Console.WriteLine("-----------------------------------------");
        System.Console.WriteLine("Enter 1 to see all reservations.");
        System.Console.WriteLine("Enter 2 to see the restaurant info.");
        System.Console.WriteLine("Enter 3 to see your accounts data.");
        System.Console.WriteLine("Enter 4 to look at finances.");
        System.Console.WriteLine("Enter 5 to log out");

        string user_logged_in_answer = System.Console.ReadLine();
        switch (user_logged_in_answer)
        {
            // see all reservations
            case "1":
                System.Console.WriteLine("All reservations: ");
                System.Console.WriteLine("Not implemented yet");
                Start(acc);
                break;

            // see restaurant info
            case "2":
                RestaurantInfo.Start();
                Start(acc);
                break;

            //  show account data
            case "3":
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

            // look at finances
            case "4":
                System.Console.WriteLine("Look at restaurant finances.");
                System.Console.WriteLine("not implemented yet");
                break;
                
            // log out
            case "5":
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
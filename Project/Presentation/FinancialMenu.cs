using Microsoft.VisualBasic;

class FinancialMenu
{
    static private ReservationLogic reservationLogic = new ReservationLogic();
    static private FinanceLogic FinanceLogic = new FinanceLogic();
    public static void Start(AccountModel acc)
    {
        bool financialStart = true;
        while (financialStart)
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

                    System.Console.WriteLine(reservationLogic.DisplayAllReservations());
                    System.Console.WriteLine("[enter]");
                    Console.ReadLine();

                    break;

                // see restaurant info
                case "2":
                    RestaurantInfo.Start();

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

                    break;

                // look at finances
                case "4":
                    System.Console.WriteLine("Look at restaurant finances.");
                    Finances.Finance();
                    break;

                // log out
                case "5":
                    acc = null;
                    financialStart = false;
                    System.Console.WriteLine("Goodbye...");
                    System.Console.WriteLine("[enter]");
                    Console.ReadLine();

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
//finance@test.nl
//TestFinance1
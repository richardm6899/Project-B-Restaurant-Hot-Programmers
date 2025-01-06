using Microsoft.VisualBasic;

class FinancialMenu
{
    static private ReservationLogic reservationLogic = new ReservationLogic();
    static private FinanceLogic FinanceLogic = new FinanceLogic();
    public static void Start(AccountModel acc)
    {
        string[] optionsFinance = {
            "See all Reservations",
            "See the Restaurant info",
            "See your accounts data",
            "Look at Finances",
            "Log Out"
        };
        bool financialStart = true;
        while (financialStart)
        {
            int selectedIndexFinance = 0;
            Console.Clear();
            selectedIndexFinance = HelperPresentation.ChooseOption("Welcome to the financial menu.", optionsFinance, selectedIndexFinance);

            switch (selectedIndexFinance)
            {
                // see all reservations
                case 0:
                    System.Console.WriteLine("All reservations: ");

                    System.Console.WriteLine(reservationLogic.DisplayAllReservations());
                    System.Console.WriteLine("[enter]");
                    Console.ReadLine();
                    break;

                // see restaurant info
                case 1:
                    RestaurantInfo.Start();
                    break;

                //  show account data
                case 2:
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
                case 3:
                    System.Console.WriteLine("Look at restaurant finances.");
                    Finances.Finance();
                    break;

                // log out
                case 4:
                    acc = null;
                    financialStart = false;
                    System.Console.WriteLine("Goodbye...");
                    System.Console.WriteLine("[enter]");
                    Console.ReadLine();
                    break;
            }

        }
    }
}
//finance@test.nl
//TestFinance1
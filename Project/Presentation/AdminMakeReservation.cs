class AdminMakeReservation
{
    private static AccountsLogic accountsLogic = new();

    public static void Start(AccountModel acc)
    {
        // public ReservationModel Create_reservation(int tableID, string name, int clientID, int howMany, DateTime date)
        // tableID = get a table id
        // name = ask the person for their name
        // clientID = give admin id
        // howMany = how many people the client wants
        // date = ask the date
        System.Console.WriteLine("Make a reservation? Y/N");
        string admin_answer = Console.ReadLine();
        if (admin_answer.ToUpper() == "N")
        {
            System.Console.WriteLine("");
            AdminMenu.Start(acc);
        }

        System.Console.WriteLine("Name: ");
        string Name = Console.ReadLine();
        System.Console.WriteLine("Phonenumber: ");
        string Number = Console.ReadLine();
        System.Console.WriteLine("Email: ");
        string Email = Console.ReadLine();
        bool emailCheck = AccountsLogic.CheckCreateEmail(Email);
        if (emailCheck == false)
        {
            while (emailCheck == false)
            {
                System.Console.WriteLine("Incorrect email, please re-enter an email: ");
                Email = Console.ReadLine();
                emailCheck = AccountsLogic.CheckCreateEmail(Email);
            }
        }

        List<AccountModel> allAccounts = accountsLogic._accounts;
        foreach (AccountModel account in allAccounts)
        {
            if (account.EmailAddress == Email)
            {
                System.Console.WriteLine("Email found in system.");
                bool linkAccount = ChoicesLogic.YesOrNo("Would you like to link the reservation to this email.");
                if (linkAccount)
                {
                    Reservation.MakeReservation(account.FullName, account.Id, account.PhoneNumber, account.EmailAddress);
                    break;
                }
            }
        }
        Reservation.MakeReservation(Name, acc.Id, Number, Email);
    }
}
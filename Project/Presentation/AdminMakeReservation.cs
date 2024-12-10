using System.Runtime.CompilerServices;

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

        // ask if admin wants to make reservation
        bool adminMakeReservation = HelperPresentation.YesOrNo("Make a reservation? Y/N");
        if (adminMakeReservation == false)
        {
            System.Console.WriteLine("Returning back to the admin menu.\nPress [enter] to continue.");
            Console.ReadKey();
            AdminMenu.Start(acc);
        }

        string Name = AdminAskName();
        string Number = AdminAskNumber();
        string Email = AdminAskEmail();
        string[] options = {
            "Name",
            "Phone number",
            "Email",
            "Info is correct",
            "Cancel reservation making",
        };

        bool checkingInfo = true;
        while (checkingInfo)
        {
            bool correctInfo = HelperPresentation.YesOrNo($"Is this info correct?\nName: {Name}\nPhone Number: {Number}\nEmail: {Email}");
            if (!correctInfo)
            {
                int selectedIndex = 0;
                selectedIndex = HelperPresentation.ChooseOption($"Name: {Name}\nPhone Number: {Number}\nEmail: {Email}\nWhat would you like to change", options, selectedIndex);
                switch (selectedIndex)
                {
                    // re-ask name
                    case 0:
                        Name = AdminAskName();
                        break;
                    // re-ask number
                    case 1:
                        Number = AdminAskNumber();
                        break;
                    // re-ask email
                    case 2:
                        Email = AdminAskEmail();
                        break;
                    // nvm info correct
                    case 3:
                        checkingInfo = false;
                        break;
                    // nvm no make reservation
                    case 4:
                        AdminMenu.Start(acc);
                        break;
                }
            }
            else checkingInfo = false;
        }

        List<AccountModel> allAccounts = accountsLogic._accounts;
        foreach (AccountModel account in allAccounts)
        {
            if (account.EmailAddress == Email)
            {
                System.Console.WriteLine("Email found in system.");
                bool linkAccount = HelperPresentation.YesOrNo("Would you like to link the reservation to this email.");
                if (linkAccount)
                {
                    Reservation.MakeReservation(account.FullName, account.Id, account.PhoneNumber, account.EmailAddress);
                    break;
                }
            }
        }
        Reservation.MakeReservation(Name, acc.Id, Number, Email);


    }

    private static string AdminAskName()
    {
        System.Console.WriteLine("Name: ");
        string? Name = Console.ReadLine();

        while (Name == null || Name.Length == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("No name was entered, please reenter name.");
            Console.ResetColor();
            System.Console.WriteLine("Name: ");
            Name = Console.ReadLine();
        }
        Name = HelperLogic.CapitalizeFirstLetter(Name);
        return Name;
    }

    private static string AdminAskNumber()
    {

        System.Console.WriteLine("Phone number: ");
        string? Number = Console.ReadLine();
        while (Number == null || Number.Length == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("No phone number was entered, please reenter Phone number.");
            Console.ResetColor();
            System.Console.WriteLine("Phone number: ");
            Number = Console.ReadLine();
        }
        return Number;
    }

    private static string AdminAskEmail()
    {
        System.Console.WriteLine("Email: ");
        string? Email = Console.ReadLine();
        while (Email == null || !HelperLogic.IsValidEmail(Email))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("Incorrect email entered, please reenter email.");
            Console.ResetColor();
            System.Console.WriteLine("Email: ");
            Email = Console.ReadLine();
        }
        return Email;
    }
}


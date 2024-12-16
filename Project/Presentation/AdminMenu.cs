class AdminMenu
{
    static private ReservationLogic reservationLogic = new ReservationLogic();
    static private AccountsLogic accountsLogic = new();
    public static void Start(AccountModel acc)
    {

        string[] options = {
            "Make a Reservation",
            "Cancel a Reservation",
            "See all Reservations",
            "Close a day",
            "See Food Menu",
            "Edit Food Menu",
            "See Drinks Menu",
            "See Restaurant Info",
            "See Accounts Data",
            "Look at Finances",
            "Make an Account",
            "Delete or Deactivate an Account",
            "Log Out",
        };

        bool adminMenu = true;

        while (adminMenu)
        {
            int selectedIndex = 0;

            Console.Clear();

            string mainPrompt = @$"Welcome to the client menu.
-----------------------------------------
Welcome back {acc.FullName}
-----------------------------------------";

            selectedIndex = HelperPresentation.ChooseOption(mainPrompt, options, selectedIndex);



            switch (selectedIndex)
            {
                // make reservation
                case 0:
                    AdminMakeReservation.Start(acc);
                    break;
                // cancel reservation
                case 1:
                    Reservation.AdminCancelReservation();
                    break;
                // see reservations
                case 2:
                    System.Console.WriteLine("All reservations: ");
                    System.Console.WriteLine(reservationLogic.DisplayAllReservations());
                    System.Console.WriteLine("[enter]");
                    System.Console.ReadLine();
                    break;
                // close restaurant for the day
                case 3:
                    CloseRestaurant();
                    break;
                // see food menu
                case 4:
                    FoodMenuDisplay.Start();
                    break;
                // edit food menu
                case 5:
                    FoodMenuDisplay.EditFoodMenuMenu();
                    break;
                // see drink menu
                case 6:
                    DrinkMenuDisplay.Start();
                    break;
                // see restaurant info
                case 7:
                    RestaurantInfo.Start();
                    break;
                // see accounts data
                case 8:
                    AccountData(acc);
                    break;
                // look at finances
                case 9:
                    Finances.Finance();
                    break;
                // make an account
                case 10:
                    System.Console.WriteLine("Not implemented yet.");
                    System.Console.WriteLine("Press [enter] to continue.");
                    Console.ReadKey();
                    break;
                // delete or deactivate an account
                case 11:
                    DeleteDeactivate(acc);
                    break;
                // log out
                case 12:
                    acc = null;
                    Menu.Start();
                    break;
            }
        }
    }
    // case 3
    private static void CloseRestaurant()
    {
        Console.Clear();
        string reservationsForDay = Reservation.AdminShowReservations();
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
    }

    // 8
    private static void AccountData(AccountModel acc)
    {
        Console.Clear();
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
    }

    // case 11
    private static void DeleteDeactivate(AccountModel acc)
    {
        bool deactivatingDeleting = true;
        do
        {
            Console.Clear();
            System.Console.WriteLine("What type of account would you like to look at.");
            System.Console.WriteLine("Enter 1 to see all accounts");
            System.Console.WriteLine("Enter 2 to see all client accounts");
            System.Console.WriteLine("Enter 3 to see all staff accounts");
            System.Console.WriteLine("Enter 4 to see all finance accounts");
            System.Console.WriteLine("Enter 5 to choose account to delete");
            System.Console.WriteLine("Enter 6 to choose account to deactivate");
            System.Console.WriteLine("Enter 7 to return");

            string adminDelDea = Console.ReadLine();

            AdminLogic adminLogic = new(acc.FullName, acc.EmailAddress, acc.Password, acc.PhoneNumber, acc.Birthdate);
            List<AccountModel> activatedAccounts = adminLogic.GetActivatedAccounts();
            switch (adminDelDea)
            {
                // show all activated accounts
                case "1":
                    foreach (AccountModel account in activatedAccounts)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("------------------------------");
                        Console.ResetColor();
                        System.Console.WriteLine($"ID: {account.Id}\nFullname: {account.FullName}\nEmail: {account.EmailAddress}\nPhone number: {account.PhoneNumber}\nType: {account.Type}");
                    }
                    Console.ReadKey();
                    break;

                // show all client accounts
                case "2":
                    System.Console.WriteLine("Client Accounts:");
                    foreach (AccountModel account in activatedAccounts)
                    {
                        if (account.Type == "client")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            System.Console.WriteLine("------------------------------");
                            Console.ResetColor();
                            System.Console.WriteLine($"ID: {account.Id}\nFullname: {account.FullName}\nEmail: {account.EmailAddress}\nPhone number: {account.PhoneNumber}");
                        }
                    }
                    Console.ReadKey();
                    break;

                // show all staff accounts
                case "3":
                    System.Console.WriteLine("Staff Accounts:");
                    foreach (AccountModel account in activatedAccounts)
                    {
                        if (account.Type == "staff")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            System.Console.WriteLine("------------------------------");
                            Console.ResetColor();
                            System.Console.WriteLine($"ID: {account.Id}\nFullname: {account.FullName}\nEmail: {account.EmailAddress}\nPhone number: {account.PhoneNumber}");
                        }
                    }
                    Console.ReadKey();
                    break;

                // show all finance accounts
                case "4":
                    System.Console.WriteLine("finance Accounts:");
                    foreach (AccountModel account in activatedAccounts)
                    {
                        if (account.Type == "finance")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            System.Console.WriteLine("------------------------------");
                            Console.ResetColor();
                            System.Console.WriteLine($"ID: {account.Id}\nFullname: {account.FullName}\nEmail: {account.EmailAddress}\nPhone number: {account.PhoneNumber}");
                        }
                    }
                    Console.ReadKey();
                    break;

                // delete account
                case "5":
                    System.Console.WriteLine("Please enter the id of the account you wish to delete..");
                    int toDeleteID = Convert.ToInt32(Console.ReadLine());
                    AccountModel toDeleteAccount = accountsLogic.GetById(toDeleteID);
                    if (toDeleteAccount.Status == "Deleted")
                    {
                        System.Console.WriteLine($"This account has already been {toDeleteAccount.Status}");
                        Console.ReadLine();
                        break;
                    }

                    bool toDelete = HelperPresentation.YesOrNo($"Is this the account you wish to delete?\nID: {toDeleteAccount.Id}\nFullName: {toDeleteAccount.FullName}\nEmail: {toDeleteAccount.EmailAddress}\nPhone number: {toDeleteAccount.PhoneNumber}\nType: {toDeleteAccount.Type}\nStatus: {toDeleteAccount.Status}");
                    if (toDelete)
                    {
                        System.Console.WriteLine("Please re-enter password.");
                        string adminPass = Console.ReadLine();
                        if (accountsLogic.ReCheckPassWord(acc, adminPass))
                        {
                            System.Console.WriteLine("Correct password, account has been deleted");
                            accountsLogic.deleteAccount(toDeleteID);
                            Console.ReadLine();
                        }
                        else System.Console.WriteLine("Incorrect password");
                    }
                    break;

                // deactivate account
                case "6":
                    System.Console.WriteLine("Please enter the id of the account you wish to deactivate..");
                    int toDeactivateID = Convert.ToInt32(Console.ReadLine());
                    AccountModel toDeactivateAccount = accountsLogic.GetById(toDeactivateID);
                    if (toDeactivateAccount.Status == "Deleted" || toDeactivateAccount.Status == "Deactivated")
                    {
                        System.Console.WriteLine($"This account has already been {toDeactivateAccount.Status}");
                        Console.ReadLine();
                        break;
                    }

                    bool toDeactivate = HelperPresentation.YesOrNo($"Is this the account you wish to delete?\nID: {toDeactivateAccount.Id}\nFullName: {toDeactivateAccount.FullName}\nEmail: {toDeactivateAccount.EmailAddress}\nPhone number: {toDeactivateAccount.PhoneNumber}\nType: {toDeactivateAccount.Type}");
                    if (toDeactivate)
                    {
                        System.Console.WriteLine("Please re-enter password.");
                        string adminPass = Console.ReadLine();
                        if (accountsLogic.ReCheckPassWord(acc, adminPass))
                        {
                            System.Console.WriteLine("Correct password, account has been deactivated");
                            accountsLogic.deactivateAccount(toDeactivateID);
                            Console.ReadKey();
                        }
                        else System.Console.WriteLine("Incorrect password");
                    }
                    break;

                // return
                case "7":
                    deactivatingDeleting = false;
                    break;

                default:
                    System.Console.WriteLine("Invalid input");
                    System.Console.WriteLine("[enter]");
                    Console.ReadLine();
                    break;
            }
        } while (deactivatingDeleting);
    }
}
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
    // main case 3
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

    // main case 8
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

    // main case 11
    private static void DeleteDeactivate(AccountModel acc)
    {
        string[] options = {
            "See all activated accounts,",
            "See aal Client accounts",
            "See all staff accounts",
            "See all finance accounts",
            "Choose account to Delete",
            "Choose account to deactivate",
            "return",
        };

        bool deactivatingDeleting = true;

        do
        {
            int selectedIndex = 0;
            Console.Clear();

            selectedIndex = HelperPresentation.ChooseOption("What type of account would you like to look at.", options, selectedIndex);


            AdminLogic adminLogic = new(acc.FullName, acc.EmailAddress, acc.Password, acc.PhoneNumber, acc.Birthdate);
            List<AccountModel> activatedAccounts = adminLogic.GetActivatedAccounts();


            switch (selectedIndex)
            {
                // show all activated accounts
                case 0:
                    Console.Clear();
                    foreach (AccountModel account in activatedAccounts)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("------------------------------");
                        Console.ResetColor();
                        System.Console.WriteLine($"ID: {account.Id}\nFullname: {account.FullName}\nEmail: {account.EmailAddress}\nPhone number: {account.PhoneNumber}\nType: {account.Type}");
                    }
                    System.Console.WriteLine("\nPress [enter] to continue.");
                    Console.ReadKey();
                    break;

                // show all client accounts
                case 1:
                    Console.Clear();
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
                    System.Console.WriteLine("\nPress [enter] to continue.");
                    Console.ReadKey();
                    break;

                // show all staff accounts
                case 2:
                    Console.Clear();
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
                    System.Console.WriteLine("\nPress [enter] to continue.");
                    Console.ReadKey();
                    break;

                // show all finance accounts
                case 3:
                    Console.Clear();
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
                    System.Console.WriteLine("\nPress [enter] to continue");
                    Console.ReadKey();
                    break;

                // delete account
                case 4:
                    Console.Clear();
                    System.Console.WriteLine("Enter the Full Name of the email of the account you wish to delete.");
                    System.Console.WriteLine("If no filter is wanted press [enter].");
                    System.Console.WriteLine("To return press [escape].");

                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        break;
                    }

                    string? filter = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(filter))
                    {
                        filter = null;
                    }

                    List<AccountModel> accounts = accountsLogic.GetAccountByNameOrEmail(filter);

                    if (accounts.Count == 0)
                    {
                        System.Console.WriteLine("No accounts found.\nPress [enter] to continue.");
                        Console.ReadKey();
                    }
                    if (accounts.Count() == 1)
                    {
                        bool deleteAccount = HelperPresentation.YesOrNo($@"Do you wish to delete this account?
Client Id: {accounts[0].Id}
Email: {accounts[0].EmailAddress}
Full Name: {accounts[0].FullName}
Birthdate: {HelperPresentation.DateTimeToReadableDate(accounts[0].Birthdate)}
Phone number: {accounts[0].PhoneNumber}
Type: {accounts[0].Type}
Last Login: {HelperPresentation.DateTimeToReadableDate(accounts[0].LastLogin)}
Status: {accounts[0].Status}");

                        if (deleteAccount)
                        {
                            System.Console.WriteLine("Please re-enter password.");
                            string adminPass = HelperPresentation.ReadPassword();
                            if (accountsLogic.ReCheckPassWord(acc, adminPass))
                            {
                                System.Console.WriteLine("Correct password, account has been deleted");
                                accountsLogic.deleteAccount(accounts[0].Id);
                                System.Console.WriteLine("Press [enter] to continue.");
                            }
                            else System.Console.WriteLine("Incorrect password\nPress [enter] to continue.");

                        }
                        Console.ReadKey();
                    }
                    else
                    {
                        selectedIndex = 0;
                        List<string> AccountsInfo = new();
                        foreach (AccountModel account in accounts)
                        {
                            AccountsInfo.Add($"Client ID: {account.Id}\nFull name: {account.FullName}\nEmail: {account.EmailAddress}");
                        }
                        int IndexSelectedAccount = HelperPresentation.ChooseItem("Select account to delete.", AccountsInfo, selectedIndex);

                        AccountModel selectedAccount = accounts[IndexSelectedAccount];

                        bool deleteAccount = HelperPresentation.YesOrNo(@$"Do you wish to delete this Account:
Client id: {selectedAccount.Id}
Email: {selectedAccount.EmailAddress}
Full Name: {selectedAccount.FullName}
Birthdate: {HelperPresentation.DateTimeToReadableDate(selectedAccount.Birthdate)}
Phone number: {selectedAccount.PhoneNumber}
Type: {selectedAccount.Type}
Last Login: {HelperPresentation.DateTimeToReadableDate(selectedAccount.LastLogin)}
Status: {selectedAccount.Status}");

                        if (deleteAccount)
                        {
                            System.Console.WriteLine("Please re-enter password.");
                            string adminPass = HelperPresentation.ReadPassword();
                            if (accountsLogic.ReCheckPassWord(acc, adminPass))
                            {
                                System.Console.WriteLine("Correct password, account has been deleted");
                                accountsLogic.deleteAccount(accounts[0].Id);
                                System.Console.WriteLine("Press [enter] to continue.");
                            }
                            else System.Console.WriteLine("Incorrect password\nPress [enter] to continue.");

                        }
                        Console.ReadKey();
                    }
                    break;

                // deactivate account
                case 5:
                    Console.Clear();
                    System.Console.WriteLine("Enter the Full Name of the email of the account you wish to Deactivate.");
                    System.Console.WriteLine("If no filter is wanted press [enter].");
                    System.Console.WriteLine("To return press [escape].");

                    string? filterDeactivate = Console.ReadLine();

                    ConsoleKeyInfo keyInfoDeactivate = Console.ReadKey(true);
                    if (keyInfoDeactivate.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                    
                    if (string.IsNullOrWhiteSpace(filterDeactivate))
                    {
                        filterDeactivate = null;
                    }

                    List<AccountModel> accountsDeactivate = accountsLogic.GetAccountByNameOrEmail(filterDeactivate);

                    if (accountsDeactivate.Count == 0)
                    {
                        System.Console.WriteLine("No accounts found.\nPress [enter] to continue.");
                        Console.ReadKey();
                    }
                    if (accountsDeactivate.Count() == 1)
                    {
                        bool deactivateAccount = HelperPresentation.YesOrNo($@"Do you wish to delete this account?
Client Id: {accountsDeactivate[0].Id}
Email: {accountsDeactivate[0].EmailAddress}
Full Name: {accountsDeactivate[0].FullName}
Birthdate: {HelperPresentation.DateTimeToReadableDate(accountsDeactivate[0].Birthdate)}
Phone number: {accountsDeactivate[0].PhoneNumber}
Type: {accountsDeactivate[0].Type}
Last Login: {HelperPresentation.DateTimeToReadableDate(accountsDeactivate[0].LastLogin)}
Status: {accountsDeactivate[0].Status}");

                        if (deactivateAccount)
                        {
                            System.Console.WriteLine("Please re-enter password.");
                            string adminPass = HelperPresentation.ReadPassword();
                            if (accountsLogic.ReCheckPassWord(acc, adminPass))
                            {
                                System.Console.WriteLine("Correct password, account has been deleted");
                                accountsLogic.deactivateAccount(accountsDeactivate[0].Id);
                                System.Console.WriteLine("Press [enter] to continue.");
                            }
                            else System.Console.WriteLine("Incorrect password\nPress [enter] to continue.");

                        }
                        Console.ReadKey();
                    }
                    else
                    {
                        selectedIndex = 0;
                        List<string> AccountsInfo = new();
                        foreach (AccountModel account in accountsDeactivate)
                        {
                            AccountsInfo.Add($"Client ID: {account.Id}\nFull name: {account.FullName}\nEmail: {account.EmailAddress}");
                        }
                        int IndexSelectedAccount = HelperPresentation.ChooseItem("Select account to delete.", AccountsInfo, selectedIndex);

                        AccountModel selectedAccount = accountsDeactivate[IndexSelectedAccount];

                        bool deleteAccount = HelperPresentation.YesOrNo(@$"Do you wish to delete this Account:
Client id: {selectedAccount.Id}
Email: {selectedAccount.EmailAddress}
Full Name: {selectedAccount.FullName}
Birthdate: {HelperPresentation.DateTimeToReadableDate(selectedAccount.Birthdate)}
Phone number: {selectedAccount.PhoneNumber}
Type: {selectedAccount.Type}
Last Login: {HelperPresentation.DateTimeToReadableDate(selectedAccount.LastLogin)}
Status: {selectedAccount.Status}");

                        if (deleteAccount)
                        {
                            System.Console.WriteLine("Please re-enter password.");
                            string adminPass = HelperPresentation.ReadPassword();
                            if (accountsLogic.ReCheckPassWord(acc, adminPass))
                            {
                                System.Console.WriteLine("Correct password, account has been deleted");
                                accountsLogic.deleteAccount(accountsDeactivate[0].Id);
                                System.Console.WriteLine("Press [enter] to continue.");
                            }
                            else System.Console.WriteLine("Incorrect password\nPress [enter] to continue.");

                        }
                        Console.ReadKey();
                    }
                    break;

            }
        } while (deactivatingDeleting);
    }
}
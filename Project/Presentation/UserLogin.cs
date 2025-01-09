using System;
using System.Text;

static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();
    static private AccountModel acc = null;

    static public int FailedLoginAttempts = 0;
    static public bool Locked = false;

    public static void Start()
    {
        acc = null;
        // check if not logged in yet, if not logged in log in
        if (acc == null)
        {
            Console.WriteLine("Welcome to the login tab");
            System.Console.WriteLine("-----------------------------------------");

            Console.WriteLine("Please enter your email address");
            string email = Console.ReadLine();

            Console.WriteLine("Please enter your password");
            string password = HelperPresentation.ReadPassword();

            acc = accountsLogic.CheckLogin(email, password);
            Locked = accountsLogic.CancelLogin(acc, email);
            int remainingSeconds = accountsLogic.CalculateRemainingSeconds(acc, email);
            // FailedLoginAttempts = accountsLogic.FailedToLoginin();
            // check if account exists
            if (accountsLogic.CheckAccountDeactivated(email))
            {
                System.Console.WriteLine("A deactivated account has been found with this email.");
                Console.ReadKey();
                bool reactivate = HelperPresentation.YesOrNo("Would you like to reactivate this account?");
                if (reactivate)
                {
                    System.Console.WriteLine("Re-enter password.");
                    password = HelperPresentation.ReadPassword();
                    accountsLogic.ActivateAccount(email);
                    acc = accountsLogic.CheckLogin(email, password);
                }

            }
            if (Locked == false)
            {
                if (acc == null)
                {
                    if (Locked)
                        {
                            Console.WriteLine($"Your account is blocked for {remainingSeconds} seconds.");
                            if (remainingSeconds <= 0)
                            {
                                acc.FailedLoginAttempts = 0;
                                FailedLoginAttempts = 0;
                                Locked = false;
                            }
                        }
                    // if (FailedLoginAttempts >= 3)
                    else
                    {
                    FailedLoginAttempts++;
                    // FailedLoginAttempts = accountsLogic.FailedToLogin();
                    Console.WriteLine($"Invalid password, try again!");
                    Console.WriteLine($"You have {3 - FailedLoginAttempts} attempts left.\nPress [enter] to continue");
                    Console.ReadKey();
                    if (FailedLoginAttempts >= 3)
                    {
                        FailedLoginAttempts = 0;
                    }
                    Menu.Start();
                    }
                }
        // Menu.Start();
        // proceed to the corresponding menu based on the account type
        // if logged in show this
        if (acc != null)
        {
            accountsLogic.CancelLogin(acc, email);
            FailedLoginAttempts = 0;
            acc.FailedLoginAttempts = 0;
            if (acc.Type == "admin")
            {
                    AdminMenu.Start(acc);   
            }
            else if (acc.Type == "client")
            {
                ClientMenu.Start(acc, accountsLogic);
            }
            else if (acc.Type == "staff")
            {
                StaffMenu.Start(acc);
            }
            else if (acc.Type == "finance")
            {
                FinancialMenu.Start(acc);
            }

        }
        
            }
        if (Locked == true)
        {
            Console.WriteLine($"Your account is blocked for {remainingSeconds} seconds.\nPress [enter] to continue");
            Console.ReadKey();
            if (remainingSeconds <= 0)
            {
                FailedLoginAttempts = 0;
                Locked = false;
            }
        }
        }
    }
    
}
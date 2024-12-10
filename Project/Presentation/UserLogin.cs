using System;
using System.Text;

static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();
    static private AccountModel acc = null;

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
            string password = ReadPassword();

            acc = accountsLogic.CheckLogin(email, password);
            // check if account exists
            if (accountsLogic.CheckAccountDeactivated(email))
            {
                System.Console.WriteLine("A deactivated account has been found with this email.");
                Console.ReadKey();
                bool reactivate = HelperPresentation.YesOrNo("Would you like to reactivate this account?");
                if (reactivate)
                {
                    System.Console.WriteLine("Re-enter password.");
                    password = ReadPassword();
                    accountsLogic.ActivateAccount(email);
                    acc = accountsLogic.CheckLogin(email, password);
                }

            }
            if (acc == null)
            {
                Console.WriteLine("No account found with that email or password, try again!");
                Menu.Start();
            }
        }

        // if logged in show this
        if (acc != null)
        {
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

    private static string ReadPassword()
    {
        StringBuilder passwordBuilder = new StringBuilder();
        while (true)
        {
            // Read a key from the console without writing it
            var keyInfo = Console.ReadKey(intercept: true);

            // Check if enter is pressed
            if (keyInfo.Key == ConsoleKey.Enter)
            {
                break;
            }
            // delete a character
            else if (keyInfo.Key == ConsoleKey.Backspace && passwordBuilder.Length > 0)
            {
                // remove character
                passwordBuilder.Length--;

                // Move the cursor back, overwrite with space, and move back again
                Console.Write("\b \b");
            }
            // add character
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                passwordBuilder.Append(keyInfo.KeyChar);
                Console.Write("*");
            }
        }
        Console.WriteLine();
        return passwordBuilder.ToString();
    }
}
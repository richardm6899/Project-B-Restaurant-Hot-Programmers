static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
    {
        Console.WriteLine("Welcome to hot peppers");
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine();
        Console.WriteLine("Please enter your password");
        string password = Console.ReadLine();
        AccountModel acc = accountsLogic.CheckLogin(email, password);
        if (acc != null)
        {
            Console.WriteLine("Welcome back " + acc.FullName);
            Console.WriteLine("Your email is " + acc.EmailAddress);
            System.Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
            //Write some code to go back to the menu
            Menu.Start();
        }
        else
        {
            Console.WriteLine("No account found with that email or password, you will be sent back to the main menu");
            System.Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
            Menu.Start();
        }
    }
}
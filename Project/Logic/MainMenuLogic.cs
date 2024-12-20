class MainMenuLogic
{

    public static bool Selected(int selectedIndex)
    {
        switch (selectedIndex)
        {
            // Login
            case 0: 
                UserLogin.Start();
                break;
            // Make an Account
            case 1: 
                UserMakeAccount.Start();
                break;
            case 2: // Look at the Menu
                FoodMenuDisplay.Start();
                break;
            case 3: // Look at Info
                RestaurantInfo.Start();
                break;
            case 4: // Quit
                Console.WriteLine("Goodbye...");
                Console.WriteLine("[press Enter to quit]");
                Console.ReadLine();
                return false; // Exit the loop
        }
        return true; // Keep running the menu
    }
}
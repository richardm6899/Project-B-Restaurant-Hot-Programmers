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
            case 3:
                ApplicationMenu.Start();
                break;
            case 4: // Look at Info
                RestaurantInfo.Start();
                break;

            case 5: // Quit
            System.Console.WriteLine("Goodbye. \nPress [enter] to continue.");
            Console.ReadKey();
                Environment.Exit(0);
                break;
                // Exit the loop
        }
        return true; // Keep running the menu
    }
}
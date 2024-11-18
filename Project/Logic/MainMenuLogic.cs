class MainMenuLogic
{
    //  go up


    public static void DisplayOptions(string[] options, int selectedIndex)
    {
        for (int i = 0; i < options.Length; i++)
        {
            if (i == selectedIndex)
            {
                Console.ForegroundColor = ConsoleColor.Green; // Highlight the selected option
                Console.WriteLine($"> {options[i]}");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"  {options[i]}");
            }
        }
    }

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
                FoodMenuDisplay.StartFoodMenu(default);
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
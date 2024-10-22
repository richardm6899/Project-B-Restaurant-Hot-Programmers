static class Menu
{
    
    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        System.Console.WriteLine("Welome to:");
        // asscii art banner
        System.Console.WriteLine(@"
 _    _   _____   _______      _____   ______       __      _______ 
| |  | | |  _  | |__   __|    |  ___| |  ____|     /  \    |__   __|
| |__| | | | | |    | |       | |___  | |____     / /\ \      | |   
|  __  | | | | |    | |       |___  | |  ____|   / ____ \     | |   
| |  | | | |_| |    | |        ___| | | |____   / /    \ \    | |   
|_|  |_| |_____|    |_|       |_____| |______| /_/      \_\   |_|  ");
        Console.WriteLine("Enter 1 to login");
        Console.WriteLine("Enter 2 to do make an account");
        Console.WriteLine("Enter 3 to look at the menu");
        Console.WriteLine("Enter 4 to look at info");
        
        string input = Console.ReadLine();
        if (input == "1")
        {
            UserLogin.Start();
        }
        else if (input == "2")
        {
            UserMakeAccount.Start();
            Start();
        }
        else if (input == "3")
        {
            FoodMenuDisplay.StartFoodMenu(default);
            Start();
        }
        else if (input == "4")
        {
            RestaurantInfo.Start();
            Start();
        }
        else
        {
            Console.WriteLine("Invalid input");
            Start();
        }

    }
}

static class Menu
{
    static public void Start()
    {
        string[] options = {
            "Login",
            "Make an Account",
            "Look at the Menu",
            "Look at Info",
            "Quit"
        };
        bool running_start = true;
        int selectedIndex = 0;

        while (running_start)
        {

            // Console.Clear(); Commented because it intervenes with how the user sees how much time he needs to wait to log in
            System.Console.WriteLine("Welcome to:");
            // ASCII art banner
            // Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(@"
 _    _   _____   _______  
| |  | | |  _  | |__   __| 
| |__| | | | | |    | |     
|  __  | | | | |    | |      
| |  | | | |_| |    | |       
|_|  |_| |_____|    |_|      ");
            Console.ResetColor();
            Console.WriteLine(@"
      _____   ______       __      _______ 
     |  ___| |  ____|     /  \    |__   __|
     | |___  | |____     / /\ \      | |   
     |___  | |  ____|   / ____ \     | |   
      ___| | | |____   / /    \ \    | |   
     |_____| |______| /_/      \_\   |_|   ");


            Console.WriteLine("Use the arrow keys to navigate and press Enter to select:");

            // Display Menu
            ChoicesLogic.DisplayOptions(options, selectedIndex);

            // Get user key press
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            ConsoleKey key = keyInfo.Key;

            if (key == ConsoleKey.UpArrow)
            {
                selectedIndex = (selectedIndex == 0) ? options.Length - 1 : selectedIndex - 1;
            }
            // go down
            else if (key == ConsoleKey.DownArrow)
            {
                selectedIndex = (selectedIndex == options.Length - 1) ? 0 : selectedIndex + 1;
            }
            // choose
            else if (key == ConsoleKey.Enter)
            {
                // enter selected thing
                running_start = MainMenuLogic.Selected(selectedIndex);
            }
        }
    }
}
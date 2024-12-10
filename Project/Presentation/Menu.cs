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
            Console.Clear();
            System.Console.WriteLine("Welcome to:");
            // ASCII art banner
            // Console.BackgroundColor = ConsoleColor.DarkBlue;
            string mainprompt = @"
 _    _   _____   _______       _____   ______       __      _______ 
| |  | | |  _  | |__   __|     |  ___| |  ____|     /  \    |__   __|
| |__| | | | | |    | |        | |___  | |____     / /\ \      | |   
|  __  | | | | |    | |        |___  | |  ____|   / ____ \     | |   
| |  | | | |_| |    | |         ___| | | |____   / /    \ \    | |   
|_|  |_| |_____|    |_|        |_____| |______| /_/      \_\   |_|   ";

            // Display Menu and use arrow keys
            MainMenuLogic.Selected(HelperPresentation.ChooseOption(mainprompt, options, selectedIndex));
        }
    }
}
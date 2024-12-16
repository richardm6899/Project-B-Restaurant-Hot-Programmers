class HelperPresentation
{
    public static bool YesOrNo(string prompt)
    {
        int selectedIndex = 0;  // Start with "Yes" as the default selection
        string[] options = { "Yes", "No" };

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"{prompt}\n");  // Display the prompt (e.g., "Are you sure?")

            // show options
            for (int i = 0; i < options.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green; // Highlight current selection
                    Console.WriteLine($"> {options[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {options[i]}");
                }
            }

            //get user input
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            ConsoleKey key = keyInfo.Key;

            if (key == ConsoleKey.UpArrow)
            {
                // Go up
                selectedIndex = (selectedIndex == 0) ? options.Length - 1 : selectedIndex - 1;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                // Go down
                selectedIndex = (selectedIndex == options.Length - 1) ? 0 : selectedIndex + 1;
            }
            else if (key == ConsoleKey.Enter)
            {
                // Return true for Yes, false for No
                return selectedIndex == 0;
            }
        }
    }

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
                Console.ResetColor();
                Console.WriteLine($"  {options[i]}");
            }
        }
    }

// enter a main prompt that will be shown above the given menu options, this is also connected to the move arrow keys
// to loop through a menu only this method needs to be called. 
    public static int ChooseOption(string mainprompt, string[] options, int selectedIndex)
    {
        while (true)
        {

            // clear screen to make cleaner
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(mainprompt);
            Console.ResetColor();
            System.Console.WriteLine("Use the arrow keys to navigate and press Enter to select:");
            // display options
            DisplayOptions(options, selectedIndex);
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
                // enter selected thing, use the selected index as a case in a switch case.
                return selectedIndex;
            }
        }
    }
    
    public static string DateTimeToReadableDate(DateTime dateTime) => dateTime.ToString("dd MMMM, yyy");
}
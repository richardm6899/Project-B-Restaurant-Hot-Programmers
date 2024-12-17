using System;
using System.Text;

static class HelperPresentation
{
    public static bool YesOrNo(string prompt)
    {
        int selectedIndex = 0;  // Start with "Yes" as the default selection
        string[] options = ["Yes", "No"];

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

    public static void DisplayOptions<T>(IEnumerable<T> options, int selectedIndex)
    {
        int index = 0;
        foreach (var option in options)
        {
            if (index == selectedIndex)
            {
                Console.ForegroundColor = ConsoleColor.Green; // Highlight the selected option
                Console.WriteLine($"> {option}");
                Console.ResetColor();
            }
            else
            {
                Console.ResetColor();
                Console.WriteLine($"  {option}");
            }
            index++;
        }
    }

    // enter a main prompt that will be shown above the given menu options, this is also connected to the move arrow keys
    // to loop through a menu only this method needs to be called. 
    public static int ChooseOption(string mainPrompt, string[] options, int selectedIndex)
    {
        while (true)
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(mainPrompt);
            Console.ResetColor();
            System.Console.WriteLine("Use the arrow keys to navigate and press Enter to select:");

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

    public static int ChooseItem<T>(string mainPrompt, List<T> items, int selectedIndex)
    {
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(mainPrompt);
            Console.ResetColor();
            Console.WriteLine("Use the arrow keys to navigate and press Enter to select:");

            DisplayOptions(items, selectedIndex);

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex = (selectedIndex == 0) ? items.Count - 1 : selectedIndex - 1;
                    break;
                case ConsoleKey.DownArrow:
                    selectedIndex = (selectedIndex == items.Count - 1) ? 0 : selectedIndex + 1;
                    break;
                case ConsoleKey.Enter:
                    return selectedIndex;
            }
        }
    }


    // star password
    public static string ReadPassword()
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

    public static string DateTimeToReadableDate(DateTime dateTime) => dateTime.ToString("dd MMMM, yyy");
}
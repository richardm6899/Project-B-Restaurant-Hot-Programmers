using Microsoft.VisualBasic;

class DrinkMenuLogic
{
    private List<DrinkMenuModel> _drinkMenu;


    public DrinkMenuLogic()
    {
        _drinkMenu = DrinkMenuAccess.LoadAll();
    }

    public List<DrinkMenuModel> GetAllMenuItems()
    {
        return _drinkMenu;
    }

    // gets all given allergies in json
    public List<string> GetAllAllergies()
    {
        List<string> Allergies = [];
        foreach (DrinkMenuModel drink in _drinkMenu)
        {
            foreach (string allergy in drink.Allergies)
            {
                if (!Allergies.Contains(allergy))
                {
                    if (allergy != "none")
                    {
                        Allergies.Add(allergy);
                    }
                }
                else
                {
                    continue;
                }
            }
        }
        return Allergies;
    }

    private DrinkMenuModel GetMenuItemByName(string drinkName)
    {
        // if no drink found returns null, else returns drink.
        return _drinkMenu.FirstOrDefault(item => item.DrinkName == drinkName);
    }

    public List<DrinkMenuModel> GetMenuExcludingAllergies(List<string> allergiesToAvoid)
    {
        if (allergiesToAvoid != null)
        {
            return _drinkMenu.Where(item =>
            item.Allergies == null || !item.Allergies.Any(allergy => allergiesToAvoid.Contains(allergy))
        ).ToList();
        }
        else return null;
    }

    public void AddDrink(string drinkName, float price, string description, List<string> type, List<string> allergies)
    {
        // Determine the next available ID
        int newId = _drinkMenu.Count > 0 ? _drinkMenu.Max(d => d.Id) + 1 : 1;

        // Create the new drink model
        DrinkMenuModel newDrink = new DrinkMenuModel(newId, drinkName, price, description, type, allergies);

        // Add to the menu list
        _drinkMenu.Add(newDrink);

        // Save updated list to JSON
        DrinkMenuAccess.WriteAll(_drinkMenu);
    }

    private List<DrinkMenuModel> GetByType(string type)
    {
        //  make new list with typed drinks
        List<DrinkMenuModel> TypedDrinks = new();

        foreach (DrinkMenuModel drink in _drinkMenu)
        {
            if (drink.Type.Contains(type))
            {
                // add drink to list
                TypedDrinks.Add(drink);
            }
        }

        return TypedDrinks; // Return the filtered list
    }
    // public string DeleteDrinkByName(string drinkName)
    // {
    //     // Find the drink by name (case-insensitive search)
    //     var drinkToRemove = GetMenuItemByName(drinkName);
    //     if (drinkToRemove != null)
    //     {
    //         if (DrinkMenuDisplay.ConfirmationForDeletion(drinkName))
    //         {
    //             _drinkMenu.Remove(drinkToRemove);

    //             DrinkMenuAccess.WriteAll(_drinkMenu);
    //             return "Dish was succesfully deleted";
    //         }
    //         else
    //         {
    //             return "Deletion has stopped.";
    //         }

    //     }

    //     return "Dish was not found.";
    // }

    private static bool MainSelected(int selectedIndex)
    {
        switch (selectedIndex)
        {
            // look at whole menu
            case 0:
                DrinkMenuDisplay.DisplayWholeMenu();
                break;
            // filter by
            case 1:
                DrinkMenuDisplay.DisplayByType();
                break;
            // filter by
            case 2:
                DrinkMenuDisplay.DisplayByAllergy();
                break;
            // return to where the user came from
            case 3:

                Console.ReadLine();
                return false; // Exit the loop
        }
        return true; // Keep running the menu
    }

    public static void GetOptionMain(string[] options)
    {
        int selectedIndex = 0;
        bool lookingAtDrinks = true;
        while (lookingAtDrinks)
        {
            Console.Clear();
            Console.ResetColor();
            System.Console.WriteLine("Welcome to the drinks menu.");
            ChoicesLogic.DisplayOptions(options, selectedIndex);

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
                lookingAtDrinks = DrinkMenuLogic.MainSelected(selectedIndex);
            }
        }
    }

    public static List<DrinkMenuModel> GetOptionTypes(string[] options)
    {
        int selectedIndex = 0;
        bool lookingAtDrinks = true;
        while (lookingAtDrinks)
        {
            Console.Clear();
            Console.ResetColor();
            System.Console.WriteLine("Welcome to the types menu.");
            ChoicesLogic.DisplayOptions(options, selectedIndex);

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            ConsoleKey key = keyInfo.Key;

            if (key == ConsoleKey.UpArrow)
            {
                selectedIndex = (selectedIndex == 0) ? options.Length - 1 : selectedIndex - 1;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                selectedIndex = (selectedIndex == options.Length - 1) ? 0 : selectedIndex + 1;
            }
            else if (key == ConsoleKey.Enter)
            {
                // Call TypeSelected and exit the menu
                return DrinkMenuLogic.TypeSelected(selectedIndex);
            }
        }

        return null; // Default return in case the loop exits unexpectedly
    }

    private static List<DrinkMenuModel> TypeSelected(int selectedIndex)
    {
        DrinkMenuLogic drinkMenuLogic = new();

        switch (selectedIndex)
        {
            case 0:
                return drinkMenuLogic.GetByType("Soft Drink");
            case 1:
                return drinkMenuLogic.GetByType("Hot Drink");
            case 2:
                return drinkMenuLogic.GetByType("Alcohol");
            case 3:
                return drinkMenuLogic.GetByType("Non-Alcoholic");
            case 4:
                return null; // User wants to exit
            default:
                return new List<DrinkMenuModel>(); // Fallback for unexpected cases
        }
    }

    public static List<string> SelectAllergies(List<string> availableAllergies)
    {
        List<string> selectedAllergies = new();
        int selectedIndex = 0;
        bool selecting = true;

        while (selecting)
        {
            Console.Clear();
            Console.WriteLine("Select allergies to avoid (press Enter to select, 'Done' to confirm):");

            // show all allergies, highlight the one the user is on
            for (int i = 0; i < availableAllergies.Count; i++)
            {
                if (i == selectedIndex)
                {
                    // Highlight the current option
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("> ");
                }
                else
                {
                    Console.Write("  ");
                }

                // Mark selected allergies
                if (selectedAllergies.Contains(availableAllergies[i]))
                {
                    Console.WriteLine($"{availableAllergies[i]} [Selected]");
                }
                else
                {
                    Console.WriteLine(availableAllergies[i]);
                }

                Console.ResetColor();
            }

            // make a done option
            if (selectedIndex == availableAllergies.Count)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("> Done");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("  Done");
            }

            // make a return option
            if (selectedIndex == availableAllergies.Count + 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("> Return");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("  Return");
            }

            // user input, arrow keys
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
            {
                selectedIndex = (selectedIndex == 0) ? availableAllergies.Count + 1 : selectedIndex - 1;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                selectedIndex = (selectedIndex == availableAllergies.Count + 1) ? 0 : selectedIndex + 1;
            }
            else if (key == ConsoleKey.Enter)
            {
                // Toggle allergy selection or confirm selection
                if (selectedIndex == availableAllergies.Count)
                {
                    // when done is clicked, exit loop
                    selecting = false;
                }
                else if (selectedIndex == availableAllergies.Count + 1)
                {
                    // Return option chosen
                    Console.WriteLine("Returning to the previous menu...");
                    Console.ReadKey();
                    return null; // Indicate that the user canceled the selection
                }

                else
                {
                    string allergy = availableAllergies[selectedIndex];
                    // if already selected, and gets selected again, item is removed
                    if (selectedAllergies.Contains(allergy))
                    {
                        selectedAllergies.Remove(allergy);
                    }
                    else
                    {
                        // allergy is added
                        selectedAllergies.Add(allergy);
                    }
                }
            }
        }
        return selectedAllergies;
    }
}

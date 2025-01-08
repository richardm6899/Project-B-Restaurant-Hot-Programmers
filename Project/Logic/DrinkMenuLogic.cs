using Microsoft.VisualBasic;

class DrinkMenuLogic
{
    private List<DrinkMenuModel> _drinkMenu;
    static private DrinkMenuAccess drinkMenuAccess = new();

    public DrinkMenuLogic()
    {
        _drinkMenu = drinkMenuAccess.LoadAll();
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
        drinkMenuAccess.WriteAll(_drinkMenu);
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
            HelperPresentation.ChooseOption("Welcome to the drinks menu.", options, selectedIndex);

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
            HelperPresentation.ChooseOption("Welcome to the types menu.", options, selectedIndex);

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
}

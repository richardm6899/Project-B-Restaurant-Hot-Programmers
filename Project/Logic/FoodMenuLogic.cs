public class FoodMenuLogic
{
    public List<FoodMenuModel> _foodMenu;

    public FoodMenuLogic()
    {
        _foodMenu = FoodMenuAccess.LoadAll();
    }

    // Method to return all food menu items
    public List<FoodMenuModel> GetAllMenuItems()
    {
        return _foodMenu;
    }

    public List<string> GetAllAllergies()
    {
        List<string> Allergies = [];
        foreach (FoodMenuModel dish in _foodMenu)
        {
            foreach (string allergy in dish.Allergies)
            {
                if (!Allergies.Contains(allergy))
                {
                    if (allergy != "None")
                    {
                        if (allergy != "none")
                        {
                            Allergies.Add(allergy);
                        }
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

    public List<string> GetAllTypes()
    {
        List<string> Types = [];
        foreach(FoodMenuModel dish in _foodMenu)
        {
            foreach(string type in dish.Type)
            {
                if(!Types.Contains(type))
                {
                    if(type != "none")
                    {
                        Types.Add(type);
                    }
                }
                else
                {
                    continue;
                }
            }
        }
        return Types;
    }
    // Method to return food menu item by name
    public FoodMenuModel GetMenuItemByName(string dishName)
    {
        return _foodMenu.FirstOrDefault(item => item.DishName == dishName);
    }

    public List<FoodMenuModel> GetMenuExcludingAllergies(List<string> allergiesToAvoid)
    {
        return _foodMenu.Where(item =>
            item.Allergies == null || !item.Allergies.Any(allergy => allergiesToAvoid.Contains(allergy))
        ).ToList();
    }

    public static void FoodOrDrinksOption(string[] options)
    {
        int selectedIndex = 0;
        bool lookingAtFood = true;
        while (lookingAtFood)
        {
            Console.Clear();
            Console.ResetColor();
            HelperPresentation.DisplayOptions(options, selectedIndex);

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
                lookingAtFood = FoodMenuLogic.SelectedMenu(selectedIndex);
            }
        }
    }

    private static bool SelectedMenu(int selectedIndex)
    {
        switch (selectedIndex)
        {
            // look at whole menu
            case 0:
                FoodMenuDisplay.StartFoodMenu();
                break;
            // filter by
            case 1:
                DrinkMenuDisplay.Start();
                break;
            // filter by
            case 2:

                Console.ReadLine();
                return false; // Exit the loop
        }
        return true; // Keep running the menu
    }
    public static void GetOptionMain(string[] options)
    {
        
        int selectedIndex = 0;
        bool lookingAtFood = true;
        while (lookingAtFood)
        {
            Console.Clear();
            Console.ResetColor();
            System.Console.WriteLine("Welcome to the food menu.");
            HelperPresentation.DisplayOptions(options, selectedIndex);

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
                lookingAtFood = FoodMenuLogic.MainSelected(selectedIndex);
            }
        }
    }
    private static bool MainSelected(int selectedIndex)
    {

        switch (selectedIndex)
        {
            // look at whole menu
            case 0:
                FoodMenuDisplay.DisplayWholeMenu();
                break;
            // filter by
            case 1:
               FoodMenuDisplay.TypesFilter();
                break;
            // filter by
            case 2:
                FoodMenuDisplay.DisplayByAllergy();
                break;
            // return to where the user came from
            case 3:

                Console.ReadLine();
                return false; // Exit the loop
        }
        return true; // Keep running the menu
    }

    public static List<FoodMenuModel> GetOptionTypes(string[] options)
    {
        FoodMenuLogic foodMenuLogic = new();
        int selectedIndex = 0;
        bool lookingAtFood = true;
        while (lookingAtFood)
        {
            Console.Clear();
            Console.ResetColor();
            System.Console.WriteLine("Welcome to the types menu.");
            HelperPresentation.DisplayOptions(options, selectedIndex);

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
                return foodMenuLogic.TypeSelected(selectedIndex, options);
            }
        }

        return null; // Default return in case the loop exits unexpectedly
    }
    
    private List<FoodMenuModel> TypeSelected(int selectedIndex, string[] options)
    {
        FoodMenuLogic foodMenuLogic = new();

        List<string>typeToFilter = [];
        typeToFilter.Add(options[selectedIndex]);
        
        return GetMenuExcludingTypes(typeToFilter); // User wants to exit        
    }
    public List<FoodMenuModel> GetMenuExcludingTypes(List<string> typesToFilter)
    {
        // Create a list to store the filtered menu items
        List<FoodMenuModel> filteredMenu = new List<FoodMenuModel>();

        // Loop through each item in the food menu
        foreach (var item in _foodMenu)
        {
            if (item.Type == null)
            {
                filteredMenu.Add(item);
            }
            else
            {
                // Check if the item's types contain any of the types to filter
                bool hasTypeToFilter = false;

                foreach (var type in item.Type)
                {
                    if (typesToFilter.Contains(type))
                    {
                        hasTypeToFilter = true;
                        break; // Exit the loop if a type is found
                    }
                }

                // If no type to filter is found, add the item to the filtered list
                if (hasTypeToFilter)
                {
                    filteredMenu.Add(item);
                }
            }
        }

        // Return the filtered menu
        return filteredMenu;
    }

    

    public List<FoodMenuModel> FilterFoodPreferences(List<string> searchedTypes)
    {
        // Create a new list to store the filtered menu items
        List<FoodMenuModel> filteredMenu = new List<FoodMenuModel>();

        // Iterate over each menu item in the food menu
        foreach (var menuItem in _foodMenu)
        {
            // Check if the item's types are null or if none of its types are in the typesToExclude list
            if (menuItem.Type == null || !menuItem.Type.Any(type => searchedTypes.Contains(type)))
            {
                // Add the item to the filtered menu if it doesn't match any of the excluded types
                filteredMenu.Add(menuItem);
            }
        }

        // Return the filtered menu
        return filteredMenu;
    }

    public void AddDish(string dishName, float price, string description, List<string> type, List<string> allergies)
    {
        // Determine the next available ID
        int newId = _foodMenu.Count > 0 ? _foodMenu.Max(d => d.Id) + 1 : 1;

        // Create the new dish model
        FoodMenuModel newDish = new FoodMenuModel(newId, dishName, price, description, type, allergies);

        // Add to the menu list
        _foodMenu.Add(newDish);

        // Save updated list to JSON
        FoodMenuAccess.WriteAll(_foodMenu);
    }


    public string DeleteDishByName(string dishName)
    {
        // Find the dish by name (case-insensitive search)
        var dishToRemove = _foodMenu.FirstOrDefault(d => d.DishName.Equals(dishName, StringComparison.OrdinalIgnoreCase));
        if (dishToRemove != null)
        {
            if (FoodMenuDisplay.ConfirmationForDeletion(dishName))
            {
                _foodMenu.Remove(dishToRemove);

                FoodMenuAccess.WriteAll(_foodMenu);
                return "Dish was successfully deleted";
            }
            else
            {
                return "Deletion has stopped.";
            }

        }

        return "Dish was not found.";
    }
}
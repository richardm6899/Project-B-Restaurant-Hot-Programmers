class DrinkMenuDisplay
{
    static private DrinkMenuLogic drinkMenuLogic = new DrinkMenuLogic();

    public static void Start()
    {
        string[] options = {
            "Show whole menu",
            "Filter by type",
            "Filter by allergies",
            "Return"
        };

        int selectedIndex = 0;
        selectedIndex = HelperPresentation.ChooseOption("Welcome to the drinks menu.", options, selectedIndex);

        switch (selectedIndex)
        {
            // whole menu
            case 0:
                DisplayWholeMenu();
                break;
            // by type
            case 1:
                DisplayByType();
                break;
            // by allergies
            case 2:
                DisplayByAllergy();
                break;
            // return
            case 3:
                return;
        }
    }

    // case 0
    public static void DisplayWholeMenu()
    {
        List<DrinkMenuModel> drinks = drinkMenuLogic.GetAllMenuItems();
        foreach (DrinkMenuModel drink in drinks)
        {
            System.Console.WriteLine("\n----------------------");
            System.Console.WriteLine($"*** {drink.DrinkName} ***\n{drink.Description}\n${drink.Price:F2}");
        }
        Console.ReadKey();
    }

    // case 1
    public static void DisplayByType()
    {
        System.Console.WriteLine("Types");
        string[] options = {
            "Soft Drinks",
            "Hot Drinks",
            "Alcohol",
            "Non-Alcoholic",
            "Return"
        };
        bool drinkMenu = true;
        while (drinkMenu)
        {
            int selectedIndexDrinks = 0;
            Console.Clear();
            selectedIndexDrinks = HelperPresentation.ChooseOption("Types", options, selectedIndexDrinks);

            switch (selectedIndexDrinks)
            {
                // soft drinks
                case 0:
                    drinkMenuLogic.GetByType("Soft Drink");
                    break;
                // hot drinks
                case 1:
                    drinkMenuLogic.GetByType("Hot Drink");
                    break;
                // alcoho;
                case 2:
                    drinkMenuLogic.GetByType("Alcohol");
                    break;
                // non alcoholic
                case 3:
                    drinkMenuLogic.GetByType("Non-Alcoholic");
                    break;
                // return
                case 4:
                    break;
            }
        }
        List<DrinkMenuModel> drinks = DrinkMenuLogic.GetOptionTypes(options);
        if (drinks != null)
        {
            foreach (DrinkMenuModel drink in drinks)
            {
                System.Console.WriteLine("\n----------------------");
                System.Console.WriteLine($"*** {drink.DrinkName} ***\n{drink.Description}\n${drink.Price:F2}");
            }
        }
        Console.ReadKey();
    }

    public static void DisplayByAllergy()
    {
        System.Console.WriteLine("Allergies");
        // all allergies in the json
        List<string> allergies = drinkMenuLogic.GetAllAllergies();
        List<string>? selectedAllergies = HelperPresentation.SelectAllergies(allergies);
        List<DrinkMenuModel>? drinks = drinkMenuLogic.GetMenuExcludingAllergies(selectedAllergies ?? new List<string>());
        if (drinks == null || drinks.Count == 0)
        {
            return;
        }
        if (drinks.Count == 0)
        {
            Console.WriteLine("No menu items available based on the selected filters.");
        }
        if (selectedAllergies != null)
        {
            foreach (DrinkMenuModel drink in drinks)
            {
                System.Console.WriteLine("\n----------------------");
                System.Console.WriteLine($"*** {drink.DrinkName} ***\n{drink.Description}\n${drink.Price:F2}\n allergies:");
                foreach (string allergy in drink.Allergies)
                {
                    System.Console.Write(allergy + " ");
                }
            }
        }
        Console.ReadKey();
    }
}
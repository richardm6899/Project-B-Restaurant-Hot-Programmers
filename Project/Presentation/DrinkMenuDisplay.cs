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
                    List<DrinkMenuModel> softDrinks = drinkMenuLogic.GetByType("Soft Drink");
                    foreach (DrinkMenuModel softdrink in softDrinks)
                    {
                        System.Console.WriteLine($@"--------------------
{softdrink.DrinkName}
{softdrink.Description}
{softdrink.Price}");
                    }
                    System.Console.WriteLine("Press [enter] to continue");
                    Console.ReadKey();
                    break;
                // hot drinks
                case 1:
                    List<DrinkMenuModel> hotDrinks = drinkMenuLogic.GetByType("Hot Drink");
                    foreach (DrinkMenuModel hotdrink in hotDrinks)
                    {
                        System.Console.WriteLine($@"--------------------
{hotdrink.DrinkName}
{hotdrink.Description}
{hotdrink.Price}");
                    }
                    System.Console.WriteLine("Press [enter] to continue");
                    Console.ReadKey();
                    break;
                // alcoho;
                case 2:
                    List<DrinkMenuModel> alcohols = drinkMenuLogic.GetByType("Alcohol");
                    foreach (DrinkMenuModel alcohol in alcohols)
                    {
                        System.Console.WriteLine($@"--------------------
{alcohol.DrinkName}
{alcohol.Description}
{alcohol.Price}");
                    }
                    System.Console.WriteLine("Press [enter] to continue");
                    Console.ReadKey();
                    break;
                // non alcoholic
                case 3:
                    List<DrinkMenuModel> nonAlcohols = drinkMenuLogic.GetByType("Non-Alcoholic");
                    foreach (DrinkMenuModel nonAlcohol in nonAlcohols)
                    {
                        System.Console.WriteLine($@"--------------------
{nonAlcohol.DrinkName}
{nonAlcohol.Description}
{nonAlcohol.Price}");
                    }
                    System.Console.WriteLine("Press [enter] to continue");
                    Console.ReadKey();
                    break;
                // return
                case 4:
                    return;
            }
        }
        List<DrinkMenuModel>? drinks = DrinkMenuLogic.GetOptionTypes(options);
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
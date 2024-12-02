class DrinkMenuDisplay
{
    static private DrinkMenuLogic drinkMenuLogic = new DrinkMenuLogic();

    public static void Start()
    {
        string[] options = {
            "Show whole menu",
            "Sort by type",
            "Sort by allergies",
            "Return"
        };
        DrinkMenuLogic.GetOptionMain(options);
        return;
    }

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
        List<string> selectedAllergies = DrinkMenuLogic.SelectAllergies(allergies);
        List<DrinkMenuModel> drinks = drinkMenuLogic.GetMenuExcludingAllergies(selectedAllergies);
        if(drinks.Count == 0)
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
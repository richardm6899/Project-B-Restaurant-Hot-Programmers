public class FoodMenuDisplay
{
    static private FoodMenuLogic foodMenuLogic = new FoodMenuLogic();

    public static void Start()
    {
        Console.WriteLine("Do you want to exclude certain allergies? (Type 'yes' or 'no')");
        string excludeChoice = Console.ReadLine().ToLower();

        List<FoodMenuModel> menuItems;

        if (excludeChoice == "yes")
        {
            // Get allergies from the user 
            Console.WriteLine("Enter allergies to avoid (comma-separated):");
            string input = Console.ReadLine();
            List<string> allergiesToAvoid = input.Split(',').Select(allergy => allergy.Trim()).ToList();

            // Get the filtered menu items
            menuItems = foodMenuLogic.GetMenuExcludingAllergies(allergiesToAvoid);
        }
        else
        {
            // Show all menu items if no allergies are excluded
            menuItems = foodMenuLogic.GetAllMenuItems();
        }

        // Display the menu
        DisplayMenuItems(menuItems);

        System.Console.WriteLine("Press enter to return to the menu.");
        Console.ReadLine();
        return;
    }

    private static void DisplayMenuItems(List<FoodMenuModel> menuItems)
    {
        if (menuItems.Any())
        {
            foreach (var item in menuItems)
            {
                // Print each menu item's details in a user-friendly format
                Console.WriteLine($"Dish Name: {item.DishName}");
                Console.WriteLine($"Price: {item.Price}$");
                Console.WriteLine($"Description: {item.Description}");
                Console.WriteLine($"Type: {item.Type}");
                Console.WriteLine($"Allergies: {string.Join(", ", item.Allergies ?? new List<string>())}");
                Console.WriteLine(new string('-', 40)); // Separator for better readability
            }
        }
        else
        {
            Console.WriteLine("No menu items available based on the selected filters.");
        }
    }
}
public class FoodMenuDisplay
{
    static private FoodMenuLogic foodMenuLogic = new FoodMenuLogic();

    public static void StartFoodMenu(List<string> allergies)
    {
        bool wrong = false;
        List<FoodMenuModel> menuItems = new List<FoodMenuModel>();
        if (allergies != null)
        {            
            do
            {
                Console.WriteLine("Do you want to exclude your allergies? (Type 'yes' or 'no')");
                string excludeChoice = Console.ReadLine().ToLower();    
                switch (excludeChoice)
                {
                    case "yes":
                        // Get allergies from the account 
                        List<string> allergiesToAvoid = allergies;

                        // Get the filtered menu items
                        menuItems = foodMenuLogic.GetMenuExcludingAllergies(allergiesToAvoid);
                        wrong = false;
                        break;

                    case "no":
                        menuItems = foodMenuLogic.GetAllMenuItems();
                        wrong = false;
                        break;

                    default:
                        Console.WriteLine("This is a wrong input, put in 'yes' or 'no'");
                        wrong = true;
                        break;
                }
            } while (wrong == true);
        }
        else
        {
            do
            {
                Console.WriteLine("Do you want to exclude certain allergies? (Type 'yes' or 'no')");
                string excludeChoice = Console.ReadLine().ToLower();
                switch (excludeChoice)
                {
                    case "yes":
                        // Get allergies from the user 
                        Console.WriteLine("Enter allergies to avoid (comma-separated):");
                        string input = Console.ReadLine();
                        List<string> allergiesToAvoid = input.Split(',').Select(allergy => allergy.Trim()).ToList();

                        // Get the filtered menu items
                        menuItems = foodMenuLogic.GetMenuExcludingAllergies(allergiesToAvoid);
                        wrong = false;
                        break;

                    case "no":
                        menuItems = foodMenuLogic.GetAllMenuItems();
                        wrong = false;
                        break;

                    default:
                        Console.WriteLine("This is a wrong input, put in 'yes' or 'no'");
                        wrong = true;
                        break;
                }
            } while (wrong == true);
        }



        // Display the menu
        DisplayMenuItems(menuItems);
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
                Console.WriteLine($"Type: {string.Join(", ", item.Type ?? new List<string>())}");
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
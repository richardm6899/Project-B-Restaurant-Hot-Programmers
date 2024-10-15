public class FoodMenuDisplay
{
    static private FoodMenuLogic foodMenuLogic = new FoodMenuLogic();

    public static void Start()
    {
        var allMenuItems = foodMenuLogic.GetAllMenuItems();
        
        foreach (var item in allMenuItems)
        {
            // Print each menu item's details in a user-friendly format
            Console.WriteLine($"ID: {item.Id}");
            Console.WriteLine($"Dish Name: {item.DishName}");
            Console.WriteLine($"Price: {item.Price}$");
            Console.WriteLine($"Description: {item.Description}");
            Console.WriteLine($"Type: {item.Type}");
            Console.WriteLine($"Allergies: {item.Allergies}");
            Console.WriteLine(new string('-', 40)); // Separator for better readability
        }    
    }


}
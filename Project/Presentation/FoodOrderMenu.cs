class FoodOrderMenu
{
    static private FoodOrderLogic foodOrderLogic = new FoodOrderLogic();

    public static List<(FoodMenuModel, int)> OrderFood()
    {
        // Get all menu items
        List<FoodMenuModel> foodMenu = foodOrderLogic.GetAllMenuItems();
        List<(FoodMenuModel, int)> FoodCart = new List<(FoodMenuModel, int)>();
        FoodMenuModel[] foodChoices = foodMenu.ToArray();

        OrderFoodMenu(foodChoices, FoodCart);
        return FoodCart;
    }

    public static void OrderFoodMenu(FoodMenuModel[] options, List<(FoodMenuModel, int)> FoodCart)
    {
        int selectedIndex = 0;
        bool selecting = true;

        while (selecting)
        {
            Console.Clear();

            // Display the food options with the current selected index
            DisplayFoodOptions(options, selectedIndex, FoodCart);

            // Display menu options
            if (selectedIndex == options.Length)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("> View Cart");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("  View Cart");
            }

            if (selectedIndex == options.Length + 1)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("> Done");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("  Done");
            }

            if (selectedIndex == options.Length + 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("> Return");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("  Return");
            }

            // Handle user input
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
            {
                selectedIndex = (selectedIndex == 0) ? options.Length + 2 : selectedIndex - 1;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                selectedIndex = (selectedIndex == options.Length + 2) ? 0 : selectedIndex + 1;
            }
            else if (key == ConsoleKey.Enter)
            {
                if (selectedIndex < options.Length)
                {
                    // Add the selected dish to the cart
                    var selectedDish = options[selectedIndex];
                    FoodOrderLogic.AddToCart(FoodCart, selectedDish);
                }
                else if (selectedIndex == options.Length)
                {
                    // "View Cart" option selected
                    ViewCart(FoodCart);
                }
                else if (selectedIndex == options.Length + 1)
                {
                    // "Done" option selected

                    selecting = false;

                }
                else if (selectedIndex == options.Length + 2)
                {
                    // "Return" option selected
                    FoodCart.Clear();
                    selecting = false;
                }
            }
        }
    }

    public static void DisplayFoodOptions(FoodMenuModel[] options, int selected, List<(FoodMenuModel, int)> FoodCart)
    {
        int index = 0;
        foreach (var item in options)
        {
            if (selected == index)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("> ");
            }
            else
            {
                Console.Write("  ");
            }

            int quantityInCart = FoodCart.Where(f => f.Item1.Id == item.Id).Sum(f => f.Item2);
            Console.WriteLine($"{item.DishName} {(quantityInCart > 0 ? $"[x{quantityInCart}]" : "")}");
            Console.ResetColor();
            Console.WriteLine($"Price: {item.Price:F2}$");
            Console.WriteLine($"Description: {item.Description}");
            Console.WriteLine(new string('-', 40));
            index++;
        }
    }

    public static void ViewCart(List<(FoodMenuModel, int)> cart)
    {
        Console.Clear();
        Console.WriteLine("Your Cart:");
        Console.WriteLine(new string('=', 40));

        if (cart.Count == 0)
        {
            Console.WriteLine("Your cart is empty.");
        }
        else
        {
            foreach (var (item, quantity) in cart)
            {
                Console.WriteLine($"{item.DishName} x{quantity}");
                Console.WriteLine($"Price: {item.Price:F2}$ each");
                Console.WriteLine($"Total: {(item.Price * quantity):F2}$");
                Console.WriteLine(new string('-', 40));
            }
        }

        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey(true);
    }
}

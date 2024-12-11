class FoodOrderMenu
{
    static private FoodMenuLogic foodMenuLogic = new FoodMenuLogic();
    static private FoodOrderLogic foodOrderLogic = new FoodOrderLogic();

    public static (List<(FoodMenuModel, int)>, List<List<string>>) OrderFood()
    {
        // Get all menu items
        List<FoodMenuModel> foodMenu = foodOrderLogic.GetAllMenuItems();
        List<(FoodMenuModel, int)> FoodCart = new List<(FoodMenuModel, int)>();
        List<List<string>> allergies = new List<List<string>>();

        FoodMenuModel[] foodChoices = foodMenu.ToArray();
        OrderFoodMenu(foodChoices, FoodCart, allergies);

        return (FoodCart, allergies);
    }

    public static void OrderFoodMenu(FoodMenuModel[] options, List<(FoodMenuModel, int)> FoodCart, List<List<string>> allergies)
    {
        int selectedIndex = 0;
        bool selecting = true;

        while (selecting)
        {
            Console.Clear();
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
                    var selectedDish = options[selectedIndex];
                    FoodOrderLogic.AddToCart(FoodCart, selectedDish);

                    // If the dish is "Chef's Menu", ask for allergies based on quantity
                    if (selectedDish.Type.Contains("Chef's Menu"))
                    {
                        Console.WriteLine($"Please select allergies for '{selectedDish.DishName}):");
                        var allergyList = AskForAllergies();
                        allergies.Add(allergyList ?? new List<string>());
                    }
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



    public static List<string> AskForAllergies()
    {
        string prompt = "Do you have any allergies our chef needs to take into account?";
        if (HelperPresentation.YesOrNo(prompt))
        {
            string[] options = foodMenuLogic.GetAllAllergies().ToArray();
            List<string> selectedAllergies = new List<string>();

            int selectedIndex = 0;
            bool selecting = true;

            while (selecting)
            {

                Console.Clear();
                Console.WriteLine("Please select your allergies (use arrow keys to navigate and Enter to select/deselect):\n");

                // Display allergy options
                for (int i = 0; i < options.Length; i++)
                {
                    if (selectedIndex == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"> {options[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write($"  {options[i]}");
                    }

                    // Indicate if the allergy is already selected
                    if (selectedAllergies.Contains(options[i]))
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(" [Selected]");
                        Console.ResetColor();
                    }

                    Console.WriteLine();
                }

                // Display additional options
                if (selectedIndex == options.Length)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("> No Allergies");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("  No Allergies");
                }

                if (selectedIndex == options.Length + 1)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("> Other (Specify)");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("  Other (Specify)");
                }

                if (selectedIndex == options.Length + 2)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("> Done");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("  Done");
                }

                if (selectedIndex == options.Length + 3)
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
                    selectedIndex = (selectedIndex == 0) ? options.Length + 3 : selectedIndex - 1;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    selectedIndex = (selectedIndex == options.Length + 3) ? 0 : selectedIndex + 1;
                }
                else if (key == ConsoleKey.Enter)
                {
                    if (selectedIndex < options.Length)
                    {
                        var selectedAllergy = options[selectedIndex];
                        if (selectedAllergies.Contains(selectedAllergy))
                        {
                            selectedAllergies.Remove(selectedAllergy); // Deselect if already selected
                        }
                        else
                        {
                            selectedAllergies.Add(selectedAllergy); // Select the allergy
                        }
                    }
                    else if (selectedIndex == options.Length)
                    {
                        // "No Allergies" option selected
                        return new List<string>();
                    }
                    else if (selectedIndex == options.Length + 1)
                    {
                        // "Other" option selected - prompt for custom allergy
                        Console.Write("\nEnter custom allergy: ");
                        string customAllergy = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(customAllergy) && !selectedAllergies.Contains(customAllergy))
                        {
                            selectedAllergies.Add(customAllergy);
                        }
                    }
                    else if (selectedIndex == options.Length + 2)
                    {
                        // "Done" option selected
                        selecting = false;
                    }
                    else if (selectedIndex == options.Length + 3)
                    {
                        // "Return" option selected
                        return null;
                    }
                }
            }

            return selectedAllergies;
        }

        // If the user says "No" to the initial prompt
        return new List<string>();
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

public class FoodMenuDisplay
{
    static private FoodMenuLogic foodMenuLogic = new FoodMenuLogic();

    public static void Start()
    {
        string[] options = {
            "Food menu",
            "Drink menu",
            "Return"
        };
        FoodMenuLogic.FoodOrDrinksOption(options);
        return;
    }

    public static void StartFoodMenu()
    {
        string[] options = {
            "Show whole menu",
            "Filter by type",
            "Filter by allergies",
            "Return"
        };
        FoodMenuLogic.GetOptionMain(options);
        return;
    }



    public static void AllergiesQuestion(List<string> allergies)
    {
        bool wrong = true;
        List<FoodMenuModel> menuItems = new List<FoodMenuModel>();
        Console.WriteLine("Welcome to the menu of Hot Seat!");
        do{
            Console.WriteLine("Would you like to filter for allergies or types of food? (yes or no)");
        
            string filteranswer = Console.ReadLine().ToLower();

            switch(filteranswer)
            {
                case "yes":
                    
                    do
                    {
                    Console.WriteLine("What filter: allergies or types?");
                    string filter = Console.ReadLine().ToLower();
                    switch(filter)
                    {
                        case "allergies":
                           // AllergiesFilter(allergies);
                            wrong = false;
                            break;
                        case "types":
                            TypesFilter();
                            wrong = false;
                            break;
                        default:
                            Console.WriteLine("This is an invalid input");
                            wrong = true;
                            break;
                            
                    }
                    }while(wrong);
                    break;
            
                case "no":
                    menuItems = foodMenuLogic.GetAllMenuItems();
                    DisplayWholeMenu();
                    wrong = false;
                    break;
                default:
                    Console.WriteLine("This is an invalid input");
                    wrong = true;
                    break;
                
            }
            }while(wrong);


        }

    public static void TypesFilter()
    {
        Console.WriteLine("Types");
        List<string> allTypes = foodMenuLogic.GetAllTypes();
        string[] allTypesOptions = allTypes.ToArray();
        List<FoodMenuModel> menuItems = FoodMenuLogic.GetOptionTypes(allTypesOptions);
        FoodMenuDisplay.DisplayMenuWithFilter(menuItems);

    }
    // private static void AllergiesFilter(List<string> allergies)
    // {
    //     bool wrong = false;
    //     List<FoodMenuModel> menuItems = new List<FoodMenuModel>();
    //     if (allergies != null)
    //     {            
    //         do
    //         {
    //             Console.WriteLine("Do you want to exclude your allergies? (Type 'yes' or 'no')");
    //             string excludeChoice = Console.ReadLine().ToLower();
    //             switch (excludeChoice)
    //             {
    //                 case "yes":
    //                     // Get allergies from the account 
    //                     List<string> allergiesToAvoid = allergies;

    //                     // Get the filtered menu items
    //                     menuItems = foodMenuLogic.GetMenuExcludingAllergies(allergiesToAvoid);
    //                     wrong = false;
    //                     break;

    //                 case "no":
    //                     menuItems = foodMenuLogic.GetAllMenuItems();
    //                     wrong = false;
    //                     break;

    //                 default:
    //                     Console.WriteLine("This is a wrong input, put in 'yes' or 'no'");
    //                     wrong = true;
    //                     break;
    //             }
    //         } while (wrong == true);
    //     }
    //     else
    //     {

    //         // Get allergies from the user 
    //         allergies = foodMenuLogic.GetAllAllergies();
    //         Console.WriteLine($"Allergies:");
    //         foreach(string allergy in allergies)
    //         {
    //             Console.WriteLine($"-{allergy}");
    //         }
            
    //         Console.WriteLine("Enter allergies to avoid (comma-separated):");
    //         string input = Console.ReadLine();
    //         if(input != null)
    //         {
    //             char.ToUpper(input[0]);
    //         }
    //         List<string> allergiesToAvoid = input.Split(',').Select(allergy => allergy.Trim()).ToList();

    //         // Get the filtered menu items
    //         menuItems = foodMenuLogic.GetMenuExcludingAllergies(allergiesToAvoid);                
    //     }



    //     // Display the menu
    //     foodMenuLogic.DisplayWholeMenu();
    // }

        public static void DisplayMenuWithFilter(List<FoodMenuModel> menuItems)
    {
        if (menuItems.Any())
        {
            
            // Define the desired display order
            var displayOrder = new List<string> { "Appetizer", "Main Course", "Side", "Dessert" };

            // Group and order menu items based on the display order
            var orderedMenu = menuItems
                .OrderBy(item => displayOrder.IndexOf(item.Type.FirstOrDefault() ?? ""))
                .GroupBy(item => item.Type.FirstOrDefault() ?? "Other");

            foreach (var group in orderedMenu)
            {
                Console.WriteLine(new string('=', 40));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\x1b[1m\x1b[4m{group.Key}\x1b[0m");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(new string('=', 40));

                foreach (var item in group)
                {
                    // Print each menu item's details
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\x1b[1m{item.DishName}\x1b[0m");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"Price: {item.Price:F2}$");
                    Console.WriteLine($"Description: {item.Description}");
                    Console.WriteLine($"Type: {string.Join(", ", item.Type ?? new List<string>())}");
                    Console.WriteLine($"Allergies: {string.Join(", ", item.Allergies ?? new List<string>())}");
                    Console.WriteLine(new string('-', 40)); // Separator for better readability
                }
            }
        }
        else
        {
            Console.WriteLine("No menu items available based on the selected filters.");
        }

        Console.WriteLine("[Press enter to go back]");
        Console.ReadLine();
    }
    public static void DisplayWholeMenu()
    {
        List<FoodMenuModel> menuItems = foodMenuLogic.GetAllMenuItems();
        if (menuItems.Any())
        {
            
            // Define the desired display order
            var displayOrder = new List<string> { "Appetizer", "Main Course", "Side", "Dessert", "Chef's Menu"};

            // Group and order menu items based on the display order
            var orderedMenu = menuItems
                .OrderBy(item => displayOrder.IndexOf(item.Type.FirstOrDefault() ?? ""))
                .GroupBy(item => item.Type.FirstOrDefault() ?? "Other");

            foreach (var group in orderedMenu)
            {
                Console.WriteLine(new string('=', 40));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\x1b[1m\x1b[4m{group.Key}\x1b[0m");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(new string('=', 40));

                foreach (var item in group)
                {
                    // Print each menu item's details
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\x1b[1m{item.DishName}\x1b[0m");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"Price: {item.Price:F2}$");
                    Console.WriteLine($"Description: {item.Description}");
                    Console.WriteLine($"Type: {string.Join(", ", item.Type ?? new List<string>())}");
                    Console.WriteLine($"Allergies: {string.Join(", ", item.Allergies ?? new List<string>())}");
                    Console.WriteLine(new string('-', 40)); // Separator for better readability
                }
            }
        }
        else
        {
            Console.WriteLine("No menu items available based on the selected filters.");
        }

        Console.WriteLine("[Press enter to go back]");
        Console.ReadLine();
    }


    public static void EditFoodMenuMenu()
    {
        Console.WriteLine("[1] Add a new dish.");
        Console.WriteLine("[2] Delete a dish");
        Console.WriteLine("[3] Back to the main menu");
        string EditMenuAnswer = Console.ReadLine();
        switch (EditMenuAnswer)
        {
            case "1":
                Console.WriteLine("Add a new dish.");
                AddDishToFoodMenu();
                break;
            
            case "2":
                Console.WriteLine("Deleting a dish."); 
                DeleteDish();
                break;
            case "3":
                Console.WriteLine("Back");
                break;
        }
    }

    public static void DeleteDish()
    {
        Console.WriteLine("Give the name of the dish you wanted to delete:");
        string deleteddish = Console.ReadLine();
        string isDeleted = foodMenuLogic.DeleteDishByName(deleteddish);
        
        Console.WriteLine(isDeleted);
    }

    public static bool ConfirmationForDeletion(string item)
    {
        string confirmanswer;
        do
        {
        Console.WriteLine($"Type 'yes' if you are certain to delete [{item}]:");
        Console.WriteLine($"Type 'no' if you do not want to delete [{item}]");
        confirmanswer = Console.ReadLine();
        switch(confirmanswer)
        {
            case "yes":
                return true;
            case "no":
                return false;
            default:
                Console.WriteLine("Invalid input.");
                break;
        }
        }while(confirmanswer == null);
        return false;
    }


    public static void AddDishToFoodMenu()
    {

        string name;
        float price;
        string description;
        List<string> types;
        List<string> allergies;
        bool dish_valid = false;
        do
        {
            bool name_valid = false;
            bool price_valid = false;
            bool description_valid = false;
            bool types_valid = false;
            bool allergies_valid = false;

            // Validate the name
            do
            {
                Console.Write("Enter dish name: ");
                name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name)) // Check if name is not null or empty
                {
                    name_valid = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Name cannot be empty.");
                }
            } while (!name_valid);

            // Validate the price
            do
            {
                Console.Write("Enter price: ");
                if (float.TryParse(Console.ReadLine(), out price) && price > 0) // Check if valid float and greater than 0
                {
                    price_valid = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Price must be a positive number.");
                }
            } while (!price_valid);

            // Validate the description
            do
            {
                Console.Write("Enter description: ");
                description = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(description)) // Check if description is not null or empty
                {
                    description_valid = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Description cannot be empty.");
                }
            } while (!description_valid);

            // Validate types input
            do
            {
                Console.Write("Enter types (comma-separated): ");
                string inputT = Console.ReadLine();
                types = inputT.Split(',').Select(type => type.Trim()).Where(type => !string.IsNullOrEmpty(type)).ToList();

                if (types.Count > 0) // Ensure at least one type is entered
                {
                    types_valid = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter at least one type.");
                }
            } while (!types_valid);

            // Validate allergies input
            Console.Write("Enter allergies (comma-separated, or leave blank for 'None'): ");
            string inputA = Console.ReadLine().ToLower();
            allergies = inputA.Split(',').Select(allergy => allergy.Trim()).Where(allergy => !string.IsNullOrEmpty(allergy)).ToList();

            if (allergies.Count == 0) // If no allergies are provided, set to "None"
            {
                allergies.Add("None");
            }
            allergies_valid = true;

            // If all fields are valid, set dish_valid to true
            if (name_valid && price_valid && description_valid && types_valid && allergies_valid)
            {
                dish_valid = true;
                Console.WriteLine("Dish creation successful!");
            }

        } while (!dish_valid);

        if(dish_valid == true){
            foodMenuLogic.AddDish(name, price, description, types, allergies);
            }
        
        
    }
}
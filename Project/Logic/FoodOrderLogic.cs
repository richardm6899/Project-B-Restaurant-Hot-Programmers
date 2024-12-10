class FoodOrderLogic
{
    private List<FoodMenuModel> _foodMenu;

    public FoodOrderLogic()
    {
        _foodMenu = FoodMenuAccess.LoadAll();
    }

    // Method to return all food menu items
    public List<FoodMenuModel> GetAllMenuItems()
    {
        return _foodMenu;
    }

    public static void OrderingFood(FoodMenuModel[] options)
    {
        int selectedIndex = 0;
        bool lookingAtFood = true;
        while (lookingAtFood)
        {
            Console.Clear();
            Console.ResetColor();
            FoodOrderMenu.DisplayFoodOptions(options, selectedIndex, []);

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

            }
        }
    }
    public static void AddToCart(List<(FoodMenuModel, int)> cart, FoodMenuModel item)
    {
        //Retrieves the first element in the cart if nothing retrieves the default outcome
        var existingItem = cart.FirstOrDefault(f => f.Item1 == item);
        if (existingItem.Item1 != null)
        {
            //If the item is already in the cart, increase the quantity
            cart.Remove(existingItem);
            cart.Add((item, existingItem.Item2 + 1));
        }
        else
        {
            //Add the item with a quantity of 1
            cart.Add((item, 1));
        }
    }

    public void AddCartToReceipt(List<(FoodMenuModel, int)> cart )
    {
        
    }
}
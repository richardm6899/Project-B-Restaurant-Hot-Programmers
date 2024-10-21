using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class FoodMenuLogic
{
    private List<FoodMenuModel> _foodMenu;

    public FoodMenuLogic()
    {
        _foodMenu = FoodMenuAccess.LoadAll();
    }

    // Method to return all food menu items
    public List<FoodMenuModel> GetAllMenuItems()
    {
        
        return _foodMenu;
    }

    // Method to return food menu item by name
    public FoodMenuModel GetMenuItemByName(string dishName)
    {
        return _foodMenu.FirstOrDefault(item => item.DishName == dishName);
    }
}
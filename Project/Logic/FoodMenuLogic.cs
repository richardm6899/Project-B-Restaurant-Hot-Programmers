using System;
using System.Collections.Generic;
using System.Globalization;
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

    public List<string> GetAllAllergies()
    {
        List<string> Allergies = [];
        foreach(FoodMenuModel dish in _foodMenu)
        {
            foreach(string allergy in dish.Allergies)
            {
                if(!Allergies.Contains(allergy))
                {
                    if(allergy != "none")
                    {
                        Allergies.Add(allergy);
                    }
                }
                else
                {
                    continue;
                }
            }
        }
        return Allergies;
    }

    // Method to return food menu item by name
    public FoodMenuModel GetMenuItemByName(string dishName)
    {
        return _foodMenu.FirstOrDefault(item => item.DishName == dishName);
    }
    
    public List<FoodMenuModel> GetMenuExcludingAllergies(List<string> allergiesToAvoid)
        {
            return _foodMenu.Where(item =>
                item.Allergies == null || !item.Allergies.Any(allergy => allergiesToAvoid.Contains(allergy))
            ).ToList();
        }

}
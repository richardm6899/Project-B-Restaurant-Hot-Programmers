using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
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
        foreach (FoodMenuModel dish in _foodMenu)
        {
            foreach (string allergy in dish.Allergies)
            {
                if (!Allergies.Contains(allergy))
                {
                    if (allergy != "none")
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

    public void AddDish(string dishName, float price, string description, List<string> type, List<string> allergies)
    {
        // Determine the next available ID
        int newId = _foodMenu.Count > 0 ? _foodMenu.Max(d => d.Id) + 1 : 1;

        // Create the new dish model
        FoodMenuModel newDish = new FoodMenuModel(newId, dishName, price, description, type, allergies);

        // Add to the menu list
        _foodMenu.Add(newDish);

        // Save updated list to JSON
        FoodMenuAccess.WriteAll(_foodMenu);
    }


    public string DeleteDishByName(string dishName)
    {
        // Find the dish by name (case-insensitive search)
        var dishToRemove = _foodMenu.FirstOrDefault(d => d.DishName.Equals(dishName, StringComparison.OrdinalIgnoreCase));
        if (dishToRemove != null)
        {
            if (FoodMenuDisplay.ConfirmationForDeletion(dishName))
            {
                _foodMenu.Remove(dishToRemove);

                FoodMenuAccess.WriteAll(_foodMenu);
                return "Dish was succesfully deleted";
            }
            else
            {
                return "Deletion has stopped.";
            }

        }

        return "Dish was not found.";
    }
}
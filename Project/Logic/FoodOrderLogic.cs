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
}
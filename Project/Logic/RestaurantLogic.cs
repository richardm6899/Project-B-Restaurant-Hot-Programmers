public class RestaurantLogic
{
    private List<RestaurantModel> _restaurant;

    public RestaurantLogic()
    {
        _restaurant = RestaurantAccess.LoadAll();
    }

    public List<RestaurantModel> GetRestaurantInfo()
    {
        return _restaurant;
    }
}
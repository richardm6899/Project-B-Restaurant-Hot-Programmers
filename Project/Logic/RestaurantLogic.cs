public class RestaurantLogic
{
    private List<RestaurantModel> _restaurant;

    public RestaurantLogic()
    {
        _restaurant = RestaurantAccess.LoadAll();
    }
}
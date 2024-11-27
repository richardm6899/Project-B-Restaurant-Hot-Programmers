using System.Runtime.CompilerServices;
public class RestaurantLogic
{
    private static List<RestaurantModel> _restaurant;
    
    static RestaurantLogic()
    {
        _restaurant = RestaurantAccess.LoadAll();
    }

    public List<RestaurantModel> GetRestaurantInfo()
    {
        return _restaurant;
    }

    // create method that checks if given date is not in the closed dates list.
    public static bool closed_Day(DateTime date)
    {
        // if closed date is chosen client/admin cant make a reservation.
        
        if (_restaurant[0].closed_dates.Contains(date.ToString("dd/MM/yyyy")))
        {
            // restaurant is closed, admin shouldn't be able to make a reservation
            return true;
        }
        else
        {
            // restaurant is open, so it should return the new date (next_open_day)
            return false;
        }
    }

    // if restaurant is closed return the next open day
    public static DateTime? next_Open_Day(DateTime date)
    {
        List<string> closed_date_list = _restaurant[0].closed_dates;
        DateTime next_date = date;

        while (true)
        {
            // adds one day to "dd" until the closed_dates_list doesnt contain next_day(in string format)
            next_date = next_date.AddDays(1);
            string next_day = next_date.ToString("dd/MM/yyyy");

            if (!closed_date_list.Contains(next_day))
            {
                return next_date;
            }
        }
    }
}
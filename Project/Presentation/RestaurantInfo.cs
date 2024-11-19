public class RestaurantInfo
{
    static private RestaurantLogic restaurantLogic = new RestaurantLogic();

    public static void Start()
    {

        List<RestaurantModel> restaurants = restaurantLogic.GetRestaurantInfo();
        foreach (var restaurant in restaurants)
        {
            System.Console.WriteLine("Welcome to the information tab, here is all the info about all our restaurants.");
            // how many restaurants we have
            System.Console.WriteLine($"We now have {restaurants.Count} restaurant at your service.");
            System.Console.WriteLine($"{restaurant.tittle}");
            System.Console.WriteLine($"{restaurant.description}");
            System.Console.WriteLine($"-------------------------------------------------");
            System.Console.WriteLine($"Our opening and closing times: ");
            foreach (var hours in restaurant.opening_hours)
            {
                System.Console.WriteLine($"{hours}");
            }
            System.Console.WriteLine($"-------------------------------------------------");
            System.Console.WriteLine("Days that we are closed:");
            foreach (var dates in restaurant.closed_dates)
            {
                System.Console.WriteLine($"- {dates}");
            }
            System.Console.WriteLine($"-------------------------------------------------");
            // basic info (number, email)
            // System.Console.WriteLine();
            // info about restaurant 1 (the one we are working on now)
            System.Console.WriteLine("Rotterdam");
            System.Console.WriteLine($"Our Rotterdam locations are located at: {restaurant.location}");
            System.Console.WriteLine($"Our Email: {restaurant.email_address}"); // add email
            System.Console.WriteLine($"Our Phone number: {restaurant.phone_number}");  // add number
            System.Console.WriteLine($"-------------------------------------------------");
            System.Console.WriteLine($"Our Frequently asked questions: ");
            System.Console.WriteLine($"-------------------------------------------------");
            foreach (var question in restaurant.faq)
            {
                System.Console.WriteLine($"{question}");
            }
            System.Console.WriteLine(""); // add location of this restaurant.

            System.Console.WriteLine("Press enter to return to the menu.");
            Console.ReadLine();
            return;
        }
    }
}

    // name: The Hot Seat
    // location: Wijnhaven 107, 3011 WN in Rotterdam
    // email: HotSeat@info.nl
    // number 6 74825483

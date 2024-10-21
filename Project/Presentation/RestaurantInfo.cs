public class RestaurantInfo
{
    public static void Start()
    { 
        System.Console.WriteLine("Welcome to the information tab, here is all the info about all our restaurants.");
        // how many restaurants we have
        System.Console.WriteLine("We now have ... restaurants."); 
        // basic info (number, email)
        System.Console.WriteLine("Our Email; ..."); // add email
        System.Console.WriteLine("Our Phone number: ...");  // add number
        System.Console.WriteLine();
        // info about restaurant 1 (the one we are working on now)
        System.Console.WriteLine("Rotterdam");
        System.Console.WriteLine("Our Rotterdam locations are located at;");
        System.Console.WriteLine(""); // add location of this restaurant.

        System.Console.WriteLine("Press enter to return to the menu.");
        Console.ReadLine();
        Menu.Start();
    }

    // name: The Hot Seat
    // location: Wijnhaven 107, 3011 WN in Rotterdam
    // email: HotSeat@info.nl
    // number 6 74825483
}
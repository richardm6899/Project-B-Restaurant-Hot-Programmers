class StaffMenu
{
    public static void Start(AccountModel acc)
    {
        string[] optionsStaff = {
            "Make Reservation",
            "Cancel Reservation",
            "Find Reservation",
            "See all reservations",
            "See food/drink menu",
            "see restaurant info",
            "See account data",
            "log out"
        };
        bool staffmenu = true;

        while (staffmenu)
        {
            if (acc != null)
            {
                int selectedIndexStaff = 0;
                Console.Clear();
                string mainPrompt = @$"Welcome to the Staff menu.
-----------------------------------------
Welcome back {acc.FullName}
-----------------------------------------";
                selectedIndexStaff = HelperPresentation.ChooseOption(mainPrompt, optionsStaff, selectedIndexStaff);

                switch (selectedIndexStaff)
                {
                    // make reservation 
                    case 0:
                        System.Console.WriteLine("Make reservation:");
                        // when you want to make a reservation as staff you have to ask all info of the person that wants to make said reservation
                        // don't add the info of the staff to the reservation.
                        System.Console.WriteLine("Not implemented yet");
                        System.Console.WriteLine("[enter]");
                        Console.ReadLine();
                        break;

                    // cancel reservation
                    case 1:
                        System.Console.WriteLine("Cancel reservation");
                        // staff can cancel all reservations
                        System.Console.WriteLine("Not implemented yet");
                        System.Console.WriteLine("[enter]");
                        Console.ReadLine();
                        break;

                    // find reservation
                    case 2:
                        System.Console.WriteLine("Find reservation.");
                        // can be found by name or maybe reservation id
                        System.Console.WriteLine("Not implemented yet.");
                        System.Console.WriteLine("[enter]");
                        Console.ReadLine();
                        break;

                    // see all reservations
                    case 3:
                        System.Console.WriteLine("All reservations: ");
                        System.Console.WriteLine("Not implemented yet");
                        System.Console.WriteLine("[enter]");
                        Console.ReadLine();
                        break;

                    // see food/drink menu
                    case 4:
                        // add question with what allergies to look at if admin wants to look at allergies
                        FoodMenuDisplay.Start();
                        break;

                    // see restaurant info
                    case 5:
                        RestaurantInfo.Start();
                        break;

                    // see acounts data
                    case 6:
                        System.Console.WriteLine("Your accounts data: ");
                        // full name
                        System.Console.WriteLine("Name: " + acc.FullName);
                        // email
                        System.Console.WriteLine("Email: " + acc.EmailAddress);
                        // phone numb
                        System.Console.WriteLine("Phone number: " + acc.PhoneNumber);
                        // pass
                        System.Console.WriteLine("Password: " + acc.Password);
                        Console.ReadLine();
                        break;

                    // logout
                    case 7:
                        acc = null!;
                        staffmenu = false;
                        break;
                }
            }
        }
    }
}
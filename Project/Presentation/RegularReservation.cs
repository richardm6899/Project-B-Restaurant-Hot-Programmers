static class RegularReservation
{

    static private ReservationLogic reservationlogic = new();
    public static void Regular(string name, int clientID, string number, string email)
    {
        int cost = 50;
        string typeofreservation;
        DateTime Date = default;
        string TimeSlot = "";
        int HowMany = 0;
        int progress = 0;
        bool running = true;
        while (running)
        {
            // user gets asked with how many people are and check is done
            if (progress == 0)
            {
                string[] nums = ["1", "2", "3", "4", "5", "6", "Quit"];
                string howMany = ReservationLogic.Choice("\nFor how many people? We have a max of 6 per table.\n----------------------------------------", nums);

                if (int.TryParse(howMany, out int HowManycheck))
                {

                    HowMany = HowManycheck;
                    progress = 20;

                    System.Console.WriteLine($"{HowMany} person(s) to be seated");
                    System.Console.WriteLine("[enter]");
                    Console.ReadLine();
                    Console.Clear();


                }
                else if (howMany == "Quit")
                {
                    System.Console.WriteLine("Goodbye....");
                    System.Console.WriteLine("[enter]");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                }


            }


            // user gets to choose timeslot of reservation
            else if (progress == 20)
            {
                string[] timeslots = ["Lunch (12:00 - 14:00)", "Dinner 1 (17:00 - 19:00)", "Dinner 2 (19:00 - 21:00)", "Dinner 3 (21:00 - 23:00)", "Return", "Quit"];
                TimeSlot = ReservationLogic.Choice("\nWhat TimeSlot would you prefer: \n----------------------------------------\nPress 'Return' to go back to choosing amount of people.\n", timeslots);

                System.Console.WriteLine("----------------------------------------");
                if (TimeSlot == "Return")
                {
                    progress = 0;
                }
                else if (TimeSlot == "Quit")
                {
                    System.Console.WriteLine("Goodbye....");
                    System.Console.WriteLine("[enter]");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                }
                else
                {
                    //    check if valid time slot
                    System.Console.WriteLine($"Time slot {TimeSlot} chosen.");
                    System.Console.WriteLine("[enter]");
                    System.Console.ReadLine();
                    Console.Clear();

                    progress = 40;

                }
            }


            else if (progress == 40)
            {
                // calender gets shown with all available dates 
                System.Console.WriteLine(@"'You can only book 3 months in advanced'");
                // check if valid date

                string UncheckedDate = reservationlogic.DisplayCalendarReservation(TimeSlot, HowMany, "Regular");
                if (DateTime.TryParse(UncheckedDate, out Date))
                {
                    //checks if user filled in date not before today and not farther than 3 months in the future
                    // can be more personalised in terms of what the user filled in wrong by making returns numbers
                    if (reservationlogic.IsValidDate(Date))
                    {

                        System.Console.WriteLine(Date + "chosen");
                        System.Console.WriteLine("[enter]");
                        System.Console.ReadLine();
                        progress = 60;


                    }
                    else
                    {
                        System.Console.WriteLine("Invalid date entered. Try again");
                        System.Console.WriteLine("[enter]");
                        System.Console.ReadLine();
                    }
                }
                // if it is equal to 20 means that they want to go back to timeslot chocie
                else if (int.TryParse(UncheckedDate, out int num) && num == 20)
                {
                    System.Console.WriteLine("Going back to timeslot choice");
                    System.Console.WriteLine("[enter]");
                    System.Console.ReadLine();
                    progress = 20;
                    Console.Clear();
                }
                else
                {
                    System.Console.WriteLine(UncheckedDate);
                    System.Console.WriteLine("[enter]");
                    System.Console.ReadLine();
                }
            }

            // available table check
            // happens down here to not override other methods (DisplayCalendarReservation)

            else if (progress == 60)
            {
                reservationlogic.AvailableTables.Clear();
                reservationlogic.CheckMin_MaxCapacity(HowMany);
                reservationlogic.CheckDate(Date, TimeSlot);
                // User gets shown restaurant and is shown what tables 
                System.Console.WriteLine("Where would you like to sit.\nChoose the table id of the table.");

                List<int> TableID = [reservationlogic.DisplayRestaurant()];

                bool TableIdValid = true;

                if (TableIdValid)
                {

                    bool TableChoice = reservationlogic.DisplayChosenSeats(TableID);
                    System.Console.WriteLine("-----------------------------------------");
                    System.Console.WriteLine(reservationlogic.displayTable(TableID[0]));


                    if (TableChoice)
                    {
                        string foodorder = "Would you like to order food in advanced?";
                        System.Console.WriteLine("-----------------------------------------");
                        System.Console.WriteLine($"{foodorder}\n");
                        bool orderfood = HelperPresentation.YesOrNo(foodorder);

                        if (orderfood)
                        {
                            List<(FoodMenuModel, int)> foodCart = FoodOrderMenu.OrderFood();
                            // reservation
                            //(int id, List<int> tableID, string name, int clientID, int howMany, DateTime date, string typeofreservation, string timeslot, bool foodOrdered)
                            // receipt
                            //(int id, int reservationId, int clientId, int cost, DateTime date,string timeslot, string name, string phoneNumber, string email,string typeofreservation,int tableID, List<(FoodMenuModel,int)> orderedFood)
                            ReservationModel Reservation = reservationlogic.Create_reservation(TableID[0], name, clientID, HowMany, Date, "Regular", TimeSlot, true);
                            ReceiptModel receipt = reservationlogic.CreateReceipt(Reservation, cost, number, email, foodCart, TableID);
                            System.Console.WriteLine();
                            System.Console.WriteLine("This is your receipt for now: ");

                            System.Console.WriteLine(reservationlogic.DisplayReceipt(receipt));
                            System.Console.WriteLine("reservation created");
                            System.Console.WriteLine("[enter]");
                            Console.ReadLine();

                            reservationlogic.AvailableTables.Clear();
                            Console.Clear();
                            break;
                        }
                        else
                        {
                            ReservationModel Reservation = reservationlogic.Create_reservation(TableID[0], name, clientID, HowMany, Date, "Regular", TimeSlot, false);

                            ReceiptModel receipt = reservationlogic.CreateReceipt(Reservation, cost, number, email, [], TableID);
                            System.Console.WriteLine();
                            System.Console.WriteLine("This is your receipt for now: ");

                            System.Console.WriteLine(reservationlogic.DisplayReceipt(receipt));
                            System.Console.WriteLine("reservation created");
                            System.Console.WriteLine("[enter]");
                            Console.ReadLine();

                            reservationlogic.AvailableTables.Clear();
                            Console.Clear();
                            break;



                        }

                    }
                    else if (TableChoice == false)
                    {
                        progress = 40;
                    }

                }

            }
        }
    }


}


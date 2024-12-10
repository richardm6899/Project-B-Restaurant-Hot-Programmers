using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;


// when hotseat is chosen
// user has to see on the dates if tables are fully booked
// if there is space make sure they are seated at place and shown where they are seated


// ideas
// make a new method to check multiple hotseats at once 
// make a new method to check if date is fully booked
// make a new display reservation function to show when res is hot seat
// make new method to make a reservation
// show receipt with costs * amount of people, table numbers


// tips 
// at the end when everything works find a way to implement generics
// so that you don't have alot of repeated code.
static class HotSeatReservation
{
    static private ReservationLogic reservationlogic = new();

    public static void HotSeat(string name, int clientID, string number, string email)
    {
        reservationlogic.AddHotSeatsAvailableTables();
        bool running = true;
        int progress = 0;
        int cost = 60;

        DateTime Date = default;
        string TimeSlot = "";
        int HowMany = 0;
        // user gets to choose timeslot of reservation
        // user gets asked with how many people are and check is done
        while (running)
        {
            if (progress == 0)
            {
                string[] nums = ["1", "2", "3", "4", "5", "6", "7", "8", "Quit"];
                string howMany = ReservationLogic.Choice("\nFor how many people? We have a max of 8 hot seats per timeslot.\nThere is a higher availability for smaller groups.\n----------------------------------------", nums);

                if (int.TryParse(howMany, out int HowManycheck))
                {
                    if (HowManycheck >= 1 && HowManycheck <= 8)
                    {
                        HowMany = HowManycheck;
                        progress = 20;
                        cost *= HowMany;
                        System.Console.WriteLine($"{HowMany} person(s) to be seated");
                        System.Console.WriteLine("[enter]");
                        Console.ReadLine();
                        Console.Clear();
                    }
                    else if (HowManycheck < 1 || HowManycheck > 8)
                    {
                        System.Console.WriteLine("no table found with amount of people. Enter again...");
                        System.Console.WriteLine("[enter]");
                        System.Console.ReadLine();
                        Console.Clear();

                    }
                    else if (howMany.ToUpper() == "Quit")
                    {
                        System.Console.WriteLine("Goodbye....");
                        System.Console.WriteLine("[enter]");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    }
                }
                else
                {
                    System.Console.WriteLine("invalid input please fill in digit");
                    System.Console.WriteLine("[enter]");
                    System.Console.ReadLine();
                    Console.Clear();
                }

            }

            else if (progress == 20)
            {

                string[] timeslots = ["Lunch (12:00 - 14:00)", "Dinner 1 (17:00 - 19:00)", "Dinner 2 (19:00 - 21:00)", "Dinner 3 (21:00 - 23:00)", "Return,Quit"];
                TimeSlot = ReservationLogic.Choice("\nWhat TimeSlot would you prefer: \n----------------------------------------\nPress 'Return' to go back to choosing amount of people.", timeslots);


                if (TimeSlot == "Return")
                {
                    progress = 0;
                }
                else if (TimeSlot == "Quit")
                {
                    System.Console.WriteLine("GoodBye...");
                    System.Console.WriteLine("[Enter]");
                    System.Console.ReadLine();
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

                string UncheckedDate = reservationlogic.DisplayCalendarReservation(TimeSlot, HowMany, "HotSeat");
                if (DateTime.TryParse(UncheckedDate, out Date))
                {
                    //checks if user filled in date not before today and not farther than 3 months in the future
                    // can be more personalised in terms of what the user filled in wrong by making returns numbers
                    if (reservationlogic.IsValidDate(Date))
                    {

                        System.Console.WriteLine(Date + "chosen");
                        System.Console.WriteLine("[enter]");
                        System.Console.ReadLine();
                        Console.Clear();
                        progress = 60;


                    }

                    else
                    {
                        System.Console.WriteLine("Invalid date entered. Try again");
                        System.Console.WriteLine("[enter]");
                        System.Console.ReadLine();
                        Console.Clear();
                    }
                }
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
                    Console.Clear();
                }
            }



            else if (progress == 60)
            {
                reservationlogic.AvailableTables.Clear();
                reservationlogic.AddHotSeatsAvailableTables();

                reservationlogic.CheckDateHotSeat(Date, TimeSlot);

                List<int> tableIDs = reservationlogic.ChooseHotSeats(HowMany);
                bool TableChoice = reservationlogic.DisplayChosenSeats(tableIDs);
                if (TableChoice)
                {
                    string foodorder = "Would you like to order food in advanced?";
                    System.Console.WriteLine("-----------------------------------------");
                    System.Console.WriteLine($"{foodorder}\n");
                    bool orderfood = ChoicesLogic.YesOrNo(foodorder);

                    if (orderfood)
                    {
                        List<(FoodMenuModel, int)> foodCart = FoodOrderMenu.OrderFood();
                        // reservation
                        //(int id, List<int> tableID, string name, int clientID, int howMany, DateTime date, string typeofreservation, string timeslot, bool foodOrdered)
                        // receipt
                        //(int id, int reservationId, int clientId, int cost, DateTime date,string timeslot, string name, string phoneNumber, string email,string typeofreservation,int tableID, List<(FoodMenuModel,int)> orderedFood)
                        List<ReservationModel> Reservation = reservationlogic.Create_reservationHotSeat(tableIDs, name, clientID, HowMany, Date, "HotSeat", TimeSlot, true);
                        ReceiptModel receipt = reservationlogic.CreateReceiptHotSeat(Reservation, cost, number, email, foodCart);
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
                        List<ReservationModel> Reservation = reservationlogic.Create_reservationHotSeat(tableIDs, name, clientID, HowMany, Date, "HotSeat", TimeSlot, true);
                        ReceiptModel receipt = reservationlogic.CreateReceiptHotSeat(Reservation, cost, number, email, []);
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


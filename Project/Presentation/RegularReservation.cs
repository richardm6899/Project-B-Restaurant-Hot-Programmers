// static class RegularReservation
// {

//     static private ReservationLogic reservationlogic = new();
//     public static void Regular(string name, int clientID, string number, string email)
//     {
//         int cost = 50;
//         string typeofreservation;
//         DateTime Date = default;
//         string TimeSlot = "";
//         int HowMany = 0;
//         int progress = 0;
//         bool running = true;
//         while (running)
//         {
//             // user gets asked with how many people are and check is done
//             if (progress == 0)
//             {
//                 HowMany = Reservation.HowManyCheck("Regular");
//                 if (HowMany > 0)
//                 {
//                     progress = 20;
//                 }
//                 else if (HowMany == 0)
//                 {

//                     break;
//                 }


//             }


//             // user gets to choose timeslot of reservation
//             else if (progress == 20)
//             {
//                 TimeSlot = Reservation.TimeSlot();
//                 System.Console.WriteLine("----------------------------------------");
//                 if (TimeSlot == "Return")
//                 {
//                     progress = 0;
//                 }
//                 else if (TimeSlot == "Quit")
//                 {
//                     break;
//                 }
//                 else
//                 {
//                     progress = 40;
//                 }
//             }


//             else if (progress == 40)
//             {
//                 Date = Reservation.ChooseDate(TimeSlot, HowMany, "Regular");
//                 if (Date != default)
//                 {
//                     progress = 60;
//                 }
//                 else
//                 {
//                     progress = 20;
//                 }

//             }

//             // available table check
//             // happens down here to not override other methods (DisplayCalendarReservation)

//             else if (progress == 60)
//             {
//                 AvailableTablesRegular(Date, TimeSlot, HowMany);
//                 // User gets shown restaurant and is shown what tables 
//                 System.Console.WriteLine("Where would you like to sit.\nChoose the table id of the table.");

//                 List<int> TableID = [Reservation.DisplayRestaurant()];

//                 bool TableIdValid = true;

//                 if (TableIdValid)
//                 {

//                     bool TableChoice = Reservation.DisplayChosenSeats(TableID);
//                     System.Console.WriteLine("-----------------------------------------");
//                     System.Console.WriteLine(reservationlogic.displayTable(TableID[0]));


//                     if (TableChoice)
//                     {
//                         Reservation.ResOrderFood(TableID, name, clientID, HowMany, Date, "Regular", TimeSlot, number, email, cost);

//                         ReservationLogic.AvailableTables.Clear();
//                         Console.Clear();
//                         break;



//                     }
//                     else if (TableChoice == false)
//                     {
//                         progress = 40;
//                     }

//                 }

//             }


//         }

//     }

//     public static int AvailableTablesRegular(DateTime Date, string TimeSlot, int HowMany)
//     {
//         ReservationLogic.AvailableTables.Clear();
//         reservationlogic.AddRegularSeats(HowMany);
//         reservationlogic.CheckDate(Date, TimeSlot);
//         return ReservationLogic.AvailableTables.Count;
//     }

// }





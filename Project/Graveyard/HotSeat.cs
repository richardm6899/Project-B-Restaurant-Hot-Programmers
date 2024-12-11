// using System.Diagnostics;
// using System.Runtime.Intrinsics.Arm;


// // when hotseat is chosen
// // user has to see on the dates if tables are fully booked
// // if there is space make sure they are seated at place and shown where they are seated


// // ideas
// // make a new method to check multiple hotseats at once 
// // make a new method to check if date is fully booked
// // make a new display reservation function to show when res is hot seat
// // make new method to make a reservation
// // show receipt with costs * amount of people, table numbers


// // tips 
// // at the end when everything works find a way to implement generics
// // so that you don't have alot of repeated code.
// static class HotSeats
// {
//     static private ReservationLogic reservationlogic = new();


//     public static void HotSeat(string name, int clientID, string number, string email)
//     {
//         reservationlogic.AddHotSeatsAvailableTables();
//         int cost = 60;
//         string typeofreservation = "HotSeat";
//         bool dateCheck = true;


//         DateTime Date = default;
//         string TimeSlot = "";
//         int HowMany = 0;

//         bool timeslotbool = true;
//         // user gets to choose timeslot of reservation
//         // user gets asked with how many people are and check is done
//         bool howmanyCheck = true;
//         while (howmanyCheck)
//         {
//             System.Console.WriteLine("\nFor how many people? We have a max of 8 hot seats per timeslot.\nThere is a higher availability for smaller groups.");

//             string howMany = Console.ReadLine();
//             if (int.TryParse(howMany, out int HowManycheck))
//             {
//                 if (HowManycheck >= 1 && HowManycheck <= 8)
//                 {
//                     HowMany += HowManycheck;
//                     howmanyCheck = false;
//                     cost *= HowMany;
//                 }
//                 else if (HowManycheck < 1 || HowManycheck > 8)
//                 {
//                     System.Console.WriteLine("no table found with amount of people. Enter again...");
//                     System.Console.WriteLine("[enter]");
//                     System.Console.ReadLine();

//                 }
//             }
//             else
//             {
//                 System.Console.WriteLine("invalid input please fill in digit");
//                 System.Console.WriteLine("[enter]");
//                 System.Console.ReadLine();
//             }

//         }
       
//         // user gets to choose timeslot of reservation
//         while (timeslotbool)
//         {
//             System.Console.WriteLine("In what timeslot would you like to book your reservation: \n1. 12:00 - 14:00\n2. 17:00 - 19:00\n3. 19:00 - 21:00\n4. 21:00 - 23:00\nChoose id:");
//             string timeslotIdcheck = Console.ReadLine();
//             TimeSlot = ReservationLogic.TimSlotChooser(timeslotIdcheck);
//             if (TimeSlot != null)
//             {
//                 //    check if valid time slot
//                 System.Console.WriteLine($"Time slot {TimeSlot} chosen.");
//                 System.Console.WriteLine("[enter]");
//                 System.Console.ReadLine();
//                 timeslotbool = false;
//                 System.Console.WriteLine(reservationlogic.AvailableTables.Count + "b");

//             }
//             else
//             {
//                 System.Console.WriteLine("Invalid ID entered. Try again");
//                 System.Console.WriteLine("[enter]");
//                 System.Console.ReadLine();
//             }
//         }


//         while (dateCheck)
//         {
//             // calender gets shown with all available dates 
//             System.Console.WriteLine(@"'You can only book 3 months in advanced'");
//             // check if valid date

//             string UncheckedDate = reservationlogic.DisplayCalendarReservation(TimeSlot, HowMany, "HotSeat");
//             if (DateTime.TryParse(UncheckedDate, out Date))
//             {
//                 //checks if user filled in date not before today and not farther than 3 months in the future
//                 // can be more personalised in terms of what the user filled in wrong by making returns numbers
//                 if (reservationlogic.IsValidDate(Date))
//                 {
//                     System.Console.WriteLine(reservationlogic.AvailableTables.Count + "c");
//                     dateCheck = false;
//                     System.Console.WriteLine(Date + "chosen");
//                     System.Console.WriteLine("[enter]");
//                     System.Console.ReadLine();


//                 }
//                 else
//                 {
//                     System.Console.WriteLine("Invalid date entered. Try again");
//                     System.Console.WriteLine("[enter]");
//                     System.Console.ReadLine();
//                 }
//             }
//             else
//             {
//                 System.Console.WriteLine(UncheckedDate);
//                 System.Console.WriteLine("[enter]");
//                 System.Console.ReadLine();
//             }
//         }
//         reservationlogic.AvailableTables.Clear();
//         reservationlogic.AddHotSeatsAvailableTables();
      
//         reservationlogic.CheckDateHotSeat(Date, TimeSlot);
       
//         List<int> tableIDs = reservationlogic.ChooseHotSeats(Date, TimeSlot, HowMany);
       
//         bool TableCheck = true;
//         while (TableCheck)
//         {
//             bool TableChoice = reservationlogic.DisplayChosenSeats(tableIDs);
//             if (TableChoice)
//             {
//                 List<ReservationModel> reservation = reservationlogic.Create_reservationHotSeat(tableIDs, name, clientID, HowMany, Date, typeofreservation, TimeSlot);
//                 ReceiptModel receipt = reservationlogic.CreateReceiptHotSeat(reservation, cost, number, email);
//                 System.Console.WriteLine();
//                 System.Console.WriteLine("This is your receipt for now: ");

//                 System.Console.WriteLine(reservationlogic.DisplayReceipt(receipt));
//                 System.Console.WriteLine("reservation created");
//                 System.Console.WriteLine("[enter]");
//                 Console.ReadLine();

//                 TableCheck = false;
//                 reservationlogic.AvailableTables.Clear();
//                 Console.Clear();
//             }
//             else
//             {
//                 return;
//             }
//         }


//     }
// }






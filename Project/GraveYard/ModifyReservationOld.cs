// // order: 
// // amount of people
// // timeslot 
// // date (calender)
// // choose table
// // confirmation -- show receipt and write to all jsons

// // hotseat possibilities 
// // only order for hotseat if type is hot seat and only check for regular seat if reservation was for regular seat
// // check if there are any tables available in both hot seat and regular seats 
// // make calender show different colors if only hotseat or regular seats are available on this day 

// // user can choose which reservation they want to modify
// // user can choose to modify Amount of people
// // if amount is bigger than max capacity of table then check if the there any other tables available
// // if yes then show new table of reservation
// // if no then user gets asked to enter another date
// // table is chosen for them 

// // user can choose to modify TimeSlot
// // user can choose to modify the timeslot
// // if no available tables
// // user will get asked to enter


// // ideas
// // make every part of making a reservation a method so that it can be easily reusable 
// // every modification needs a certain combination 
// // put the functions in the right order get the right outcome.
// // ask floor to make a function that displays all reservations and details and returns an id of the reservation that i want to modify


// class ModifyRes
// {
//     static ReservationLogic reservationLogic = new();
//     public static void ModifyReservation(string name, int clientID, string number, string email)
//     {
//         bool running = true;
//         while (running)
//         {
//             if (reservationLogic.DisplayReservations(clientID) != "")
//             {



//                 System.Console.WriteLine(reservationLogic.DisplayReservations(clientID));
//                 System.Console.WriteLine("Which reservation would you like to modify\nEnter ID:");
//                 System.Console.WriteLine("-----------------------------------------------------");
//                 int reservationID = Convert.ToInt32(System.Console.ReadLine());


//                 if (reservationLogic.GetReservationById(reservationID) != null)
//                 {

//                     ReservationModel reservation = reservationLogic.GetReservationById(reservationID);
//                     List<TableModel> tables = reservationLogic.GetTablesByReservation(reservation);

//                     string[] choiceArr = ["Amount of people", "Timeslot", "Date", "Food and drinks"];
//                     string choice = Choice("What would you like to modify of this reservation", choiceArr);
//                     if (choice == "Amount of people")
//                     {
//                         System.Console.WriteLine($"choice amount of people you want to sit with.");
//                         string[] amountarr = ["1", "2", "3", "4", "5", "6", "7", "8", "Quit"];
//                         string newAmount = Choice("Choose new amount: ", amountarr);
//                         //  check if new amount is bigger or smaller than table capacity


//                         if (Convert.ToInt32(newAmount) < tables[0].MinCapacity || Convert.ToInt32(newAmount) > tables[0].MaxCapacity)
//                         {
//                             // check if there are available table with current prefference and date
//                             // if not then check
//                             if (newAmount == "1")
//                             {

//                                 if (AvailableTablesHotSeat(reservation.Date, reservation.TimeSlot, 1) >= 1)
//                                 {

//                                     List<int> tableIDs = reservationLogic.ChooseSeats(Convert.ToInt32(newAmount));
//                                     bool TableChoice = DisplayChosenSeats(tableIDs);
//                                     if (TableChoice)
//                                     {
//                                         System.Console.WriteLine("Table has been changed");
//                                         System.Console.WriteLine("[enter]");
//                                         System.Console.ReadLine();
//                                         Console.Clear();
//                                         reservationLogic.AvailableTables.Clear();
//                                         break;
//                                     }
//                                 }
//                                 else
//                                 {

//                                     System.Console.WriteLine("You are going to have to select a different Date");
//                                     System.Console.WriteLine("[enter]");
//                                     System.Console.ReadLine();
//                                     // display date n shit
//                                 }
//                             }
//                         }
//                         else
//                         {
//                             reservation.HowMany = Convert.ToInt32(newAmount);
//                         }
//                     }
//                     else if (choice == "Timeslot")
//                     {

//                     }
//                     else if (choice == "Date")
//                     {

//                     }
//                     else if (choice == "Food and drinks")
//                     {

//                     }
//                 }
//                 else
//                 {
//                     System.Console.WriteLine("Invalid id given");
//                     System.Console.WriteLine("[Enter]");
//                     System.Console.ReadLine();

//                 }
//             }
//             else
//             {
//                 System.Console.WriteLine("There are no reservations to modify.");
//                 System.Console.WriteLine("[enter]");
//                 Console.ReadLine();
//                 break;
//             }
//         }

//     }


// }
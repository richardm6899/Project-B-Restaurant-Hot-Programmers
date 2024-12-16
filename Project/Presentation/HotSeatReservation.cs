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
                HowMany = Reservation.HowManyCheck("HotSeat");
                if (HowMany > 0)
                {
                    progress = 20;
                }
                else if (HowMany == 0)
                {

                    break;
                }


            }


            // user gets to choose timeslot of reservation
            else if (progress == 20)
            {
                TimeSlot = Reservation.TimeSlot();
                System.Console.WriteLine("----------------------------------------");
                if (TimeSlot == "Return")
                {
                    progress = 0;
                }
                else if (TimeSlot == "Quit")
                {
                    break;
                }
                else
                {
                    progress = 40;
                }
            }


            else if (progress == 40)
            {
                Date = Reservation.ChooseDate(TimeSlot, HowMany);
                if (Date != default)
                {
                    progress = 60;
                }
                else
                {
                    progress = 20;
                }

            }


            else if (progress == 60)
            {
                Reservation.AvailableTablesHotSeat(Date, TimeSlot);

                List<int> tableIDs = reservationlogic.ChooseSeats(HowMany);
                bool TableChoice = Reservation.DisplayChosenSeats(tableIDs);
                if (TableChoice)
                {
                    Reservation.ResOrderFood(tableIDs, name, clientID, HowMany, Date, "HotSeat", TimeSlot, number, email, cost);
                    reservationlogic.AvailableTables.Clear();
                    Console.Clear();
                    break;
                }
                else if (TableChoice == false)
                {

                    progress = 40;
                }

            }


        }
    }

    
}


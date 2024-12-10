
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;


// order of working
// user gets asked for how many people user wants to reserve
// user gets asked if they want hotseats or regular seats
// how do you check if tables are next to each other
// user gets asked timeslot
//user gets shown  calender
// if not hot seat: user gets option to choose table.
// If there are no available hot seats, user gets notified that there are no available hot seats for the amount of people and can choose again.
// user gets asked confirmation
static class Reservation
{
    static private ReservationLogic reservationlogic = new();

    // displays table restaurant

     public static void MakeReservation(string name, int clientID, string number, string email)
    {

        bool running = true;
        while (running)
        {
            // user has to enter yes or no to go further

            bool Client_answer = ChoicesLogic.YesOrNo("Hello would you like to make a reservation?");

            if (Client_answer)
            {

                string[] HotOrReg = ["Regular", "HotSeat", "Quit"];
                string HotOrRegChoice = ReservationLogic.Choice("Do you want to reserve a Hotseat or a Regular Seat\nHotSeat cost 10 euro extra.\nA Hotseat wil give you a seat at the edge of the restaurant.\n----------------------------------------", HotOrReg);

                if (HotOrRegChoice == "HotSeat")
                {
                    // go trough process of as a hotseat
                    HotSeatReservation.HotSeat(name, clientID, number, email);

                    break;
                }

                else if (HotOrRegChoice == "Regular")
                {
                    RegularReservation.Regular(name, clientID, number, email);
                    break;
                }
                else if (HotOrRegChoice == "Quit")
                {
                    System.Console.WriteLine("Goodbye...");
                    System.Console.WriteLine("[enter]");
                    System.Console.ReadLine();
                    Console.Clear();
                    break;
                }
            }
            else if (Client_answer == false)
            {
                System.Console.WriteLine("Goodbye....");
                System.Console.WriteLine("[enter]");
                System.Console.ReadLine();
                Console.Clear();
                break;

            }
        }
    }
    public static void CancelReservation(int clientID)
    {
        bool cancelreservation = true;
        while (cancelreservation)
        {
            System.Console.WriteLine("Would you like to cancel your reservation? (Y/N)");
            string choice = Console.ReadLine().ToUpper();

            if (choice == "Y")
            {

                if (reservationlogic.DisplayReservations(clientID) != "")
                {
                    bool ReservationIDCheck = true;
                    while (ReservationIDCheck)
                    {
                        System.Console.WriteLine("Which reservation would you like to cancel?\n");
                        System.Console.WriteLine(reservationlogic.DisplayReservations(clientID));
                        System.Console.WriteLine("enter ID: ");
                        string str_id = Console.ReadLine();
                        {
                            if (int.TryParse(str_id, out int reservationid))
                            {
                                // can only choose id if in reserved by person
                                if (reservationlogic.DisplayReservationByID(reservationid) != null && reservationlogic.IsReservationInAccount(clientID, reservationid).Contains(reservationid))
                                {

                                    ReservationIDCheck = false;
                                    bool confirmation = true;
                                    while (confirmation)
                                    {

                                        System.Console.WriteLine(reservationlogic.DisplayReservationByID(reservationid));
                                        System.Console.WriteLine("Are You sure you want to cancel this reservation? (Y/N)");

                                        string choice2 = Console.ReadLine().ToUpper();
                                        if (choice2 == "Y")
                                        {
                                            Console.Clear();
                                            ReservationModel reservation = reservationlogic.GetReservationById(reservationid);
                                            System.Console.WriteLine(reservationlogic.DisplayReservationByID(reservation.Id) + " has been canceled.\n");
                                            reservationlogic.RemoveReservationByID(reservationid);

                                            System.Console.WriteLine("[enter]");
                                            Console.ReadLine();
                                            Console.Clear();

                                            confirmation = false;
                                        }
                                        else if (choice2 == "N")
                                        {
                                            confirmation = false;

                                        }

                                    }
                                }
                                else
                                {
                                    System.Console.WriteLine("Invalid id given");
                                    System.Console.WriteLine("[enter]");
                                    Console.ReadLine();
                                }
                            }

                            else
                            {
                                System.Console.WriteLine("invalid input");
                                System.Console.WriteLine("[enter]");
                                System.Console.ReadLine();

                            }
                        }
                    }
                }
                else
                {
                    System.Console.WriteLine("You have no available reservations to cancel");
                    System.Console.WriteLine("[enter]");
                    System.Console.ReadLine();
                    cancelreservation = false;
                }

            }
            else if (choice == "N")
            {
                System.Console.WriteLine("Goodbye....");
                System.Console.WriteLine("[enter]");
                System.Console.ReadLine();
                cancelreservation = false;
            }
            else
            {
                System.Console.WriteLine("Please fill in (Y/N)");
                System.Console.WriteLine("[enter]");
                System.Console.ReadLine();
            }
        }
    }

    public static void AdminCancelReservation()
    {
        bool cancelreservation = true;
        while (cancelreservation)
        {
            if (ChoicesLogic.YesOrNo("Would you like to cancel a reservation? (Y/N)"))
            {
                List<string> ongoingReservations = reservationlogic.DisplayAllOngoingReservations();
                foreach (string reservation in ongoingReservations)
                {
                    System.Console.WriteLine(reservation);
                }

                bool Canceling = true;
                while (Canceling)
                {
                    // make this so when an incorrect input is entered ask again
                    // make an escape
                    System.Console.WriteLine("ID to cancel: ");
                    int cancelID = Convert.ToInt32(Console.ReadLine());

                    ReservationModel toCancelReservation = reservationlogic.GetReservationById(cancelID);
                    bool cancelReservation = ChoicesLogic.YesOrNo($"Are you sure you want to cancel this reservation:\nId: {toCancelReservation.Id}\nName: {toCancelReservation.Name}\nTotal people: {toCancelReservation.HowMany}\nDate: {toCancelReservation.Date.ToShortDateString()} {toCancelReservation.TimeSlot}\nType of reservation: {toCancelReservation.TypeOfReservation}");
                    if (cancelReservation)
                    {
                        reservationlogic.RemoveReservationByID(cancelID);
                    }
                    Canceling = false;

                }
            }
            else
            {
                System.Console.WriteLine("Goodbye....");
                System.Console.WriteLine("[enter]");
                System.Console.ReadLine();
                cancelreservation = false;
            }
        }
    }

    public static string AdminShowReservations()
    {
        List<string> ongoingReservations = reservationlogic.DisplayAllOngoingReservations();
        string reservation_string = "";
        foreach (string reservation in ongoingReservations)
        {
            reservation_string += reservation;
        }
        return reservation_string;
    }


    // Admin closes restaurant, removes reservation by date from user and refunds them
    public static void AdminCloseDay()
    {
        System.Console.Write("Enter date (mm/dd/yyyy): ");
        string dateInput = Console.ReadLine();
        DateTime date;
        // System.Console.Write("Enter table ID:");
        // int tableID = Convert.ToInt32(Console.ReadLine());
        // string typeofreservation = reservationlogic.TypeOfReservation(tableID);
        if (DateTime.TryParse(dateInput, out date))
        {
            reservationlogic.RemoveReservationsByDate(date);
            System.Console.WriteLine("Reservations for the specified date have been canceled.");
            // Users that had a reservation on that day get a refund. (paid out of financials)
            foreach (var reservation in reservationlogic._reservations)
            {

                if (reservation.TypeOfReservation == "HotSeat")
                {
                    FinanceLogic.SubtractFromRevenue(60);
                    Console.WriteLine($"{reservation.TypeOfReservation} seat refunded:\nSubtracted from Revenue: 60$)");
                }
                else if (reservation.TypeOfReservation == "Regular")
                {
                    FinanceLogic.SubtractFromRevenue(50);
                    Console.WriteLine($"{reservation.TypeOfReservation} seat refunded:\nSubtracted from Revenue: 50$");
                }
                else if (reservation.TypeOfReservation is null)
                {
                    Console.WriteLine("No reservation to be funded");
                }
            }
        }
        else
        {
            System.Console.WriteLine("Invalid date format. Please try again.");
        }
    }
}
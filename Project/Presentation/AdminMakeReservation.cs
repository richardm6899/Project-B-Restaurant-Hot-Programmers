class AdminMakeReservation
{

    public static void Start(AccountModel acc)
    {
        // public ReservationModel Create_reservation(int tableID, string name, int clientID, int howMany, DateTime date)
        // tableID = get a table id
        // name = ask the person for their name
        // clientID = give admin id
        // howMany = how many people the client wants
        // date = ask the date
        System.Console.WriteLine("Make a reservation? Y/N");
        string admin_answer = Console.ReadLine();
        if (admin_answer.ToUpper() == "N")
        {
            System.Console.WriteLine("");
            AdminMenu.Start(acc);
        }

        System.Console.WriteLine("Name: ");
        string Name = Console.ReadLine();
        Reservation.MakeReservation(Name, acc.Id);
        

    }
}
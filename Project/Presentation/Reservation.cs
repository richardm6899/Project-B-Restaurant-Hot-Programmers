static class Reservation
{
    static private ReservationLogic reservationlogic = new();

    public static void MakeReservation()
    {
        
        System.Console.WriteLine("Hello would you like to make a reservation? (Y/N) ");
        string choice = Console.ReadLine().ToUpper();

        if (choice == "Y")
        {
            System.Console.WriteLine("On what day would you like to reserve a table?");
            // check if valid date
            DateTime date = Convert.ToDateTime(Console.ReadLine());

            System.Console.WriteLine("For how many people?");
            int HowMany = Convert.ToInt32(Console.ReadLine());
            reservationlogic.CheckMin_MaxCapacity(HowMany);
            reservationlogic.ChecKDate(date);

            foreach (var table in reservationlogic.AvailableTables)
            {
                System.Console.WriteLine("available tables: ");
                System.Console.WriteLine(table.Id);
                System.Console.WriteLine(table.Chairs);
                System.Console.WriteLine(table.MinCapacity);
                System.Console.WriteLine(table.MaxCapacity);
                System.Console.WriteLine();
            }





        }
    }
}
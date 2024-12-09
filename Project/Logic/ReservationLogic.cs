using System.ComponentModel;
using System.Runtime.CompilerServices;
// to fix: reservation id duplicates, reservation id and prob table id can be duplicate,
//for now it chooses the first item in the list with the id
public class ReservationLogic
{
    public List<ReceiptModel> _receipts { get; set; }
    public List<ReservationModel> _reservations { get; set; }
    public List<TableModel> _tables { get; set; }
    public List<TableModel> AvailableTables { get; set; } //for available table check
    private AccountsLogic accountsLogic = new AccountsLogic();

    // load tables into properties
    public ReservationLogic()
    {
        _tables = TableAccess.LoadAllTables();
        _reservations = ReservationAccess.LoadAllReservations();
        _receipts = ReceiptAccess.LoadAllReceipts();
        AvailableTables = new(); // available tables is always empty at first
    }
    // create reservation -----------------------------------------
    // create table with given  info and checks
    public TableModel Createtable(int chairs, int minCapacity, int maxCapacity, string type) //tested
    {

        int new_id = _tables.Count + 1;
        TableModel table = new TableModel(new_id, chairs, minCapacity, maxCapacity, type);
        _tables.Add(table);
        TableAccess.WriteAllTables(_tables);
        return table;
    }

    // create reservation with given checks
    public ReservationModel Create_reservation(int tableID, string name, int clientID, int howMany, DateTime date, string typeofreservation, string timeslot, bool foodOrdered)//tested
    {
        int new_id = _reservations.Count + 1;
        ReservationModel reservation = new(new_id, tableID, name, clientID, howMany, date, typeofreservation, timeslot, foodOrdered);
        // add reservation to table.reservation list
        AssignTable(tableID, reservation);
        _reservations.Add(reservation);

        AccountModel acc = accountsLogic.GetById(clientID);
        if (acc.ReservationIDs == null)
        {
            acc.ReservationIDs = new List<int>(); // Initialize the list
        }
        acc.ReservationIDs.Add(reservation.Id);
        accountsLogic.UpdateList(acc);

        ReservationAccess.WriteAllReservations(_reservations);
        TableAccess.WriteAllTables(_tables);
        return reservation;

    }
    //to add reservation to table.reservation list
    public void AssignTable(int table_id, ReservationModel reservation)
    {
        foreach (var table in _tables)
        {

            if (table_id == table.Id)
            {
                table.Reservations.Add(reservation);
            }

        }
    }

    public TableModel GetTableById(int table_id)
    {
        foreach (var table in _tables)
        {
            if (table.Id == table_id)
            { return table; }
        }
        return null;
    }
    public ReservationModel GetReservationById(int reservation_id)
    {
        foreach (var reservation in _reservations)
        {
            if (reservation.Id == reservation_id)
            { return reservation; }
        }
        return null;
    }
    // date is today will be changed later for time of day
    // check if table is already reserved at the same time

    // public void CheckDate(DateTime date)
    // {
    //     List<int> tables_to_remove = new();
    //     // add all ids of table where already booked at the same time of day
    //     foreach (var table in _tables)
    //     {
    //         foreach (var res in table.Reservations)
    //         {
    //             if (res.Date == date && !tables_to_remove.Contains(table.Id))
    //             {

    //                 tables_to_remove.Add(table.Id);
    //             }
    //         }
    //     }

    //     //remove all tables from available tables where if id of table is in tables_to_remove list
    //     for (int i = AvailableTables.Count - 1; i >= 0; i--) // reversed loop AvailableTables so you dont get iteration error
    //     {
    //         foreach (var table_id in tables_to_remove)
    //         {
    //             if ((AvailableTables.Count - 1) >= 0)
    //             {

    //                 if (AvailableTables[i].Id == table_id)
    //                 {
    //                     AvailableTables.RemoveAt(i);
    //                 }

    //             }


    //         }
    //     }

    // }
    public void CheckDate(DateTime date, string timeSlot)
    {
        // Create a collection to hold IDs of tables that are already booked
        HashSet<int> bookedTableIds = new();

        // Identify tables that have reservations on the specified date and time slot
        foreach (var table in _tables)
        {
            // Check if the table has any reservation matching the given date and time slot
            bool isBooked = table.Reservations.Any(reservation =>
                reservation.Date == date && reservation.TimeSlot == timeSlot);

            if (isBooked)
            {
                bookedTableIds.Add(table.Id); // Add the table's ID to the set
            }
        }

        // Remove all tables from AvailableTables that have matching IDs in bookedTableIds
        AvailableTables.RemoveAll(table => bookedTableIds.Contains(table.Id));
    }

    // check which table person is allowed to book based on how many people are coming
    public void CheckMin_MaxCapacity(int HowMany)
    {

        foreach (var table in _tables)
        {

            if (HowMany >= table.MinCapacity && HowMany <= table.MaxCapacity)
            {
                AvailableTables.Add(table);
            }


        }

    }
    //check if date is valid
    // extensions:
    // person must only be able to book ahead of current date and 3 months in advanced
    // also should check if date is not when restaurant is closed
    // check if not fully booked for day
    public bool IsValidDate(DateTime date)
    {
        // if reservation is before the date then return false
        // if reservation is past 3 months return false

        if (date < DateTime.Today)
        {
            return false;
        }
        else if (date > DateTime.Now.AddMonths(3))
        {

            return false;
        }
        else
            return true;
    }
    // cancel reservation -------------------------------------------  
    //remove table by id from reservations

    public ReservationModel RemoveReservationByID(int reservation_id)
    {
        foreach (var reservation in _reservations)
        {
            if (reservation.Id == reservation_id)
            {

                reservation.Status = "Canceled";
                UnassignTable(reservation_id);

                if (GetReceiptById(reservation_id) != null)
                {
                    GetReceiptById(reservation_id).Status = "Canceled";
                }
                ReservationAccess.WriteAllReservations(_reservations);
                TableAccess.WriteAllTables(_tables);
                ReceiptAccess.WriteAllReceipts(_receipts);

                return reservation;
            }
        }

        return null;
    }
    // unassign reservation from table by reservation id
    public ReservationModel UnassignTable(int reservation_id)
    {
        foreach (var table in _tables)
        {
            foreach (var reservation in table.Reservations)
            {
                if (reservation_id == reservation.Id)
                {
                    table.Reservations.Remove(reservation);
                    return reservation;
                }
            }
        }
        return null;
    }
    // printable stuff --------------------------------------------------
    public string displayTable(int table_id)
    {

        TableModel table = GetTableById(table_id);
        return $"Table ID: {table.Id}\nChairs available: {table.Chairs}\nMinimum capacity: {table.MinCapacity}\nMaximum capacity: {table.MaxCapacity}\nType of table: {table.TypeOfTable}\n\n";


    }
    // for tables that are due for reservation
    public string displayAvailableTable(int table_id)
    {
        string ReturnString = "";
        foreach (var table in AvailableTables)
        {
            if (table.Id == table_id)
            {

                ReturnString += $"Table ID:  {table.Id}\nChairs at this table: {table.Chairs}\nType of table: {table.TypeOfTable}\n\n";

            }
        }
        return ReturnString;

    }
    // prints all available tables
    public string PrintAvailableTables()
    {
        string ReturnString = "";
        foreach (var table in AvailableTables)
        {
            ReturnString += $"Table ID:  {table.Id}\nChairs at this table: {table.Chairs}\nType of table: {table.TypeOfTable}\n\n";
        }


        return ReturnString;
    }
    public string DisplayAllReservations()
    {
        string ReturnString = "";
        foreach (var reservation in _reservations)
        {
            ReturnString += $"reservation details:\nReservation ID: {reservation.Id}\nTable number: {reservation.TableID}\nName: {reservation.Name}\nPersonal ID: {reservation.ClientID}\nPerson Amount: {reservation.HowMany}\nDate of Reservation: {reservation.Date.Date}\nType of reservation: {reservation.TypeOfReservation}\n\n";
        }
        return ReturnString;
    }
    public string DisplayReservationByID(int reservation_id)
    {

        foreach (var reservation in _reservations)
        {
            if (reservation.Status != "Canceled")
            {
                if (reservation_id == reservation.Id)
                {
                    return $"reservation details:\nReservation ID: {reservation.Id}\nTable number: {reservation.TableID}\nName: {reservation.Name}\nPersonal ID: {reservation.ClientID}\nPerson Amount: {reservation.HowMany}\nDate of Reservation: {reservation.Date.Date}\nType of reservation: {reservation.TypeOfReservation}\n\n";
                }
            }
        }
        return null;
    }

    public List<ReservationModel> DisplayAllReservationsByStatus(string status)
    {
        List<ReservationModel> reservations = new();
        if (status == null)
        {
            foreach (ReservationModel reservation in _reservations)
            {
                reservations.Add(reservation);
            }
        }
        foreach (ReservationModel reservation in _reservations)
        {
            if (reservation.Status == status)
            {
                reservations.Add(reservation);
            }
        }
        return reservations;
    }

    public List<ReservationModel> DisplayAllReservationsByDate(DateTime date)
    {
        List<ReservationModel> reservations = new();
        foreach (ReservationModel reservation in _reservations)
        {
            if (reservation.Date.Date == date.Date)
            {
                reservations.Add(reservation);
            }
        }
        return reservations;
    }

    public List<ReservationModel> DisplayAllReservationsByStatusAndID(int id, string status)
    {
        List<ReservationModel> reservations = new();
        foreach (ReservationModel reservation in _reservations)
        {
            if (status == null && reservation.ClientID == id)
            {
                reservations.Add(reservation);
            }
            else if (reservation.Status == status && reservation.ClientID == id)
            {
                reservations.Add(reservation);
            }
        }
        return reservations;
    }

    public List<ReservationModel> DisplayAllReservationsByDateAndID(int id, DateTime date)
    {
        List<ReservationModel> reservations = new();
        foreach (ReservationModel reservation in _reservations)
        {
            if (reservation.Date.Date == date.Date && reservation.ClientID == id)
            {
                reservations.Add(reservation);
            }
        }
        return reservations;
    }

    public List<ReservationModel> DisplayAllReservationsByClientID(int id)
    {
        List<ReservationModel> reservations = new();
        foreach (ReservationModel reservation in _reservations)
        {
            if (reservation.ClientID == id)
            {
                reservations.Add(reservation);
            }
        }
        return reservations;
    }

    public string DisplayReservations(int clientID)
    {
        string ReturnString = "";
        foreach (var reservation in _reservations)
        {
            if (reservation.Status != "Canceled")
            {
                if (clientID == reservation.ClientID)
                {
                    ReturnString += $"reservation details:\nReservation ID: {reservation.Id}\nTable number: {reservation.TableID}\nName: {reservation.Name}\nPersonal ID: {reservation.ClientID}\nPerson Amount: {reservation.HowMany}\nDate of Reservation: {reservation.Date.Date}\nType of reservation: {reservation.TypeOfReservation}\n\n";
                }
            }
        }
        return ReturnString;
    }
    public List<int> IsReservationInAccount(int clientID, int reservation_id)
    {
        List<int> valid_reservations = new();
        foreach (var client in accountsLogic._accounts)
        {
            if (clientID == client.Id)
            {

                foreach (var reservationID in client.ReservationIDs)
                {
                    if (reservation_id == reservationID)
                    {
                        if (GetReservationById(reservation_id).Status != "Canceled")
                        { valid_reservations.Add(reservation_id); }
                    }
                }

            }
        }
        return valid_reservations;
    }


    public List<string> DisplayAllReservationsList()

    {
        List<string> Reservations = new();
        foreach (ReservationModel reservation in _reservations)
        {
            Reservations.Add($"reservation details:\nReservation ID: {reservation.Id}\nTable number: {reservation.TableID}\nName: {reservation.Name}\nPersonal ID: {reservation.ClientID}\nPerson Amount: {reservation.HowMany}\nDate of Reservation: {reservation.Date.Date}\nStatus of reservation: {reservation.Status}\n");
        }
        return Reservations;
    }

    public List<string> DisplayAllOngoingReservations()
    {
        List<string> Reservations = new();
        foreach (ReservationModel reservation in _reservations)
        {
            if (reservation.Status == "Ongoing")
            {
                Reservations.Add($"reservation details:\nReservation ID: {reservation.Id}\nTable number: {reservation.TableID}\nName: {reservation.Name}\nPersonal ID: {reservation.ClientID}\nPerson Amount: {reservation.HowMany}\nDate of Reservation: {reservation.Date.Date}\nStatus of reservation: {reservation.Status}\n");
            }
        }
        return Reservations;
    }

    public string TypeOfReservation(int table_id)
    {
        foreach (var table in _tables)
        {
            if (table.Id == table_id)
            {
                return table.TypeOfTable;
            }
        }
        return null;
    }

    // receipt ------------------------------------------
    public ReceiptModel CreateReceipt(ReservationModel reservation, int cost, string number, string email, List<(FoodMenuModel, int)> foodOrdered)
    {
        int id = _receipts.Count() + 1;
        ReceiptModel receipt = new(id, reservation.Id, reservation.ClientID, cost, reservation.Date, reservation.TimeSlot, reservation.Name, number, email, reservation.TypeOfReservation, reservation.TableID, foodOrdered);
        _receipts.Add(receipt);
        ReceiptAccess.WriteAllReceipts(_receipts);
        return receipt;
    }
    public string DisplayReceipt(ReceiptModel receipt)
    {
        if(receipt.OrderedFood.Count() > 0)
        {
            string return_string = "";
            float totalCost = 0;

            return_string += $" -------------------------------------------- \n";
            return_string += $"          Hot Restaurant                       \n";
            return_string += $"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~  \n";
            return_string += $" Reservation No.: {receipt.ReservationId,-13}  \n";
            return_string += $" Date:            {receipt.Date.ToShortDateString(),-13}\n";
            return_string += $" TimeSlot:        {receipt.TimeSlot,-13}\n";
            return_string += $"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n";
            return_string += $" Name:            {receipt.Name,-13}\n";
            return_string += $" Email:           {receipt.Email,-13}\n";
            return_string += $"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n";
            return_string += $" Table No.:       {receipt.TableID,-13}\n";
            return_string += $" Type:            {receipt.TypeOfReservation,-13}\n";
            return_string += $"---------------------------------------------\n";
            return_string += $" Ordered:                             \n";
            foreach(var(item, quantity) in receipt.OrderedFood)
            {
            return_string += $"                   {item.DishName,-13} {quantity}x €{item.Price:F2}\n";
            totalCost += item.Price *quantity;
            }
            return_string += $"                                      \n";
            return_string += $" Total:           €{totalCost:F2}                  \n";
            return_string += $"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n";
            return_string += $" Cost:            €{receipt.Cost,-13}\n";
            return_string += $"                                     \n";
            return_string += $" --------------------------------------------- \n";

            return return_string;
        }
        else
        {
            string return_string = "";

            return_string += $" -------------------------------------------- \n";
            return_string += $"          Hot Restaurant                       \n";
            return_string += $"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~  \n";
            return_string += $" Reservation No.: {receipt.ReservationId,-13}  \n";
            return_string += $" Date:            {receipt.Date.ToShortDateString(),-13}\n";
            return_string += $" TimeSlot:        {receipt.TimeSlot,-13}\n";
            return_string += $"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n";
            return_string += $" Name:            {receipt.Name,-13}\n";
            return_string += $" Email:           {receipt.Email,-13}\n";
            return_string += $"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n";
            return_string += $" Table No.:       {receipt.TableID,-13}\n";
            return_string += $" Type:            {receipt.TypeOfReservation,-13}\n";
            return_string += $"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n";
            return_string += $" Cost:            €{receipt.Cost,-13}\n";
            return_string += $"                                     \n";
            return_string += $" --------------------------------------------- \n";

            return return_string;
        }






    }
    public ReceiptModel GetReceiptById(int id)
    {
        foreach (var receipt in _receipts)
        {
            if (receipt.ReservationId == id)
            {
                return receipt;
            }
        }
        return null;
    }
    // return_string += $" -------------------\n";
    // return_string += $"|   Hot Retaurant   |\n";
    // return_string += $"|~~~~~~~~~~~~~~~~~~~|\n";
    // return_string += $"|Date: {receipt.Date.ToShortDateString()}   |\n";
    // return_string += $"|Client id: {receipt.ClientId}       |\n";
    // return_string += $"|Reservation no.{receipt.ReservationId}  |\n";
    // return_string += $"|Ordered:           |\n";
    // return_string += $"|                   |\n";
    // return_string += $"|total:             |\n";
    // return_string += $"|~~~~~~~~~~~~~~~~~~~|\n";
    // return_string += $"|{receipt.Cost}                 |\n";
    // return_string += $"|                   |\n";
    // return_string += $" -------------------\n";

    // timeslot --------------------------------------------
    public static string TimSlotChooser(string id)
    {
        if (id == "1")
        {
            return "Lunch (12:00 - 14:00)";
        }
        else if (id == "2")
        {
            return "Dinner 1 (17:00 - 19:00)";
        }
        else if (id == "3")
        {
            return "Dinner 2 (19:00 - 21:00)";
        }
        else if (id == "4")
        {
            return "Dinner 3 (21:00 - 23:00)";
        }
        return null;
    }

    // Remove Reservation by choosing date
    public void RemoveReservationsByDate(DateTime date)
    {
        foreach (var reservation in _reservations)
        {
            if (reservation.Date.Date == date.Date)
            {

                reservation.Status = "Canceled";
                UnassignTable(reservation.Id);

                if (GetReceiptById(reservation.Id) != null)
                {
                    GetReceiptById(reservation.Id).Status = "Canceled";
                }
                ReservationAccess.WriteAllReservations(_reservations);
                TableAccess.WriteAllTables(_tables);
                ReceiptAccess.WriteAllReceipts(_receipts);
            }
        }
    }

    public int DisplayRestaurant()
    {
        // Initialize restaurant layout using a List of Lists
        var RestaurantLayout = new List<List<string>>
    {
        new() {"[  K  ]","[  K  ]","[  K  ]","[  K  ]","[  K  ]","[H:16 ]", "[H:17 ]"},
        new() { "[R:1  ]", "[R:2  ]", "[R:3  ]", "[R:4  ]", "[R:5  ]","[R:6  ]" ,"[H:18 ]",},
        new() { "[R:7  ]", "[R:8  ]", "[R:9  ]", "[R:10 ]", "[R:11 ]","[R:12 ]", "[H:19 ]" },
        new() { "[R:13 ]", "[R:14 ]", "[R:15 ]", "[H:20 ]", "[H:21 ]","[H:22 ]", "[H:23 ]" }
    };

        List<string> availableTableIDs = new List<string>() { };
        foreach (var table in AvailableTables)
        {
            availableTableIDs.Add(Convert.ToString(table.Id));
        }

        int currentRow = 0;
        int currentCol = 0;
        bool running = true;
        while (running)
        {
            Console.Clear();

            // Render the layout
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("(R)egular seats have a 50€ deposist.");
            Console.ForegroundColor = ConsoleColor.Magenta;
            System.Console.WriteLine("(H)otSeats Cost an extra 10 € on top of your 50 € deposit");
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("[X] These Tables are already booked for this timeslot");
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine("---------------------------------------------------------");
            for (int row = 0; row < RestaurantLayout.Count; row++)
            {
                for (int col = 0; col < RestaurantLayout[row].Count; col++)
                {
                    string table = RestaurantLayout[row][col];
                    bool isCursorPosition = row == currentRow && col == currentCol;


                    if (isCursorPosition)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write("[  X  ]");
                    }
                    else if (table.Contains("H"))
                    {
                        string id = table.Split(":")[1].Trim(']').Trim();
                        if (availableTableIDs.Contains(id))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            table = "[  X  ]";
                        }
                        Console.Write(table);
                    }
                    else if (table.Contains("R"))
                    {
                        string id = table.Split(":")[1].Trim(']').Trim(); ;
                        if (availableTableIDs.Contains(id))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            table = "[  X  ]";
                        }
                        Console.Write(table);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write(table);
                    }

                    Console.ResetColor();
                    Console.Write(" "); // Add spacing between items
                }
                Console.WriteLine(); // Move to the next row
            }

            // Instructions
            Console.WriteLine("\n-------------------------------------------------------");
            Console.WriteLine("\nUse arrow keys to move, Enter to book, and Esc to exit.");

            // Read key input
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow && currentRow > 0) currentRow--;
            if (key == ConsoleKey.DownArrow && currentRow < RestaurantLayout.Count - 1) currentRow++;
            if (key == ConsoleKey.LeftArrow && currentCol > 0) currentCol--;
            if (key == ConsoleKey.RightArrow && currentCol < RestaurantLayout[currentRow].Count - 1) currentCol++;

            // Booking a table
            if (key == ConsoleKey.Enter)
            {
                string selectedTable = RestaurantLayout[currentRow][currentCol];

                if (selectedTable.Contains("R") || selectedTable.Contains("H"))
                {
                    string id = selectedTable.Split(":")[1].Trim(']').Trim();
                    if (availableTableIDs.Contains(id))
                    {
                        RestaurantLayout[currentRow][currentCol] = "[  X  ]";
                        availableTableIDs.Remove(id);
                        return Convert.ToInt32(id);
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid table chosen.");
                        Console.ReadKey(true);
                    }
                }
            }
            // Exit on Esc
            if (key == ConsoleKey.Escape)
                return 0;

        }
        return 0;
    }

}



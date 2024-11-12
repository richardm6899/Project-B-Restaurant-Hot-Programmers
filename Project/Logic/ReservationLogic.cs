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
    public ReservationModel Create_reservation(int tableID, string name, int clientID, int howMany, DateTime date, string typeofreservation)//tested
    {
        int new_id = _reservations.Count + 1;
        ReservationModel reservation = new(new_id, tableID, name, clientID, howMany, date, typeofreservation);
        // add reservation to table.reservation list
        AssignTable(tableID, reservation);
        _reservations.Add(reservation);

        AccountModel acc = accountsLogic.GetById(clientID);
        if (acc.ReservationIDs == null)
        {
            acc.ReservationIDs = new();
            acc.ReservationIDs.Add(reservation.Id);
        }
        else acc.ReservationIDs.Add(reservation.Id);
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
    public void CheckDate(DateTime date)
    {
        HashSet<int> tables_to_remove = new();

        // Collect IDs of tables that are already booked at the same time of day
        foreach (var table in _tables)
        {
            if (table.Reservations.Any(res => res.Date == date))
            {
                tables_to_remove.Add(table.Id);
            }
        }

        // Remove all tables from AvailableTables if their ID is in tables_to_remove
        AvailableTables.RemoveAll(table => tables_to_remove.Contains(table.Id));
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
                ReservationAccess.WriteAllReservations(_reservations);
                if (GetReceiptById(reservation_id) != null)
                {
                    GetReceiptById(reservation_id).Status = "Canceled";
                }
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
    public string DisplayReservation(int reservation_id)
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
    public ReceiptModel CreateReceipt(ReservationModel reservation, int cost, string number, string email)
    {
        int id = _receipts.Count() + 1;
        ReceiptModel receipt = new(id, reservation.Id, reservation.ClientID, cost, reservation.Date, reservation.Name, number, email);
        _receipts.Add(receipt);
        ReceiptAccess.WriteAllReceipts(_receipts);
        return receipt;
    }
    public string DisplayReceipt(ReceiptModel receipt)
    {
        string return_string = "";
     
        return_string += $" -----------------------\n";
        return_string += $"|     Hot Retaurant     |\n";
        return_string += $"|~~~~~~~~~~~~~~~~~~~~~~~|\n";
        return_string += $"|Status: {receipt.Status}        |\n";
        return_string += $"|Date: {receipt.Date.ToShortDateString()}       |\n";
        return_string += $"|Client Name: {receipt.Name}       |\n";
        return_string += $"|Client Email: {receipt.Email}           |\n";
        return_string += $"|Reservation no.{receipt.ReservationId}  |\n";
        return_string += $"|Ordered:           |\n";
        return_string += $"|                   |\n";
        return_string += $"|total:             |\n";
        return_string += $"|~~~~~~~~~~~~~~~~~~~|\n";
        return_string += $"|{receipt.Cost}                 |\n";
        return_string += $"|                   |\n";
        return_string += $" -------------------\n";
        return return_string;
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

}
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Globalization;
using System.Reflection.Metadata;
// to fix: reservation id duplicates, reservation id and prob table id can be duplicate,
//for now it chooses the first item in the list with the id
public class ReservationLogic
{
    static private TableAccess tableAccess = new();
    static private ReservationAccess reservationAccess = new();
    static private ReceiptAccess receiptAccess = new();
    public List<ReceiptModel> _receipts { get; set; }
    public List<ReservationModel> _reservations { get; set; }
    public List<TableModel> _tables { get; set; }
    public static List<TableModel> AvailableTables = new();//for available table check
    private AccountsLogic accountsLogic = new AccountsLogic();


    // load tables into properties
    public ReservationLogic()
    {
        _tables = tableAccess.LoadAll();
        _reservations = reservationAccess.LoadAll();
        _receipts = receiptAccess.LoadAll();
        // available tables is always empty at first
    }
    // create reservation -----------------------------------------
    // create table with given  info and checks
    public TableModel Createtable(int chairs, int minCapacity, int maxCapacity, string type) //tested
    {

        int new_id = _tables.Count + 1;
        TableModel table = new TableModel(new_id, chairs, minCapacity, maxCapacity, type);
        _tables.Add(table);
        tableAccess.WriteAll(_tables);
        return table;
    }

    public void UpdateReservationsList(ReservationModel res)
    {
        //Find if there is already an model with the same id
        int index = _reservations.FindIndex(s => s.Id == res.Id);

        if (index != -1)
        {
            //update existing model
            _reservations[index] = res;
        }
        else
        {
            //add new model
            _reservations.Add(res);
        }
        reservationAccess.WriteAll(_reservations);

    }

    // create reservation with given checks
    public ReservationModel Create_reservation(int tableID, string name, int clientID, int howMany, DateTime date, string typeofreservation, string timeslot, bool foodOrdered)//tested
    {
        int new_id = _reservations.Count + 1;

        ReservationModel reservation = new(new_id, new List<int>() { tableID }, name, clientID, howMany, date, typeofreservation, timeslot, foodOrdered);

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

        reservationAccess.WriteAll(_reservations);
        tableAccess.WriteAll(_tables);
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
    public void AssignTables(List<int> tableIDs, ReservationModel reservation)
    {
        foreach (var tableID in tableIDs)
        {
            AssignTable(tableID, reservation);
        }
    }
    public List<TableModel> TemporarilyUnassignTable(int reservation_id)
    {
        foreach (var table in _tables)
        {
            for (int i = 0; i < table.Reservations.Count; i++)
            {
                if (table.Reservations[i].Id == reservation_id)
                {
                    table.Reservations.RemoveAt(i);
                }
            }
        }
        return _tables;
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
                tableAccess.WriteAll(_tables);
                receiptAccess.WriteAll(_receipts);
                UpdateReservationsList(reservation);
                return reservation;
            }
        }
        return null;

    }

    public void UnassignTable(int reservation_id)
    {
        foreach (var table in _tables)
        {
            for (int i = 0; i < table.Reservations.Count; i++)
            {
                if (table.Reservations[i].Id == reservation_id)
                {

                    table.Reservations.RemoveAt(i);

                }
            }
        }
        tableAccess.WriteAll(_tables);

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
            ReturnString += $"reservation details:\nReservation ID: {reservation.Id}\nTable number: {reservation.TableID[0]}\nName: {reservation.Name}\nPersonal ID: {reservation.ClientID}\nPerson Amount: {reservation.HowMany}\nDate of Reservation: {reservation.Date.Date}\nType of reservation: {reservation.TypeOfReservation}\n\n";
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
                    return $"reservation details:\nReservation ID: {reservation.Id}\nTable number: {reservation.TableID[0]}\nName: {reservation.Name}\nPersonal ID: {reservation.ClientID}\nPerson Amount: {reservation.HowMany}\nDate of Reservation: {reservation.Date.Date}\nType of reservation: {reservation.TypeOfReservation}\n\n";
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

    // return reservations that date is more than or equal 48 hours from today


    public List<ReservationModel> DisplayAllReservationsByStatusAndID(int id, string status)
    {
        //  if _reservations doesn't update add this to the method. :)
        _reservations = reservationAccess.LoadAll();
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
                    ReturnString += $"reservation details:\nReservation ID: {reservation.Id}\nTable number: {string.Join(", ", reservation.TableID)}\nName: {reservation.Name}\nPersonal ID: {reservation.ClientID}\nPerson Amount: {reservation.HowMany}\nDate of Reservation: {reservation.Date.Date}\nTimeSlot: {reservation.TimeSlot}\nType of reservation: {reservation.TypeOfReservation}\n\n";
                }
            }
        }
        return ReturnString;
    }

    public List<ReservationModel> AllOngoingReservationsByID(int id)
    {
        List<ReservationModel> reservations = new();
        foreach (ReservationModel reservation in _reservations)
        {
            if (reservation.ClientID == id && reservation.Status == "Ongoing")
            {
                reservations.Add(reservation);
            }
        }
        return reservations;
    }

    public List<ReservationModel> AllOngoingReservationsListReservationModel()
    {
        List<ReservationModel> reservations = new();
        foreach (ReservationModel reservation in _reservations)
        {
            if (reservation.Status == "Ongoing")
            {
                reservations.Add(reservation);
            }
        }
        return reservations;
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
            Reservations.Add($"reservation details:\nReservation ID: {reservation.Id}\nTable number: {reservation.TableID[0]}\nName: {reservation.Name}\nPersonal ID: {reservation.ClientID}\nPerson Amount: {reservation.HowMany}\nDate of Reservation: {reservation.Date.Date}\nStatus of reservation: {reservation.Status}\n");
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
                Reservations.Add($"reservation details:\nReservation ID: {reservation.Id}\nTable number: {reservation.TableID[0]}\nName: {reservation.Name}\nPersonal ID: {reservation.ClientID}\nPerson Amount: {reservation.HowMany}\nDate of Reservation: {reservation.Date.Date}\nStatus of reservation: {reservation.Status}\n");
            }
        }
        return Reservations;
    }

    // not using wel getest
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

    public ReceiptModel CreateReceipt(ReservationModel reservation, int cost, string number, string email, List<(FoodMenuModel, int)> foodOrdered, List<int> tableId)
    {
        int id = _receipts.Count() + 1;

        ReceiptModel receipt = new(id, reservation.Id, reservation.ClientID, cost, reservation.Date, reservation.TimeSlot, reservation.Name, number, email, reservation.TypeOfReservation, Convert.ToString(reservation.TableID[0]), foodOrdered);

        _receipts.Add(receipt);
        receiptAccess.WriteAll(_receipts);
        return receipt;
    }
    public string DisplayReceipt(ReceiptModel receipt, List<List<string>> allergies)
    {
        if (receipt.OrderedFood.Count() > 0)
        {
            int position = 0;
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
            foreach (var (item, quantity) in receipt.OrderedFood)
            {


                float itemTotal = item.Price * quantity; // Calculate total price for this item
                totalCost += itemTotal; // Add to the total cost

                // Display dish name, quantity, and item price
                return_string += $" {item.DishName,-20} {quantity}x €{item.Price:F2}  (Total: €{itemTotal:F2})\n";

                // Check if the item is a "Chef's Menu" and allergies are provided
                if (item.Type.Contains("Chef's Menu"))
                {
                    for (int i = 0; i < quantity; i++)
                    {
                        if (position < allergies.Count && allergies[position].Count > 0)
                        {
                            return_string += $"   Allergies: {string.Join(", ", allergies[position])}\n";
                        }
                        else
                        {
                            return_string += $"   Allergies: None\n";
                        }
                        position++;
                    }
                }
            }
            return_string += $"                                      \n";
            return_string += $" Total Food Cost: €{totalCost:F2}            \n";
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
    public ReceiptModel GetReceiptById(int reservationid)
    {
        foreach (var receipt in _receipts)
        {
            if (receipt.ReservationId == reservationid)
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


    // Remove Reservation by choosing date
    public void RemoveReservationsByDate(DateTime date)
    {
        foreach (var reservation in _reservations)
        {
            if (reservation.Date == date)
            {

                reservation.Status = "Canceled";
                UnassignTable(reservation.Id);

                if (GetReceiptById(reservation.Id) != null)
                {
                    GetReceiptById(reservation.Id).Status = "Canceled";
                }
            }
        }
                reservationAccess.WriteAll(_reservations);
                tableAccess.WriteAll(_tables);
                receiptAccess.WriteAll(_receipts);
              
    }


    // calender checks/hot seat checks---------------------------------------

    // check for days that are at restaurant max capacity --  could make this grouped by
    public List<DateTime> MaxCapDays()
    {
        List<DateTime> MaxCapDays = new() { };
        Dictionary<DateTime, int> datecount = new();
        // check if date already in datecout
        foreach (var table in _tables)
        {
            foreach (var res in table.Reservations)
            {
                if (!datecount.Keys.Contains(res.Date))
                {
                    datecount[res.Date] = 1;
                }
                else
                {
                    datecount[res.Date]++;
                }
            }
        }
        // if date is has equal amount of reservations as cap then add to max capacity days
        foreach (var item in datecount)
        {
            if (!MaxCapDays.Contains(item.Key) && item.Value == 92)
            {
                MaxCapDays.Add(item.Key);
            }
        }
        return MaxCapDays;
    }

    // check which table person is allowed to book based on how many people are coming
    public void AddRegularSeats(int HowMany)
    {

        foreach (var table in _tables)
        {

            if (HowMany >= table.MinCapacity && HowMany <= table.MaxCapacity && table.TypeOfTable != "HotSeat")
            {
                AvailableTables.Add(table);
            }


        }

    }
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
    public List<DateTime> MaxTimeSlotRegularSeat(string timeslotid, int amount)
    {

        List<DateTime> MaxTimeSlotDays = new();

        foreach (var table in _tables)
        {
            foreach (var res in table.Reservations)
            {
                AddRegularSeats(amount);
                CheckDate(Convert.ToDateTime(res.Date), timeslotid);

                if (AvailableTables.Count() == 0)
                {

                    MaxTimeSlotDays.Add(res.Date);
                }
                AvailableTables.Clear();

            }
        }


        return MaxTimeSlotDays;
    }




    // hotseat shizzlee -----------------------------------------------
    public void AddHotSeatsAvailableTables()
    {

        foreach (var table in _tables)
        {

            if (table.TypeOfTable == "HotSeat")
            {
                AvailableTables.Add(table);
            }


        }

    }

    // get all hotseats into the available tables so we an work with them
    public void CheckDateHotSeat(DateTime date, string timeSlot)
    {
        // Create a collection to hold IDs of tables that are already booked
        HashSet<int> bookedTableIds = new();

        // Identify tables that have reservations on the specified date and time slot
        foreach (var table in _tables)
        {
            // Check if the table has any reservation matching the given date and time slot
            bool isBooked = table.Reservations.Any(reservation =>
                reservation.Date.Date == date.Date && reservation.TimeSlot == timeSlot && reservation.TypeOfReservation == "HotSeat");

            if (isBooked)
            {
                bookedTableIds.Add(table.Id); // Add the table's ID to the set
            }
        }
        // Remove all tables from AvailableTables that have matching IDs in bookedTableIds
        AvailableTables.RemoveAll(table => bookedTableIds.Contains(table.Id));

    }
    //gives al unavailable hotseats for the calender display
    public List<DateTime> MaxTimeSlotHotSeat(string timeslotid, int amount)
    {

        List<DateTime> MaxTimeSlotDays = new();

        foreach (var table in _tables)
        {
            // check reservations in hot seat table
            if (table.TypeOfTable == "HotSeat")
            {

                foreach (var res in table.Reservations)
                {


                    AddHotSeatsAvailableTables();
                    CheckDateHotSeat(Convert.ToDateTime(res.Date), timeslotid);

                    if (AvailableTables.Count() < amount)
                    {

                        MaxTimeSlotDays.Add(res.Date);
                    }
                    AvailableTables.Clear();
                }

            }
        }


        return MaxTimeSlotDays;
    }
    public static List<int> ChooseSeats(int amount)
    {

        //initialize availableTables for hot hotseats

        List<int> tableIDs = new();
        int i = 0;

        foreach (var table in AvailableTables)
        {
            if (i < amount)
            {

                tableIDs.Add(table.Id);
                i++;
            }

        }
        return tableIDs;
    }
    // assigns reservations to tables and adds one reservation to the json and gives one reservation to the receipt
    public List<ReservationModel> Create_reservationHotSeat(List<int> tableid, string name, int clientID, int howMany, DateTime date, string typeofreservation, string timeslot, bool foodOrdered)//tested
    {

        int new_id = _reservations.Count + 1;
        List<ReservationModel> reservationList = new();
        ReservationModel reservation = new(new_id, tableid, name, clientID, howMany, date, typeofreservation, timeslot, foodOrdered);
        foreach (var tableID in tableid)
        {
            // add reservation to table.reservation list
            AssignTable(tableID, reservation);

        }
        reservationList.Add(reservation);
        AccountModel acc = accountsLogic.GetById(clientID);
        if (acc.ReservationIDs == null)
        {
            acc.ReservationIDs = new List<int>(); // Initialize the list
        }
        acc.ReservationIDs.Add(reservation.Id);
        accountsLogic.UpdateList(acc);
        _reservations.Add(reservationList[0]);
        tableAccess.WriteAll(_tables);
        reservationAccess.WriteAll(_reservations);
        return reservationList;

    }
    public ReceiptModel CreateReceiptHotSeat(List<ReservationModel> reservations, int cost, string number, string email, List<(FoodMenuModel, int)> orderdFood)
    {
        int id = _receipts.Count() + 1;
        ReceiptModel receipt = new(id, reservations[0].Id, reservations[0].ClientID, cost, reservations[0].Date, reservations[0].TimeSlot, reservations[0].Name, number, email, reservations[0].TypeOfReservation, string.Join(",", reservations[0].TableID), orderdFood);
        _receipts.Add(receipt);
        receiptAccess.WriteAll(_receipts);
        return receipt;
    }

    public List<TableModel> GetTablesByReservation(ReservationModel reservation)
    {
        List<TableModel> returntables = new();
        foreach (var tableid in reservation.TableID)
        {
            foreach (var table in _tables)
            {
                if (tableid == table.Id)
                {
                    returntables.Add(table);
                }
            }
        }
        return returntables;
    }
    public void ModifyReservation<T>(ReservationModel reservation, T item)
    {
        // modify amount int
        // modify timeslot string
        // modify Date DateTime
        foreach (var res in _reservations)
        {
            if (res.Id == reservation.Id)
            {

                if (item is int howmany)
                {

                    reservation.HowMany = howmany;
                }
                else if (item is DateTime date)
                {
                    reservation.Date = date;
                }
                else if (item is string timeSlot)
                {
                    reservation.TimeSlot = timeSlot;
                }
            }
        }
        foreach (var table in _tables)
        {
            foreach (var res in table.Reservations)
            {
                if (res.Id == reservation.Id)
                {

                    if (item is int howmany)
                    {
                        res.HowMany = howmany;
                    }
                    else if (item is DateTime date)
                    {
                        res.Date = date;
                    }
                    else if (item is string timeSlot)
                    {
                        res.TimeSlot = timeSlot;
                    }
                }
            }
        }
        reservationAccess.WriteAll(_reservations);

        tableAccess.WriteAll(_tables);
    }

    public void ModifyReservation(ReservationModel reservation, List<int> TableID, int Howmany, DateTime Date, string Timeslot, string typeofreservation, bool FoodOrdered)
    {
        // modify amount int
        // modify timeslot string
        // modify Date DateTime
        foreach (var res in _reservations)
        {
            if (res.Id == reservation.Id)
            {

                reservation.TableID = TableID;
                reservation.HowMany = Howmany;
                reservation.Date = Date;
                reservation.TimeSlot = Timeslot;
                reservation.TypeOfReservation = typeofreservation;
                reservation.FoodOrdered = FoodOrdered;

            }
        }
        reservationAccess.WriteAll(_reservations);


    }
    public void ModifyReservationTable(ReservationModel reservation, List<int> tableID)
    {

        // Unassign collected reservations
        UnassignTable(reservation.Id);
        // Assign new tables
        foreach (var table_id in tableID)
        {
            AssignTable(table_id, reservation);
        }


        // Write updated reservations to storage
        tableAccess.WriteAll(_tables);
    }
    public List<ReservationModel> ModifyableReservations(int client_id)
    {

        return _reservations.Where(reservation => reservation.Status == "Ongoing" &&
        reservation.ClientID == client_id &&
        reservation.Date.Date >= DateTime.Today.AddHours(48))
        .ToList();
    }
    public ReceiptModel ModifyReceipt(ReservationModel reservation, List<(FoodMenuModel, int)>? foodOrdered)
    {
        ReceiptModel receipt = GetReceiptById(reservation.Id);
        if (receipt == null)
        {
            return null;
        }
        receipt.Date = reservation.Date;
        receipt.TimeSlot = reservation.TimeSlot;
        receipt.TableID = string.Join(",", reservation.TableID);
        receipt.TypeOfReservation = reservation.TypeOfReservation;
        if (foodOrdered != null)
        {
            receipt.OrderedFood = foodOrdered;
        }
        receiptAccess.WriteAll(_receipts);
        return receipt;
    }

}


public class ReservationLogic
{
    private List<ReservationModel> _reservations { get; set; }
    private List<TableModel> _tables { get; set; }
    public List<TableModel> AvailableTables { get; set; }

    // load tables into properties
    public ReservationLogic()
    {
        _tables = TableAccess.LoadAllTables();
        _reservations = ReservationAccess.LoadAllReservations();
        AvailableTables = new();
    }

    // create table with given  info and checks
    public void Create_table(int chairs, int minCapacity, int maxCapacity)
    {
        int new_id = _tables.Count + 1;
        _tables.Add(new TableModel(new_id, chairs, minCapacity, maxCapacity));
        TableAccess.WriteAllTables(_tables);
    }

    // create reservation with given checks
    public void Create_reservation(int tableID, string name, int clientID, int howMany, DateTime date)
    {
        int new_id = _reservations.Count + 1;
        ReservationModel reservation = new(new_id, tableID, name, clientID, howMany, date);

        AssignTable(tableID, reservation);
        _reservations.Add(reservation);


        ReservationAccess.WriteAllReservations(_reservations);
        TableAccess.WriteAllTables(_tables);

    }
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

    // date is today will be changed later for time of day
    public void ChecKDate(DateTime date)
    {
        List<int> tables_to_remove = new();
        foreach (var table in _tables)
        {
            foreach (var res in table.Reservations)
            {
                if (res.Date == date && !tables_to_remove.Contains(table.Id))
                {

                    tables_to_remove.Add(table.Id);
                }
            }
        }
        
        foreach (var table in AvailableTables)
        {
            foreach (var table_id in tables_to_remove)
            {
                if (table.Id == table_id)
                {
                    AvailableTables.Remove(table);
                }
            }


        }

    }

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




}
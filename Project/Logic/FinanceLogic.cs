public class FinanceLogic
{
    public List<AccountModel> _accounts;
    public List<ReceiptModel> _receipts;
    public List<ReservationModel> _reservations;
    public static int Revenue = 0;
    public FinanceLogic()
    {
        _accounts = AccountsAccess.LoadAll();
        _receipts = ReceiptAccess.LoadAllReceipts();
        _reservations = ReservationAccess.LoadAllReservations();
    }
    public static void AddToRevenue(int cost)
    {
        Revenue += cost;
    }

    public static void SubtractFromRevenue(int cost)
    {
        Revenue += cost;
        //  Users that had a reservation on that day get a refund. (paid out of financials)
        
    }
    public int ProfitsDay(DateTime date)
    {
        int total = 0;
        foreach (var receipt in _receipts)
        {
            if (receipt.Status != "Canceled")
            {
                if (date == receipt.Date)

                {

                    total += receipt.Cost;
                }
            }


        }
        return total;
    }
    public int ProfitsMonth(DateTime date)
    {
        int total = 0;
        foreach (var receipt in _receipts)
        {
            if (receipt.Status != "Canceled")
            {
                if (date.Month == receipt.Date.Month && date.Year == receipt.Date.Year)

                {

                    total += receipt.Cost;
                }
            }


        }
        return total;
    }
    public int ProfitsWeek(DateTime startofweek, DateTime endofweek)
    {
        int total = 0;
        foreach (var receipt in _receipts)
        {
            if (receipt.Status != "Canceled")
            {
                if (receipt.Date >= startofweek && receipt.Date <= endofweek)

                {

                    total += receipt.Cost;
                }
            }


        }
        return total;
    }
    public int ProfitsYear(DateTime date)
    {
        int total = 0;
        foreach (var receipt in _receipts)
        {
            if (receipt.Status != "Canceled")
            {
                if (date.Year == receipt.Date.Year)

                {

                    total += receipt.Cost;
                }
            }


        }
        return total;
    }
    public int TotalProfits()
    {
        int total = 0;
        foreach (var receipt in _receipts)
        {
            if (receipt.Status != "Canceled")
            {

                total += receipt.Cost;

            }


        }
        return total;
    }
    public int TotalReservationsDay(DateTime date)
    {

        int total = 0;
        foreach (var reservation in _reservations)
        {
            if (reservation.Status != "Canceled")
            {
                if (date == reservation.Date)

                {

                    total++;
                }
            }


        }
        return total;

    }
    public int TotalReservationsMonth(DateTime date)
    {

        int total = 0;
        foreach (var reservation in _reservations)
        {
            if (reservation.Status != "Canceled")
            {
                if (date.Month == reservation.Date.Month && date.Year == reservation.Date.Year)
                {

                    total++;
                }
            }



        }
        return total;

    }
    public int TotalReservationsYear(DateTime date)
    {

        int total = 0;
        foreach (var reservation in _reservations)
        {
            if (reservation.Status != "Canceled")
            {
                if (date.Year == reservation.Date.Year)
                {

                    total++;
                }
            }



        }
        return total;

    }
    public int TotalReservations()
    {

        int total = 0;
        foreach (var reservation in _reservations)
        {
            if (reservation.Status != "Canceled")
            {



                total++;

            }



        }
        return total;

    }
    public int ReservationsWeek(DateTime startofweek, DateTime endofweek)
    {
        int total = 0;
        foreach (var receipt in _receipts)
        {
            if (receipt.Status != "Canceled")
            {
                if (receipt.Date >= startofweek && receipt.Date <= endofweek)

                {

                    total++;
                }
            }


        }
        return total;


    }


}
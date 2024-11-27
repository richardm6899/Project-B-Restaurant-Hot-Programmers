public static class MessageLogic
{
    public static List<ReservationModel> _reservations { get; set; }
    public static List<ReceiptModel> _receipts { get; set; }

    static MessageLogic()
    {
        _reservations = ReservationAccess.LoadAllReservations();
        _receipts = ReceiptAccess.LoadAllReceipts();
    }

    public static bool Inbox(int clientID)
    {
        bool hasMessage = false;
        foreach (var reservation in _reservations)
        {
            if (clientID == reservation.ClientID)
            {
                if (reservation.Status == "Canceled")
                {
                    hasMessage = true;
                }
            }
        }
        return hasMessage;
    }
}
    // public ReceiptModel CreateReceipt(ReservationModel reservation, int cost, string number, string email)
    // {
    //     int id = _receipts.Count() + 1;
    //     ReceiptModel receipt = new(id, reservation.Id, reservation.ClientID, cost, reservation.Date, reservation.Name, number, email);
    //     _receipts.Add(receipt);
    //     ReceiptAccess.WriteAllReceipts(_receipts);
    //     return receipt;
    // }
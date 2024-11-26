using System.Text.Json.Serialization;


public class ReceiptModel
{

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("reservationid")]
    public int ReservationId { get; set; }

    [JsonPropertyName("clientid")]
    public int ClientId { get; set; }

    [JsonPropertyName("cost")]
    public int Cost { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("timeslot")]
    public string TimeSlot { get; set; }


    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("typeofreservation")]
    public string TypeOfReservation { get; set; }

    [JsonPropertyName("tableid")]
    public int TableID { get; set; }



    public ReceiptModel(int id, int reservationId, int clientId, int cost, DateTime date,string timeslot, string name, string phoneNumber, string email,string typeofreservation,int tableID)

    {
        Id = id;
        ReservationId = reservationId;
        ClientId = clientId;
        Cost = cost;
        Date = date;
        TimeSlot = timeslot;
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        Status = "Ongoing";
        TypeOfReservation = typeofreservation;
        TableID = tableID;
        

    }
}





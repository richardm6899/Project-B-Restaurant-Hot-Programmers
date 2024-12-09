using System.Text.Json.Serialization;


public  class ReservationModel
{
    [JsonPropertyName("id")]

    public int Id { get; set; }

    [JsonPropertyName("tableID")]
    public List<int> TableID { get; set; }


    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("clientID")]
    public int ClientID { get; set; }

    [JsonPropertyName("howMany")]
    public int HowMany { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("timeslot")]
    public string TimeSlot { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("typeofreservation")]
    public string TypeOfReservation { get; set; }



    public ReservationModel(int id, List<int> tableID, string name, int clientID, int howMany, DateTime date, string typeofreservation, string timeslot)
    {
        Id = id;

        TableID = tableID;
        Name = name;

        ClientID = clientID;
        HowMany = howMany;

        Date = Convert.ToDateTime(date);
        TimeSlot = timeslot;
        Status = "Ongoing";
        TypeOfReservation = typeofreservation;
    }


}





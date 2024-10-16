using System.Text.Json.Serialization;


public class ReservationModel
{
    [JsonPropertyName("id")]

    public int Id { get; set; }

    [JsonPropertyName("tableID")]
    public int TableID { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("clientID")]
    public int ClientID { get; set; }

    [JsonPropertyName("howMany")]
    public int HowMany { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    public ReservationModel(int id, int tableID, string name, int clientID, int howMany, DateTime date)
    {
        Id = id;

        TableID = tableID;
        Name = name;

        ClientID = clientID;
        HowMany = howMany;
        Date = Convert.ToDateTime(date);
    }


}





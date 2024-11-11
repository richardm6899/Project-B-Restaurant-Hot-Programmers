using System.Text.Json.Serialization;


public class TableModel
{
    [JsonPropertyName("id")]

    public int Id { get; set; }

    [JsonPropertyName("chairs")]
    public int Chairs { get; set; }

    [JsonPropertyName("minCapacity")]
    public int MinCapacity { get; set; }

    [JsonPropertyName("maxCapacity")]
    public int MaxCapacity { get; set; }

    [JsonPropertyName("reservations")]
    public List<ReservationModel> Reservations { get; set; }

    [JsonPropertyName("typeoftable")]
    public string TypeOfTable { get; set; }
    public TableModel(int id, int chairs, int minCapacity, int maxCapacity,string typeoftable)
    {
        Id = id;
        Chairs = chairs;
        MinCapacity = minCapacity;
        MaxCapacity = maxCapacity;
        Reservations = new();
        TypeOfTable = typeoftable;

    }

}





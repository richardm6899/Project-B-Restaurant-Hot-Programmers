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

    [JsonPropertyName("status")]
    public string Status { get; set; }


    public ReceiptModel(int id, int reservationId, int clientId, int cost, DateTime date)
    {
        Id = id;
        ReservationId = reservationId;
        ClientId = clientId;
        Cost = cost;
        Date = date;
        Status = "Ongoing";
    }
}





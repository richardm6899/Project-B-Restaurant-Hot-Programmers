using System.Text.Json.Serialization;

public class AttendeesModel
{
    [JsonPropertyName("attendeeId")]
    public int attendeeId {get; set;}

    [JsonPropertyName("clientId")]
    public int Id { get; set; }

    [JsonPropertyName("PeopleToAttend")]
    public int PeopleToAttend { get; set; }

    [JsonPropertyName("Cost")]
    public int Cost { get; set; }
    
    [JsonPropertyName("EventID")]
    public int EventID {get; set;}
}
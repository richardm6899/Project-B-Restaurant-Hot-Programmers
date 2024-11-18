using System.Text.Json.Serialization;


public class RestaurantModel
{
    [JsonPropertyName("tittle")]
    public string tittle { get; set; }

    [JsonPropertyName("description")]
    public string description { get; set; }

    [JsonPropertyName("openingHours")]
    public List<string> opening_hours { get; set; }

    [JsonPropertyName("closedDates")]
    public List<string> closed_dates { get; set; }

    [JsonPropertyName("location")]
    public string location { get; set; }

    [JsonPropertyName("phoneNumber")]
    public string phone_number { get; set; }

    [JsonPropertyName("emailAddress")]
    public string email_address { get; set; }
    //  list will be made even if no allergies are given.
    [JsonPropertyName("FAQ")]
    public List<string> faq { get; set; }

    public RestaurantModel(string tittle, string description, List<string> opening_hours, List<string> closed_dates,string location, string phone_number, string email_address, List<string> faq)
    {
        this.tittle = tittle;
        this.description = description;
        this.opening_hours = opening_hours;
        this.closed_dates = closed_dates;
        this.location = location;
        this.phone_number = phone_number;
        this.email_address = email_address;
        this.faq = faq ?? new List<string>();
    }

}
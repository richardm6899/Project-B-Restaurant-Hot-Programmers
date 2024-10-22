using System.Text.Json.Serialization;


public class RestaurantModel
{
    [JsonPropertyName("Tittle")]
    public string tittle { get; set; }

    [JsonPropertyName("description")]
    public string description { get; set; }

    [JsonPropertyName("openingHours")]
    public string opening_hours { get; set; }

    [JsonPropertyName("location")]
    public string location { get; set; }

    [JsonPropertyName("phoneNumber")]
    public string phone_number { get; set; }

    [JsonPropertyName("emailAddress")]
    public string email_address { get; set; }
    //  list will be made even if no allergies are given.
    [JsonPropertyName("FAQ")]
    public List<string> faq { get; set; }

    public RestaurantModel(string tittle, string description, string opening_hours, string location, string phone_number, string email_address, List<string> faq)
    {
        this.tittle = tittle;
        this.description = description;
        this.opening_hours = opening_hours;
        this.location = location;
        this.phone_number = phone_number;
        this.faq = faq ?? new List<string>();
    }

}
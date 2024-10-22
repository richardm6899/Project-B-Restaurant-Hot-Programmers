using System.Text.Json.Serialization;


public class AccountModel
{
    [JsonPropertyName("clientId")]
    public int Id { get; set; }

    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("fullName")]
    public string FullName { get; set; }

    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; }

    [JsonPropertyName("allergies")]
    //  list will be made even if no allergies are given.
    public List<string> Allergies { get; set; } = new List<string>();

    [JsonPropertyName("reservationId")]
    public List<int> ReservationIDs { get; set; }

    // tbh idk why this needs to be here, but it does. it doesnt work without.
    public AccountModel() { }

    public AccountModel(int id, string emailAddress, string password, string fullName, string phoneNumber, List<string> allergies, List<int> reservationsIDs)
    {
        this.Id = id;
        this.EmailAddress = emailAddress;
        this.Password = password;
        this.FullName = fullName;
        this.PhoneNumber = phoneNumber;
        this.Allergies = allergies ?? new List<string>();
        this.ReservationIDs = reservationsIDs ?? new List<int>();
    }

}
using System.Text.Json.Serialization;


public class AccountModel
{
    /*{
    "clientId": 10,
    "emailAddress": "client@test.nl",
    "password": "ClientTest123",
    "fullName": "Client Test",
    "age": 53,
    "phoneNumber": "123456789",
    "allergies": [],
    "reservationId": [
      5,
      6
    ],
    "type": "client",
    "status" : "Activated"
  }*/
    [JsonPropertyName("clientId")]
    public int Id { get; set; }

    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("fullName")]
    public string FullName { get; set; }

    [JsonPropertyName("age")]
    public int Age { get; set; }

    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; }

    [JsonPropertyName("allergies")]
    //  list will be made even if no allergies are given.
    public List<string> Allergies { get; set; } = new List<string>();

    [JsonPropertyName("reservationId")]
    public List<int> ReservationIDs { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    // tbh idk why this needs to be here, but it does. it doesnt work without.
    public AccountModel() { }


    public AccountModel(int id, string emailAddress, string password, string fullName, int age, string phoneNumber, List<string> allergies, List<int> reservationsIDs, string type)
    {
        this.Id = id;
        this.EmailAddress = emailAddress;
        this.Password = password;
        this.FullName = fullName;
        this.Age = age;
        this.PhoneNumber = phoneNumber;
        this.Allergies = allergies ?? new List<string>();
        this.ReservationIDs = reservationsIDs ?? new List<int>();
        this.Type = type;
        this.Status = "Activated";
    }

}
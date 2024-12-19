using System.Text.Json.Serialization;


public class ApplicationModel
{
    //Application Name,Name,Birthdate,gender,Email, Motivation, Cv, Status
    
    [JsonPropertyName("name")]
    public string ApplicationName { get; set; }
    [JsonPropertyName("birthdate")]
    public string Birthdate { get; set; }
    [JsonPropertyName("gender")]
    public string Gender { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("motivation")]
    public string Motivation { get; set; }
    [JsonPropertyName("cv")]
    public string Cv { get; set; }
    [JsonPropertyName("status")]
    public string Status { get; set; }

     
    public ApplicationModel(string applicationName, string birthdate, string gender, string email, string motivation, string cv, string status)
    {
        ApplicationName = applicationName;
        Birthdate = birthdate;
        Gender = gender;
        Email = email;
        Motivation = motivation;
        Cv = cv;
        Status = status;
    }
}
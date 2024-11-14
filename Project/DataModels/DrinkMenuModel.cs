using System.Text.Json.Serialization;


public class DrinkMenuModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("drinkName")]
    public string DrinkName { get; set; }

    [JsonPropertyName("price")]
    public float Price { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("type")]
    public List<string> Type { get; set;}

    [JsonPropertyName("allergies")]
    public List<string> Allergies { get; set;}

    public DrinkMenuModel(int id, string drinkName, float price, string description, List<string> type, List<string> allergies)
    {
        Id = id;
        DrinkName = drinkName;
        Price = price;
        Description = description;
        Type = type;
        Allergies = allergies;
    }

}





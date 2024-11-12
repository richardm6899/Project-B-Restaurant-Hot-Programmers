using System.Text.Json.Serialization;


public class FoodMenuModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("dishName")]
    public string DishName { get; set; }

    [JsonPropertyName("price")]
    public float Price { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("type")]
    public List<string> Type { get; set;}

    [JsonPropertyName("allergies")]
    public List<string> Allergies { get; set;}

    public FoodMenuModel(int id, string dishName, float price, string description, List<string> type, List<string> allergies)
    {
        Id = id;
        DishName = dishName;
        Price = price;
        Description = description;
        Type = type;
        Allergies = allergies;
    }

}





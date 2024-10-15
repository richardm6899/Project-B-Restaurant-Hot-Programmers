using System.Text.Json.Serialization;


public class FoodMenuModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("dishName")]
    public string DishName { get; set; }

    [JsonPropertyName("price")]
    public int Price { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set;}

    [JsonPropertyName("allergies")]
    public string Allergies { get; set;}

    public FoodMenuModel(int id, string dishName, int price, string description, string type, string allergies)
    {
        Id = id;
        DishName = dishName;
        Price = price;
        Description = description;
        Type = type;
        Allergies = allergies;
    }

}





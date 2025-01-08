using System.Text.Json;


public class RestaurantAccess :IJsonable<RestaurantModel>
{
    private string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/restaurant.json"));


    public  List<RestaurantModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<RestaurantModel>>(json);
    }


    public  void WriteAll(List<RestaurantModel> accounts)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(accounts, options);
        File.WriteAllText(path, json);
    }
}
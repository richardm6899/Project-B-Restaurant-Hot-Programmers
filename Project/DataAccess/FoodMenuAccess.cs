using System.Text.Json;

public class FoodMenuAccess :IJsonable<FoodMenuModel>
{
    private string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/foodmenu.json"));


    public  List<FoodMenuModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<FoodMenuModel>>(json);
    }


    public  void WriteAll(List<FoodMenuModel> foodMenu)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(foodMenu, options);
        File.WriteAllText(path, json);
    }



}
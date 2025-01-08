using System.Text.Json;

public class DrinkMenuAccess : IJsonable<DrinkMenuModel>
{
    private string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/drinkmenu.json"));


    public  List<DrinkMenuModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<DrinkMenuModel>>(json);
    }


    public  void WriteAll(List<DrinkMenuModel> foodMenu)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(foodMenu, options);
        File.WriteAllText(path, json);
    }



}
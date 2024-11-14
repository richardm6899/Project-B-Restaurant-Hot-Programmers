using System.Text.Json;

static class DrinkMenuAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/drinkmenu.json"));


    public static List<DrinkMenuModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<DrinkMenuModel>>(json);
    }


    public static void WriteAll(List<DrinkMenuModel> foodMenu)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(foodMenu, options);
        File.WriteAllText(path, json);
    }



}
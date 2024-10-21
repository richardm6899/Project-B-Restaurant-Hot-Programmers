using System.Text.Json;

static class FoodMenuAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/foodmenu.json"));


    public static List<FoodMenuModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<FoodMenuModel>>(json);
    }


    public static void WriteAll(List<FoodMenuModel> foodMenu)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(foodMenu, options);
        File.WriteAllText(path, json);
    }



}
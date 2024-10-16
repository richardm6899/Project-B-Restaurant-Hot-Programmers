using System.Text.Json;

static class TableAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/tables.json"));


    public static List<TableModel> LoadAllTables()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<TableModel>>(json);
    }


    public static void WriteAllTables(List<TableModel> tables)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(tables, options);
        File.WriteAllText(path, json);
    }



}
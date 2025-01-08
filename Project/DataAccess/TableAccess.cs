using System.Text.Json;

public class TableAccess : IJsonable<TableModel>
{
    private string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/tables.json"));


    public  List<TableModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<TableModel>>(json);
    }


    public void WriteAll(List<TableModel> tables)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(tables, options);
        File.WriteAllText(path, json);
    }



}
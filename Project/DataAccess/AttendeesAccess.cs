using System.Text.Json;

public class AttendeesAccess: IJsonable<AttendeesModel>
{
    private string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/attendees.json"));


    public  List<AttendeesModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<AttendeesModel>>(json);
    }


    public  void WriteAll(List<AttendeesModel> accounts)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(accounts, options);
        File.WriteAllText(path, json);
    }



}
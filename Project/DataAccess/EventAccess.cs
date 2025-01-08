using System.Text.Json;

public class EventAccess: IJsonable<EventModel>
{
    private string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/events.json"));


    public  List<EventModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<EventModel>>(json);
    }


    public  void WriteAll(List<EventModel> events)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(events, options);
        File.WriteAllText(path, json);
    }



}
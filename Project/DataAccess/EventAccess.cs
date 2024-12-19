using System.Text.Json;

static class EventAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/events.json"));


    public static List<EventModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<EventModel>>(json);
    }


    public static void WriteAll(List<EventModel> events)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(events, options);
        File.WriteAllText(path, json);
    }



}
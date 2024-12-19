using System.Text.Json;

static class ApplicationAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/application.json"));

    public static List<ApplicationModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<ApplicationModel>>(json);
    }

    public static void WriteAll(List<ApplicationModel> applications)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(applications, options);
        File.WriteAllText(path, json);
    }
}
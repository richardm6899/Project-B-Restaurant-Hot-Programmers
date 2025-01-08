using System.Text.Json;

public class ApplicationAccess : IJsonable<ApplicationModel>
{
    private string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/application.json"));

    public List<ApplicationModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<ApplicationModel>>(json);
    }

    public void WriteAll(List<ApplicationModel> applications)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(applications, options);
        File.WriteAllText(path, json);
    }
}
using System.Text.Json;
using System.IO;

static class ApplicationAccess
{
    public static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/application.json"));

    public static List<ApplicationModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<ApplicationModel>>(json);
    }

    public static void WriteAll(List<ApplicationModel> applications)
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(applications, options);

            // Debugging: Display the serialized JSON in the console
            Console.WriteLine("Serialized JSON Data:");
            Console.WriteLine(json);

            try
            {
                File.WriteAllText(path, json);
                Console.WriteLine($"JSON written successfully to: {path}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write JSON to file: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while writing to the JSON file: {ex.Message}");
        }
    }

}
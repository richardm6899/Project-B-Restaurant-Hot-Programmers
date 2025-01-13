using System.Text.Json;

public class TableAccess : IJsonable<TableModel>
{
    private string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/tables.json"));

    public List<TableModel> LoadAll()
    {
        try
        {
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<TableModel>>(json);
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as needed
            Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
            return null;
        }
    }

    public void WriteAll(List<TableModel> applications)
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(applications, options);

            try
            {
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occurred.");
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while writing to the JSON file: {ex.Message}");
        }
    }



}
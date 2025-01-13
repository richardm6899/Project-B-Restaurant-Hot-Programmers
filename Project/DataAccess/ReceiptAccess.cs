using System.Text.Json;

public class ReceiptAccess :IJsonable<ReceiptModel>
{
    private string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/receipts.json"));


   public List<ReceiptModel> LoadAll()
    {
        try
        {
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<ReceiptModel>>(json);
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as needed
            Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
            return null;
        }
    }

    public void WriteAll(List<ReceiptModel> applications)
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
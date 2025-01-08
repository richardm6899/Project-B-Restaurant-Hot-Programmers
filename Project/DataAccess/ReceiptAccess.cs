using System.Text.Json;

public class ReceiptAccess :IJsonable<ReceiptModel>
{
    private string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/receipts.json"));


    public  List<ReceiptModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<ReceiptModel>>(json);
    }


    public  void WriteAll(List<ReceiptModel> receiptModels)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(receiptModels, options);
        File.WriteAllText(path, json);
    }



}
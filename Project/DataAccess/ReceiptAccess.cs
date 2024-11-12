using System.Text.Json;

static class ReceiptAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/receipts.json"));


    public static List<ReceiptModel> LoadAllReceipts()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<ReceiptModel>>(json);
    }


    public static void WriteAllReceipts(List<ReceiptModel> receiptModels)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(receiptModels, options);
        File.WriteAllText(path, json);
    }



}
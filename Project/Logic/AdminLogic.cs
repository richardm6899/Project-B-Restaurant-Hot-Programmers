public class AdminLogic : AccountsLogic
{

    /*{
    "clientId": 7,
    "emailAddress": "test@admin.nl",
    "password": "Test123!",
    "fullName": "Test Test",
    "phoneNumber": "19407399",
    "allergies": [],
    "type": "admin"
  }*/
    private string Name;
    private string Email;
    private string Password;
    private string PhoneNumber;
    private string Type = "admin";
    private DateTime Birthdate;
    private new DateTime LastLogin = DateTime.MinValue;

    public AdminLogic(string name, string email, string password, string phoneNumber, DateTime birthdate)
    {
        this.Name = name;
        this.Email = email;
        this.Password = password;
        this.PhoneNumber = phoneNumber;
        this.Birthdate = birthdate;
    }


    // public static string CreateAccount(string fullName, string email, string password, string phoneNumber, int age, List<string> allergies, string type, bool locked, int failedloginattempts, DateTime lastlogin)
    public void CreateAdmin()
    {
        AccountsLogic.CreateAccount(this.Name, this.Email, this.Password, this.PhoneNumber, this.Birthdate, new List<string> (), this.Type, this.LastLogin);
    }


    public override AccountModel? GetById(int ID)
    {
        return base.GetById(ID);
    }

    public List<AccountModel> GetAccountsByType(string type)
    {
        List<AccountModel> accounts = base.GetAccounts();
        List<AccountModel> returnAccounts = new();

        foreach (AccountModel account in accounts)
        {
            if (account.Type == type)
            {
                returnAccounts.Add(account);
            }
        }
        return returnAccounts;

    }
    public List<AccountModel> GetActivatedAccounts()
    {
        List<AccountModel> activatedAccounts = new();
        List<AccountModel> allAccounts = base.GetAccounts();
        foreach (AccountModel account in allAccounts)
        {
            if (account.Status == "Activated")
            {
                activatedAccounts.Add(account);
            }
        }
        return activatedAccounts;
    }
}
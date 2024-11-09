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

    public AdminLogic(string name, string email, string password, string phoneNumber)
    {
        this.Name = name;
        this.Email = email;
        this.Password = password;
        this.PhoneNumber = phoneNumber;
    }


    //     public static string CreateAccount(string fullName, string email, string password, string phoneNumber, List<string> allergies, string type)
    public void CreateAdmin()
    {
        AccountsLogic.CreateAccount(this.Name, this.Email, this.Password, this.PhoneNumber, default, this.Type);
    }

    public override AccountModel GetById(int ID)
    {
        return base.GetById(ID);
    }

    public List<AccountModel> GetAccounts(string type)
    {
        List<AccountModel> accounts = base.GetAccounts();
        List<AccountModel> returnAccounts = new();

        foreach(AccountModel account in accounts)
        {
            if (account.Type == type)
            {
                returnAccounts.Add(account);
            }
        }
        return returnAccounts;
        
    }
}
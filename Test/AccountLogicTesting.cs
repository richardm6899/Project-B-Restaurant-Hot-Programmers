namespace Testing;

[TestClass]
public class TestAccountLogic
{
    [TestMethod]
    [DataRow(null, "password")]
    [DataRow("email", null)]
    [DataRow(null, null)]
    public void CheckLogin_NullValues_ReturnNull(string email, string password)
    {
        AccountsLogic ac = new AccountsLogic();
        AccountModel actual = ac.CheckLogin(email, password);
        Assert.IsNull(actual);
    }


    // check make account
    // string fullName, string email, string password, string phoneNumber, List<string>allergies
    // [TestMethod]
    // [DataRow("Nick Wilde", "guvr.com", "Fijn45!!b", "98462947", new string[] { "apple", "peanut" })]
    // public void CreateAccount_Test_Wrong_email(string fullName, string email, string password, string phoneNumber, List<string> allergies)
    // {
    //     Assert.IsFalse(AccountsLogic.CreateAccount(fullName, email, password, phoneNumber, allergies));
    // }

    // check correct email check
    [TestMethod]
    [DataRow("ubi@nvgfr.nl")]
    public void CheckEmail_True(string email)
    {
        Assert.IsTrue(AccountsLogic.CheckCreateEmail(email));
    }


    //  check wrong email
    [TestMethod]
    [DataRow("fue@fe")]
    [DataRow("guvr.com")]

    public void CheckEmail_False(string email)
    {
        Assert.IsFalse(AccountsLogic.CheckCreateEmail(email));
    }




    [TestMethod]
    public void CheckEmailInJson_Already_used_true()
    {
        AccountsLogic ac = new AccountsLogic();
        Assert.IsTrue(ac.CheckEmailInJson("t@b.nl"));
    }

    [TestMethod]
    public void CheckEmailInJson_Not_Used_False()
    {
        AccountsLogic ac = new AccountsLogic();
        Assert.IsFalse(ac.CheckEmailInJson("febh@egr.nl"));
    }

    [TestMethod]
    [DataRow("neineho")]
    [DataRow("bfeuF")]
    [DataRow("YGodoe")]
    public void CheckCreatePassWord_IncorrectPassword(string password)
    {
        Assert.AreNotEqual("Password has been set", AccountsLogic.CheckCreatePassword(password));
    }



    [TestMethod]
    [DataRow("FYbui5uheov")]
    [DataRow("vyi5CTY@vnio")]
    [DataRow("YV5bi!vnipe")]
    public void CheckCreatePassword_CorrectPass(string password)
    {
        Assert.AreEqual("Password has been set", AccountsLogic.CheckCreatePassword(password));
    }


    public AccountsLogic _accountsLogic;

    [TestInitialize]
    public void Initialize()
    {
        _accountsLogic = new AccountsLogic();
    }


    [TestMethod]
    // Compares if two accounts are equal, which are first added in a list through UpdateList().
    public void UpdateList_ExistingAccount_UpdateSuccessful()
    {
        // int id, string emailAddress, string password, string fullName, string phoneNumber, List<string> allergies, List<int> reservationsIDs)
        // Arrange
        var existingAccount = new AccountModel(1, "test@example.com", "password", "John Doe", default, "1234567890", ["Dough"], [3], "client", false, 0, DateTime.Now);
        _accountsLogic.UpdateList(existingAccount);

        var updatedAccount = new AccountModel(1, "updated@example.com", "newpassword", "John Doe", default, "0987654321", ["Dough"], [3], "client", false, 0, DateTime.Now);

        // Act
        _accountsLogic.UpdateList(updatedAccount);
        // Assert
        var updatedAccountInList = _accountsLogic.GetById(1);
        Assert.AreEqual(updatedAccount, updatedAccountInList);
        Assert.AreEqual(updatedAccount, updatedAccountInList);
    }
    [TestMethod]
    // Returns true if the new account is added to the list
    public void UpdateList_NewAccount_AddedToAccounts()
    {
        // Arrange
        var newAccount = new AccountModel(2, "richard@example.com", "richardpassword", "Richard Morris", default, "0653269420", ["spicy food"], [], "client", false, 0, DateTime.Now);

        // Act
        _accountsLogic.UpdateList(newAccount);

        // Assert
        Assert.IsTrue(_accountsLogic.GetAccounts().Contains(newAccount));
    }
    [TestMethod]
    // Checks if an account exists with Id = 2
    public void GetById_ExistingAccount_ReturnsAccount()
    {
        // Arrange
        var existingAccount = _accountsLogic.GetById(2);

        // Act

        // Assert
        Assert.IsNotNull(existingAccount);
    }
    [TestMethod]
    // Checks if an account doesn't exist with Id = 0
    public void GetById_NonExistingAccount_ReturnsNull()
    {
        // Act
        var account = _accountsLogic.GetById(0);

        // Assert
        Assert.IsNull(account);
    }
    [TestMethod]
    // Checks the account that attempts to login exists and if the account meets the requirements
    public void CheckLogin_ValidCredentials_ReturnsAccount()
    {
        // Arrange
        var account = new AccountModel(2, "richard@example.com", "richardpassword", "Richard Morris", default, "0653269420", ["spicy food"], [], "client", false, 0, DateTime.Now);
        _accountsLogic.UpdateList(account);

        // Act
        var result = _accountsLogic.CheckLogin(account.EmailAddress, account.Password);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(account.EmailAddress, result.EmailAddress);
        Assert.AreEqual(account.Password, result.Password);
    }
    [TestMethod]
    // Checks if the account that attempts to login doesn't exist
    public void CheckLogin_InvalidCredentials_ReturnsNull()
    {
        // Act
        var result = _accountsLogic.CheckLogin("invalid@example.com", "wrongpassword");

        // Assert
        Assert.IsNull(result);
    }
}
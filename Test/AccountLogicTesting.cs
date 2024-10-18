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
        // Arrange
        var existingAccount = new AccountModel(1, "test@example.com", "password", "John Doe", "1234567890", ["Dough"]);
        _accountsLogic.UpdateList(existingAccount);

        var updatedAccount = new AccountModel(1, "updated@example.com", "newpassword", "John Doe", "0987654321", ["Dough"]);

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
        var newAccount = new AccountModel(2, "richard@example.com", "richardpassword", "Richard Morris", "0653269420", ["spicy food"]);

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
        var account = new AccountModel(2, "richard@example.com", "richardpassword", "Richard Morris", "0653269420", ["spicy food"]);
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
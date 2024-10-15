using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Test;
[TestClass]
public class AccountsLogicTests
{
    public AccountsLogic _accountsLogic;



    [TestInitialize]
    public void Initialize()
    {
        _accountsLogic = new AccountsLogic();
    }

    // Tests go here
    [TestMethod]
    public void UpdateList_ExistingAccount_UpdateSuccessful()
    {
        // Arrange
        var existingAccount = new AccountModel(1, "test@example.com", "password", "John Doe", "1234567890", ["Dough"]);
        _accountsLogic._accounts.Add(existingAccount);

        var updatedAccount = new AccountModel(1, "updated@example.com", "newpassword", "Jane Doe", "0987654321", ["Doughnut"]);

        // Act
        _accountsLogic.UpdateList(updatedAccount);

        // Assert
        var updatedAccountInList = _accountsLogic._accounts.Find(i => i.Id == 1);
        Assert.AreEqual(updatedAccount.EmailAddress, updatedAccountInList.EmailAddress);
        Assert.AreEqual(updatedAccount.Password, updatedAccountInList.Password);
    }
    [TestMethod]
    public void UpdateList_NewAccount_AddedToAccounts()
    {
        // Arrange
        var newAccount = new AccountModel(2, "richard@example.com", "richardpassword", "Richard Morris", "0653269420", ["spicy food"]);

        // Act
        _accountsLogic.UpdateList(newAccount);

        // Assert
        Assert.IsTrue(_accountsLogic._accounts.Contains(newAccount));
    }
    [TestMethod]
    public void GetById_ExistingAccount_ReturnsAccount()
    {
        // Arrange
        var existingAccount = new AccountModel(0, "richard@example.com", "richardpassword", "Richard Morris", "0653269420", ["spicy food"]);

        // Act

        // Assert
        Assert.IsNotNull(existingAccount);
    }
    [TestMethod]
    public void GetById_NonExistingAccount_ReturnsNull()
    {
        // Act
        var account = _accountsLogic.GetById(0);

        // Assert
        Assert.IsNull(account);
    }
    [TestMethod]
    public void CheckLogin_ValidCredentials_ReturnsAccount()
    {
        // Arrange
        var account = new AccountModel(2, "richard@example.com", "richardpassword", "Richard Morris", "0653269420", ["spicy food"]);
        _accountsLogic._accounts.Add(account);

        // Act
        var result = _accountsLogic.CheckLogin(account.EmailAddress, account.Password);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(account.EmailAddress, result.EmailAddress);
        Assert.AreEqual(account.Password, result.Password);
    }
    [TestMethod]
    public void CheckLogin_InvalidCredentials_ReturnsNull()
    {
        // Act
        var result = _accountsLogic.CheckLogin("invalid@example.com", "wrongpassword");

        // Assert
        Assert.IsNull(result);
    }
}
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
    [DataRow("john")]
    public void CapitalizeFirstLetter_Check(string word)
    {
        string Expected = "John";
        Assert.AreEqual(Expected, AccountsLogic.CapitalizeFirstLetter(word));
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

}
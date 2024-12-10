namespace Testing;

[TestClass]
public class HelperLogicTesting
{
    [TestMethod]
    [DataRow("12345678")]
    [DataRow("0987654")]
    public void CheckIfDigit_IsDigit_True(string number)
    {
        Assert.IsTrue(HelperLogic.CheckIfStringIsInt(number));
    }

    [TestMethod]
    [DataRow("3456word")]
    [DataRow("0987456word654")]
    [DataRow("word")]
    [DataRow("!!!!!")]
    public void CheckIfStringIsInt_IsNotDigit_False(string notNumber)
    {
        System.Console.WriteLine(HelperLogic.CheckIfStringIsInt(notNumber));
        Assert.IsFalse(HelperLogic.CheckIfStringIsInt(notNumber));
    }

    [TestMethod]
    public void CheckIfNull_IsNull_True()
    {
        string? stringToCheck = null;
        Assert.IsTrue(HelperLogic.CheckIfNull(stringToCheck));
        List<string>? listToCheck = null;
        Assert.IsTrue(HelperLogic.CheckIfNull(listToCheck));
    }

    [TestMethod]
    public void CheckIfNull_IsNotNull_False()
    {
        string stringToCheck = "Word";
        Assert.IsFalse(HelperLogic.CheckIfNull(stringToCheck));
        List<string> listToCheck = new();
        Assert.IsFalse(HelperLogic.CheckIfNull(listToCheck));
        int intToCheck = 1234;
        Assert.IsFalse(HelperLogic.CheckIfNull(intToCheck));
    }

    [TestMethod]
    [DataRow("12345", 12345)]
    [DataRow("123678", 123678)]
    [DataRow("98765", 98765)]

    public void ConvertStringToInt_IsInt_GivenStringToInt(string toConvert, int expected)
    {
        Assert.AreEqual(expected, HelperLogic.ConvertStringToInt(toConvert));
    }

    [TestMethod]
    [DataRow("wert")]
    [DataRow("2345wer")]
    [DataRow("words")]

    public void ConvertStringToInt_IsNotInt_Zero(string toConvert)
    {
        Assert.AreEqual(0, HelperLogic.ConvertStringToInt(toConvert));
    }

    [TestMethod]
    [DataRow("john", "John")]
    [DataRow("smith", "Smith")]
    public void CapitalizeFirstLetter_Check(string word, string expected)
    {
        Assert.AreEqual(expected, HelperLogic.CapitalizeFirstLetter(word));
    }

    [TestMethod]
    [DataRow("word@school.nl")]
    [DataRow("Happy@life.com")]
    [DataRow("Beep@boop.hr")]
    [DataRow("Slip@borp.yeah")]
    public void IsValidEmail_ValidEmail_True(string email)
    {
        Assert.IsTrue(HelperLogic.IsValidEmail(email));
    }

    [TestMethod]
    [DataRow("word@school")]
    [DataRow("HappyLife.com")]
    [DataRow("Beep@BoomHr")]
    [DataRow("SlipBopYeah")]
    public void IsValidEmail_InValidEmail_False(string email)
    {
        Assert.IsFalse(HelperLogic.IsValidEmail(email));
    }


    // [TestMethod]
    // public void DateTimeToReadableDate_HappyFlow()
    // {
    //     DateTime testDate =  new DateTime(2024, 11, 24);

    //     Assert.AreEqual("24 November, 2024",HelperLogic.DateTimeToReadableDate(testDate)); 
    // }


    /*[TestMethod]
    [DataRow(null, "password")]
    [DataRow("email", null)]
    [DataRow(null, null)]
    public void CheckLogin_NullValues_ReturnNull(string email, string password)
    {
        AccountsLogic ac = new AccountsLogic();
        AccountModel actual = ac.CheckLogin(email, password);
        Assert.IsNull(actual);
    }*/

}
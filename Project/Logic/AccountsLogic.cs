using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


//This class is not static so later on we can use inheritance and interfaces
public class AccountsLogic
{
    public  List<AccountModel> _accounts;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    static public AccountModel? CurrentAccount { get; private set; }

    public AccountsLogic()
    {
        _accounts = AccountsAccess.LoadAll();
    }


    public void UpdateList(AccountModel acc)
    {
        //Find if there is already an model with the same id
        int index = _accounts.FindIndex(s => s.Id == acc.Id);

        if (index != -1)
        {
            //update existing model
            _accounts[index] = acc;
        }
        else
        {
            //add new model
            _accounts.Add(acc);
        }
        AccountsAccess.WriteAll(_accounts);

    }

    public AccountModel GetById(int id)
    {
        return _accounts.Find(i => i.Id == id);
    }

    public AccountModel CheckLogin(string email, string password)
    {
        if (email == null || password == null)
        {
            return null;
        }
        CurrentAccount = _accounts.Find(i => i.EmailAddress == email && i.Password == password);
        return CurrentAccount;
    }

    public List<AccountModel> GetAccounts()
    {
        return _accounts;
    }
    // if email already used returns true else false
    public bool CheckEmailInJson(string email)
    {
        foreach (AccountModel account in _accounts)
        {
            if (account.EmailAddress == email)
            {
                return true;
            }
        }
        return false;
    }

    public static string CreateAccount(string fullName, string email, string password, string phoneNumber, int age, List<string> allergies)
    {
        int newID = AccountsAccess.LoadAll().Count + 1;
        AccountModel account = new(newID, email, password, fullName, age, phoneNumber, allergies, default);
        AccountsLogic ac = new AccountsLogic();
        ac.UpdateList(account);
        if (ac.GetById(newID) == null)
        {
            return "Something went wrong. :(";
        }

        else return "Account made";
    }

    public static bool CheckCreateEmail(string email)
    {
        // check if email contains; @ and (.com or .nl or .hr)
        if (email.Contains("@") & (email.Contains(".com") || email.Contains(".nl") || email.Contains(".hr")))
        {
            return true;
        }
        return false;
    }


    //  CheckPassWord: Checks if password strong
    // must contain at least 8 characters
    // must contain at least one capital letter
    // must contain number or symbol
    public static string CheckCreatePassword(string password)
    {
        if (password.Count() < 8)
        {
            return "Password not long enough, must contain 8 characters";
        }
        if (!password.Any(char.IsUpper))
        {
            return "Password must contain an Uppercase Letter.";
        }
        if (!password.Any(char.IsSymbol) && !password.Any(char.IsNumber))
        {
            return "Passwrord must contain a Number or Symbol";
        }
        else return "Password has been set";

    }

    public static string CapitalizeFirstLetter(string toCapitalize) => char.ToUpper(toCapitalize[0]) + toCapitalize.Substring(1);

    public void ChangeName(int id, string newFullName)
{
    AccountModel account = GetById(id);
    if (account != null)
    {
        account.FullName = newFullName;
        UpdateList(account);
    }
}
    public void ChangeAge(int id, int age)
{
    AccountModel account = GetById(id);
    if (account != null)
    {
        account.Age = age;
        UpdateList(account);
    }
}
    public void ChangeAllergies(int id, List<string> newAllergies)
{
    AccountModel account = GetById(id);
    if (account != null)
    {
        account.Allergies = newAllergies;
        UpdateList(account);
    }
}
    public string ChangePassword(int id, string oldPassword, string newPassword)
{
    AccountModel account = GetById(id);
    if (account != null && account.Password == oldPassword)
    {
        string passwordCheck = CheckCreatePassword(newPassword);
        if (passwordCheck == "Password has been set")
        {
            account.Password = newPassword;
            UpdateList(account);
            return "Password changed successfully";
        }
        else
        {
            return passwordCheck;
        }
    }
    else
    {
        return "Invalid old password";
    }
}
    public string ChangeEmail(int id, string newEmail)
{
    AccountModel account = GetById(id);
    if (account != null)
    {
        if (!CheckEmailInJson(newEmail))
        {
            account.EmailAddress = newEmail;
            UpdateList(account);
            return "Email changed successfully";
        }
        else
        {
            return "Email already exists";
        }
    }
    else
    {
        return "Account not found";
    }
}
    public string UserInfo(int id)
{
    AccountModel account = GetById(id);
    if (account != null)
    {
        return $"Your accountsdata:\nName: {account.FullName}\nEmail: {account.EmailAddress}\nPhone Number: {account.PhoneNumber}\nAllergies: {string.Join(", ", account.Allergies)}";
    }
    else
    {
        return "Account not found";
    }
}
}

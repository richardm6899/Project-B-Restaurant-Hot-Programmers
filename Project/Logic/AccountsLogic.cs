using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


//This class is not static so later on we can use inheritance and interfaces
public class AccountsLogic
{
    private List<AccountModel> _accounts;

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

    public static AccountModel CreateAccount(string fullName, string email, string password, string phoneNumber, List<string> allergies)
    {
        int newID = AccountsAccess.LoadAll().Count + 1;
        AccountModel account = new(newID, email, password, fullName, phoneNumber, allergies);

        return account;
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
}





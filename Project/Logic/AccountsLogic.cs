using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


//This class is not static so later on we can use inheritance and interfaces
public class AccountsLogic
{
    public List<AccountModel> _accounts;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    static public AccountModel? CurrentAccount { get; private set; }

    public int FailedLoginAttempts = 0;

    public bool Locked = false;

    public DateTime LastLogin = DateTime.Now;


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

    public virtual AccountModel GetById(int id)
    {
        return _accounts.Find(i => i.Id == id);
    }

    public virtual AccountModel GetByEmail(string email)
    {
        return _accounts.Find(i => i.EmailAddress == email);
    }

    public AccountModel CheckLogin(string email, string password)
    {
        if (email == null || password == null)
        {
            return null;
        }
        CurrentAccount = _accounts.Find(i => i.EmailAddress == email && i.Password == password && i.Status == "Activated");
        if (CurrentAccount != null && (CurrentAccount.Locked || Locked))
        {
            return null;
        }
        return CurrentAccount;
    }

    // check if given email is connected to a deactivated account, return true if found
    public bool CheckAccountDeactivated(string email)
    {
        foreach (AccountModel account in _accounts)
        {
            if (account.EmailAddress == email && account.Status == "Deactivated")
            {
                return true;
            }
        }
        return false;
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

    public static string CreateAccount(string fullName, string email, string password, string phoneNumber, DateTime birthdate, List<string> allergies, string type, DateTime LastLogin)
    {
        int newID = AccountsAccess.LoadAll().Count + 1;
        AccountModel account = new(newID, email, password, fullName, age, phoneNumber, allergies, default, type, false, 0, LastLogin);
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
        string passMessage = "Password ";
        bool isValid = true;

        if (password.Length < 8)
        {
            passMessage += "is not long enough, must contain at least 8 characters. ";
            isValid = false;
        }
        if (!password.Any(char.IsUpper))
        {
            passMessage += "must contain at least one uppercase letter. ";
            isValid = false;
        }
        if (!password.Any(char.IsLower))
        {
            passMessage += "must contain at least one lowercase letter. ";
            isValid = false;
        }
        // a lambda is used so that both classifications are checked. ch => .....
        if (!password.Any(char.IsDigit) && !password.Any(ch => char.IsSymbol(ch) || char.IsPunctuation(ch)))
        {
            passMessage += "must contain at least one number or symbol. ";
            isValid = false;
        }

        if (isValid)
        {
            passMessage = "Password has been set.";
        }

        return passMessage;
    }

    public string ChangeName(int id, string newFullName)
    {

        AccountModel account = GetById(id);
        if (account != null)
        {
            account.FullName = newFullName;
            UpdateList(account);
            return "Name changed successfully";
        }
        else
        {
            return "Account not found";
        }
    }

    // public string ChangeAge(int id, int age)
    // {
    //     if (age < 18 || age > 150)
    //     {
    //         return "Age must be between 18 and 150";
    //     }

    //     AccountModel account = GetById(id);
    //     if (account != null)
    //     {
    //         account.Age = age;
    //         UpdateList(account);
    //         return "Age changed successfully";
    //     }
    //     else
    //     {
    //         return "Account not found";
    //     }
    // }

    public string ChangeAllergies(int id, List<string> newAllergies)
    {
        if (newAllergies == null || newAllergies.Count == 0)
        {
            return "Allergies list is required";
        }

        AccountModel account = GetById(id);
        if (account != null)
        {
            account.Allergies = newAllergies;
            UpdateList(account);
            return "Allergies changed successfully";
        }
        else
        {
            return "Account not found";
        }
    }

    public string ChangePassword(int id, string oldPassword, string newPassword)
    {
        if (string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword))
        {
            return "Old password and new password are required";
        }

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
        if (string.IsNullOrWhiteSpace(newEmail))
        {
            return "Email is required";
        }

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
            return $"Your accounts data:\nName: {account.FullName}\nEmail: {account.EmailAddress}\nPhone Number: {account.PhoneNumber}\nAllergies: {string.Join(", ", account.Allergies)}";
        }
        else
        {
            return "Account not found";
        }
    }

    public bool deactivateAccount(int clientid)
    {
        AccountModel account = GetById(clientid);
        if (account == null)
        {
            return false;
        }

        account.Status = "Deactivated";
        UpdateList(account);
        if (account.Status == "Deactivated") { return true; }


        return false;
    }

    public bool ActivateAccount(string email)
    {
        AccountModel account = GetByEmail(email);
        if (account == null)
        {
            return false;
        }

        account.Status = "Activated";
        UpdateList(account);
        if (account.Status == "Activated") { return true; }


        return false;
    }

    public bool deleteAccount(int clientid)
    {
        AccountModel account = GetById(clientid);
        if (account == null)
        {
            return false;
        }
        // changing
        account.EmailAddress = null;
        account.Password = null;
        account.FullName = null;
        account.Birthdate = default;
        account.PhoneNumber = null;
        account.Allergies = null;
        account.ReservationIDs = null;
        account.Type = null;
        account.Status = "Deleted";

        UpdateList(account);

        if (account.Status == "Deleted")
        { return true; }

        return false;
    }

    public bool ReCheckPassWord(AccountModel acc, string passToCheck)
    {
        if (acc.Password == passToCheck)
        {
            return true;
        }
        return false;
    }

    // Menu.cs Console.clear() is working weird, so its commented so that user can see how he attempts to log in
    // Returns true or false if account is null or not null. True meaning its Locked, so account won't be able to log in
    public bool CancelLogin(AccountModel logged_in_account, string email)
    {
        
        if (logged_in_account != null)
        {
            if(logged_in_account.Locked == false)
            {
                
                logged_in_account.FailedLoginAttempts = 0;
                logged_in_account.Locked = false;
                UpdateList(logged_in_account);
                return logged_in_account.Locked; // false
            }
            else
            {
                UpdateList(logged_in_account);
                return logged_in_account.Locked; // true
            }
        }
         
        else // logged_in_account is null
        {
            AccountModel? accountfound = _accounts.Find(a => a.EmailAddress == email && i.Status == "Activated");
            if (accountfound != null) // there is an existing account with the email
            {
                if(accountfound.Locked == false)
                {
                    accountfound.FailedLoginAttempts++;
                    if (accountfound.FailedLoginAttempts >= 3)
                    {
                        accountfound.Locked = true;
                        accountfound.LastLogin = DateTime.Now;
                        UpdateList(accountfound);
                        return accountfound.Locked; // true
                    }
                    else
                    {
                        UpdateList(accountfound);
                        return accountfound.Locked; // false
                    }
                }
                else
                {
                        if (CalculateRemainingSeconds(accountfound, email) <= 0)
                        {
                        accountfound.Locked = false;
                        accountfound.LastLogin = DateTime.Now;
                        UpdateList(accountfound);
                        return accountfound.Locked; //false
                        }
                        else
                        {
                            return accountfound.Locked;// true
                        }
                }
            }
            else // accountfound is null
            {
                if (Locked == false)
                {
                    FailedLoginAttempts++;
                    if (FailedLoginAttempts >= 3)
                    {
                        Locked = true;
                        LastLogin = DateTime.Now;
                        return Locked; // true
                    }
                    else
                    {
                        return Locked; // false
                    }
                }
                else
                {
                    return Locked; // true
                }
            }
        }
    }
    // Returns remaining seconds starting from 30. When reached to 0, user can log in
    public int CalculateRemainingSeconds(AccountModel account, string email)
    {
        int remainingSeconds = 0;
        if (account != null)
        {
        
        TimeSpan timeSinceLock = DateTime.Now - account.LastLogin;
        remainingSeconds = 30 - (int)timeSinceLock.TotalSeconds;
            if (account.Locked)
            {
                if (remainingSeconds <= 0)
                {
                    account.FailedLoginAttempts = 0;
                    account.Locked = false;
                    remainingSeconds = 0;
                    UpdateList(account);
                    return remainingSeconds;
                }
                else
                {
                    return remainingSeconds;
                }
            }

            return remainingSeconds;

        }
        else // acount is null
        {
        AccountModel? accountfound = _accounts.Find(a => a.EmailAddress == email);
        if (accountfound != null)
        {
        TimeSpan timeSinceLock = DateTime.Now - accountfound.LastLogin;
        remainingSeconds = 30 - (int)timeSinceLock.TotalSeconds;
            if (Locked)
                {
                    if (remainingSeconds <= 0)
                    {
                        accountfound.FailedLoginAttempts = 0;
                        accountfound.Locked = false;
                        remainingSeconds = 0;
                        return remainingSeconds;
                    }
                    else
                    {
                        return remainingSeconds;
                    }
                }
                return remainingSeconds;
        }
        else
        {
        TimeSpan timeSinceLock = DateTime.Now - LastLogin;
        remainingSeconds = 30 - (int)timeSinceLock.TotalSeconds;
            if (Locked)
                {
                    if (remainingSeconds <= 0)
                    {
                        FailedLoginAttempts = 0;
                        Locked = false;
                        remainingSeconds = 0;
                        return remainingSeconds;
                    }
                    else
                    {
                        return remainingSeconds;
                    }
                }
                return remainingSeconds;
        }
        }
    }
  
    public DateTime GetBirthday()
    {
        // Array of months
        string[] months = {
        "January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December"
    };

        // Years between 1900 and 2007
        int startYear = 1900;
        int endYear = 2007;
        int[] years = new int[endYear - startYear + 1];
        for (int i = 0; i < years.Length; i++)
        {
            years[i] = startYear + i;
        }

        // Days array
        int[] days = new int[31];
        for (int i = 0; i < days.Length; i++)
        {
            days[i] = i + 1;
        }

        // Year selection (10 columns)
        int selectedYear = years[NavigateBirthdayGrid(years, 10, "Select your birth year:")];

        // Month selection (4 columns)
        int selectedMonth = NavigateBirthdayGrid(months, 4, $"{selectedYear} \nSelect your birth month:") + 1;

        // Day selection (7 columns)
        int maxDays = GetDaysInMonth(selectedMonth, selectedYear);
        int selectedDay = days[NavigateBirthdayGrid(days, 7, $"{selectedYear} {months[selectedMonth - 1]}\nSelect your birth day:", maxDays)];

        return new DateTime(selectedYear, selectedMonth, selectedDay);
    }


    private static int NavigateBirthdayGrid<T>(T[] options, int columns, string prompt, int limit = 0)
    {
        int currentIndex = 0;
        limit = (limit == 0 || limit > options.Length) ? options.Length : limit; // Limit the options
        int rows = (limit + columns - 1) / columns; // Calculate number of rows

        while (true)
        {
            Console.Clear();
            System.Console.WriteLine(prompt);
            // Print grid
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    int index = r * columns + c;
                    if (index < limit)
                    {
                        if (index == currentIndex)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            // Adjust width for alignment
                            Console.Write($"{options[index],-10}");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write($"{options[index],-10}");
                        }
                    }
                }
                Console.WriteLine();
            }

            // move highlighted year, month, day
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    currentIndex = (currentIndex - columns + limit) % limit;
                    break;
                case ConsoleKey.DownArrow:
                    currentIndex = (currentIndex + columns) % limit;
                    break;
                case ConsoleKey.LeftArrow:
                    currentIndex = (currentIndex - 1 + limit) % limit;
                    break;
                case ConsoleKey.RightArrow:
                    currentIndex = (currentIndex + 1) % limit;
                    break;
                case ConsoleKey.Enter:
                    return currentIndex;
            }
        }
    }

    // Method to get the days in a month
    private static int GetDaysInMonth(int month, int year)
    {
        return month switch
        {
            1 => 31,
            // check if leap year
            2 => HelperLogic.IsLeapYear(year) ? 29 : 28,
            3 => 31,
            4 => 30,
            5 => 31,
            6 => 30,
            7 => 31,
            8 => 31,
            9 => 30,
            10 => 31,
            11 => 30,
            12 => 31,
            _ => 31,
        };
    }
}
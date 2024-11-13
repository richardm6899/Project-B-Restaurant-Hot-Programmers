using System.ComponentModel;

public class ModifyData
{
    // static private AccountsLogic accountsLogic;

    public static void Start(AccountModel account, AccountsLogic accountsLogic)
    {
        Console.WriteLine("Enter 1 to change name");
        Console.WriteLine("Enter 2 to change age");
        Console.WriteLine("Enter 3 to change allergies");
        Console.WriteLine("Enter 4 to change password");
        Console.WriteLine("Enter 5 to change email");
        Console.WriteLine("Enter 6 to view user info");

        string user_answer = Console.ReadLine();

        switch (user_answer)
        {
            case "1":
                bool valid_name = false;
                while (valid_name == false)
                {
                    Console.Write("Enter new name: ");
                    string newName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newName) && !newName.Any(char.IsDigit) && newName.Any(char.IsLetter))
                    {
                        string nameChange = accountsLogic.ChangeName(account.Id, newName);
                        Console.WriteLine(nameChange);
                        valid_name = true;
                    }
                    else
                    {
                        Console.WriteLine("Must only contain letters");
                    }
                }
                break;
            case "2":
                bool valid_age = false;
                while (valid_age == false)
                {
                    Console.Write("Enter new age: ");
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out int newAge))
                    {
                        string ageChange = accountsLogic.ChangeAge(account.Id, newAge);
                        if (newAge >= 18 && newAge < 151)
                        {
                            Console.WriteLine(ageChange);
                            valid_age = true;
                        }
                        else
                        {
                            Console.WriteLine("Age must be between 18 and 150");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Age must be a number");
                    }
                }
                break;
            case "3":
                bool valid_allergies = false;
                while (valid_allergies == false)
                {
                    Console.Write("Enter new allergies (separated by commas): ");
                    string newAllergies = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newAllergies))
                    {
                        string allergiesChange = accountsLogic.ChangeAllergies(account.Id, newAllergies.Split(',').ToList());
                        Console.WriteLine(allergiesChange);
                        valid_allergies = true;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid allergy");
                    }
                }
                break;
            case "4":
                bool valid_password = false;
                while (valid_password == false)
                {
                    Console.Write("Enter old password: ");
                    string oldPassword = Console.ReadLine();
                    Console.Write("Enter new password: ");
                    string newPassword = Console.ReadLine();
                    if (oldPassword == account.Password)
                    {
                        string passwordChange = accountsLogic.ChangePassword(account.Id, oldPassword, newPassword);
                        Console.WriteLine(passwordChange);
                        if (passwordChange == "Password changed successfully")
                        {
                            valid_password = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid password");
                    }
                }
                break;
            case "5":
                bool valid_email = false;
                while (valid_email == false)
                {
                    Console.Write("Enter new email: ");
                    string newEmail = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newEmail) && newEmail.Contains("@."))
                    {
                        string emailChange = accountsLogic.ChangeEmail(account.Id, newEmail);
                        Console.WriteLine(emailChange);
                        valid_email = true;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid email address, must contain @");
                    }
                }
                break;
            case "6":
                string userInfo = accountsLogic.UserInfo(account.Id);
                Console.WriteLine(userInfo);
                break;
        }
    }
}
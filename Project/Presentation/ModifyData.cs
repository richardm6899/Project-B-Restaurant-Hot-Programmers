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
                Console.Write("Enter new name: ");
                string newName = Console.ReadLine();
                accountsLogic.ChangeName(account.Id, newName);
                Console.WriteLine($"Name changed to {account.FullName}");
                break;
            case "2":
                Console.Write("Enter new age: ");
                int newAge = int.Parse(Console.ReadLine());
                if(newAge < 18 || newAge > 150)
                {
                Console.WriteLine("Age must be between 18 and 150");
                }
                else
                {
                accountsLogic.ChangeAge(account.Id, newAge);
                Console.WriteLine($"Age changed to {account.Age}");
                }
                break;
            case "3":
                Console.Write("Enter new allergies (comma separated): ");
                string newAllergies = Console.ReadLine();
                accountsLogic.ChangeAllergies(account.Id, newAllergies.Split(',').ToList());
                Console.WriteLine($"Allergies changed to {account.Allergies}");
                break;
            case "4":
                Console.Write("Enter old password: ");
                string oldPassword = Console.ReadLine();
                Console.Write("Enter new password: ");
                string newPassword = Console.ReadLine();
                accountsLogic.ChangePassword(account.Id, oldPassword, newPassword);
                Console.WriteLine($"Password changed to {account.Password}");
                break;
            case "5":
                Console.Write("Enter new email: ");
                string newEmail = Console.ReadLine();
                string emailResult = accountsLogic.ChangeEmail(account.Id, newEmail);
                Console.WriteLine($"Email changed to {account.EmailAddress}");
                break;
            case "6":
                string userInfo = accountsLogic.UserInfo(account.Id);
                Console.WriteLine(userInfo);
                break;
        }
    }
}
using System.ComponentModel;

public class ModifyData
{
    static private AccountsLogic accountsLogic = new();

    public static void Start(AccountModel account)
    {
        string[] options = {
            "Change Name.",
            "Change Birthdate.",
            "Change Email.",
            "Change Phone number.",
            "Change Allergies.",
            "Change Password.",
            "Return",
        };
        // Console.WriteLine("Enter 1 to change name");
        // Console.WriteLine("Enter 2 to change age");
        // Console.WriteLine("Enter 3 to change allergies");
        // Console.WriteLine("Enter 4 to change password");
        // Console.WriteLine("Enter 5 to change email");
        // Console.WriteLine("Enter 6 to view user info");

        bool changingData = true;

        while (changingData)
        {
            int selectedIndex = 0;
            Console.Clear();
            string mainPrompt = accountsLogic.UserInfo(account.Id);

            selectedIndex = HelperPresentation.ChooseOption(mainPrompt, options, selectedIndex);


            switch (selectedIndex)
            {
                case 0:// name
                    ChangeName(account);
                    break;
                case 1:
                    // change birthdate
                    ChangeBirthdate(account);
                    // System.Console.WriteLine("Not created yet.");
                    break;
                case 2:
                    // change email
                    ChangeEmail(account);
                    System.Console.WriteLine("Not implemented yet");
                    break;
                case 3:// phone number
                    System.Console.WriteLine("Not implemented yet");
                    break;
                case 4:
                    // allergies
                    System.Console.WriteLine("Not implemented yet");
                    break;
                case 5:// password
                    break;
                case 6:// return
                    return;
                    break;
            }
        }


    }
    // case 0 change name
    private static void ChangeName(AccountModel acc)
    {
        string[] options = {
        "Change first name",
        "Change infix",
        "Change surname",
        "Return",
    };

        // Split the fullname
        string[] nameParts = acc.FullName.Split(' ');
        // first part is first name
        string firstName = nameParts[0];
        // second part is infix
        string infix = nameParts.Length > 2 ? nameParts[1] : "";
        // third part is surname(everything after first name if no infix else everything after infix)
        string surname = nameParts.Length > 1 ? string.Join(" ", nameParts[1..]) : "";

        bool changingName = true;

        while (changingName)
        {
            int selectedIndex = 0;
            Console.Clear();
            string mainPrompt = $"Given name: {acc.FullName}";

            selectedIndex = HelperPresentation.ChooseOption(mainPrompt, options, selectedIndex);

            switch (selectedIndex)
            {
                case 0: // Change first name
                    System.Console.WriteLine("Please enter first name: ");
                    string? newFirstName = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(newFirstName))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("No name entered, please re-enter the first name.");
                        Console.ResetColor();
                        System.Console.WriteLine("Name: ");
                        newFirstName = Console.ReadLine();
                    }
                    firstName = HelperLogic.CapitalizeFirstLetter(newFirstName.Trim());
                    break;

                case 1: // Change infix
                    System.Console.WriteLine("Please enter infix (or leave blank if none): ");
                    string? newInfix = Console.ReadLine();
                    infix = !string.IsNullOrWhiteSpace(newInfix) ? newInfix.Trim() : "";
                    break;

                case 2: // Change surname
                    System.Console.WriteLine("Please enter surname: ");
                    string? newSurname = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(newSurname))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("No surname entered, please re-enter the surname.");
                        Console.ResetColor();
                        System.Console.WriteLine("Surname: ");
                        newSurname = Console.ReadLine();
                    }
                    surname = HelperLogic.CapitalizeFirstLetter(newSurname.Trim());
                    break;

                case 3: // Return
                    changingName = false;
                    break;
            }

            // Update the full name
            acc.FullName = HelperLogic.CreateFullName(firstName, infix, surname);
            accountsLogic.ChangeName(acc.Id, HelperLogic.CreateFullName(firstName, infix, surname));
        }
    }
    // case 1 change birthdate 
    private static void ChangeBirthdate(AccountModel acc)
    {
        bool changingBirthday = true;
        while (changingBirthday)
        {
            bool correctBirthday = HelperPresentation.YesOrNo($"Is this your birthday?\n{HelperPresentation.DateTimeToReadableDate(acc.Birthdate)}");
            if (correctBirthday == false)
            {
                DateTime newBirthday = accountsLogic.GetBirthday();
                acc.Birthdate = newBirthday;
                accountsLogic.UpdateList(acc);
            }
            else
                break;
        }
    }
    // case 2 change email
    private static void ChangeEmail(AccountModel acc)
    {
        string[] options = {
            "Change Email",
            "return",
        };
        bool changingEmail = true;

        while (changingEmail)
        {
            int selectedIndex = 0;
            Console.Clear();

            selectedIndex = HelperPresentation.ChooseOption($"Given Email: {acc.EmailAddress}", options, selectedIndex);

            switch (selectedIndex)
            {
                case 0:
                    System.Console.WriteLine("Enter new Email: ");
                    string? newEmail = Console.ReadLine();
                    while (newEmail == null || !HelperLogic.IsValidEmail(newEmail))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("Invalid email entered. Press [esc] to go back");
                        Console.ResetColor();
                        System.Console.WriteLine("New Email: ");
                        // if user press esc go back
                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                        if (keyInfo.Key == ConsoleKey.Escape)
                        {
                            newEmail = acc.EmailAddress;
                            break;
                        }
                        newEmail = Console.ReadLine();
                    }
                    acc.EmailAddress = newEmail;
                    accountsLogic.ChangeEmail(acc.Id, newEmail);
                    break;
                case 1:
                    changingEmail = false;
                    break;
            }
        }
        /*
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
        */
    }
    // case 3 change phone number
    private static void ChangeNumber()
    {

    }
    // case 4 change allergies
    private static void ChangeAllergies()
    {
        /*
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
        */
    }
    // case 5 change password
    private static void ChangePassword()
    {
        /*
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
        */
    }
    // case 6 return
    private static void Return()
    {

    }
}
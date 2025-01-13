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
                    ChangePassword(account);
                    break;
                case 6:// return
                    return;
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
            accountsLogic.ModifyName(acc.Id, HelperLogic.CreateFullName(firstName, infix, surname));
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
    private static void ChangePassword(AccountModel acc)
    {

        bool changingPassword = true;
        do
        {
            Console.Clear();
            Console.WriteLine("Please enter your current password:");

            string oldPass = HelperPresentation.ReadPassword();

            if (oldPass != acc.Password)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Incorrect Password entered.");
                Console.ResetColor();
                Console.WriteLine("Press [Enter] to try again or [Escape] to cancel.");

                // Check if the user wants to try again or cancel
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Escape)
                {
                    changingPassword = false;
                    break;
                }
                continue;
            }

            // Old password is correct, go to new pass
            Console.Clear();
            Console.WriteLine("Please enter your new password:");

            string newPassword;
            string checkNewPass;
            do
            {
                newPassword = HelperPresentation.ReadPassword();
                checkNewPass = AccountsLogic.CheckCreatePassword(newPassword);

                if (checkNewPass != "Password has been set.")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(checkNewPass);
                    Console.ResetColor();
                    Console.WriteLine("Please try again:");
                }
            } while (checkNewPass != "Password has been set.");

            Console.WriteLine("Please re-enter your new password to confirm:");
            string confirmPassword = HelperPresentation.ReadPassword();

            if (newPassword == confirmPassword)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("New Password has been set successfully.");
                Console.ResetColor();

                // Update account
                acc.Password = newPassword;
                accountsLogic.UpdateList(acc);
                break;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Passwords do not match.");
                Console.ResetColor();

                bool tryAgain = HelperPresentation.YesOrNo("Would you like to try setting a new password again?");
                if (!tryAgain)
                {
                    changingPassword = false;
                    break;
                }
            }

        } while (changingPassword);
    }
}
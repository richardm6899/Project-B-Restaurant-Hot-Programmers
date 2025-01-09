static class UserMakeAccount
{
    private static AccountsLogic accountsLogic = new AccountsLogic();

    public static void Start()
    {

        System.Console.WriteLine("Welcome to the make an account page.");
        bool user_answer = HelperPresentation.YesOrNo("Are you sure you want to make an account?");
        if (!user_answer)
        {
            Menu.Start();
        }

        // email
        System.Console.WriteLine("What is your Email: ");
        string? email = Console.ReadLine();
        // check if email a correct email, and keep asking if not a correct email
        bool correct_email = AccountsLogic.CheckCreateEmail(email);
        while (correct_email != true)
        {
            System.Console.WriteLine("Invalid Email, please re-enter Email: ");
            email = Console.ReadLine();
            correct_email = AccountsLogic.CheckCreateEmail(email);
        }
        // check if email is already in json file (if there is already an account with this email)
        bool emailAlreadyExists = accountsLogic.CheckEmailInJson(email);
        if (emailAlreadyExists)
        {
            System.Console.WriteLine("This email already exists.");
            Menu.Start();
        };

        // first name
        System.Console.WriteLine("What is your First Name: ");
        string? firstNameLower = Console.ReadLine();
        while (firstNameLower == null || firstNameLower == "")
        {
            System.Console.WriteLine("Invalid name, please re-enter your First Name: ");
            firstNameLower = Console.ReadLine();
        }
        string FirstName = HelperLogic.CapitalizeFirstLetter(firstNameLower);

        // last name
        System.Console.WriteLine("What is your Last Name: ");
        string? lastNameLower = Console.ReadLine();
        while (lastNameLower == null || lastNameLower == "")
        {
            System.Console.WriteLine("Invalid name, please re-enter your Last Name: ");
            lastNameLower = Console.ReadLine();
        }
        string LastName = HelperLogic.CapitalizeFirstLetter(lastNameLower);

        // pass
        System.Console.WriteLine("(Password must contain a capital letter, a lowercase letter, must be 8 characters or longer\n and needs to contain a number or symbol)");
        System.Console.WriteLine("What is your password: ");
        string password = Console.ReadLine();
        bool correct_password = false;
        do
        {
            string pass_massage = AccountsLogic.CheckCreatePassword(password);
            Console.WriteLine(pass_massage);
            if (pass_massage == "Password has been set.") { correct_password = true; break; }
            System.Console.WriteLine("New Password:");
            password = Console.ReadLine();

        } while (correct_password != true);
        // maybe add user must enter pass again to check

        // number
        System.Console.WriteLine("Enter your phonenumber: ");
        Console.Write("+");
        string phoneNumber = Console.ReadLine();
        while (phoneNumber.Count() <= 8)
        {
            System.Console.WriteLine("Phone number not long enough, please enter another number: ");
            Console.Write("+");
            phoneNumber = Console.ReadLine();
        }

        // age
        DateTime birthday = default;
        bool correct_age = false;
        do
        {
            System.Console.WriteLine("What is your Birthdate: ");
            birthday = accountsLogic.GetBirthday();
            if (HelperPresentation.YesOrNo($"Is this correct? {birthday.ToShortDateString()}"))
            {
                correct_age = true;
            }
        } while (correct_age == false);

        // int age = Convert.ToInt32(Console.ReadLine());
        // do
        // {
        //     try
        //     {
        //         if (age <= 0 || age > 150)
        //         {
        //             System.Console.WriteLine("Invalid age, please re-enter Age: ");
        //             age = Convert.ToInt32(Console.ReadLine());
        //         }
        //         else if (age < 18)
        //         {
        //             System.Console.WriteLine("You are too young to make an account.");
        //             Menu.Start();
        //         }
        //         else correct_age = true;

        //     }
        //     catch (FormatException)
        //     {
        //         System.Console.WriteLine("Please enter a number.");
        //     }
        // } while (correct_age != true);

        // allergies
        List<string> allergies = [];
        bool user_answer_allergies = HelperPresentation.YesOrNo("Do you have/ do you want to fill in you allergies.");

        if (user_answer_allergies)
        {
            System.Console.WriteLine(@"Here are a list of our allergies:
            1. Fish
            2. Nuts
            3. Shellfish
            If you see any of your allergies please enter the numbers, separate numbers by comma/space: ");
            string user_allergies = Console.ReadLine();
            foreach (char num in user_allergies)
            {
                // ignore the whitespace.
                if (char.IsWhiteSpace(num)) continue;

                if (num == '1')
                {
                    allergies.Add("fish");
                }
                if (num == '2')
                {
                    allergies.Add("nuts");
                }
                if (num == '3')
                {
                    allergies.Add("shellfish");
                }
            }

        }


        // make full name
        string fullName = $"{FirstName} {LastName}";
        // make an account with all given info
        System.Console.WriteLine(AccountsLogic.CreateAccount(fullName, email, password, phoneNumber, birthday, allergies, "client", DateTime.MinValue));
    }
}
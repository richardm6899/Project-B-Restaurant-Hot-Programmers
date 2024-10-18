static class UserMakeAccount
{

    public static void Start()
    {
        
        System.Console.WriteLine("Welcome to the make an account page.");
        System.Console.WriteLine("Are you sure you want to make an account? Y/N");
        string user_answer = System.Console.ReadLine();
        if (user_answer.ToLower() == "n" || user_answer.ToLower() == "no")
        {
            Menu.Start();
        }
        else if(user_answer.ToLower() != "y" && user_answer.ToLower() != "yes")
        {
            System.Console.WriteLine("Invalid input");
            Start();
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
        AccountsLogic accountsLogic = new AccountsLogic();
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
        string FirstName = AccountsLogic.CapitalizeFirstLetter(firstNameLower);
        System.Console.WriteLine(FirstName);

        // last name
        System.Console.WriteLine("What is your Last Name: ");
        string? lastNameLower = Console.ReadLine();
        while (lastNameLower == null || lastNameLower == "")
        {
            System.Console.WriteLine("Invalid name, please re-enter your Last Name: ");
            lastNameLower = Console.ReadLine();
        }
        string LastName = AccountsLogic.CapitalizeFirstLetter(lastNameLower);
        System.Console.WriteLine(LastName);

        // pass
        System.Console.WriteLine("What is you password: ");
        string password = Console.ReadLine();
        bool correct_password = false;
        do
        {
            string pass_massage = AccountsLogic.CheckCreatePassword(password);
            Console.WriteLine(pass_massage);
            if (pass_massage == "Password has been set") { correct_password = true; break; }
            System.Console.WriteLine("New Password:");
            password = Console.ReadLine();

        } while (correct_password != true);
        // maybe add user must enter pass again to check

        // number
        System.Console.WriteLine("Enter your phonenumber: ");
        Console.Write("+31 6");
        string phoneNumber = Console.ReadLine();
        while (phoneNumber.Count() != 8)
        {
            System.Console.WriteLine("Phone number not long enough, please enter another number: ");
            Console.Write("+31 6");
            phoneNumber = Console.ReadLine();
        }

        // age
        bool correct_age = false;
        System.Console.WriteLine("What is your age: ");
        do
        {
            try
            {
                int age = Convert.ToInt32(Console.ReadLine());
                if (age <= 0 || age >= 130)
                {
                    System.Console.WriteLine("Invalid age, please re-enter Age: ");
                }
                else if (age < 18)
                {
                    System.Console.WriteLine("You are too young to make an account.");
                    Menu.Start();
                }
                else correct_age = true;
            }
            catch (FormatException)
            {
                System.Console.WriteLine("Please enter a number.");
            }
        } while (correct_age != true);

        // allergies
        List<string> allergies = [];



        // make full name
        string fullName = $"{FirstName} {LastName}";
        // make an account with all given info
        AccountModel account = AccountsLogic.CreateAccount(fullName, email, password, phoneNumber, allergies);
        
    }
}
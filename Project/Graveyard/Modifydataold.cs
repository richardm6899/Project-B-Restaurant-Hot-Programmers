

// change age
/*
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
*/


/*
    AccountsLogic accountsLogic = new();
        bool valid_name = false;
        while (valid_name == false)
        {
            Console.Write("Enter new name: ");
            string? newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName) && !newName.Any(char.IsDigit) && newName.Any(char.IsLetter))
            {
                string nameChange = accountsLogic.ChangeName(acc.Id, newName);
                Console.WriteLine(nameChange);
                valid_name = true;
            }
            else
            {
                Console.WriteLine("Must only contain letters");
            }
        }
*/

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
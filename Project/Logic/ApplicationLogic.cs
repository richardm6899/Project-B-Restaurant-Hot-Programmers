
class ApplicationLogic
{
    static private ApplicationAccess applicationAccess = new();
    public static string GetValidName()
    {
        Console.Write("Full name: ");
        string name;
        while (true)
        {
            name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name) && !HelperLogic.CheckIfNull(name) && IsNameValid(name))
            {
                break;
            }
            Console.Write("Invalid name. Please enter a valid name: ");
        }
        return HelperLogic.CapitalizeFirstLetter(name);
    }

    private static bool IsNameValid(string name)
    {
        // Ensure that the name only contains letters, spaces, hyphens, and apostrophes
        foreach (char c in name)
        {
            if (!char.IsLetter(c) && c != ' ' && c != '-' && c != '\'')
            {
                return false;
            }
        }
        return true;
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

    public static string GetValidEmail()
    {
        Console.Write("Email: ");
        string email;
        while (true)
        {
            email = Console.ReadLine();
            if (!HelperLogic.CheckIfNull(email) && HelperLogic.IsValidEmail(email))
            {
                break;
            }
            Console.Write("Invalid email. Please enter a valid email address: ");
        }
        return email;
    }

    public static string GetProjectDirectory()
    {
        // Get the base directory of the application
        string baseDirectory = AppContext.BaseDirectory;

        // Traverse upwards to find the `Project-B-Restaurant-Hot-Programmers` folder
        string currentDirectory = baseDirectory;
        while (!string.IsNullOrEmpty(currentDirectory))
        {
            string potentialMatch = Path.Combine(currentDirectory, "Project-B-Restaurant-Hot-Programmers");
            if (Directory.Exists(potentialMatch))
            {
                return potentialMatch;
            }

            // Move to the parent directory
            currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
        }

        // If not found, return null or handle the error
        Console.WriteLine("The directory 'Project-B-Restaurant-Hot-Programmers' was not found.");
        return null;
    }

    public static string GetValidFilePath(string[] validExtensions)
    {
        string filePath;
        while (true)
        {
            filePath = ApplicationMenu.GetFile();
            if (!string.IsNullOrEmpty(filePath))
            {
                string searchedFile = SearchFile(Path.GetDirectoryName(filePath), Path.GetFileName(filePath), validExtensions);

                if (!string.IsNullOrEmpty(searchedFile))
                {
                    return searchedFile; // Return the valid file path
                }
            }

            Console.Write($"Invalid file. Please provide a valid file path ({string.Join(", ", validExtensions)}): ");
        }
    }



    public static string SearchFile(string directory, string fileName, string[] validExtensions = null)
    {
        if (!Directory.Exists(directory))
        {
            Console.WriteLine($"Directory does not exist: {directory}");
            return null;
        }

        try
        {
            // Get all files in the current directory
            string[] files = Directory.GetFiles(directory);

            foreach (string file in files)
            {
                if (Path.GetFileName(file).Equals(fileName, StringComparison.OrdinalIgnoreCase))
                {
                    // Validate file extension if provided
                    if (validExtensions == null || Array.Exists(validExtensions, ext => Path.GetExtension(file).Equals(ext, StringComparison.OrdinalIgnoreCase)))
                    {
                        Console.WriteLine($"File found: {file}");
                        return file; // Return the file path if found
                    }
                }
            }

            // Recursively search in subdirectories
            string[] subDirectories = Directory.GetDirectories(directory);

            foreach (string subDir in subDirectories)
            {
                string result = SearchFile(subDir, fileName, validExtensions); // Recursive call
                if (!string.IsNullOrEmpty(result)) // If file is found, stop searching
                {
                    return result;
                }
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine($"Access denied to: {directory}");
        }
        catch (PathTooLongException)
        {
            Console.WriteLine($"Path too long: {directory}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred in '{directory}': {ex.Message}");
        }

        Console.WriteLine($"File '{fileName}' not found in directory '{directory}' or its subdirectories.");
        return null; // Return null if the file is not found
    }

    
    public static int CalculateAge(DateTime birthDate)
    {
        var today = DateTime.Today;
        var age = today.Year - birthDate.Year;
        if (birthDate.Date > today.AddYears(-age)) age--;
        return age;
    }

    public static void SaveApplicationToJson(string vacancy, string name, DateTime birthDate, string gender, string email, string phoneNumber, string cvPath, string motivation, bool storeCvAsBase64 = true)
    {
        string cvData;

        // Encode or store the CV based on the flag
        if (storeCvAsBase64)
        {
            // Encode the CV as Base64
            cvData = Convert.ToBase64String(File.ReadAllBytes(cvPath));
        }
        else
        {
            // Store the CV file path
            cvData = cvPath;
        }

        // Create the application model
        var application = new ApplicationModel(
            applicationName: vacancy,
            applicantName: name,
            birthdate: birthDate.ToString("yyyy-MM-dd"),
            gender: gender,
            email: email,
            motivation: motivation,
            cv: FileToJson(storeCvAsBase64, cvData),
            status: "Submitted"
        );

        // Load existing applications and add the new one
        var applications = applicationAccess.LoadAll();
        applications.Add(application);
        applicationAccess.WriteAll(applications);
        Console.WriteLine("Your applicant information:");
        Console.WriteLine($"Application: {application.ApplicationName}");
        Console.WriteLine($"Name: {application.ApplicantName}");
        Console.WriteLine($"Birthdate: {application.Birthdate}");
        Console.WriteLine($"Gender: {application.Gender}");
        Console.WriteLine($"Email: {application.Email}");
        Console.WriteLine($"Motivation: {application.Motivation}");
        Console.WriteLine($"Cv: {application.Cv}");
        Console.ReadLine();
    }

    public static string FileToJson(bool storeCvAsBase64, string cvData)
    {
        string decodedCv = "";
        // Debug and verify the CV content
        if (storeCvAsBase64)
        {
            // Decode and display Base64 data
            try
            {
                byte[] cvBytes = Convert.FromBase64String(cvData);
                decodedCv = System.Text.Encoding.UTF8.GetString(cvBytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to decode Base64 CV data: {ex.Message}");
            }
        }
        else
        {
            // Display the file path
            Console.WriteLine($"CV File Path: {cvData}");
        }
        return decodedCv;
    }

}

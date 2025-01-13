
class ApplicationLogic
{

    static private ApplicationAccess applicationAccess = new();

    public static bool IsNameValid(string name)
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

    // Method to get the days in a month
    public static int GetDaysInMonth(int month, int year)
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
            ApplicationMenu.InvalidFileText(validExtensions);
        }
    }



    public static string SearchFile(string directory, string fileName, string[] validExtensions = null)
    {
        if (!Directory.Exists(directory))
        {
            ApplicationMenu.DirectoryNotFound(directory);
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
        catch (Exception ex)
        {
            ApplicationMenu.ErrorDirectory(directory);
        }
        ApplicationMenu.FileNotFound(fileName);
        
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
            cv: ApplicationMenu.FileToJson(storeCvAsBase64, cvData),
            status: "Submitted"
        );

        // Load existing applications and add the new one
        var applications = applicationAccess.LoadAll();
        applications.Add(application);

        applicationAccess.WriteAll(applications);
        ApplicationMenu.ShowApplication(application);

    }
}

using System;
using System.Collections.Generic;
using System.IO;

public class ApplicationMenu
{
    static private ApplicationLogic applicationLogic = new ApplicationLogic();
    static List<string> Vacancies = new List<string> { "Chef", "Waiter", "Manager", "Dishwasher" };

    public static void Start()
    {
        int selectedOption;
        do
        {
            string mainMenuPrompt = "Welcome to the Restaurant Vacancy Application System!";
            string[] mainMenuOptions = { "View Vacancies", "Apply for a Vacancy", "Exit" };

            selectedOption = HelperPresentation.ChooseOption(mainMenuPrompt, mainMenuOptions, 0);

            switch (selectedOption)
            {
                case 0:
                    DisplayVacancies();
                    break;
                case 1:
                    ApplyForVacancy();
                    break;
                case 2:
                    Console.WriteLine("Thank you for using the application. Goodbye!");
                    break;
            }

        } while (selectedOption != 2);
    }

    public static void ShowApplication(ApplicationModel application)
    {
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

    public static string GetFile()
    {
        Console.WriteLine("The file name:");
        string fileName = Console.ReadLine();

        string directory = ApplicationLogic.GetProjectDirectory();
        if (directory == null)
        {
            Console.WriteLine("The required directory could not be found.");
            return null;
        }

        return ApplicationLogic.SearchFile(directory, fileName);
    }


    public static string GetGender()
    {
        string question = "What is your preferenced gender?";
        string[] options = ["Male", "Female", "Other"];
        return options[HelperPresentation.ChooseOption(question, options, 0)];
    }

    public static DateTime GetValidBirthDate()
    {
        Console.Write("Birth Date (yyyy-MM-dd): ");
        DateTime birthday = default;
        bool correct_age = false;
        do
        {
            System.Console.WriteLine("What is your Birthdate: ");
            birthday = GetBirthday();
            if (HelperPresentation.YesOrNo($"Is this correct? {birthday.ToShortDateString()}"))
            {
                correct_age = true;
            }
        } while (correct_age == false);
        return birthday;
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

    static void DisplayVacancies()
    {
        Console.Clear();
        Console.WriteLine("Available Vacancies:");
        foreach (var vacancy in Vacancies)
        {
            Console.WriteLine("- " + vacancy);
        }
        Console.WriteLine("\nPress any key to return to the main menu...");
        Console.ReadKey();
    }

    public static void ApplyForVacancy()
    {
        Console.Clear();
        string prompt = "Select a vacancy to apply for:";
        int vacancyIndex = HelperPresentation.ChooseOption(prompt, Vacancies.ToArray(), 0);

        string selectedVacancy = Vacancies[vacancyIndex];
        Console.Clear();
        Console.WriteLine($"You selected: {selectedVacancy}\n");

        Console.WriteLine("Enter your details:");

        string name = GetValidName();
        DateTime birthDate = GetValidBirthDate();
        string gender = GetGender();
        string email = GetValidEmail();
        Console.Write("Phone Number: ");
        string phoneNumber = Console.ReadLine();

        Console.Write("Provide the path to your CV (Word or TXT format): ");
        string cvPath = ApplicationLogic.GetValidFilePath(new[] { ".txt", ".docx" });

        Console.Write("Provide a short motivation: ");
        string motivation = Console.ReadLine();

        Console.Clear();
        Console.WriteLine("Confirm your application details:");
        Console.WriteLine($"Vacancy: {selectedVacancy}");
        Console.WriteLine($"Name: {name}");
        Console.WriteLine($"Birth Date: {HelperPresentation.DateTimeToReadableDate(birthDate)}");
        Console.WriteLine($"Email: {email}");
        Console.WriteLine($"Phone Number: {phoneNumber}");
        Console.WriteLine($"CV Path: {cvPath}");
        Console.WriteLine($"Motivation: {motivation}\n");

        bool confirm = HelperPresentation.YesOrNo("Do you want to submit your application?");

        if (confirm)
        {
            ApplicationLogic.SaveApplicationToJson(selectedVacancy, name, birthDate, gender, email, phoneNumber, cvPath, motivation);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Your application has been submitted successfully!");
            Console.ResetColor();
            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Application canceled.");
            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
        }
    }
    
    public static DateTime GetBirthday()
    {
        // Array of months
        string[] months = {
        "January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December"};

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
        int maxDays = ApplicationLogic.GetDaysInMonth(selectedMonth, selectedYear);
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
    public static string GetValidName()
    {
        Console.Write("Full name: ");
        string name;
        while (true)
        {
            name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name) && !HelperLogic.CheckIfNull(name) && ApplicationLogic.IsNameValid(name))
            {
                break;
            }
            Console.Write("Invalid name. Please enter a valid name: ");
        }
        return HelperLogic.CapitalizeFirstLetter(name);
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

    public static void InvalidFileText(string[] validExtensions)
    {
        Console.Write($"Invalid file. Please provide a valid file path ({string.Join(", ", validExtensions)}): ");
    }

    public static void DirectoryNotFound(string directory)
    {
        Console.WriteLine($"Directory does not exist: {directory}");
    }

    public static void FileNotFound(string fileName)
    {
        Console.WriteLine($"File '{fileName}' not found in the directory");
    }
    public static void ErrorDirectory(string directory)
    {
        Console.WriteLine($"An error occurred in '{directory}'");
    }
}
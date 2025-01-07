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
            birthday = applicationLogic.GetBirthday();
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

        string name = ApplicationLogic.GetValidName();
        DateTime birthDate = GetValidBirthDate();
        string gender = GetGender();
        string email = ApplicationLogic.GetValidEmail();
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
            Console.WriteLine("Your application has been submitted successfully!");
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
}
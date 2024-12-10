using System.Text.RegularExpressions;

public class HelperLogic
{
    // check if given string is actually an integer, return true if is int
    public static bool CheckIfStringIsInt(string toCheck)
    {
        return !string.IsNullOrEmpty(toCheck) && toCheck.All(char.IsDigit);
    }

    // used to check if given item is null, if null return false
    public static bool CheckIfNull<T>(T toCheck)
    {
        if (toCheck == null)
        {
            return true;
        }
        else return false;
    }

    // converts given string to int. if string cannot be converted to int, method will return 0
    public static int ConvertStringToInt(string convert)
    {
        if (CheckIfStringIsInt(convert))
        {
            int converted = Convert.ToInt32(convert);
            return converted;
        }
        return default;
    }

    public static string CapitalizeFirstLetter(string toCapitalize) => char.ToUpper(toCapitalize[0]) + toCapitalize.Substring(1);

    // checks if leap year, if leap year returns true
    public static bool IsLeapYear(int year) => (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);


    // checks valid email, if valid return true
    public static bool IsValidEmail(string email) => Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    // regex explanation
    /*
    1. ^: match starts at beginning string
    2. [^@\s]: checks if there are one or more characters before the @ (except (^)"@" or (\s)whitespaces)
    3. +@: checks if there is an "@"
    4. [^@\s]: same as 2. but checks if thats also behind the @
    5. +\.: checks if there is a "." but because "." is a special character a "\" is needed 
    6. [^@\s]: same as 2. and 4. but checks after the "."
    7. +$: ends the match
    */ 

    // moved to HelperPresentation, but tested here
    // public static string DateTimeToReadableDate(DateTime dateTime) => dateTime.ToString("dd MMMM, yyyy");
}
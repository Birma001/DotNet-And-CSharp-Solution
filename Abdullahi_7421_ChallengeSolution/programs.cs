using System;
using System.Text.RegularExpressions;

public class DateConverter
{
    public static string ReverseDateFormat(string input)
    {
        // Regex pattern to match mm/dd/yyyy format
        string pattern = @"^(?<mon>\d{1,2})/(?<day>\d{1,2})/(?<year>\d{2,4})$";

        // Timeout duration for the regex match (in milliseconds)
        TimeSpan timeout = TimeSpan.FromSeconds(1); // Set timeout to 1 second

        try
        {
            // Timeout handling setup
            Regex regex = new Regex(pattern, RegexOptions.None, timeout);

            // Match the input against the regex pattern
            Match match = regex.Match(input);

            // Check if the input matches the expected format
            if (match.Success)
            {
                // Extract components from named capturing groups
                int month = int.Parse(match.Groups["mon"].Value);
                int day = int.Parse(match.Groups["day"].Value);
                int year = int.Parse(match.Groups["year"].Value);

                // Validate and convert to yyyy-mm-dd format
                if (IsValidDate(year, month, day))
                {
                    return $"{year:D4}-{month:D2}-{day:D2}";
                }
            }
        }
        catch (RegexMatchTimeoutException)
        {
            // Handle timeout gracefully by returning the original input
            return input;
        }

        // Return original input if not matched or invalid date components
        return input;
    }

    private static bool IsValidDate(int year, int month, int day)
    {
        // Validate using DateTime.TryParse
        return DateTime.TryParseExact($"{month}/{day}/{year}", "M/d/yyyy", null, System.Globalization.DateTimeStyles.None, out _);
    }

    public static void Main(string[] args)
    {
        // Example usage: prompt user for input
        while (true)
        {
            Console.WriteLine("Enter a date in mm/dd/yyyy format (or 'exit' to quit):");
            string input = Console.ReadLine().Trim();

            if (input.ToLower() == "exit")
                break;

            // Convert and output the converted date
            string convertedDate = ReverseDateFormat(input);
            Console.WriteLine($"Converted date: {convertedDate}\n");
        }
    }
}
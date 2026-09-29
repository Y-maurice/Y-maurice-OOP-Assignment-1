using System.Globalization;

namespace Part1_ProceduralToOOP;

public static class Helpers
{
    public static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int value))
                return value;
            Console.WriteLine("Please enter a whole number.");
        }
    }

    public static DateOnly ReadDate(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (DateOnly.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", out DateOnly value))
                return value;
            Console.WriteLine("Please use the format YYYY-MM-DD.");
        }
    }

    public static decimal ReadDecimal(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (decimal.TryParse(Console.ReadLine(), NumberStyles.Number,
                    CultureInfo.InvariantCulture, out decimal value))
                return value;
            Console.WriteLine("Please enter a number (for example 250.50).");
        }
    }

    public static string ReadText(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? text = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(text))
                return text.Trim();
            Console.WriteLine("This cannot be empty.");
        }
    }

    public static bool ReadYesNo(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? answer = Console.ReadLine()?.Trim().ToLower();
            if (answer == "y" || answer == "yes") return true;
            if (answer == "n" || answer == "no") return false;
            Console.WriteLine("Please answer y or n.");
        }
    }
}


using Part2_HotelReservationSystem.Enums;
using System.Globalization;

namespace Part2_HotelReservationSystem;

/// <summary>
/// Reads and validates what the user types in the console. It keeps asking until
/// the input has the right format, so the menu never has to deal with parsing.
/// It checks format only; business rules are enforced by the model classes.
/// </summary>
public static class InputHelper
{
    private const string DateFormat = "yyyy-MM-dd";

    public static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int value))
                return value;

            Console.WriteLine("Invalid input. Please enter a whole number.");
        }
    }

    // Used for menu choices: keeps asking until the number is within the range.
    public static int ReadIntInRange(string prompt, int min, int max)
    {
        while (true)
        {
            int value = ReadInt(prompt);

            if (value >= min && value <= max)
                return value;

            Console.WriteLine($"Please choose a number between {min} and {max}.");
        }
    }

    public static decimal ReadDecimal(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture,
                                 out decimal value))
                return value;

            Console.WriteLine("Invalid input. Please enter a number (for example 120.50).");
        }
    }

    public static string ReadNonEmptyString(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
                return input.Trim();

            Console.WriteLine("This field cannot be empty.");
        }
    }

    public static DateOnly ReadDate(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt} ({DateFormat}): ");
            string? input = Console.ReadLine();

            if (DateOnly.TryParseExact(input, DateFormat, CultureInfo.InvariantCulture,
                                       DateTimeStyles.None, out DateOnly date))
                return date;

            Console.WriteLine($"Invalid date. Use the format {DateFormat}, for example 2026-12-25.");
        }
    }

    // Shows the room types as a numbered list so the user never types an invalid value.
    public static RoomType ReadRoomType()
    {
        RoomType[] types = Enum.GetValues<RoomType>();

        Console.WriteLine("Room types:");
        for (int i = 0; i < types.Length; i++)
            Console.WriteLine($"  {i + 1}) {types[i]}");

        int choice = ReadIntInRange("Choose a room type: ", 1, types.Length);
        return types[choice - 1];
    }
}
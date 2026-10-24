using System.Globalization;
 
namespace WestcoastEducation.UI;
 
public static class ConsoleInput

{

    public const string DateFormat = "yyyy-MM-dd";

    public static string ReadLine() => Console.ReadLine() ?? throw new EndOfStreamException("Input closed.");
 
    public static string ReadText(string label, bool required = true)

    {

        while (true)

        {

            Console.Write($"{label}: ");

            var input = ReadLine().Trim();

            if (input.Length > 0 || !required)

            {

                return input;

            }
 
            Console.WriteLine("  A value is required.");

        }

    }
 
    public static int ReadInt(string label, int min, int max)

    {

        while (true)

        {

            var input = ReadText($"{label} ({min}-{max})");

            if (int.TryParse(input, out var value) && value >= min && value <= max)

            {

                return value;

            }
 
            Console.WriteLine($"  Enter a whole number between {min} and {max}.");

        }

    }
 
    public static DateOnly ReadDate(string label, DateOnly? notBefore = null)

    {

        while (true)

        {

            var input = ReadText($"{label} ({DateFormat})");

            if (!DateOnly.TryParseExact(input, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))

            {

                Console.WriteLine($"  Use the format {DateFormat}.");

            }

            else if (notBefore is { } earliest && date < earliest)

            {

                Console.WriteLine($"  The date cannot be before {earliest.ToString(DateFormat)}.");

            }

            else

            {

                return date;

            }

        }

    }

    public static char ReadChoice(string label, params char[] options)

    {

        var optionText = string.Join("/", options);

        while (true)

        {

            var input = ReadText($"{label} ({optionText})").ToLowerInvariant();

            if (input.Length == 1 && Array.IndexOf(options, input[0]) >= 0)

            {

                return input[0];

            }
 
            Console.WriteLine($"  Choose one of: {optionText}.");

        }

    }

}
 
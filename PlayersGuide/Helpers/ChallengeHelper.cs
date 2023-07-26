using PlayersGuide.Models;
using System.Text;
using static PlayersGuide.Models.Enums;

namespace PlayersGuide.Helpers
{
  public static class ChallengeHelper
  {
    public static T GetInput<T>(string prompt = null, string invalidMessage = null)
    {
GetInput:
      ConsoleHelper.WriteWithColor(prompt ?? $"Input a {typeof(T).Name} value: ", ConsoleColors.Inquisitive, useSameLine: true);
      var input = Console.ReadLine();
      if (string.IsNullOrWhiteSpace(input))
      {
        ConsoleHelper.WriteWithColor("Cannot be empty..", ConsoleColors.Warning);
        goto GetInput;
      }
      if (typeof(T) == typeof(string))
      {
        if (input.Contains("nigger", StringComparison.InvariantCultureIgnoreCase))
        {
          ConsoleHelper.WriteWithColor(invalidMessage ?? "Stop abusing my app, Nick... ", ConsoleColors.Warning);
          goto GetInput;
        }
        if (int.TryParse(input, out _))
        {
          ConsoleHelper.WriteWithColor(invalidMessage ?? "That seems more like just a number, try again... ", ConsoleColors.Warning);
          goto GetInput;
        }
        return (T)(object)input;
      }
      if (!TryParseInput(input, out T? value))
      {
        ConsoleHelper.WriteWithColor(invalidMessage ?? "Invalid input, try again... ", ConsoleColors.Warning);
        goto GetInput;
      }
      else
      {
        return value!;
      }
    }

    private static bool TryParseInput<T>(string input, out T? parsedValue)
    {
      if (input is not null)
      {
        if (typeof(T) == typeof(int))
        {
          if (int.TryParse(input, out var parsedInt))
          {
            parsedValue = (T)(object)parsedInt;
            return true;
          }
        }
        if (typeof(T) == typeof(uint))
        {
          if (uint.TryParse(input, out var parsedInt))
          {
            parsedValue = (T)(object)parsedInt;
            return true;
          }
        }
      }
      parsedValue = default;
      return false;
    }

    public static bool GetContinuationDecision()
    {
      Console.ForegroundColor = ConsoleColor.White;
      var messages = new List<string>()
      {
        "Again?",
        "Fancy another go?",
        "One more time?",
        "I assume you want to try again?",
        "More?",
        "Again, right?",
        "Would you fancy having another go?",
        "Do you fancy giving it another shot?",
        "Shall we give it another try, old chap?",
        "Are you keen on having another attempt?",
        "Would you care to give it another whirl?",
        "Fancy having a crack at it once more?",
        "Are you up for having another bash?",
        "Do you feel like having another stab at it?",
        "Would you be game for giving it another bash?",
        "Are you interested in having another crack at it, mate?"
      };
      var random = new Random().Next(messages.Count());

      ConsoleHelper.FormatSpacing(() => Console.WriteLine(messages[random]), spacesAfter: 1);
      return GetBoolInput();
    }

    public static bool GetBoolInput(string? message = null)
    {
      var input = GetInput<string>(message ?? "'y' to continue, anything else to quit..: ");
      return input.ToLower() switch
      {
        "y" => true,
        _ => false
      };
    }

    public static void GetInputFromEnum<T>(T[] source, out T? selectedItem) where T : Enum
    {
      var arrayType = source.GetType();
      var name = arrayType?.GetElementType()?.Name;

      ConsoleHelper.FormatSpacing(() => Console.WriteLine($"Available {name}s"), spacesBefore: 1);

      foreach (var item in source)
      {
        ConsoleHelper.WriteWithColor($"{item.ToString().AddSpacing()}", ConsoleColors.Informative);
      }
GetInput:
      ConsoleHelper.AddSpace(1);
      var input = GetInput<string>($"Which {name?.ToLower()} do you choose?: ");
      if (!TryParseEnumInput(input, out T? value))
      {
        ConsoleHelper.WriteWithColor($"'{input}'", ConsoleColors.Informative, useSameLine: true);
        ConsoleHelper.WriteWithColor(" doesn't seem to be available, please choose again..", ConsoleColors.Warning);
        goto GetInput;
      }
      selectedItem = value;
    }

    private static bool TryParseEnumInput<T>(string input, out T? parsedValue) where T : Enum
    {
      if (input is not null)
      {
        var formattedInput = input.Replace(oldValue: " ", newValue: "");
        if (typeof(T) == typeof(Seasoning))
        {
          if (Enum.TryParse<Seasoning>(formattedInput, ignoreCase: true, out var value))
          {
            parsedValue = (T)(object)value;
            return true;
          }
        }
        if (typeof(T) == typeof(Ingredient))
        {
          if (Enum.TryParse<Ingredient>(formattedInput, ignoreCase: true, out var value))
          {
            parsedValue = (T)(object)value;
            return true;
          }
        }
        if (typeof(T) == typeof(FoodType))
        {
          if (Enum.TryParse<FoodType>(formattedInput, ignoreCase: true, out var value))
          {
            parsedValue = (T)(object)value;
            return true;
          }
        }
        if (typeof(T) == typeof(ArrowHead))
        {
          if (Enum.TryParse<ArrowHead>(formattedInput, ignoreCase: true, out var value))
          {
            parsedValue = (T)(object)value;
            return true;
          }
        }
        if (typeof(T) == typeof(Fletching))
        {
          if (Enum.TryParse<Fletching>(formattedInput, ignoreCase: true, out var value))
          {
            parsedValue = (T)(object)value;
            return true;
          }
        }
        if (typeof(T) == typeof(GenericArrow))
        {
          if (Enum.TryParse<GenericArrow>(formattedInput, ignoreCase: true, out var value))
          {
            parsedValue = (T)(object)value;
            return true;
          }
        }
      }
      parsedValue = default;
      return false;
    }

    public static string AddSpacing(this string input)
    {
      var outputBuilder = new StringBuilder();
      for (int i = 0; i < input.Length; i++)
      {
        char currentChar = input[i];
        char nextChar = (i + 1 < input.Length) ? input[i + 1] : '\0';
        char prevChar = (i - 1 >= 0) ? input[i - 1] : '\0';

        if (i > 0 && char.IsUpper(currentChar) && char.IsLower(nextChar))
        {
          outputBuilder.Append(" ");
        }

        outputBuilder.Append(currentChar);
      }
      return outputBuilder.ToString();
    }
  }
}

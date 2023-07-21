using PlayersGuide.Models;

namespace PlayersGuide.Helpers
{
  public static class ConsoleHelper
  {
    public static T GetInput<T>(string prompt = null, string invalidMessage = null)
    {
GetInput:
      WriteWithColor(prompt ?? $"Input a {typeof(T).Name} value: ", ConsoleColors.Inquisitive, useSameLine: true);
      var input = Console.ReadLine();
      if (string.IsNullOrWhiteSpace(input))
      {
        WriteWithColor("Cannot be empty..", ConsoleColors.Warning);
        goto GetInput;
      }
      if (typeof(T) == typeof(string))
      {
        if (string.Equals(input, "nigger", StringComparison.InvariantCultureIgnoreCase))
        {
          WriteWithColor(invalidMessage ?? "Stop abusing my app, Nick... ", ConsoleColors.Warning);
          goto GetInput;
        }
        if (int.TryParse(input, out _))
        {
          WriteWithColor(invalidMessage ?? "That seems more like a number, try again... ", ConsoleColors.Warning);
          goto GetInput;
        }
        return (T)(object)input;
      }
      if (!TryParseInput(input, out T value))
      {
        WriteWithColor(invalidMessage ?? "Invalid input, try again... ", ConsoleColors.Warning);
        goto GetInput;
      }
      else
      {
        return value!;
      }
    }

    public static void WriteWithColor(string message, ConsoleColor color, bool useSameLine = false)
    {
      Console.ForegroundColor = color;
      if (useSameLine)
        Console.Write(message);
      else
        Console.WriteLine(message);
      Console.ForegroundColor = ConsoleColor.White;
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
      Console.WriteLine(messages[random]);
      Console.WriteLine();
      var input = GetInput<string>(prompt: "\"y\" to continue, anything else to quit.. : ");
      return input switch
      {
        "y" => true,
        _ => false
      };
    }
    public static void Clear() => Console.Clear();
  }
}

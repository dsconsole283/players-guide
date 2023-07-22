using PlayersGuide.Helpers;
using PlayersGuide.Models;

namespace PlayersGuide.Challenges
{
  public class FreachChallenge : ChallengeBase
  {
    public static readonly string DisplayName = "The Laws of Freach";
    private static int MaxArraySize = 1000;
    public FreachChallenge() { }

    public override void Run()
    {
      while (ShouldContinue)
      {
        ConsoleHelper.Clear();

        ConsoleHelper.FormatSpacing(() => Titles.Freach(TitleColor), spacesBefore: 1, spacesAfter: 1);

AskForArraySize:
        var arrayLength = ChallengeHelper.GetInput<uint>($"Enter a positive integer to set the size of an array (limit is {MaxArraySize}): ");
        if (arrayLength > MaxArraySize)
        {
          ConsoleHelper.WriteWithColor("Maximum exceeded, try again..", ConsoleColors.Warning);
          goto AskForArraySize;
        }
        Console.WriteLine("Now press any key to fill your array with random numbers..");
        Console.ReadKey();

        ConsoleHelper.FormatSpacing(() => Console.WriteLine("Random numbers in array:"), spacesBefore: 1, spacesAfter: 1);

        int[] array = new int[arrayLength];
        for (int i = 0; i < array.Length; i++)
        {
          var random = new Random();
          array[i] = random.Next(int.MinValue, int.MaxValue);
          if (i == array.Length - 1)
          {
            ConsoleHelper.WriteWithColor($"{array[i]}", ConsoleColors.Informative);
          }
          else
          {
            ConsoleHelper.WriteWithColor($"{array[i]}, ", ConsoleColors.Informative, true);
          }
        }
        int currentSmallest = int.MaxValue;
        foreach (int index in array)
        {
          if (index < currentSmallest)
            currentSmallest = index;
        }

        ConsoleHelper.FormatSpacing(() => ConsoleHelper.WriteWithColor($"Smallest value = {currentSmallest}", ConsoleColors.Favorable), spacesBefore: 1);

        int total = 0;
        foreach (int index in array)
          total += index;
        decimal average = (decimal)total / array.Length;

        ConsoleHelper.FormatSpacing(() => ConsoleHelper.WriteWithColor($"Average = {average}", ConsoleColors.Favorable), spacesAfter: 1);

        ShouldContinue = ChallengeHelper.GetContinuationDecision();

        ConsoleHelper.Clear();
      }
    }
  }
}

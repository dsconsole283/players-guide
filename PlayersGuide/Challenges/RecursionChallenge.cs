using PlayersGuide.Helpers;
using PlayersGuide.Models;

namespace PlayersGuide.Challenges
{
  public class RecursionChallenge : ChallengeBase
  {
    public static readonly string DisplayName = "Countdown (recursion challenge)";
    private static int MaxInput = 9999;
    private static int MinInput = -9999;
    public RecursionChallenge() { }

    public override void Run()
    {
      while (ShouldContinue)
      {
        ConsoleHelper.Clear();

        ConsoleHelper.FormatSpacing(() => Titles.Countdown(TitleColor), 1, 1);

AskForInput:
        var startingNumber = ChallengeHelper.GetInput<int>($"Enter a number to start the countdown (Allowed range {MinInput} - {MaxInput}): ");
        if (startingNumber > MaxInput || startingNumber < MinInput)
        {
          ConsoleHelper.WriteWithColor("Limit exceeded, try again..", ConsoleColors.Warning);
          goto AskForInput;
        }

        ConsoleHelper.FormatSpacing(() => Countdown(startingNumber), spacesAfter: 1);

        ShouldContinue = ChallengeHelper.GetContinuationDecision();

        ConsoleHelper.Clear();
      }
    }

    public static int Countdown(int start)
    {
      if (start == 0) return 0;
      Console.WriteLine(start);
      if (start < 0)
        return start - Countdown(start + 1);
      else
        return start - Countdown(start - 1);
    }
  }
}

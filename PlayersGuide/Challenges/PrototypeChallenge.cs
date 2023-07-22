using PlayersGuide.Helpers;
using PlayersGuide.Models;

namespace PlayersGuide.Challenges
{
  public class PrototypeChallenge : ChallengeBase
  {
    public static readonly string DisplayName = "The Prototype";
    private int numberToGuess { get; set; }

    public PrototypeChallenge() { }

    public override void Run()
    {
      while (ShouldContinue)
      {
        ConsoleHelper.Clear();

        ConsoleHelper.FormatSpacing(() => Titles.PrototypeChallenge(TitleColor), spacesBefore: 1, spacesAfter: 1);

        var isValidNumber = false;
        while (!isValidNumber)
        {
          numberToGuess = ChallengeHelper.GetInput<int>(prompt: "User 1, enter a number between 0 and 100: ");
          isValidNumber = numberToGuess >= 0 && numberToGuess <= 100;
        }

        ConsoleHelper.Clear();

        var numberGuess = ChallengeHelper.GetInput<int>(prompt: "User 2, guess the number: ");
        while (numberGuess != numberToGuess)
        {
          ConsoleHelper.WriteWithColor($"{numberGuess} is too {(numberGuess > numberToGuess ? "high" : "low")}.", ConsoleColors.Warning);

          numberGuess = ChallengeHelper.GetInput<int>("What is your next guess? : ");
        }

        ConsoleHelper.FormatSpacing(() => ConsoleHelper.WriteWithColor("You guessed the number!", ConsoleColors.Favorable), 1, 1);

        ShouldContinue = ChallengeHelper.GetContinuationDecision();

        ConsoleHelper.Clear();
      }
    }
  }
}


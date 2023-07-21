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

        Console.WriteLine();
        Titles.PrototypeChallenge(TitleColor);
        Console.WriteLine();

        var isValidNumber = false;
        while (!isValidNumber)
        {
          numberToGuess = ConsoleHelper.GetInput<int>(prompt: "User 1, enter a number between 0 and 100: ");
          isValidNumber = numberToGuess >= 0 && numberToGuess <= 100;
        }

        ConsoleHelper.Clear();

        var numberGuess = ConsoleHelper.GetInput<int>(prompt: "User 2, guess the number: ");
        while (numberGuess != numberToGuess)
        {
          ConsoleHelper.WriteWithColor($"{numberGuess} is too {(numberGuess > numberToGuess ? "high" : "low")}.", ConsoleColors.Warning);

          numberGuess = ConsoleHelper.GetInput<int>("What is your next guess? : ");
        }
        Console.WriteLine();
        ConsoleHelper.WriteWithColor("You guessed the number!", ConsoleColors.Favorable);
        Console.WriteLine();
        ShouldContinue = ConsoleHelper.GetContinuationDecision();

        ConsoleHelper.Clear();
      }
    }
  }
}


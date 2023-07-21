using PlayersGuide.Helpers;
using PlayersGuide.Models;

namespace PlayersGuide.Challenges
{
  public class WatchTowerChallenge : ChallengeBase
  {
    public static readonly string DisplayName = "The Watch Tower";
    private int XCord { get; set; }
    private int YCord { get; set; }

    public WatchTowerChallenge() { }

    public override void Run()
    {
      string text = "The enemy is";
      string firstCardinal = "";
      string secondCardinal = "";
      while (ShouldContinue)
      {
        ConsoleHelper.Clear();
        Console.WriteLine();
        Titles.TowerChallenge(TitleColor);
        Console.WriteLine();
X:
        try
        {
          XCord = ChallengeHelper.GetInput<int>(prompt: "Enter a value for x: ");
          if (XCord > 0)
          {
            secondCardinal = "East";
          }
          if (XCord < 0)
          {
            secondCardinal = "West";
          }

        }
        catch (ArgumentNullException)
        {
          ConsoleHelper.WriteWithColor("Cannot be empty!!", ConsoleColors.Warning);
          goto X;
        }
Y:
        try
        {
          YCord = ChallengeHelper.GetInput<int>(prompt: "Enter a value for y: ");
          if (YCord > 0)
          {
            firstCardinal = "North";
          }
          if (YCord < 0)
          {
            firstCardinal = "South";
          }
        }
        catch (ArgumentNullException)
        {
          ConsoleHelper.WriteWithColor("Cannot be empty!!", ConsoleColors.Warning);
          goto Y;
        }
        Console.WriteLine();
        ConsoleHelper.WriteWithColor(XCord == 0 && YCord == 0 ? $"{text} here!" : $"{text} is to the {firstCardinal}{secondCardinal}!", ConsoleColors.Favorable);
        Console.WriteLine();
        ShouldContinue = ChallengeHelper.GetContinuationDecision();

        ConsoleHelper.Clear();
      }
    }
  }
}


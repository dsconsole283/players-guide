using PlayersGuide.Helpers;
using PlayersGuide.Models;

namespace PlayersGuide.Challenges
{
  public class WatchTowerChallenge : ChallengeBase
  {
    public static readonly string DisplayName = "The Watch Tower";
    private int XCord { get; set; }
    private int YCord { get; set; }
    private string FirstCardinal { get; set; } = string.Empty;
    private string SecondCardinal { get; set; } = string.Empty;

    public WatchTowerChallenge() { }

    public override void Run()
    {
      string text = "The enemy is";

      while (ShouldContinue)
      {
        ConsoleHelper.Clear();
        ClearCoordinateData();

        ConsoleHelper.FormatSpacing(() => Titles.TowerChallenge(TitleColor), spacesBefore: 1, spacesAfter: 1);
X:
        try
        {
          XCord = ChallengeHelper.GetInput<int>(prompt: "Enter a value for x: ");
          if (XCord > 0)
          {
            SecondCardinal = "East";
          }
          if (XCord < 0)
          {
            SecondCardinal = "West";
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
            FirstCardinal = "North";
          }
          if (YCord < 0)
          {
            FirstCardinal = "South";
          }
        }
        catch (ArgumentNullException)
        {
          ConsoleHelper.WriteWithColor("Cannot be empty!!", ConsoleColors.Warning);
          goto Y;
        }

        ConsoleHelper.FormatSpacing(() => ConsoleHelper.WriteWithColor(XCord == 0 && YCord == 0 ? $"{text} here!" : $"{text} is to the {FirstCardinal}{SecondCardinal}!", ConsoleColors.Favorable), spacesBefore: 1, spacesAfter: 1);

        ShouldContinue = ChallengeHelper.GetContinuationDecision();

        ConsoleHelper.Clear();
      }
    }

    private void ClearCoordinateData()
    {
      XCord = 0;
      YCord = 0;
      FirstCardinal = string.Empty;
      SecondCardinal = string.Empty;
    }
  }
}


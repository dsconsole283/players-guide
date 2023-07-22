using PlayersGuide.Helpers;
using PlayersGuide.Models;
using static PlayersGuide.Models.Enums;

namespace PlayersGuide.Challenges
{
  public class ClassChallenge : ChallengeBase
  {
    public static readonly string DisplayName = "Vin Fletcher's Arrows";
    public Arrow? Arrow { get; set; }
    public ArrowHead[] ArrowHead { get; set; } = (ArrowHead[])Enum.GetValues(typeof(ArrowHead));
    public Fletching[] Fletching { get; set; } = (Fletching[])Enum.GetValues(typeof(Fletching));
    private const int _maxArrowLength = 100;
    private const int _minArrowLength = 60;
    public ClassChallenge() { }

    public override void Run()
    {
      while (ShouldContinue)
      {
        ConsoleHelper.Clear();

        ConsoleHelper.FormatSpacing(() => Titles.VinFletchersArrows(TitleColor), spacesBefore: 1, spacesAfter: 1);

        ChallengeHelper.GetInputFromEnum(ArrowHead, out var selectedArrowHead);
        ChallengeHelper.GetInputFromEnum(Fletching, out var selectedFletching);

GetLength:
        ConsoleHelper.AddSpace(1);

        var arrowLength = ChallengeHelper.GetInput<int>("How long would you like your arrow to be? ");
        if (arrowLength < _minArrowLength)
        {
          ConsoleHelper.FormatSpacing(() => ConsoleHelper.WriteWithColor($"Arrow length is too short, must be at least {_minArrowLength}.", ConsoleColors.Warning), spacesAfter: 1);
          goto GetLength;
        }
        if (arrowLength > _maxArrowLength)
        {
          ConsoleHelper.FormatSpacing(() => ConsoleHelper.WriteWithColor($"Arrow length is too long, must be less than {_maxArrowLength}.", ConsoleColors.Warning), spacesAfter: 1);
          goto GetLength;
        }

        Arrow = new Arrow(selectedArrowHead, arrowLength, selectedFletching);

        ConsoleHelper.FormatSpacing(() => ConsoleHelper.WriteWithColor($"The arrow you requested costs {Arrow.GetCost()} gold.", ConsoleColors.Favorable), spacesBefore: 1);

        ConsoleHelper.AddSpace(1);
        ShouldContinue = ChallengeHelper.GetContinuationDecision();

        ConsoleHelper.Clear();
      }
    }
  }
}

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

        Console.WriteLine();
        Titles.VinFletchersArrows(TitleColor);
        Console.WriteLine();

        ChallengeHelper.GetInputFromEnum(ArrowHead, out var selectedArrowHead);
        ChallengeHelper.GetInputFromEnum(Fletching, out var selectedFletching);

GetLength:
        Console.WriteLine();
        var arrowLength = ChallengeHelper.GetInput<int>("How long would you like your arrow to be? ");
        if (arrowLength < _minArrowLength)
        {
          ConsoleHelper.WriteWithColor($"Arrow length is too short, must be at least {_minArrowLength}.", ConsoleColors.Warning);
          Console.WriteLine();
          goto GetLength;
        }
        if (arrowLength > _maxArrowLength)
        {
          ConsoleHelper.WriteWithColor($"Arrow length is too long, must be less than {_maxArrowLength}.", ConsoleColors.Warning);
          Console.WriteLine();
          goto GetLength;
        }

        Arrow = new Arrow
        {
          _arrowHead = selectedArrowHead,
          _fletching = selectedFletching,
          _length = arrowLength
        };

        Console.WriteLine();
        ConsoleHelper.WriteWithColor($"The arrow you requested costs {Arrow.GetCost()} gold.", ConsoleColors.Favorable);

        Console.WriteLine();
        ShouldContinue = ChallengeHelper.GetContinuationDecision();

        ConsoleHelper.Clear();
      }
    }
  }
}

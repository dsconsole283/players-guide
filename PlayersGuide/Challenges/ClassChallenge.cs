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
    public GenericArrow[] GenericArrows { get; set; } = (GenericArrow[])Enum.GetValues(typeof(GenericArrow));
    public int OrderQty { get; private set; } = 1;

    private const int _maxArrowLength = 100;
    private const int _minArrowLength = 60;
    public ClassChallenge() { }

    public override void Run()
    {
      while (ShouldContinue)
      {
        ConsoleHelper.Clear();

        ConsoleHelper.FormatSpacing(() => Titles.VinFletchersArrows(TitleColor), spacesBefore: 1, spacesAfter: 1);

        var isGenericOrder = ChallengeHelper.GetBoolInput("Would you like to buy some of our pre-made arrows?: ");

        if (isGenericOrder)
        {
          ChallengeHelper.GetInputFromEnum(GenericArrows, out var genericArrowType);
          Arrow = genericArrowType switch
          {
            GenericArrow.Beginner => Arrow.CreateBeginnerArrow(),
            GenericArrow.Marksman => Arrow.CreateMarksmanArrow(),
            GenericArrow.Elite => Arrow.CreateEliteArow()
          };
        }
        else
        {
          ChallengeHelper.GetInputFromEnum(ArrowHead, out var selectedArrowHead);
          ChallengeHelper.GetInputFromEnum(Fletching, out var selectedFletching);
GetLength:
          ConsoleHelper.AddSpace(1);

          var arrowLength = ChallengeHelper.GetInput<int>("How long would you like your arrow to be? ");
          if (arrowLength < _minArrowLength)
          {
            ConsoleHelper.WriteWithColor($"Arrow length is too short, must be at least {_minArrowLength}.", ConsoleColors.Warning);
            goto GetLength;
          }
          if (arrowLength > _maxArrowLength)
          {
            ConsoleHelper.WriteWithColor($"Arrow length is too long, must be less than {_maxArrowLength}.", ConsoleColors.Warning);
            goto GetLength;
          }

          Arrow = new Arrow(selectedArrowHead, selectedFletching, arrowLength);
        }

        OrderQty = Math.Abs(ChallengeHelper.GetInput<int>("How many?: "));

        ConsoleHelper.FormatSpacing(() => ConsoleHelper.WriteWithColor($"{OrderQty} arrows with {Arrow.ArrowHead} arrowheads, {Arrow.Fletching} fletching that are {Arrow.Length}cm long cost {Arrow.GetCost() * OrderQty} gold.", ConsoleColors.Favorable), spacesBefore: 1);

        ConsoleHelper.AddSpace(1);
        ShouldContinue = ChallengeHelper.GetContinuationDecision();

        ConsoleHelper.Clear();
      }
    }
  }
}

using PlayersGuide.Helpers;
using PlayersGuide.Models;
using static PlayersGuide.Models.Enums;

namespace PlayersGuide.Challenges
{
  public class EnumChallenge : ChallengeBase
  {
    public static readonly string DisplayName = "Simula's Test";
    public BarrierState BoxState { get; set; } = BarrierState.Locked;
    public EnumChallenge() { }

    public override void Run()
    {
      while (ShouldContinue)
      {
        ConsoleHelper.Clear();

        ConsoleHelper.FormatSpacing(() => Titles.Simula(TitleColor), spacesBefore: 1, spacesAfter: 1);

        var keepRunning = true;

        while (keepRunning)
        {
          Console.Write($"The chest is {BoxState.ToString().ToLower()}.");
          var boxAction = ChallengeHelper.GetInput<string>(" What do you want to do? ").ToLower();

          keepRunning = !string.Equals(boxAction, "exit", StringComparison.InvariantCultureIgnoreCase);
          BoxState = boxAction switch
          {
            "close" => BoxState == BarrierState.Open ? BarrierState.Closed : BoxState,
            "open" => BoxState == BarrierState.Closed ? BarrierState.Open : BoxState,
            "lock" => BoxState == BarrierState.Closed ? BarrierState.Locked : BoxState,
            "unlock" => BoxState == BarrierState.Locked ? BarrierState.Closed : BoxState,
            _ => BoxState = BoxState
          };
        }

        ConsoleHelper.AddSpace(1);
        ShouldContinue = ChallengeHelper.GetContinuationDecision();

        ConsoleHelper.Clear();
      }
    }
  }
}

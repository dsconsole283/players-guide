using PlayersGuide.Helpers;
using PlayersGuide.Models;
using static PlayersGuide.Models.Enums;

namespace PlayersGuide.Challenges
{
  public class EnumChallenge : ChallengeBase
  {
    public static readonly string DisplayName = "Simula's Test";
    public BoxState BoxState { get; set; } = BoxState.Locked;
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
            "close" => BoxState == BoxState.Open ? BoxState.Closed : BoxState,
            "open" => BoxState == BoxState.Closed ? BoxState.Open : BoxState,
            "lock" => BoxState == BoxState.Closed ? BoxState.Locked : BoxState,
            "unlock" => BoxState == BoxState.Locked ? BoxState.Closed : BoxState,
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

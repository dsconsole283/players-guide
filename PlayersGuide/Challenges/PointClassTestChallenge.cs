using PlayersGuide.Helpers;
using PlayersGuide.Models;
using static PlayersGuide.Models.Enums;

namespace PlayersGuide.Challenges
{
  public class PointClassTestChallenge : ChallengeBase
  {
    public static readonly string DisplayName = "The Locked Door";

    public override void Run()
    {
      while (ShouldContinue)
      {
        ConsoleHelper.Clear();
        var door = new Door(ChallengeHelper.GetInput<int>("Enter the passcode for your brand new door: "));
        ConsoleHelper.Clear();

        var keepGoing = true;
        do
        {
          var command = string.Empty;
          ConsoleHelper.FormatSpacing(() =>
          {
            ConsoleHelper.WriteWithColor($"The door is {door.DoorState}. ", ConsoleColors.Informative, useSameLine: true);
            command = ChallengeHelper.GetInput<string>("What would you like to do?: ");
          }, spacesAfter: 1);
          if (string.Equals(command, "exit", StringComparison.InvariantCultureIgnoreCase))
          {
            keepGoing = false;
          }

          door.DoorState = door.TryChangeState(command);
        }
        while (keepGoing);

        ConsoleHelper.AddSpace(1);
        ShouldContinue = ChallengeHelper.GetContinuationDecision();

        ConsoleHelper.Clear();
      }
    }
  }
}

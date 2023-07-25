using PlayersGuide.Helpers;
using PlayersGuide.Models;
using static PlayersGuide.Models.Enums;

namespace PlayersGuide.Challenges
{
  public class PointClassTestChallenge : ChallengeBase
  {
    public static readonly string DisplayName = "The Locked Door";

    public Door Door { get; set; }

    public override void Run()
    {
      while (ShouldContinue)
      {
        ConsoleHelper.Clear();
        Door = new Door(ChallengeHelper.GetInput<int>("Enter the passcode for your brand new door: "));
        ConsoleHelper.Clear();

        var keepGoing = true;
        do
        {
          var command = string.Empty;
          ConsoleHelper.FormatSpacing(() =>
          {
            ConsoleHelper.WriteWithColor($"The door is {Door.State}. ", ConsoleColors.Informative, useSameLine: true);
            command = ChallengeHelper.GetInput<string>("What would you like to do?: ");
          }, spacesAfter: 1);
          if (string.Equals(command, "exit", StringComparison.InvariantCultureIgnoreCase))
          {
            keepGoing = false;
          }

          Door.State = TryChangeState(command);
        }
        while (keepGoing);

        ConsoleHelper.AddSpace(1);
        ShouldContinue = ChallengeHelper.GetContinuationDecision();

        ConsoleHelper.Clear();
      }
    }

    public BarrierState TryChangeState(string command)
    {
      switch (command.ToLower())
      {
        case "open":
          return Door.State == BarrierState.Closed ? BarrierState.Open : Door.State;
        case "close":
          return Door.State == BarrierState.Open ? BarrierState.Closed : Door.State;
        case "lock":
          return Door.State == BarrierState.Closed ? BarrierState.Locked : Door.State;
        case "unlock":
          var input = ChallengeHelper.GetInput<int>("Enter the passcode: ");
          ConsoleHelper.Clear();
          return CheckCodeMatch(input) ? BarrierState.Closed : Door.State;
        case "change code":
          if (Door.State != BarrierState.Open)
          {
            ConsoleHelper.WriteWithColor("Door must be open to change the code.", ConsoleColors.Warning);
            return Door.State;
          }
          var isGood = false;
          do
          {
            var code = ChallengeHelper.GetInput<int>("Enter new passcode: ");
            var verifiedCode = ChallengeHelper.GetInput<int>("Confirm new passcode: ");
            if (code != verifiedCode)
            {
              ConsoleHelper.WriteWithColor("Provided codes did not match, try again.", ConsoleColors.Warning);
              continue;
            }
            var changeCodeResult = TryChangeCode(code);
            if (changeCodeResult.Succeeded)
            {
              ConsoleHelper.Clear();
              ConsoleHelper.WriteWithColor("Passcode successfully changed.", ConsoleColors.Favorable);
              isGood = true;
            }
          }
          while (!isGood);
          return Door.State;
        default: return Door.State;
      }
    }

    private bool CheckCodeMatch(int codeToMatch) => codeToMatch == Door.Code;
    public ChangeCodeResult TryChangeCode(int newCode)
    {
      if (CheckCodeMatch(newCode))
      {
        return new ChangeCodeResult { Succeeded = false, FailureReason = "New code cannot be the same as the old code" };
      }
      Door.Code = newCode;
      return new ChangeCodeResult { Succeeded = true };
    }
  }
}

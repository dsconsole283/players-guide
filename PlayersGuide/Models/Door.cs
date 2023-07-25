using PlayersGuide.Helpers;
using static PlayersGuide.Models.Enums;

namespace PlayersGuide.Models
{
  public class Door
  {
    public BarrierState DoorState { get; set; }
    private int _code;
    public Door(int initalCode)
    {
      _code = initalCode;
      DoorState = BarrierState.Locked;
    }

    private bool CheckCodeMatch(int codeToMatch) => codeToMatch == _code;
    public ChangeCodeResult TryChangeCode(int newCode)
    {
      if (CheckCodeMatch(newCode))
      {
        return new ChangeCodeResult { Succeeded = false, FailureReason = "New code cannot be the same as the old code" };
      }
      _code = newCode;
      return new ChangeCodeResult { Succeeded = true };
    }

    public BarrierState TryChangeState(string command)
    {
      switch (command.ToLower())
      {
        case "open":
          return DoorState == BarrierState.Closed ? BarrierState.Open : DoorState;
        case "close":
          return DoorState == BarrierState.Open ? BarrierState.Closed : DoorState;
        case "lock":
          return DoorState == BarrierState.Closed ? BarrierState.Locked : DoorState;
        case "unlock":
          var input = ChallengeHelper.GetInput<int>("Enter the passcode: ");
          ConsoleHelper.Clear();
          return CheckCodeMatch(input) ? BarrierState.Closed : DoorState;
        case "change code":
          if (DoorState != BarrierState.Open)
          {
            ConsoleHelper.WriteWithColor("Door must be open to change the code.", ConsoleColors.Warning);
            return DoorState;
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
          return DoorState;
        default: return DoorState;
      }
    }
  }
}

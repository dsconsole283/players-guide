using PlayersGuide.Helpers;
using PlayersGuide.Models;

namespace PlayersGuide.Challenges
{
  public class ReplicatorChallenge : ChallengeBase
  {
    public static readonly string DisplayName = "The Replicator of D'To";
    public ReplicatorChallenge() { }

    public override void Run()
    {
      while (ShouldContinue)
      {
        ConsoleHelper.Clear();

        ConsoleHelper.FormatSpacing(() => Titles.Replicator(TitleColor), spacesBefore: 1, spacesAfter: 1);

        int[] initial = new int[5];
        int[] copy = new int[5];
        for (int i = 0; i < initial.Length; i++)
        {
          initial[i] = ChallengeHelper.GetInput<int>($"Enter the integer you want to occupy position {i + 1} of the array: ");
          copy[i] = initial[i];
        }

        ConsoleHelper.FormatSpacing(() => ConsoleHelper.WriteWithColor("Replicating...", ConsoleColors.Warning), spacesBefore: 1);

        //Simulate important calculations being made...
        AwaitDelay().GetAwaiter().GetResult();

        ConsoleHelper.FormatSpacing(() =>
        {
          ConsoleHelper.WriteWithColor($"Original array:   [{initial[0],2}, {initial[1],2}, {initial[2],2}, {initial[3],2}, {initial[4],2}]", ConsoleColors.Favorable);
          ConsoleHelper.WriteWithColor($"Replicated array: [{copy[0],2}, {copy[1],2}, {copy[2],2}, {copy[3],2}, {copy[4],2}]", ConsoleColors.Favorable);
        }, spacesBefore: 1, spacesAfter: 1);

        ShouldContinue = ChallengeHelper.GetContinuationDecision();

        ConsoleHelper.Clear();
      }
    }

    private async Task AwaitDelay()
    {
      await Task.Delay(3000);
    }
  }
}

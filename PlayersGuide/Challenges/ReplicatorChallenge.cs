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

        Console.WriteLine();
        Titles.Replicator(TitleColor);
        Console.WriteLine();

        int[] initial = new int[5];
        int[] copy = new int[5];
        for (int i = 0; i < initial.Length; i++)
        {
          initial[i] = ConsoleHelper.GetInput<int>($"Enter the integer you want to occupy position {i + 1} of the array: ");
          copy[i] = initial[i];
        }

        Console.WriteLine();
        ConsoleHelper.WriteWithColor("Replicating...", ConsoleColors.Warning);
        Task.Delay(300);
        Console.WriteLine();

        ConsoleHelper.WriteWithColor($"Original array:   [{initial[0],2}, {initial[1],2}, {initial[2],2}, {initial[3],2}, {initial[4],2}]", ConsoleColors.Favorable);
        ConsoleHelper.WriteWithColor($"Replicated array: [{copy[0],2}, {copy[1],2}, {copy[2],2}, {copy[3],2}, {copy[4],2}]", ConsoleColors.Favorable);

        Console.WriteLine();
        ShouldContinue = ConsoleHelper.GetContinuationDecision();

        ConsoleHelper.Clear();
      }
    }
  }
}

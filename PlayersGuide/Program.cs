using PlayersGuide;
using PlayersGuide.Helpers;
using PlayersGuide.Models;
using System.Reflection;

class Program
{
  public static List<Type> challenges = new List<Type>();
  static void Main()
  {
    Console.Title = @"C Sharp Player's Guide Challenges";
    GetChallenges();
    int selection = 0;
    do
    {
      ConsoleHelper.Clear();
      Menu.Display();
      Console.WriteLine();
      selection = ConsoleHelper.GetInput<int>("Choice: ");

      try
      {
        StartChallenge(selection);
      }
      catch (ArgumentOutOfRangeException ex) { }
    }
    while (selection != Menu.ExitCode);
    ConsoleHelper.WriteWithColor("Exiting program..", ConsoleColors.Warning);
  }

  private static void GetChallenges()
  {
    challenges = (from type in Assembly.GetExecutingAssembly().GetTypes()
                  where type.IsClass && type.Namespace == "PlayersGuide.Challenges"
                  select type).ToList();
    challenges.RemoveAll(type => type.Name == "ChallengeBase");
  }

  static void StartChallenge(int selection)
  {
    if (selection == Menu.ExitCode) return;

    var challenge = Activator.CreateInstance(challenges[selection - 1]);
    if (challenge != null)
    {
      MethodInfo runMethod = challenge.GetType().GetMethod("Run");
      runMethod?.Invoke(challenge, null);
    }
  }
}

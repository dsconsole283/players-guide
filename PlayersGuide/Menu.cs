using PlayersGuide.Models;

namespace PlayersGuide
{
  public static class Menu
  {
    public static List<string> ChallengeMenuItems = GetMenuItems();
    public static int ExitCode => ChallengeMenuItems.Count();

    public static void Display()
    {
      Console.WriteLine();
      Titles.MainMenu(ConsoleColor.Red);
      Console.WriteLine();
      for (int i = 0; i < ChallengeMenuItems.Count; i++)
      {
        Console.WriteLine($"{i + 1}. {ChallengeMenuItems[i]}");
      }
    }
    public static void ShowInventory(Dictionary<string, int> inventory)
    {
      Console.WriteLine("The following items are available:");
      for (int i = 0; i < inventory.Count; i++)
      {
        Console.WriteLine($"{i + 1} - {inventory.ElementAt(i).Key}");
      }
    }
    private static List<string> GetMenuItems()
    {
      var list = new List<string>();
      if (Program.challenges.Count > 0)
      {
        foreach (var item in Program.challenges)
        {
          var fields = item.GetFields();

          var field = fields.FirstOrDefault(f => f.Name == "DisplayName");
          if (field == null) continue;

          var value = field.GetValue(null);
          list.Add(value?.ToString());
        }
      }
      else
        return new List<string>();
      list.Add("Exit");
      return list;
    }
  }
}

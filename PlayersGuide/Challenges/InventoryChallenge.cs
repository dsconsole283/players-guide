using PlayersGuide.Helpers;
using PlayersGuide.Models;

namespace PlayersGuide.Challenges
{
  public class InventoryChallenge : ChallengeBase
  {
    public static readonly string DisplayName = "The Shoppe";

    private readonly Dictionary<string, int> _inventory;
    public InventoryChallenge()
    {
      _inventory = new Dictionary<string, int>()
      {
        { "Rope", 10 },
        { "Torches", 16 },
        { "Climbing Equipment", 24 },
        { "Clean Water", 2 },
        { "Machete", 20 },
        { "Canoe", 200 },
        { "Food Supplies", 2 },
      };
    }

    public override void Run()
    {
      while (ShouldContinue)
      {
        ConsoleHelper.Clear();

        Console.WriteLine();
        Titles.ShopChallenge(TitleColor);
        Console.WriteLine();

        Menu.ShowInventory(_inventory);
Selection:
        int selection = ChallengeHelper.GetInput<int>("Which number do you want to see the price of? ");
        if (selection > _inventory.Count || selection < 1)
        {
          ConsoleHelper.WriteWithColor("Not a valid selection", ConsoleColors.Warning);
          goto Selection;
        }
        var name = ChallengeHelper.GetInput<string>("Enter your name: ");
        Console.WriteLine();
        var cost = _inventory.ElementAt(selection - 1).Value;
        if (string.Equals(name, "Beavis", StringComparison.InvariantCultureIgnoreCase))
        {
          cost /= 2;
        }
        string output = $"{_inventory.ElementAt(selection - 1).Key} cost: {cost} gold";
        ConsoleHelper.WriteWithColor(output, ConsoleColors.Favorable);
        Console.WriteLine();
        ShouldContinue = ChallengeHelper.GetContinuationDecision();

        ConsoleHelper.Clear();
      }
    }
  }
}

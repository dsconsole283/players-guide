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

        ConsoleHelper.FormatSpacing(() => Titles.ShopChallenge(TitleColor), spacesBefore: 1, spacesAfter: 1);

        Menu.ShowInventory(_inventory);
Selection:
        ConsoleHelper.AddSpace(1);
        int selection = ChallengeHelper.GetInput<int>("Which number do you want to see the price of? ");
        if (selection > _inventory.Count || selection < 1)
        {
          ConsoleHelper.WriteWithColor("Not a valid selection", ConsoleColors.Warning);
          goto Selection;
        }
        var name = ChallengeHelper.GetInput<string>("Enter your name: ");

        ConsoleHelper.AddSpace(1);

        var cost = _inventory.ElementAt(selection - 1).Value;
        if (string.Equals(name, "Beavis", StringComparison.InvariantCultureIgnoreCase))
        {
          cost /= 2;
        }
        string output = $"{_inventory.ElementAt(selection - 1).Key} cost: {cost} gold";

        ConsoleHelper.FormatSpacing(() => ConsoleHelper.WriteWithColor(output, ConsoleColors.Favorable), spacesAfter: 1);

        ShouldContinue = ChallengeHelper.GetContinuationDecision();

        ConsoleHelper.Clear();
      }
    }
  }
}

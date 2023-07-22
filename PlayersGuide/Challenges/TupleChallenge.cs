using PlayersGuide.Helpers;
using PlayersGuide.Models;
using static PlayersGuide.Models.Enums;

namespace PlayersGuide.Challenges
{
  public class TupleChallenge : ChallengeBase
  {
    public static readonly string DisplayName = "Simula's Soup";
    public (Seasoning seasoning, Ingredient ingredient, FoodType foodType) Recipe { get; set; }
    public Seasoning[] Seasonings { get; set; } = (Seasoning[])Enum.GetValues(typeof(Seasoning));
    public FoodType[] FoodTypes { get; set; } = (FoodType[])Enum.GetValues(typeof(FoodType));
    public Ingredient[] Ingredients { get; set; } = (Ingredient[])Enum.GetValues(typeof(Ingredient));
    public TupleChallenge() { }

    public override void Run()
    {
      while (ShouldContinue)
      {
        ConsoleHelper.Clear();

        ConsoleHelper.FormatSpacing(() => Titles.SimulaSoup(TitleColor), spacesBefore: 1, spacesAfter: 1);

        ChallengeHelper.GetInputFromEnum(Seasonings, out var selectedSeasoning);
        ChallengeHelper.GetInputFromEnum(Ingredients, out var selectedIngredient);
        ChallengeHelper.GetInputFromEnum(FoodTypes, out var selectedFoodType);

        Recipe = (selectedSeasoning, selectedIngredient, selectedFoodType);

        ConsoleHelper.FormatSpacing(() => ConsoleHelper.WriteWithColor($"You made {Recipe.seasoning} {Recipe.ingredient} {Recipe.foodType}", ConsoleColors.Favorable), spacesBefore: 1, spacesAfter: 1);

        ShouldContinue = ChallengeHelper.GetContinuationDecision();

        ConsoleHelper.Clear();
      }
    }
  }
}

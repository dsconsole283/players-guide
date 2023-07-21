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

        Console.WriteLine();
        Titles.SimulaSoup(TitleColor);
        Console.WriteLine();

        var userInput = string.Empty;

GetSeasoning:
        GetRecipeByType(Seasonings, out var selectedSeasoning, out var isValidSeasoning, out userInput);
        if (!isValidSeasoning)
        {
          ConsoleHelper.WriteWithColor($"'{userInput}'", ConsoleColors.Informative, true);
          ConsoleHelper.WriteWithColor(" doesn't seem to be available, please choose again..", ConsoleColors.Warning);
          goto GetSeasoning;
        }

GetIngredient:
        GetRecipeByType(Ingredients, out var selectedIngredient, out var isValidIngredient, out userInput);
        if (!isValidIngredient)
        {
          ConsoleHelper.WriteWithColor($"'{userInput}'", ConsoleColors.Informative, true);
          ConsoleHelper.WriteWithColor(" doesn't seem to be available, please choose again..", ConsoleColors.Warning);
          goto GetIngredient;
        }

GetFoodType:
        GetRecipeByType(FoodTypes, out var selectedFoodType, out var isValidFoodType, out userInput);
        if (!isValidFoodType)
        {
          ConsoleHelper.WriteWithColor($"'{userInput}'", ConsoleColors.Informative, true);
          ConsoleHelper.WriteWithColor(" doesn't seem to be available, please choose again..", ConsoleColors.Warning);
          goto GetFoodType;
        }

        Recipe = (selectedSeasoning, selectedIngredient, selectedFoodType);

        Console.WriteLine();
        ConsoleHelper.WriteWithColor($"You made {Recipe.seasoning} {Recipe.ingredient} {Recipe.foodType}", ConsoleColors.Favorable);

        Console.WriteLine();
        ShouldContinue = ConsoleHelper.GetContinuationDecision();

        ConsoleHelper.Clear();
      }
    }

    private void GetRecipeByType<T>(T[] neededRecipeItem, out T? selectedItem, out bool isValid, out string originalInput) where T : Enum
    {
      var outputItem = default(T);
      var outputBool = false;
      var arrayType = neededRecipeItem.GetType();
      var name = arrayType.GetElementType().Name;
      Console.WriteLine();
      Console.WriteLine($"Available {name}s");
      foreach (var item in neededRecipeItem)
      {
        ConsoleHelper.WriteWithColor($"{item}", ConsoleColors.Informative);
      }

      Console.WriteLine();
      var input = ConsoleHelper.GetInput<string>($"Which {name?.ToLower()} do you choose?: ");
      if (typeof(T) == typeof(Seasoning))
      {
        if (Enum.TryParse<Seasoning>(input, true, out var value))
        {
          outputItem = (T)(object)value;
          outputBool = true;
        }
      }
      if (typeof(T) == typeof(Ingredient))
      {
        if (Enum.TryParse<Ingredient>(input, true, out var value))
        {
          outputItem = (T)(object)value;
          outputBool = true;
        }
      }
      if (typeof(T) == typeof(FoodType))
      {
        if (Enum.TryParse<FoodType>(input, true, out var value))
        {
          outputItem = (T)(object)value;
          outputBool = true;
        }
      }
      selectedItem = outputItem;
      isValid = outputBool;
      originalInput = input;
    }
  }
}

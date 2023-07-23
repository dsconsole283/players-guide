namespace PlayersGuide.Models
{
  public static class Enums
  {
    public enum BoxState
    {
      Open,
      Closed,
      Locked
    }

    public enum FoodType
    {
      Soup,
      Stew,
      Gumbo
    }

    public enum Ingredient
    {
      Mushroom,
      Chicken,
      Carrot,
      Potatoe
    }

    public enum Seasoning
    {
      Spicy,
      Salty,
      Sweet
    }

    public enum ArrowHead
    {
      Steel = 10,
      Wood = 3,
      Obsidian = 5
    }

    public enum Fletching
    {
      Plastic = 10,
      TurkeyFeathers = 5,
      GooseFeathers = 3
    }

    public enum GenericArrow
    {
      Beginner,
      Marksman,
      Elite
    }
  }
}

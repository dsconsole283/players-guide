namespace PlayersGuide.Challenges
{
  public abstract class ChallengeBase
  {
    public bool ShouldContinue { get; set; } = true;
    public ConsoleColor TitleColor { get; }

    public ChallengeBase()
    {
      ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
      var blackIndex = Array.IndexOf(colors, ConsoleColor.Black);
      var restrictedColorSet = colors.Where(color => color != ConsoleColor.Black).ToArray();
      TitleColor = restrictedColorSet[new Random().Next(restrictedColorSet.Length)];
    }
    public abstract void Run();
  }
}

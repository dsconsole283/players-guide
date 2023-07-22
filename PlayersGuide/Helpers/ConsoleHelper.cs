namespace PlayersGuide.Helpers
{
  public static class ConsoleHelper
  {
    public static void WriteWithColor(string message, ConsoleColor color, bool useSameLine = false)
    {
      Console.ForegroundColor = color;
      if (useSameLine)
        Console.Write(message);
      else
        Console.WriteLine(message);
      Console.ForegroundColor = ConsoleColor.White;
    }

    public static void Clear() => Console.Clear();

    public static void FormatSpacing(Action action, int spacesBefore = 0, int spacesAfter = 0)
    {
      for (int i = 0; i < spacesBefore; i++)
      {
        Console.WriteLine();
      }
      action();
      for (int i = 0; i < spacesAfter; i++)
      {
        Console.WriteLine();
      }
    }

    public static void AddSpace(int spaces)
    {
      for (int i = 0; i < spaces; i++)
      {
        Console.WriteLine();
      }
    }
  }
}

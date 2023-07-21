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
  }
}

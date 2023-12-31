﻿using PlayersGuide.Helpers;
using PlayersGuide.Models;

namespace PlayersGuide.Challenges
{
  public class MagicCannonChallenge : ChallengeBase
  {
    public static readonly string DisplayName = "The Magic Cannon";

    public MagicCannonChallenge() { }

    public override void Run()
    {
      while (ShouldContinue)
      {
        ConsoleHelper.Clear();

        ConsoleHelper.FormatSpacing(() => Titles.MagicCannon(TitleColor), spacesBefore: 1, spacesAfter: 1);

        ConsoleHelper.WriteWithColor("Press any key to fire the cannon!", ConsoleColors.Inquisitive);
        Console.ReadKey();
        ConsoleHelper.Clear();

        for (int i = 1; i <= 100; i++)
        {
          string result = "Normal";
          bool fire = i % 3 == 0;
          bool electric = i % 5 == 0;
          if (fire && electric)
          {
            Console.ForegroundColor = ConsoleColor.Blue;
            result = "Fire and Electric";
          }
          else if (fire)
          {
            Console.ForegroundColor = ConsoleColor.Red;
            result = "Fire";
          }
          else if (electric)
          {
            Console.ForegroundColor = ConsoleColor.Yellow;
            result = "Electric";
          }
          else
            Console.ForegroundColor = ConsoleColor.White;
          ConsoleHelper.FormatSpacing(() => Console.WriteLine($"{i}: {result}"), spacesAfter: 1);
        }

        ShouldContinue = ChallengeHelper.GetContinuationDecision();

        ConsoleHelper.Clear();
      }
    }
  }
}


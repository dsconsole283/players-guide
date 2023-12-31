﻿using PlayersGuide.Helpers;
using PlayersGuide.Models;

namespace PlayersGuide.Challenges
{
  public class ManticoreChallenge : ChallengeBase
  {
    public static readonly string DisplayName = "Manticore Hunt";

    private static readonly int CityHealthMax = 15;
    private static readonly int ManticoreHealthMax = 10;
    private static readonly int PositionMax = 1000;
    private static readonly int PositionMin = 0;
    public int ManticorePosition { get; set; }

    public ManticoreChallenge() { }

    public override void Run()
    {
      while (ShouldContinue)
      {
        ConsoleHelper.Clear();

        ConsoleHelper.FormatSpacing(() => Titles.ManticoreHunt(TitleColor), spacesBefore: 1, spacesAfter: 2);

GetManticorePosition:
        ManticorePosition = ChallengeHelper.GetInput<int>($"Player 1, how far away from the city do you want to station the Manticore? (between {PositionMin} and {PositionMax}): ");
        if (ManticorePosition < PositionMin || ManticorePosition > PositionMax)
        {
          ConsoleHelper.WriteWithColor("Please stay within the allowed range. Try again.. ", ConsoleColors.Warning);
          goto GetManticorePosition;
        }

        ConsoleHelper.Clear();

        var cityHealth = CityHealthMax;
        var manticoreHealth = ManticoreHealthMax;

        ConsoleHelper.FormatSpacing(() => Console.WriteLine("Player 2, it is your turn."), spacesBefore: 1);

        int round = 1;

        while (manticoreHealth > 0 && cityHealth > 0)
        {
          var strength = DetermineShotStrength(round);

          ConsoleHelper.FormatSpacing(() => Console.WriteLine(@"-------------------------------------------------------------"), spacesBefore: 1);
          ConsoleHelper.WriteWithColor($"STATUS: Round: {round}  City: {cityHealth}/{CityHealthMax}  Manticore: {manticoreHealth}/{ManticoreHealthMax}", ConsoleColors.Favorable);
          ConsoleHelper.WriteWithColor($"The cannon is expected to deal {strength} damage this round.", ConsoleColor.Red);

          var shot = ChallengeHelper.GetInput<int>("Enter desired cannon range: ");
          var shotResult = GetShotResult(shot);

          Console.Write("That round ");
          ConsoleHelper.WriteWithColor(shotResult == -1 ? "FELL SHORT of the target" : shotResult == 1 ? "OVERSHOT the target" : "was a DIRECT HIT!", ConsoleColors.Informative);

          cityHealth--;
          manticoreHealth = shotResult == 0 ? manticoreHealth - strength : manticoreHealth;
          round = round + 1;
        }

        ConsoleHelper.FormatSpacing(() =>
        {
          if (manticoreHealth <= 0)
            ConsoleHelper.WriteWithColor("The Manticore has been destroyed! The city of Consolas has been saved!", ConsoleColors.Favorable);
          else
            ConsoleHelper.WriteWithColor("The city has been destroyed, you suck.", ConsoleColors.Warning);
        }, spacesBefore: 1, spacesAfter: 1);

        ShouldContinue = ChallengeHelper.GetContinuationDecision();

        ConsoleHelper.Clear();
      }
    }

    private int GetShotResult(int shot) => shot == ManticorePosition ? 0 : shot > ManticorePosition ? 1 : -1;

    private int DetermineShotStrength(int round)
    {
      bool fire = round % 3 == 0;
      bool electric = round % 5 == 0;
      return (fire || electric) ? 3 : (fire && electric) ? 10 : 1;
    }
  }
}

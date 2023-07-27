using PlayersGuide.Helpers;
using PlayersGuide.Models;
using System.Text.Json;
using static PlayersGuide.Models.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PlayersGuide.Challenges
{
  public class PaperRockScissorsChallenge : ChallengeBase
  {
    public static readonly string DisplayName = "Paper Rock Scissors";
    public static readonly string DataFolder = @"F:\Practice Repos\PlayersGuide\PlayersGuide\Data\";
    public static readonly string FileName = "historical_win_data.json";
    public static readonly string FilePath = Path.Combine(DataFolder, FileName);
    public int FirstPlayerTotalWins { get; private set; }
    public int SecondPlayerTotalWins { get; private set; }
    public PaperRockScissorsChallenge()
    {
      GetWinData();
    }

    public override void Run()
    {
      while (ShouldContinue)
      {
        ConsoleHelper.Clear();

        //ConsoleHelper.FormatSpacing(() => Titles.Simula(TitleColor), spacesBefore: 1, spacesAfter: 1);

        ConsoleHelper.WriteWithColor($"Player 1 total wins: {FirstPlayerTotalWins}", ConsoleColors.Informative);
        ConsoleHelper.WriteWithColor($"Player 2 total wins: {SecondPlayerTotalWins}", ConsoleColors.Informative);

        ConsoleHelper.AddSpace(1);

        var player1Input = ParseInput(ChallengeHelper.GetMaskedInput(prompt: "Player One, what do you throw?: "));
        var player2Input = ParseInput(ChallengeHelper.GetMaskedInput(prompt: "Player Two, what do you throw?: "));

        AwaitDelay(500).GetAwaiter().GetResult();
        ConsoleHelper.WriteWithColor("One....", ConsoleColors.Favorable);

        AwaitDelay(1000).GetAwaiter().GetResult();
        ConsoleHelper.WriteWithColor("Two....", ConsoleColors.Informative);

        AwaitDelay(1000).GetAwaiter().GetResult();
        ConsoleHelper.WriteWithColor("THREE....", ConsoleColors.Warning);

        ConsoleHelper.FormatSpacing(() =>
        {
          ConsoleHelper.WriteWithColor($"Player 1 threw {player1Input}.", ConsoleColors.Informative);
          ConsoleHelper.WriteWithColor($"Player 2 threw {player2Input}.", ConsoleColors.Informative);
        }, spacesBefore: 1, spacesAfter: 1);


        var winner = DetermineWinner(player1Input, player2Input);
        ProcessResult(winner);
        SaveWinData();

        ConsoleHelper.WriteWithColor(winner == 0 ? "It was a tie.." : $"Player {winner} wins!", ConsoleColors.Informative);

        ConsoleHelper.AddSpace(1);
        ShouldContinue = ChallengeHelper.GetContinuationDecision();

        ConsoleHelper.Clear();
      }
    }

    private void GetWinData()
    {
      if (!File.Exists(FilePath))
      {
        try
        {
          HistoricalWinData newHistoricalData = new HistoricalWinData
          {
            PlayerOneWins = FirstPlayerTotalWins,
            PlayerTwoWins = SecondPlayerTotalWins
          };

          string jsonData = JsonSerializer.Serialize(newHistoricalData);

          File.WriteAllText(FilePath, jsonData);

          ConsoleHelper.WriteWithColor("File created and initialized with default data: " + FilePath, ConsoleColors.Favorable);
        }
        catch (IOException ex)
        {
          ConsoleHelper.WriteWithColor("Error creating or writing to the file: " + ex.Message, ConsoleColors.Warning);
        }
        catch (JsonException ex)
        {
          ConsoleHelper.WriteWithColor("Error serializing the JSON: " + ex.Message, ConsoleColors.Warning);
        }
      }
      else
      {
        try
        {
          string jsonData = File.ReadAllText(FilePath);

          if (!string.IsNullOrWhiteSpace(jsonData))
          {
            HistoricalWinData historicalData = JsonSerializer.Deserialize<HistoricalWinData>(jsonData)!;
            FirstPlayerTotalWins += historicalData.PlayerOneWins;
            SecondPlayerTotalWins += historicalData.PlayerTwoWins;
          }
        }
        catch (IOException ex)
        {
          ConsoleHelper.WriteWithColor("Error reading the file: " + ex.Message, ConsoleColors.Warning);
        }
        catch (JsonException ex)
        {
          ConsoleHelper.WriteWithColor("Error deserializing the JSON: " + ex.Message, ConsoleColors.Warning);
        }
      }
    }

    private void SaveWinData()
    {
      try
      {
        HistoricalWinData newHistoricalData = new HistoricalWinData
        {
          PlayerOneWins = FirstPlayerTotalWins,
          PlayerTwoWins = SecondPlayerTotalWins
        };

        string jsonData = JsonSerializer.Serialize(newHistoricalData);

        File.WriteAllText(FilePath, jsonData);
      }
      catch (IOException ex)
      {
        ConsoleHelper.WriteWithColor("Error creating or writing to the file: " + ex.Message, ConsoleColors.Warning);
      }
      catch (JsonException ex)
      {
        ConsoleHelper.WriteWithColor("Error serializing the JSON: " + ex.Message, ConsoleColors.Warning);
      }
    }

    private async Task AwaitDelay(int delay)
    {
      await Task.Delay(delay);
    }

    private RPS ParseInput(string input)
    {
      return input.ToLower() switch
      {
        var rockInput when rockInput.Contains("ro", StringComparison.OrdinalIgnoreCase) => RPS.Rock,
        var paperInput when paperInput.Contains("pa", StringComparison.OrdinalIgnoreCase) => RPS.Paper,
        var scissorsInput when scissorsInput.Contains("sc", StringComparison.OrdinalIgnoreCase) => RPS.Scissors,
        _ => RPS.Unknown
      };
    }

    private int DetermineWinner(RPS player1, RPS player2)
    {
      if (player1 == player2) return 0;
      return player1 switch
      {
        RPS.Paper => player2 == RPS.Scissors ? 2 : 1,
        RPS.Rock => player2 == RPS.Paper ? 2 : 1,
        RPS.Scissors => player2 == RPS.Rock ? 2 : 1,
        _ => player2 != RPS.Unknown ? 2 : 0
      };
    }

    private void ProcessResult(int winner)
    {
      if (winner == 1)
      {
        FirstPlayerTotalWins++;
      }
      else if (winner == 2)
      {
        SecondPlayerTotalWins++;
      }
    }
  }
}

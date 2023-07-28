using PlayersGuide.API;
using PlayersGuide.Helpers;
using PlayersGuide.Models;
using System.Text.RegularExpressions;

namespace PlayersGuide.Challenges
{
  public class HangmanChallenge : ChallengeBase
  {
    public static readonly string DisplayName = "Hangman";
    private static readonly RandomWordsManager _wordManager = new();
    private string _wordToGuess = string.Empty;
    private string _maskedWord = string.Empty;
    private static int _minWordLength = 4;
    private static int _maxWordLength = 15;
    public int WordLength { get; set; }
    public List<string> GuessedLetters { get; set; } = new();
    public string IncorrectLetters { get; set; } = string.Empty;
    public int NumberOfGuessesRemaing { get; set; } = 10;
    public int NumberOfLettersRemaing { get; set; }
    public HangmanChallenge()
    {
    }

    public override void Run()
    {
      while (ShouldContinue)
      {
        ConsoleHelper.Clear();
        ResetGame();
//ConsoleHelper.FormatSpacing(() => Titles.Freach(TitleColor), spacesBefore: 1, spacesAfter: 1);
GetWordLength:
        WordLength = ChallengeHelper.GetInput<int>("How long should the Hangman word be?: ");
        if (WordLength < _minWordLength || WordLength > _maxWordLength)
        {
          ConsoleHelper.WriteWithColor($"Word length should be between {_minWordLength} and {_maxWordLength}. Try again..", ConsoleColors.Warning);
          goto GetWordLength;
        }

        var wordResponse = GetWordAwaiter().GetAwaiter().GetResult();
        _wordToGuess = wordResponse.Word!;
        _maskedWord = MaskWord(_wordToGuess);
        var wonGame = false;
        var letterGuess = string.Empty;
        do
        {
          ConsoleHelper.WriteWithColor($"Word: {_maskedWord} | Remaining: {NumberOfGuessesRemaing} | Incorrect {IncorrectLetters.ToUpper()} | Guess: {letterGuess}", ConsoleColors.Informative);
GetLetter:
          letterGuess = ChallengeHelper.GetInput<string>("Guess a letter: ").ToLower();
          var regex = new Regex(@"^[a-zA-Z]$");
          if (!regex.IsMatch(letterGuess))
          {
            ConsoleHelper.WriteWithColor($"Only one letter of the alphabet, please. Try again..", ConsoleColors.Warning);
            goto GetLetter;
          }
          if (GuessedLetters.Contains(letterGuess))
          {
            ConsoleHelper.WriteWithColor("You already guessed that letter. Try again..", ConsoleColors.Warning);
            goto GetLetter;
          }

          GuessedLetters.Add(letterGuess);
          CheckGuess(letterGuess);


          if (NumberOfLettersRemaing == 0)
          {
            wonGame = true;
            break;
          }
        }
        while (NumberOfGuessesRemaing > 0);

        ConsoleHelper.WriteWithColor(wonGame ? "Congrats!! You won!!" : "Waahh WAAAAHHHHH", wonGame ? ConsoleColors.Favorable : ConsoleColors.Warning);
        Console.WriteLine(_wordToGuess);
        ShouldContinue = ChallengeHelper.GetContinuationDecision();

        ConsoleHelper.Clear();
      }
    }

    private string MaskWord(string word)
    {
      string output = string.Empty;
      for (int i = 0; i < word.Length; i++)
      {
        output += "_";
      }

      return output;
    }

    private void CheckGuess(string letter)
    {
      if (!_wordToGuess.Contains(letter))
      {
        IncorrectLetters += letter;
        NumberOfGuessesRemaing--;
        return;
      }
      var array = _maskedWord.ToArray();
      var letterToInsert = char.Parse(letter);
      int index = _wordToGuess.IndexOf(letter);
      while (index > -1)
      {
        array[index] = letterToInsert;
        _maskedWord = new string(array).ToUpper();
        index = _wordToGuess.IndexOf(letter, index + 1);
      }

      var countOfBlanks = 0;
      foreach (var i in _maskedWord)
      {
        if (i == '_')
        {
          countOfBlanks++;
        }
      }
      NumberOfLettersRemaing = countOfBlanks;

    }

    private async Task<Hangman> GetWordAwaiter()
    {
      return await _wordManager.GetRandomWord(WordLength);
    }

    private void ResetGame()
    {
      _wordToGuess = string.Empty;
      _maskedWord = string.Empty;
      GuessedLetters = new();
      IncorrectLetters = string.Empty;
      NumberOfGuessesRemaing = 10;
      NumberOfLettersRemaing = int.MaxValue;
    }
  }
}

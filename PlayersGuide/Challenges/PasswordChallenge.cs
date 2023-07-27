using PlayersGuide.Helpers;
using PlayersGuide.Models;
using static PlayersGuide.Models.Enums;

namespace PlayersGuide.Challenges
{
  public class PasswordChallenge : ChallengeBase
  {
    public static readonly string DisplayName = "The Password Validator";
    private static readonly char[] _illegalChars = { 'T', '&' };
    private const int _maxLength = 13;
    private const int _minLength = 6;
    public PasswordChallenge() { }

    public override void Run()
    {
      while (ShouldContinue)
      {
        ConsoleHelper.Clear();

//ConsoleHelper.FormatSpacing(() => Titles.Simula(TitleColor), spacesBefore: 1, spacesAfter: 1);

GetInput:
        var password = ChallengeHelper.GetInput<string>("Enter the password you would like to be validated: ");
        var hasUpper = false;
        var hasLower = false;
        var hasNumber = false;
        if (password.Length < _minLength || password.Length > _maxLength)
        {
          ConsoleHelper.WriteWithColor($"Your password doesn't meet the length requirements. Password must be between {_minLength} and {_maxLength} characters.", ConsoleColors.Warning);
          goto GetInput;
        }
        if (password.Any(p => _illegalChars.Contains(p)))
        {
          ConsoleHelper.WriteWithColor($"Your password contains an illegal character. The current set of illegal characters is: ", ConsoleColors.Warning);
          for (int i = 0; i < _illegalChars.Length; i++)
          {
            ConsoleHelper.WriteWithColor($"{_illegalChars[i]}", ConsoleColors.Informative);
          }
          goto GetInput;
        }

        foreach (char c in password)
        {
          hasUpper = hasUpper == true ? true : char.IsUpper(c);
          hasLower = hasLower == true ? true : char.IsLower(c);
          hasNumber = hasNumber == true ? true : char.IsNumber(c);
        }

        var metRequirements = hasUpper && hasLower && hasNumber;
        if (!metRequirements)
        {
          ConsoleHelper.WriteWithColor($"Your password does not meet the standards. Must contain an upper and lower-cased letter and a number.", ConsoleColors.Warning);
          goto GetInput;
        }
        ConsoleHelper.WriteWithColor($"Your password of {password} passes validation, congrats.", ConsoleColors.Favorable);


        ConsoleHelper.AddSpace(1);
        ShouldContinue = ChallengeHelper.GetContinuationDecision();

        ConsoleHelper.Clear();
      }
    }
  }
}

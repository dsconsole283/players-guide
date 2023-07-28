using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlayersGuide.Models;

namespace PlayersGuide.API
{
  internal class RandomWordsManager
  {
    private const string _address = @"https://random-word-api.herokuapp.com/";
    public RandomWordsManager()
    {
    }

    public async Task<Hangman> GetRandomWord(int length)
    {
      var word = new Hangman();
      using (var httpClient = new HttpClient())
      {
        try
        {
          var response = await httpClient.GetStringAsync($"{_address}word?length={length}");
          JArray array = JArray.Parse(response);
          word.Word = array[0].ToString();
        }
        catch (HttpRequestException ex)
        {
          Console.WriteLine("Error making the request: " + ex.Message);
        }
        catch (JsonException ex)
        {
          Console.WriteLine("Error parsing the JSON response: " + ex.Message);
        }
        return word ?? new Hangman { Word = "Failed" };
      }
    }
  }
}

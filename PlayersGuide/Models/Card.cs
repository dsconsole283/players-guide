using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PlayersGuide.Models.Enums;

namespace PlayersGuide.Models
{
  public class Card
  {
    public CardColor CardColor { get; init; }
    public CardRank CardRank { get; init; }
    public string Name { get; private set; }

    public Card(CardColor color, CardRank rank)
    {
      CardColor = color;
      CardRank = rank;
      Name = $"The {CardColor} {CardRank}";
    }

    public CardType GetCardType(Card card) => (int)card.CardRank <= 10 ? CardType.Number : CardType.Symbol;
  }
}

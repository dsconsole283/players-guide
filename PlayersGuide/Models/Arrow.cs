using static PlayersGuide.Models.Enums;

namespace PlayersGuide.Models
{
  public class Arrow
  {
    public ArrowHead ArrowHead { get; init; }
    public Fletching Fletching { get; init; }
    public int Length { get; private set; }

    public Arrow() { }
    public Arrow(ArrowHead arrowHead, Fletching fletching, int length)
    {
      if (length < 60) throw new ArgumentOutOfRangeException("length");
      if (length > 100) throw new ArgumentOutOfRangeException("length");
      ArrowHead = arrowHead;
      Fletching = fletching;
      Length = length;
    }

    public float GetCost() => (int)ArrowHead + (int)Fletching + Length * .05F;
  }
}

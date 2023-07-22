using static PlayersGuide.Models.Enums;

namespace PlayersGuide.Models
{
  public class Arrow
  {
    public ArrowHead _arrowHead;
    public int _length;
    public Fletching _fletching;

    public Arrow() { }
    public Arrow(int length)
    {
      if (length < 60) throw new ArgumentOutOfRangeException("length");
      if (length > 100) throw new ArgumentOutOfRangeException("length");
    }

    public float GetCost() => (int)_arrowHead + (int)_fletching + _length * .05F;
  }
}

using static PlayersGuide.Models.Enums;

namespace PlayersGuide.Models
{
  public class Arrow
  {
    private ArrowHead _arrowHead;
    private int _length;
    private Fletching _fletching;

    public Arrow() { }
    public Arrow(ArrowHead arrowHead, int length, Fletching fletching)
    {
      if (length < 60) throw new ArgumentOutOfRangeException("length");
      if (length > 100) throw new ArgumentOutOfRangeException("length");
      _arrowHead = arrowHead;
      _fletching = fletching;
      _length = length;
    }

    public float GetCost() => (int)_arrowHead + (int)_fletching + _length * .05F;
  }
}

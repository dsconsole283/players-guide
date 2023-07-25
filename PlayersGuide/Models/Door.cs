using static PlayersGuide.Models.Enums;

namespace PlayersGuide.Models
{
  public class Door
  {
    public BarrierState State { get; set; }
    public int Code { get; set; }
    public Door(int initalCode)
    {
      Code = initalCode;
      State = BarrierState.Locked;
    }
  }
}

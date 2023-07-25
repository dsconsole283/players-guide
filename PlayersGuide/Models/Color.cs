namespace PlayersGuide.Models
{
  public class Color
  {
    private int _redValue;
    private int _greenValue;
    private int _blueValue;
    public int RedValue
    {
      get => _redValue;
      set { _redValue = value > _maxValue ? 255 : value < _minValue ? 0 : value; }
    }
    public int GreenValue
    {
      get => _greenValue;
      set { _greenValue = value > _maxValue ? 255 : value < _minValue ? 0 : value; }
    }
    public int BlueValue
    {
      get => _blueValue;
      set { _blueValue = value > _maxValue ? 255 : value < _minValue ? 0 : value; }
    }

    public static Color Green => new Color(redValue: 0, greenValue: 128, blueValue: 0);
    public static Color White => new Color(redValue: 255, greenValue: 255, blueValue: 255);
    public static Color Black => new Color(redValue: 0, greenValue: 0, blueValue: 0);
    public static Color Red => new Color(redValue: 255, greenValue: 0, blueValue: 0);
    public static Color Orange => new Color(redValue: 255, greenValue: 165, blueValue: 0);
    public static Color Yellow => new Color(redValue: 255, greenValue: 255, blueValue: 0);
    public static Color Blue => new Color(redValue: 0, greenValue: 0, blueValue: 255);
    public static Color Purple => new Color(redValue: 128, greenValue: 0, blueValue: 128);

    private const int _maxValue = 255;
    private const int _minValue = 0;

    public Color() { }
    public Color(int redValue, int greenValue, int blueValue)
    {
      RedValue = redValue;
      GreenValue = greenValue;
      BlueValue = blueValue;
    }
  }
}


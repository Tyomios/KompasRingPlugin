namespace Model;

/// <summary>
/// Гравировка.
/// </summary>
public class Engraving
{
    private string _text;

    public string Text
    {
        get => _text;
        set
        {
            _text = value;
        }
    }

    private uint _textSize;

    public uint TextSize
    {
        get => _textSize;
        set
        {
            _textSize = value;
        }
    }

    private float _height;

    public float Height
    {
        get => _height;
        set
        {
            _height = value;
        }
    }
}
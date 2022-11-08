namespace Model;

/// <summary>
/// Гравировка.
/// </summary>
public class Engraving
{
    private string _text;

    /// <summary>
    /// Возвращает или задает текст гравировки.
    /// </summary>
    public string Text
    {
        get => _text;
        set
        {
            _text = value;
        }
    }

    private uint _textSize;

    /// <summary>
    /// Возвращает или задает размер текста гравировки.
    /// </summary>
    public uint TextSize
    {
        get => _textSize;
        set
        {
            _textSize = value;
        }
    }

    private float _height;

    /// <summary>
    /// Возвращает или задает высоту эскиза с гравировкой.
    /// </summary>
    public float Height
    {
        get => _height;
        set
        {
            _height = value;
        }
    }
}
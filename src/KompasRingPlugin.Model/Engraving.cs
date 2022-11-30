using String = System.String;

namespace Model;

/// <summary>
/// Гравировка.
/// </summary>
public class Engraving
{
    /// <summary>
    /// Создает экземпляр класса <see cref="Engraving"/>>.
    /// </summary>
    public Engraving()
    {
        Text = String.Empty;
        TextSize = 4;
        Height = 2;
    }

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
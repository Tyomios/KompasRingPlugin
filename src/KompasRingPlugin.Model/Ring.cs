using Color = System.Windows.Media.Color;

namespace Model;

/// <summary>
/// Кольцо.
/// </summary>
public class Ring
{
    private float _width;

    /// <summary>
    /// Возвращает или задает ширину кольца.
    /// </summary>
    public float Width
    {
        get => _width;
        set
        {
            _width = value;
        }
    }


    private float _height;

    /// <summary>
    /// Возвращает или задает толщину кольца.
    /// </summary>
    public float Height
    {
        get => _height;
        set
        {
            _height = value;
        }
    }


    private float _radius;

    /// <summary>
    /// Возвращает или задает радиус кольца.
    /// </summary>
    public float Radius
    {
        get => _radius;
        set
        {
            _radius = value;
        }
    }

    private uint _roundScale;

    /// <summary>
    /// Возвращает или задает угол скругления граней кольца.
    /// </summary>
    public uint RoundScale
    {
        get => _roundScale;
        set
        {
            _roundScale = value;
        }
    }

    private Engraving _engraving;

    /// <summary>
    /// Возвращает или задает гравировку кольца.
    /// </summary>
    public Engraving Engraving
    {
        get => _engraving;
        set
        {
            _engraving = value;
        }
    }

    private Color _color;

    /// <summary>
    /// Возвращает или задает цвет кольца.
    /// </summary>
    public Color Color
    {
        get => _color;
        set
        {
            _color = value;
        }
    }

    /// <summary>
    /// Проверка кольца на определения всех параметров для построения
    /// и их соответствия установленным ограничениям.
    /// </summary>
    /// <param name="ring">Проверяемое кольцо.</param>
    /// <returns>
    /// @retval true - кольцо готово к построению.
    /// @retval false - кольцо не готово к построению.
    /// </returns>
    public static bool IsReadyForBuild(Ring ring)
    {
        if (ring.Width.Equals(0) 
            || ring.Height.Equals(0) 
            || ring.Radius.Equals(0))
        {
            return false;
        }

        return true;
    }
}

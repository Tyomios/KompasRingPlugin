using Color = System.Windows.Media.Color;

namespace Model;

/// <summary>
/// Кольцо.
/// </summary>
public class Ring
{
    /// <summary>
    /// Создает экземпляр класса <see cref="Ring"/>.
    /// </summary>
    public Ring()
    {
        Engraving = new Engraving();
    }

    /// <summary>
    /// Возвращает или задает ширину кольца.
    /// </summary>
    public float Width { get; set; }


    /// <summary>
    /// Возвращает или задает толщину кольца.
    /// </summary>
    public float Height { get; set; }


    /// <summary>
    /// Возвращает или задает радиус кольца.
    /// </summary>
    public float Radius { get; set; }

    /// <summary>
    /// Возвращает или задает угол скругления граней кольца.
    /// </summary>
    public uint RoundScale { get; set; }

    /// <summary>
    /// Возвращает или задает гравировку кольца.
    /// </summary>
    public Engraving Engraving { get; set; }

    /// <summary>
    /// Возвращает или задает цвет кольца.
    /// </summary>
    public Color Color { get; set; }

    /// <summary>
    /// Возвращает или задает угол выреза по окружности кольца.
    /// </summary>
    public uint JewelryAngle { get; set; }

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
        if (ring is null)
        {
            return false;
        }
        if (ring.Width.Equals(0) 
            || ring.Height.Equals(0) 
            || ring.Radius.Equals(0))
        {
            return false;
        }

        return true;
    }
}

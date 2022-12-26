namespace KompasRingPlugin;

/// <summary>
/// Действие в построении кольца.
/// </summary>
public enum ActionType
{
    /// <summary>
    /// Текст гравировки.
    /// </summary>
    EngravingText,

    /// <summary>
    /// Кегль гравировки.
    /// </summary>
    TextSize,

    /// <summary>
    /// Глубина гравировки.
    /// </summary>
    EngravingWidth,

    /// <summary>
    /// Ширина кольца.
    /// </summary>
    RingHeight,

    /// <summary>
    /// Глубина кольца.
    /// </summary>
    RingWidth,

    /// <summary>
    /// Размер кольца.
    /// </summary>
    RingSize,

    /// <summary>
    /// Угол скругления кольца.
    /// </summary>
    RoundScale
}
namespace Core;

/// <summary>
/// Тип операции компаса.
/// </summary>
public enum KompasEntityType
{
    /// <summary>
    /// Вся деталь.
    /// </summary>
    TopPart = -1,

    /// <summary>
    /// Скетч.
    /// </summary>
    Sketch = 5,
    
    /// <summary>
    /// Вспомогательная плоскость.
    /// </summary>
    AdditionalPlane = 14,

    /// <summary>
    /// Выдавливание.
    /// </summary>
    BaseExtrusion = 24,

    /// <summary>
    /// Вычитание выдавливанием.
    /// </summary>
    CutExtrusion = 26,

    /// <summary>
    /// Скругление.
    /// </summary>
    Fillet = 34,
}
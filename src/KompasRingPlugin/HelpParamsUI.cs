namespace KompasRingPlugin;

/// <summary>
/// Параметры действия для отображения.
/// </summary>
public class HelpParamsUI
{
    /// <summary>
    /// Возвращает или задает дополнительную информацию по вводу значений.
    /// </summary>
    public string AdditionInfo { get; private set; }

    /// <summary>
    /// Возвращает или задает тип действия.
    /// </summary>
    public ActionType ToAction { get; private set; }

    /// <summary>
    /// Создает экземпляр класса <see cref="HelpParamsUI"/>.
    /// </summary>
    /// <param name="action"> Тип действия. </param>
    /// <param name="info"> Дополнительная информация по вводу. </param>
    public HelpParamsUI(ActionType action, string info)
    {
        AdditionInfo = info;
        ToAction = action;
    }
}
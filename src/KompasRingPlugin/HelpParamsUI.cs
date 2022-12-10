namespace KompasRingPlugin;

/// <summary>
/// 
/// </summary>
public class HelpParamsUI
{
    public string AdditionInfo { get; set; }

    public ActionType ToAction { get; set; }

    public HelpParamsUI(ActionType action, string info)
    {
        AdditionInfo = info;
        ToAction = action;
    }
}
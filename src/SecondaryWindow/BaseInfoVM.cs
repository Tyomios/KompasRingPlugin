using CommunityToolkit.Mvvm.ComponentModel;


namespace SecondaryWindow;

/// <summary>
/// 
/// </summary>
[INotifyPropertyChanged]
public abstract partial class BaseInfoVM
{
    /// <summary>
    /// Сообщение.
    /// </summary>
    [ObservableProperty]
    private string _message;
}
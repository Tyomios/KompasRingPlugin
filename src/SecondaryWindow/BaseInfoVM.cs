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

    [ObservableProperty]
    private uint _progress;

    /// <summary>
    /// Произошедшая ошибка.
    /// </summary>
    [ObservableProperty]
    private string _errorMessage;

    private string _capture;

    public string Capture
    {
        get => _capture;
        protected set
        {
            _capture = value;
            OnPropertyChanged();
        }
    }
}
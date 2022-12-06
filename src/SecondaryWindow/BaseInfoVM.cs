using CommunityToolkit.Mvvm.ComponentModel;

namespace SecondaryWindow;

[INotifyPropertyChanged]
public abstract partial class BaseInfoVM
{
    /// <summary>
    /// Сообщение.
    /// </summary>
    [ObservableProperty]
    private string _message;

    /// <summary>
    /// Контекст информации.
    /// </summary>
    private InfoType _infoType;

    /// <summary>
    /// Возвращает или задает контекст информации.
    /// </summary>
    public InfoType InfoType
    {
        get => _infoType;
        private set
        {
            _infoType = value;
        }
    }
}
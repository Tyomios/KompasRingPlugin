using CommunityToolkit.Mvvm.ComponentModel;

namespace SecondaryWindow.viewModels;

[INotifyPropertyChanged]
public partial class WarningVM : BaseInfoVM
{
    /// <summary>
    /// Произошедшая ошибка.
    /// </summary>
    [ObservableProperty]
    private string _errorMessage;

    private string _capture;

    public string Capture
    {
        get => _capture;
        private set
        {
            _capture = value;
            OnPropertyChanged();
        }
    }

    public WarningVM(string errorMessage, string capture = "Внимание")
    {
        ErrorMessage = errorMessage;
        Capture = capture;
        Message = "Построение кольца не может быть завершено. Произошла ошибка:";
    }
}
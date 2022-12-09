using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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

    public WarningVM(string message, string errorMessage, string capture = "Внимание")
    {

    }

    public WarningVM()
    {

    }

    [ICommand]
    private void Assert(object o)
    {
        if (o is null || o is not AdvancedDialogWindow)
        {
            return;
        }
        CloseView((AdvancedDialogWindow)o);
    }


    [ICommand]
    private void CloseView(AdvancedDialogWindow window)
    {
        window.Close();
    }
}
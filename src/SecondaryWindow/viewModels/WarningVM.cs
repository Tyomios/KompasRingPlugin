using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SecondaryWindow.viewModels;

[INotifyPropertyChanged]
public partial class WarningVM : BaseInfoVM
{
    public WarningVM(string errorMessage, string capture = "Внимание")
    {
        ErrorMessage = errorMessage;
        Capture = capture;
        Message = "Построение кольца не может быть завершено. Произошла ошибка:";
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
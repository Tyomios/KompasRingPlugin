using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SecondaryWindow.viewModels;

[INotifyPropertyChanged]
public partial class WarningVM : BaseInfoVM
{
    public WarningVM(string errorMessage)
    {
        ErrorMessage = errorMessage;
        Message = "Построение кольца не может быть завершено. Произошла ошибка:";
    }
}
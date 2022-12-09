using CommunityToolkit.Mvvm.ComponentModel;

namespace SecondaryWindow.viewModels;

[INotifyPropertyChanged]
public partial class SuccessVM : BaseInfoVM
{
    public SuccessVM(string message = "Задача успешно завершена")
    {
        Message = message;
    }
}
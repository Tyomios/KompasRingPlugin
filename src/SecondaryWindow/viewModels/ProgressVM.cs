using CommunityToolkit.Mvvm.ComponentModel;

namespace SecondaryWindow.viewModels;

[INotifyPropertyChanged]
public partial class ProgressVM : BaseInfoVM 
{
    [ObservableProperty]
    private uint _progress;

    public ProgressVM(string message, uint progress)
    {
        Message = message;
        Progress = progress;
    }
}
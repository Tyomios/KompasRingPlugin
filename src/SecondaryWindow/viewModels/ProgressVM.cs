using CommunityToolkit.Mvvm.ComponentModel;

namespace SecondaryWindow.viewModels;

[INotifyPropertyChanged]
public partial class ProgressVM : BaseInfoVM 
{
    public ProgressVM(string message, uint progress)
    {
        Message = message;
        Progress = progress;
        IsVisible = true;
    }
}
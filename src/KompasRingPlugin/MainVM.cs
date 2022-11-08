using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;

namespace KompasRingPlugin;

[INotifyPropertyChanged]
public partial class MainVM
{
    [ObservableProperty]
    private Ring _ring = new();

    [ICommand]
    private void OpenKompas3D()
    {
        KompasConnector.Instance.Connect();
    }

    [ICommand]
    private void Build()
    {

    }
}
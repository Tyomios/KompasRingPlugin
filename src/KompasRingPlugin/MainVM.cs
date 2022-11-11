using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KompasAPI7;
using Model;


namespace KompasRingPlugin;

[INotifyPropertyChanged]
public partial class MainVM
{
    [ObservableProperty]
    private Ring _ring = new();

    [ICommand]
    private async void OpenKompas3D()
    {
        IKompasDocument3D doc;
        await Task.Run((() =>
        {
            doc = KompasConnector.Instance.GetDocument().Result;
        }));
    }

    [ICommand]
    private void Build()
    {

    }

    /// <summary>
    /// Закрытие приложение, вместе с закрытием приложения КОМПАС-3D.
    /// </summary>
    [ICommand]
    private void CloseApplication()
    {
        KompasConnector.Instance.Disconnect();
        Application.Current.Shutdown();
    }
}
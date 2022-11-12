using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kompas6API5;
using Model;
using Application = System.Windows.Application;


namespace KompasRingPlugin;

[INotifyPropertyChanged]
public partial class MainVM
{
    [ObservableProperty]
    private Ring _ring = new();

    [ICommand]
    private async void OpenKompas3D()
    {
        Document3D doc;
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
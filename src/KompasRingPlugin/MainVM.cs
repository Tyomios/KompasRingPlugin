using System.Net.Mime;
using System.Windows;
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
        //var doc = KompasConnector.Instance.GetDocument();
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
        KompasConnector.Instance.Dispose();
        Application.Current.Shutdown();
    }
}
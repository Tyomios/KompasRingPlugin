using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kompas6API5;
using Model;
using Application = System.Windows.Application;

namespace KompasRingPlugin;

/// <summary>
/// Отвечает за взаимодействие с пользователем.
/// </summary>
[INotifyPropertyChanged]
public partial class MainVM
{
    /// <summary>
    /// Кольцо.
    /// </summary>
    [ObservableProperty]
    private Ring _ring = new();

    /// <summary>
    /// Построение детали в приложении КОМПАС-3D.
    /// </summary>
    [ICommand]
    private void Build()
    {
        var ringBuilder = new RingBuilder();
        ringBuilder.Build(_ring);
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
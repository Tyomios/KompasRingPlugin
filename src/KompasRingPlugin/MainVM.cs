using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core;
using Model;
using SecondaryWindow;
using SecondaryWindow.viewModels;
using Application = System.Windows.Application;


namespace KompasRingPlugin;

//todo добавить анимации для каждого действия
//todo сравнить размеры элементов управления с указанным в гайдлайне

//todo нагрузочные тесты

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

    [ObservableProperty]
    private BaseInfoVM _dialogVM;

    public MainVM()
    {
        DialogService.Dispatcher = Dispatcher.CurrentDispatcher;
    }

    /// <summary>
    /// Построение детали в приложении КОМПАС-3D.
    /// </summary>
    [ICommand]
    private async void Build()
    {
        try
        {
            RingParamsValidator.CheckCorrectValues(_ring);
            var ringBuilder = new RingBuilder();
            DialogVM = new ProgressVM("Проверка параметров кольца", 0);
            ringBuilder.OnProgressing += SetProgressData;
            ringBuilder.OnBuildingError += ShowWarningView;
            ringBuilder.OnBuildingSuccess += ShowSuccessView;
            await ringBuilder.Build(_ring);
            if (DialogVM.IsVisible)
            {
                DialogVM.IsVisible = false;
            }
        }
        catch (Exception exception)
        {
            ShowWarningView(exception.Message);
            Thread.Sleep(2000);
            DialogVM.IsVisible = false;
        }
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

    private void ShowWarningView(string message)
    {
        DialogVM = new WarningVM(message);
        DialogVM.IsVisible = true;
    }

    private void ShowSuccessView(string message, int delay)
    {
        DialogVM = new SuccessVM(message);
        DialogVM.IsVisible = true;
        Thread.Sleep(delay);
        DialogVM.IsVisible = false;
    }

    private void SetProgressData(string message, uint progress)
    {
        DialogVM.Progress = progress;
        DialogVM.Message = message;
    }
}
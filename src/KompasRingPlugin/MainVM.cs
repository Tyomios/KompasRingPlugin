using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core;
using Model;
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

    public MainVM()
    {
        DialogService.Dispatcher = Dispatcher.CurrentDispatcher;
    }

    /// <summary>
    /// Построение детали в приложении КОМПАС-3D.
    /// </summary>
    [ICommand]
    private void Build()
    {
        try
        {
            RingParamsValidator.CheckCorrectValues(_ring);
            var ringBuilder = new RingBuilder();
            ringBuilder.Build(_ring);
        }
        catch (Exception exception)
        {
            var dialogService = new DialogService(new ProgressVM("Выполняется построение кольца",40));
            dialogService.Dialog();
            //Thread.Sleep(2000);

            //dialogService.CurrentVM = new ProgressVM();
            //dialogService.CurrentVM.Message = "20";
            //Thread.Sleep(1000);
            //dialogService.CurrentVM.Message = "30";

            //dialogService.CurrentVM = new SuccessVM();

            //dialogService.DialogEnd(1000);
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
}
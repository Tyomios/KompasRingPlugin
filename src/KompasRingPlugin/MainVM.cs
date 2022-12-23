using System;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core;
using Model;
using Application = System.Windows.Application;


namespace KompasRingPlugin;

//todo сравнить размеры элементов управления с указанным в гайдлайне
//todo валидация угла выреза и размера гравировки.
//todo вырез по центру или вырез двумя полосками все-таки?

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
    private Ring _ring = new()
    {
        Width = 30,
        Height = 30,
        Radius = 40,
        JewelryAngle = 45,
        RoundScale = 5
    };

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
            await ringBuilder.Build(_ring);
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
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
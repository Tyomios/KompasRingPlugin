﻿using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core;
using Model;
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
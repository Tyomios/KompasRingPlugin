using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kompas6API5;
using Model;
using Application = System.Windows.Application;

namespace KompasRingPlugin;

//todo встроить граничные значения в текстбокс.
//todo добавить стиль ошибки валидации значения по диапазону.

//todo добавить анимации для каждого действия
//todo добавить экран информирования о процессе\результате
//todo сравнить размеры элементов управления с указанным в гайдлайне

//todo настройка расположения текста на детали
//todo добавить валидаторы.
//todo разобраться с ошибкой выдавливания

//todo модульные тесты
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
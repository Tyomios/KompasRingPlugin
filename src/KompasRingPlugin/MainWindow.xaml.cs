using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;
using KompasRingPlugin.Controls;

namespace KompasRingPlugin
{

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    private Dictionary<string, HelpParamsUI> _controlsParams = new()
    {
        { "engravingTextAdvancedTextBox", new HelpParamsUI(ActionType.EngravingText, 
                                                        "Текст гравировки.") },

        {
            "engravingWidthAdvancedTextBox",
            new HelpParamsUI(ActionType.EngravingWidth, "Глубина гравировки." + 
                            "\nУказанное значение не должно превышать толщину кольца.")
        },

        { "ringHeightAdvancedTextBox", 
            new HelpParamsUI(ActionType.RingHeight, "Толщина кольца. " + 
                                                    "\nДоступный диапазон от 25 до 70 мм.") },

        { "ringSizeAdvancedTextBox", new HelpParamsUI(ActionType.RingSize, "Размер кольца. " + 
                                                        "\nДоступный диапазон от 20 до 150 мм.") },

        { "ringWidthAdvancedTextBox", new HelpParamsUI(ActionType.RingWidth, "Ширина кольца. " 
                                                            + "\nДоступный диапазон от 10 до 100 мм.") },

        { "ringRoundScaleAdvancedTextBox", new HelpParamsUI(ActionType.RoundScale, "Угол скругления кольца. " 
                                                                + "\nДоступный диапазон от 0 до 45 градусов."
                                            + "\nУказанное значение должно быть меньше ширины кольца.") }
    };
    public MainWindow()
	{
		InitializeComponent();
	}

    /// <summary>
    /// Перемещение окна.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void headerThumb_DragDelta(object sender, DragDeltaEventArgs e)
    {
        Left = Left + e.HorizontalChange;
        Top = Top + e.VerticalChange;
    }

    /// <summary>
    /// Сворачивает окно.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void clapBtn_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void UIElement_OnGotFocus(object sender, RoutedEventArgs e)
    {
        var control = (AdvancedTextbox)sender;
        if (control is null)
        {
            return;
        }

        UserHelperControl.PrimaryInfo = _controlsParams[control.Name];
    }
}
}
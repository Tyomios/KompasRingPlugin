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
        { "engravingTextAdvancedTextBox", new HelpParamsUI(ActionType.EngravingText, "Это текст гравировки") },

        {
            "engravingWidthAdvancedTextBox",
            new HelpParamsUI(ActionType.EngravingWidth, "Указанное значение не должно превышать толщину кольца")
        },

        { "ringHeightAdvancedTextBox", new HelpParamsUI(ActionType.RingHeight, String.Empty) },

        { "ringSizeAdvancedTextBox", new HelpParamsUI(ActionType.RingSize, String.Empty) },

        { "ringWidthAdvancedTextBox", new HelpParamsUI(ActionType.RingWidth, String.Empty) },

        { "ringRoundScaleAdvancedTextBox", new HelpParamsUI(ActionType.RoundScale, String.Empty) }
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
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace KompasRingPlugin.Controls;

/// <summary>
/// Interaction logic for AdvancedTextbox.xaml
/// </summary>
public partial class AdvancedTextbox : UserControl
{
    public AdvancedTextbox()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty InputDataProperty = DependencyProperty.Register(
        nameof(InputData), typeof(string), typeof(AdvancedTextbox), new PropertyMetadata(default(string)));

    public string InputData
    {
        get
        {
            return (string)GetValue(InputDataProperty);
        }
        set
        {
            SetValue(InputDataProperty, value);
        }
    }

    public double MinValue { get; set; }

    public double MaxValue { get; set; }

    /// <summary>
    /// Режим ввода данных.
    /// </summary>
    public bool IsDoubleOnly { get; set; }

    private void TextBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        InputData = textBox.Text;
    }

    private void TextBox_OnLostFocus(object sender, RoutedEventArgs e)
    {
        try
        {
            if (MinValue.Equals(Double.NaN) //todo тест
                || MaxValue.Equals(Double.NaN))
            {
                return;
            }

            Double.TryParse(textBox.Text, out double data);
            if (data < MinValue)
            {
                textBox.Text =
                    $"Введите значение в диапазоне от {Math.Round(MinValue, 2)} до {Math.Round(MaxValue, 2)}";

                textBox.Tag = "errorStyle";
            }
        }
        catch //todo обработка на наличие точки.
        {
            
        }
    }

    private void TextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        if (IsDoubleOnly)
        {
            e.Handled = !IsCorrectDouble(((TextBox)sender).Text + e.Text);
        }
    }
    
    private static bool IsCorrectDouble(string str)
    {
        return Double.TryParse(str, out double result);
    }
}
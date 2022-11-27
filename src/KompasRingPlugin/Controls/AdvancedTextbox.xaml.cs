using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace KompasRingPlugin.Controls;

/// <summary>
/// Interaction logic for AdvancedTextbox.xaml
/// </summary>
public partial class AdvancedTextbox : UserControl
{
    public AdvancedTextbox()
    {
        InitializeComponent();
        if (Info is not null)
        {
            infoTextBlock.Text = Info;
        }
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

    /// <summary>
    /// Возвращает или задает минимальное граничное значение.
    /// </summary>
    public double MinValue { get; set; }

    /// <summary>
    /// Возвращает или задает максимальное граничное значение.
    /// </summary>
    public double MaxValue { get; set; }

    /// <summary>
    /// Отображает информацию, как об ошибке валидации,
    /// так и об ответственности элемента за данные детали.
    /// </summary>
    public string Info { get; set; }

    public static readonly DependencyProperty InfoProperty = DependencyProperty.Register(
        nameof(Info), typeof(string), typeof(AdvancedTextbox),
        new FrameworkPropertyMetadata("Введите значение",
            FrameworkPropertyMetadataOptions.AffectsRender,
            new PropertyChangedCallback(OnContainElementChanged)));

    private static void OnContainElementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((AdvancedTextbox)d).infoTextBlock.Text = (string)e.NewValue;
    }

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
            if (data < MinValue || data > MaxValue)
            {
                infoTextBlock.Text =
                    $"Введите значение в диапазоне от {Math.Round(MinValue, 2)} мм до {Math.Round(MaxValue, 2)} мм";

                textBox.Tag = "errorStyle";
                textBox.Text = String.Empty;
                infoTextBlock.Visibility = Visibility.Visible;
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

    private void InfoTextBlock_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        infoTextBlock.Visibility = Visibility.Hidden;
        if (textBox.Tag.Equals("errorStyle"))
        {
            textBox.Tag = "regular";
        }
        textBox.Focus();
    }

    private void TextBox_OnGotFocus(object sender, RoutedEventArgs e)
    {
        if (infoTextBlock.Visibility == Visibility.Visible)
        {
            infoTextBlock.Visibility = Visibility.Hidden;
        }
    }
}
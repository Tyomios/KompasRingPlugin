using System.Windows;
using System.Windows.Controls.Primitives;

namespace KompasRingPlugin;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
	}

    private void headerThumb_DragDelta(object sender, DragDeltaEventArgs e)
    {
        Left = Left + e.HorizontalChange;
        Top = Top + e.VerticalChange;
    }
}

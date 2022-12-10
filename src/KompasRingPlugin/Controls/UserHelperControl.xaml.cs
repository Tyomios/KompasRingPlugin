using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace KompasRingPlugin.Controls
{
    /// <summary>
    /// Interaction logic for UserHelperControl.xaml
    /// </summary>
    public partial class UserHelperControl : UserControl
    {
        /// <summary>
        /// Содержит путь к ресурсу у соответствующего действия.
        /// </summary>
        private static Dictionary<ActionType, string> _actionsAnimations = new ()
        {
            {ActionType.RoundScale, @"resources/CornerRadiusAnimation.mp4"},
            {ActionType.EngravingText, @"resources/EngravingTextAnimation.mp4"},
            {ActionType.EngravingWidth, @"resources/EngravingWidthAnimation.mp4"},
            {ActionType.RingHeight, @"resources/RingBiggerRadiusAnimation.mp4"},
            {ActionType.RingSize, @"resources/RingSizeAnimation.mp4"},
            {ActionType.RingWidth, @"resources/RingWidthAnimation.mp4"}
        };

        public UserHelperControl()
        {
            InitializeComponent();
        }

        private HelpParamsUI _test;

        public HelpParamsUI Test
        {
            get => _test;
            set
            {
                _test = value;
                addInfoTextBlock.Text = _test.AdditionInfo;
                MediaElement.Source = new Uri(_actionsAnimations[_test.ToAction], UriKind.Relative);
            }
        }

        private void MediaElement_OnMediaFailed(object? sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show(e.ErrorException.Message);
        }
    }
}

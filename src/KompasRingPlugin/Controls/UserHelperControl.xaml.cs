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

        /// <summary>
        /// Создает экземпляр класса <see cref="UserHelperControl"/>.
        /// </summary>
        public UserHelperControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Отображает анимацию для действия.
        /// </summary>
        private HelpParamsUI _primaryInfo;

        /// <summary>
        /// Устанавливает анимацию и сообщение, содержащиеся в параметре.
        /// </summary>
        public HelpParamsUI PrimaryInfo
        {
            get => _primaryInfo;
            set
            {
                _primaryInfo = value;
                addInfoTextBlock.Text = _primaryInfo.AdditionInfo;
                MediaElement.Source = new Uri(_actionsAnimations[_primaryInfo.ToAction], UriKind.Relative);
            }
        }

        private void MediaElement_OnMediaFailed(object? sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show(e.ErrorException.Message);
        }
    }
}

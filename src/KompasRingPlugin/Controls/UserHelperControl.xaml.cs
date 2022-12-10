using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            {ActionType.RoundScale, "file:///../resources/CornerRadiusAnimation.mp4"},
            {ActionType.EngravingText, "file:///../resources/EngravingTextAnimation.mp4"},
            {ActionType.EngravingWidth, "file:///../resources/EngravingWidthAnimation.mp4"},
            {ActionType.RingHeight, "file:///../resources/RingBiggerRadiusAnimation.mp4"},
            {ActionType.RingSize, "file:///../resources/RingSizeAnimation.mp4"},
            {ActionType.RingWidth, "file:///../resources/RingWidthAnimation.mp4"}
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
                MediaElement.Source = new Uri(_actionsAnimations[_test.ToAction]);
            }
        }

        public HelpParamsUI AdditInfo
        {
            get { return (HelpParamsUI)GetValue(AdditInfoProperty); }
            set { SetValue(AdditInfoProperty, value); }
        }

        public static readonly DependencyProperty AdditInfoProperty = DependencyProperty.Register(
            nameof(AdditInfo), typeof(HelpParamsUI), typeof(AdvancedTextbox),
            new FrameworkPropertyMetadata(null,
                FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(OnAddInfoChangedChanged)));

        private static void OnAddInfoChangedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var p = (HelpParamsUI)e.NewValue;
            ((UserHelperControl)d).addInfoTextBlock.Text = p.AdditionInfo;
            ((UserHelperControl)d).MediaElement.Source = new Uri(_actionsAnimations[p.ToAction]);
        }
    }
}

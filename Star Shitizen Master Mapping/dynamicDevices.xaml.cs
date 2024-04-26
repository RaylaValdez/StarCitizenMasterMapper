using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Star_Shitizen_Master_Mapping
{
    /// <summary>
    /// Interaction logic for dynamicDevices.xaml
    /// </summary>
    public partial class dynamicDevices : UserControl
    {
        internal double ScrollOffset
        {
            get { return (double)GetValue(ScrollOffsetProperty); }
            set { SetValue(ScrollOffsetProperty, value); }
        }
        internal static readonly DependencyProperty ScrollOffsetProperty = DependencyProperty.Register("ScrollOffset", typeof(double), typeof(ScrollViewer), new PropertyMetadata(0.0, new PropertyChangedCallback(OnHorizontalScrollOffsetChanged)));

        private static void OnHorizontalScrollOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var scroll = (ScrollViewer)d;
            scroll.ScrollToHorizontalOffset((double)e.NewValue);
        }

        public string deviceName { get; set; }
        public string deviceID { get; set; }

        public bool selected { get; set; }
        

        SolidColorBrush CloseHoverFill = new SolidColorBrush();
        SolidColorBrush CloseDefaultFill = new SolidColorBrush();
        SolidColorBrush CloseFontHover = new SolidColorBrush();
        SolidColorBrush CloseFontDefault = new SolidColorBrush();
        SolidColorBrush StrokeDefault = new SolidColorBrush();

        public dynamicDevices()
        {
            selected = false;
            InitializeComponent();
            DataContext = this;
            CloseHoverFill.Color = Color.FromArgb(127, 0, 0, 0);
            CloseDefaultFill.Color = Color.FromArgb(0, 0, 0, 0);
            CloseFontHover.Color = Color.FromArgb(255, 255, 255, 255);
            CloseFontDefault.Color = Color.FromArgb(255, 58, 125, 177);
            StrokeDefault.Color = Color.FromArgb(255, 58, 113, 135);
        }

        private void uiDeviceEnter(object sender, MouseEventArgs e)
        {
            uiDeviceRoundRect.Fill = CloseHoverFill;
            uiDeviceRoundRect.Stroke = CloseFontHover;
            uiDeviceLabel.Foreground = CloseFontHover;

            var anim = new DoubleAnimation(0.0f, uiDeviceScrollContainer.ScrollableWidth, TimeSpan.FromSeconds(2.5), FillBehavior.Stop) { AutoReverse = true, RepeatBehavior = RepeatBehavior.Forever };
            uiDeviceScrollContainer.BeginAnimation(ScrollOffsetProperty, anim);
        }

        private void uiDeviceLeave(object sender, MouseEventArgs e)
        {
            if (!selected)
            {
                uiDeviceRoundRect.Fill = CloseDefaultFill;
                uiDeviceRoundRect.Stroke = StrokeDefault;
                uiDeviceLabel.Foreground = CloseFontDefault;
                uiDeviceScrollContainer.ScrollToHorizontalOffset(0.0f);
                uiDeviceScrollContainer.BeginAnimation(ScrollOffsetProperty, null);
            }
        }

        private void uiDeviceClick(object sender, MouseButtonEventArgs e)
        {
            MainWindow.Instance.deviceSelect(this);
            Debug.WriteLine("You clicked " +  deviceName);
        }

        public void setSelected(bool selected)
        {
            if (!selected)
            {
                uiDeviceRoundRect.Fill = CloseDefaultFill;
                uiDeviceRoundRect.Stroke = StrokeDefault;
                uiDeviceLabel.Foreground = CloseFontDefault;
                uiDeviceScrollContainer.BeginAnimation(ScrollOffsetProperty, null);
                this.selected = false;

            }
            else
            {
                uiDeviceRoundRect.Fill = CloseHoverFill;
                uiDeviceRoundRect.Stroke = CloseFontHover;
                uiDeviceLabel.Foreground = CloseFontHover;
                this.selected = true;

            }
        }
    }
}

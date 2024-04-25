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

namespace Star_Shitizen_Master_Mapping
{
    /// <summary>
    /// Interaction logic for inputWindow.xaml
    /// </summary>
    public partial class inputWindow : UserControl
    {
        private Action<float>? action = null;
        private float originalValue = 0f;
        SolidColorBrush HoverColor = new SolidColorBrush();
        SolidColorBrush HoverColorStroke = new SolidColorBrush();
        SolidColorBrush TglOn = new SolidColorBrush();
        SolidColorBrush TglOn2 = new SolidColorBrush();
        SolidColorBrush TglOff = new SolidColorBrush();
        SolidColorBrush TglOffStroke = new SolidColorBrush();
        SolidColorBrush CloseHoverFill = new SolidColorBrush();
        SolidColorBrush CloseDefaultFill = new SolidColorBrush();
        SolidColorBrush CloseFontHover = new SolidColorBrush();
        SolidColorBrush CloseFontDefault = new SolidColorBrush();
        public inputWindow()
        {
            InitializeComponent();

            HoverColor.Color = Color.FromArgb(127, 0, 0, 0);
            HoverColorStroke.Color = Color.FromArgb(255, 255, 255, 255);
            TglOn.Color = Color.FromArgb(255, 58, 125, 177);
            TglOn2.Color = Color.FromArgb(255, 31, 71, 99);
            TglOff.Color = Color.FromArgb(201, 10, 29, 41);
            TglOffStroke.Color = Color.FromArgb(255, 58, 113, 135);
            CloseHoverFill.Color = Color.FromArgb(127, 0, 0, 0);
            CloseDefaultFill.Color = Color.FromArgb(0, 0, 0, 0);
            CloseFontHover.Color = Color.FromArgb(255, 255, 255, 255);
            CloseFontDefault.Color = Color.FromArgb(255, 58, 125, 177);
        }

        public void SetVisible(bool visible)
        {
            if (!visible)
            {
                Visibility = Visibility.Hidden;
            }
            else
            {
                Visibility = Visibility.Visible;
            }
        }

        public void Update(float initialValue, string description)
        {
            originalValue = initialValue;
            uiDeviceConfigValueDescriptionLabel.Content = description;
            uiDeviceConfigValueTextBox.Text = initialValue.ToString("F3"); // 3 decimal places
        }

        public void updateValue(Action<float> value)
        {
            action = value;
        }

        private void eventTextBoxChanged(object sender, TextChangedEventArgs e)
        {
            float newValue;
            if (float.TryParse(uiDeviceConfigValueTextBox.Text, out newValue))
            {
                newValue = MathF.Min(MathF.Max(newValue, 0f), 1f); // clamp value from 0-1
                action?.Invoke(newValue); // call callback function (if it exists)
            }
            else
            {
                uiDeviceConfigValueTextBox.Text = originalValue.ToString("F3"); // 3 decimal places
            }
        }

        private void eventSaveMouseButtonUp(object sender, MouseButtonEventArgs e)
        {
            SetVisible(false);
            action = null;
        }
    }
}

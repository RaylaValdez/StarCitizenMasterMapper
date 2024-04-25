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
        private Action<float> action;
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

        public void Update(dynamicDevices device)
        {

        }

        public void updateValue(Action<float> value)
        {
            action = value;
        }

        private void eventTextBoxChanged(object sender, TextChangedEventArgs e)
        {
            float newValue;
            float.TryParse(uiDeviceConfigValueTextBox.Text, out newValue);
            action(newValue);
        }
    }
}

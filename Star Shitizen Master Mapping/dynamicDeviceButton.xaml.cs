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
using System.Xml.Linq;

namespace Star_Shitizen_Master_Mapping
{
    /// <summary>
    /// Interaction logic for dynamicDeviceButton.xaml
    /// </summary>
    public partial class dynamicDeviceButton : UserControl
    {
        public int buttonNumber = 0;

        SolidColorBrush defaultFill = new SolidColorBrush();
        SolidColorBrush defaultStroke = new SolidColorBrush();
        SolidColorBrush defaultFont = new SolidColorBrush();
        SolidColorBrush activeFill = new SolidColorBrush();
        SolidColorBrush activeStroke = new SolidColorBrush();
        SolidColorBrush activeFont = new SolidColorBrush();

        public dynamicDeviceButton(int i)
        {
            defaultFill.Color = Color.FromArgb(201,10,29,41);
            defaultStroke.Color = Color.FromArgb(255, 58, 113, 135);
            defaultFont.Color = Color.FromArgb(255,55,125,177);
            activeFill.Color = Color.FromArgb(201, 0, 0, 0);
            activeStroke.Color = Color.FromArgb(255, 255, 255, 255);
            activeFont.Color = Color.FromArgb(255,255,255,255);

            buttonNumber = i;
            InitializeComponent();

            uiDeviceButtonLabel.Content = ((i + 1).ToString());
        }

        public void updateButtonState(bool state)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(() => updateButtonState(state));
            }
            else
            {
                if (!state)
                {
                    // default colors
                    uiDeviceButton.Fill = defaultFill;
                    uiDeviceButton.Stroke = defaultStroke;
                    uiDeviceButtonLabel.Foreground = defaultFont;
                }
                else
                {
                    // active colors
                    uiDeviceButton.Fill = activeFill;
                    uiDeviceButton.Stroke = activeStroke;
                    uiDeviceButtonLabel.Foreground = activeFont;
                }
            }
            
        }
    }
}

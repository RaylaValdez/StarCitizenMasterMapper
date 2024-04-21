using Microsoft.Win32;
using SharpDX.DirectInput;
using System.Text;
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
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Star_Shitizen_Master_Mapping
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Define MainWindow Instance
        public static MainWindow Instance { get; private set; }

        // Define Brushs
        SolidColorBrush TglHoverCol = new SolidColorBrush();
        SolidColorBrush TglDefaultCol = new SolidColorBrush();
        SolidColorBrush CloseHoverFill = new SolidColorBrush();
        SolidColorBrush CloseDefaultFill = new SolidColorBrush();
        SolidColorBrush CloseFontHover = new SolidColorBrush();
        SolidColorBrush CloseFontDefault = new SolidColorBrush();
        SolidColorBrush StrokeDefault = new SolidColorBrush();

        // Animations
        ThicknessAnimation toggleAnimationOff = new ThicknessAnimation(new Thickness(0, 19, 197, 0), new Duration(TimeSpan.FromSeconds(0.05)));
        ThicknessAnimation toggleAnimationOn = new ThicknessAnimation(new Thickness(0, 18, 177, 0), new Duration(TimeSpan.FromSeconds(0.05)));

        // Variables
        enum uiElement {Devices, Binding, Visual, Toggle, vJoy}
        static uiElement Selected = 0;
        static bool isCorrupt = false;
        static bool vJoyOverride = false;
        public static bool vJoySelected = false;

        // Dynamic Device List
        List<dynamicDevices> directInputDevices = new List<dynamicDevices>();

        // Define devicesConfig.ini
        public static IniFile devicesConfig = new IniFile("devicesConfig.ini");


        public MainWindow()
        {
            var directInput = new DirectInput();
            
            foreach (var deviceInstance in directInput.GetDevices().Where(x => IsWanted(x)))
            {
                if (!deviceInstance.ProductName.Contains("vJoy Device"))
                {
                    directInputDevices.Add(new dynamicDevices() { deviceName = deviceInstance.ProductName, deviceID = deviceInstance.ProductGuid.ToString() });
                    if (!devicesConfig.KeyExists("Physical_ID", deviceInstance.ProductName))
                    {
                        devicesConfig.Write("Physical_ID", deviceInstance.ProductGuid.ToString(), deviceInstance.ProductName);
                        devicesConfig.WriteText("");
                        if (devicesConfig.KeyExists("Physical_ID", deviceInstance.ProductName))
                        {
                            devicesConfig.Write("X_Axis_Invert", "false", deviceInstance.ProductName);
                            devicesConfig.Write("Y_Axis_Invert", "false", deviceInstance.ProductName);
                            devicesConfig.Write("Z_Axis_Invert", "false", deviceInstance.ProductName);
                            devicesConfig.Write("RX_Axis_Invert", "false", deviceInstance.ProductName);
                            devicesConfig.Write("RY_Axis_Invert", "false", deviceInstance.ProductName);
                            devicesConfig.Write("RZ_Axis_Invert", "false", deviceInstance.ProductName);
                            devicesConfig.Write("Slider_Axis_Invert", "false", deviceInstance.ProductName);
                            devicesConfig.Write("Dial_Axis_Invert", "false", deviceInstance.ProductName);
                            devicesConfig.WriteText("");

                        }
                    }
                    
                }
            }


            Instance = this;

            TglDefaultCol.Color = Color.FromArgb(255,147,188,199);
            CloseHoverFill.Color = Color.FromArgb(127,0,0,0);
            CloseDefaultFill.Color = Color.FromArgb(0, 0, 0, 0);
            CloseFontHover.Color = Color.FromArgb(255, 255, 255, 255);
            CloseFontDefault.Color = Color.FromArgb(255, 58, 125, 177);
            StrokeDefault.Color = Color.FromArgb(255, 58, 113, 135);
            TglHoverCol.Color = Colors.Blue;
            Selected = uiElement.Devices;
            vJoySelected = false;
            vJoyOverride = true;

            InitializeComponent();

            uiVjoySettingsGrid.IsEnabled = false;
            uiVjoySettingsGrid.Visibility = Visibility.Hidden;

            dynamicDeviceConfig.Visibility = Visibility.Hidden;


            if (IsSoftwareInstalled("vjoy"))
            {
                uiVjoySettingsInstalledLabel.Content = "vJoy is detected on your system.\r\nYou're good to go.";
                uiVjoySettingsDownloadRRect.IsEnabled = false;
                uiVjoySettingsDownloadRRect.Visibility = Visibility.Hidden;
                uiVjoySettingsDownloadLabel.IsEnabled = false;
                uiVjoySettingsDownloadLabel.Visibility = Visibility.Hidden;
            }
            else
            {
                uiVjoySettingsInstalledLabel.Content = "vJoy isn't present on your system.\r\nThe button below will open a browser window.";
                uiVjoySettingsDownloadRRect.IsEnabled = true;
                uiVjoySettingsDownloadRRect.Visibility = Visibility.Visible;
                uiVjoySettingsDownloadLabel.IsEnabled = true;
                uiVjoySettingsDownloadLabel.Visibility = Visibility.Visible;
            }


            

            switch (Selected)
            {
                case uiElement.Devices:
                {
                    // Highlight Devices
                    uiDevicesRoundRect.Fill = CloseHoverFill;
                    uiDevicesRoundRect.Stroke = CloseHoverFill;
                    uiDevicesLabel.Foreground = CloseFontHover;
                    // Default Everything Else
                    defaultColor(uiElement.Binding);
                    defaultColor(uiElement.Visual);
                    break;
                }
                case uiElement.Binding:
                {
                    // Highlight Binding
                    uiBindingRoundRect.Fill = CloseHoverFill;
                    uiBindingRoundRect.Stroke = CloseHoverFill;
                    uiBindingLabel.Foreground = CloseFontHover;
                    // Defualt everything else
                    defaultColor(uiElement.Devices);
                    defaultColor(uiElement.Visual);
                    break;
                }
                case uiElement.Visual:
                {
                    // Highlight Visual
                    uiVisualRoundRect.Fill = CloseHoverFill;
                    uiVisualRoundRect.Stroke = CloseHoverFill;
                    uiVisualLabel.Foreground = CloseFontHover;
                    // Default everything else
                    defaultColor(uiElement.Devices);
                    defaultColor(uiElement.Binding);
                    break;
                }
            }

            foreach (dynamicDevices i in directInputDevices)
            {
               uiDeviceStackPanel.Children.Add(i);
            }
            

        }


    }
}
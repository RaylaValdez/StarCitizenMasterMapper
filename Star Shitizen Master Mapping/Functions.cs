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
    public partial class MainWindow : Window
    {
        // Color Changes
        private void highlightColor(uiElement element)
        {
            if (element == uiElement.Devices)
            {
                uiDevicesRoundRect.Fill = CloseHoverFill;
                uiDevicesRoundRect.Stroke = CloseHoverFill;
                uiDevicesLabel.Foreground = CloseFontHover;
            }

            if (element == uiElement.Binding)
            {
                uiBindingRoundRect.Fill = CloseHoverFill;
                uiBindingRoundRect.Stroke = CloseHoverFill;
                uiBindingLabel.Foreground = CloseFontHover;
            }

            if (element == uiElement.Visual)
            {
                uiVisualRoundRect.Fill = CloseHoverFill;
                uiVisualRoundRect.Stroke = CloseHoverFill;
                uiVisualLabel.Foreground = CloseFontHover;
            }

            if (element == uiElement.Toggle)
            {
                uiToggleSwitch.Fill = CloseFontHover;
            }
        }

        private void defaultColor(uiElement element)
        {
            if (element == uiElement.Devices)
            {
                uiDevicesRoundRect.Fill = CloseDefaultFill;
                uiDevicesRoundRect.Stroke = StrokeDefault;
                uiDevicesLabel.Foreground = CloseFontDefault;
            }

            if (element == uiElement.Binding)
            {
                uiBindingRoundRect.Fill = CloseDefaultFill;
                uiBindingRoundRect.Stroke = StrokeDefault;
                uiBindingLabel.Foreground = CloseFontDefault;
            }

            if (element == uiElement.Visual)
            {
                uiVisualRoundRect.Fill = CloseDefaultFill;
                uiVisualRoundRect.Stroke = StrokeDefault;
                uiVisualLabel.Foreground = CloseFontDefault;
            }

            if (element == uiElement.Toggle)
            {
                uiToggleSwitch.Fill = CloseFontDefault;
            }

        }

        // Device Page State
        public void devicePage(bool state)
        {
            if (!state)
            {
                uiDevicePageScroll.Visibility = Visibility.Hidden;
                dynamicDeviceConfig.Visibility = Visibility.Hidden;
                uiDevicePageScroll.IsEnabled = false;
            }
            else
            {
                uiDevicePageScroll.Visibility = Visibility.Visible;
                dynamicDeviceConfig.Visibility = Visibility.Visible;
                uiDevicePageScroll.IsEnabled = true;
            }
        }

        // Dynamic device list events.

        public void deviceSelect(dynamicDevices device)
        {
            vJoySelected = false;
            uiVjoySettingsGrid.IsEnabled = false;
            uiVjoySettingsGrid.Visibility = Visibility.Hidden;
            uiVjoyRoundRect.Fill = CloseDefaultFill;
            uiVjoyRoundRect.Stroke = StrokeDefault;
            uiVjoyLabel.Foreground = CloseFontDefault;
            foreach (dynamicDevices i in directInputDevices)
            {
                if (i == device)
                {
                    i.setSelected(true);
                }
                else
                {
                    i.setSelected(false);
                }
            }

            if (device != null)
            {
                dynamicDeviceConfig.Visibility = Visibility.Visible;
                dynamicDeviceConfig.Update(device);
            }
            else
            {
                dynamicDeviceConfig.Visibility = Visibility.Hidden;
            }


        }

        // Device Filtering
        static public bool IsWanted(DeviceInstance deviceInstance)
        {
            return deviceInstance.Type == DeviceType.Joystick
                   || deviceInstance.Type == DeviceType.Gamepad
                   || deviceInstance.Type == DeviceType.FirstPerson
                   || deviceInstance.Type == DeviceType.Flight
                   || deviceInstance.Type == DeviceType.Driving
                   || deviceInstance.Type == DeviceType.Supplemental
                   || deviceInstance.Type == DeviceType.Keyboard
                   || deviceInstance.Type == DeviceType.Mouse;
        }

        // vJoy Detection
        static bool IsSoftwareInstalled(string softwareName)
        {
            string relFilePath = @"vJoy\x64\vJoyInterface.dll";

            string programFilesPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), relFilePath);



            return File.Exists(programFilesPath);
        }

        // URL Open in default Browser
        private void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}

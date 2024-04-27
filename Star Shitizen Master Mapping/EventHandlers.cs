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

        // Back Ground Media Events
        private void bgEnd(object sender, RoutedEventArgs e)
        {
            bgMedia.Position = TimeSpan.Zero;
            bgMedia.Play();
        }

        // Title Buttons (Minimize / Maximize / Close / Corrupt Video Toggle
        private void mouseOverClose(object sender, MouseEventArgs e)
        {
            uiCloseButtonBox.Fill = CloseHoverFill;
            uiCloseButtonFont.Foreground = CloseFontHover;
        }

        private void mouseLeftCLose(object sender, MouseEventArgs e)
        {
            uiCloseButtonBox.Fill = CloseDefaultFill;
            uiCloseButtonFont.Foreground = CloseFontDefault;
        }

        private void mouseOverCFont(object sender, MouseEventArgs e)
        {
            uiCloseButtonBox.Fill = CloseHoverFill;
            uiCloseButtonFont.Foreground = CloseFontHover;
        }

        private void mouseLeftCFont(object sender, MouseEventArgs e)
        {
            uiCloseButtonBox.Fill = CloseDefaultFill;
            uiCloseButtonFont.Foreground = CloseFontDefault;
        }

        private void mouseCloseNow(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            
        }

        private void mainWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        private void mouseTitleGrab(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void mouseOverMaximize(object sender, MouseEventArgs e)
        {
            uiMaximizeButtonBox.Fill = CloseHoverFill;
            uiMaximizeButtonFont.Foreground = CloseFontHover;
        }

        private void mouseLeftMaximize(object sender, MouseEventArgs e)
        {
            uiMaximizeButtonBox.Fill = CloseDefaultFill;
            uiMaximizeButtonFont.Foreground = CloseFontDefault;
        }

        private void mouseMaxNow(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
        }

        private void mouseOverMin(object sender, MouseEventArgs e)
        {
            uiMinButtonBox.Fill = CloseHoverFill;
            uiMinButtonFont.Foreground = CloseFontHover;
        }

        private void mouseLeftMin(object sender, MouseEventArgs e)
        {
            uiMinButtonBox.Fill = CloseDefaultFill;
            uiMinButtonFont.Foreground = CloseFontDefault;
        }

        private void mouseMinNow(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void uiTglEnter(object sender, MouseEventArgs e)
        {
            highlightColor(uiElement.Toggle);
        }

        private void uiTglLeave(object sender, MouseEventArgs e)
        {
            defaultColor(uiElement.Toggle);
        }

        private void uiTglClick(object sender, MouseButtonEventArgs e)
        {
            if (!isCorrupt)
            {
                Uri corruptBgMedia = new Uri("./Media/bg_media.mpg", UriKind.RelativeOrAbsolute);
                uiToggleSwitch.BeginAnimation(MarginProperty, toggleAnimationOff);
                bgMedia.Source = corruptBgMedia;
                isCorrupt = true;
            }
            else
            {
                Uri cleanBgMedia = new Uri("./Media/bg_media.mp4", UriKind.RelativeOrAbsolute);
                uiToggleSwitch.BeginAnimation(MarginProperty, toggleAnimationOn);
                bgMedia.Source = cleanBgMedia;
                isCorrupt = false;
            }
        }

        // Tabs (Devices / Binding / Visual)
        private void uiDevicesEnter(object sender, MouseEventArgs e)
        {
            highlightColor(uiElement.Devices);
            uiDevicesRoundRect.Stroke = CloseFontHover;

        }

        private void uiDevicesLeave(object sender, MouseEventArgs e)
        {
            if (Selected != uiElement.Devices)
            {
                defaultColor(uiElement.Devices);
            }
            else
            {
                uiDevicesRoundRect.Stroke = CloseFontHover;
            }
        }

        private void uiDevicesClick(object sender, MouseButtonEventArgs e)
        {
            Selected = uiElement.Devices;
            defaultColor(uiElement.Binding);
            defaultColor(uiElement.Visual);
            devicePage(true);
            deviceSelect(null);
            vJoySelected = true;
            uiVjoySettingsGrid.IsEnabled = true;
            uiVjoySettingsGrid.Visibility = Visibility.Visible;
            uiVjoyRoundRect.Fill = CloseHoverFill;
            uiVjoyRoundRect.Stroke = CloseFontHover;
            uiVjoyLabel.Foreground = CloseFontHover;
            uiDevicesRoundRect.Stroke = CloseFontHover;
            uiBindingPageScroll.Visibility = Visibility.Hidden;
        }

        private void uiBindingEnter(object sender, MouseEventArgs e)
        {
            highlightColor(uiElement.Binding);
            uiBindingRoundRect.Stroke = CloseFontHover;
        }

        private void uiBindingLeave(object sender, MouseEventArgs e)
        {
            if (Selected != uiElement.Binding)
            {
                defaultColor(uiElement.Binding);
            }
            else
            {
                uiBindingRoundRect.Stroke = CloseFontHover;
            }

        }

        private void uiBindingClick(object sender, MouseButtonEventArgs e)
        {
            Selected = uiElement.Binding;
            defaultColor(uiElement.Devices);
            defaultColor(uiElement.Visual);
            uiVjoySettingsGrid.Visibility = Visibility.Hidden;
            devicePage(false);
            uiBindingRoundRect.Stroke = CloseFontHover;
            uiBindingPageScroll.Visibility = Visibility.Visible;
        }

        private void uiVisualEnter(object sender, MouseEventArgs e)
        {
            highlightColor(uiElement.Visual);
            uiVisualRoundRect.Stroke = CloseFontHover;
        }

        private void uiVisualLeave(object sender, MouseEventArgs e)
        {
            if (Selected != uiElement.Visual)
            {
                defaultColor(uiElement.Visual);
            }
            else
            {
                uiVisualRoundRect.Stroke = CloseFontHover;
            }
        }

        private void uiVisualClick(object sender, MouseButtonEventArgs e)
        {
            Selected = uiElement.Visual;
            defaultColor(uiElement.Binding);
            defaultColor(uiElement.Devices);
            devicePage(false);
            uiVisualRoundRect.Stroke = CloseFontHover;
            uiBindingPageScroll.Visibility = Visibility.Hidden;
        }

        // Devices (Hardcoded)
        private void uiVjoyEnter(object sender, MouseEventArgs e)
        {
            uiVjoyRoundRect.Fill = CloseHoverFill;
            uiVjoyRoundRect.Stroke = CloseFontHover;
            uiVjoyLabel.Foreground = CloseFontHover;
        }

        private void uiVjoyLeave(object sender, MouseEventArgs e)
        {
            if (!vJoySelected)
            {
                uiVjoyRoundRect.Fill = CloseDefaultFill;
                uiVjoyRoundRect.Stroke = StrokeDefault;
                uiVjoyLabel.Foreground = CloseFontDefault;
            }
        }

        private void uiVjoyClick(object sender, MouseButtonEventArgs e)
        {
            deviceSelect(null);
            vJoySelected = true;
            uiVjoySettingsGrid.IsEnabled = true;
            uiVjoySettingsGrid.Visibility = Visibility.Visible;
            uiVjoyRoundRect.Fill = CloseHoverFill;
            uiVjoyRoundRect.Stroke = CloseFontHover;
            uiVjoyLabel.Foreground = CloseFontHover;
        }

        private void uiVjoySettingsDownloadEnter(object sender, MouseEventArgs e)
        {
            uiVjoySettingsDownloadRRect.Fill = CloseHoverFill;
            uiVjoySettingsDownloadRRect.Stroke = CloseHoverFill;
            uiVjoySettingsDownloadLabel.Foreground = CloseFontHover;
        }

        private void uiVjoySettingsDownloadLeave(object sender, MouseEventArgs e)
        {
            uiVjoySettingsDownloadRRect.Fill = new SolidColorBrush(Color.FromArgb(255, 15, 38, 52));
            uiVjoySettingsDownloadRRect.Stroke = new SolidColorBrush(Color.FromArgb(255, 58, 113, 135));
            uiVjoySettingsDownloadLabel.Foreground = new SolidColorBrush(Color.FromArgb(255, 55, 125, 177));
        }

        private void uiVjoySettingsDownloadClick(object sender, MouseButtonEventArgs e)
        {
            OpenUrl("https://updov.com/download-vjoy/");
        }
    }
}

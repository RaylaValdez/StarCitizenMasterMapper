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
    /// Interaction logic for elipsesbutton.xaml
    /// </summary>
    public partial class XYCurves : UserControl
    {
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

        ToolTip invertTT = new ToolTip();



        public Thickness MinSliderMargin = new Thickness();
        public Thickness MaxSliderMargin = new Thickness();
        public Thickness MinSatSliderMargin = new Thickness();
        public Thickness MaxSatSliderMargin = new Thickness();

        private Point mouseStartPosition;
        private bool isDragging = false;

        public XYCurves()
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

            MinSliderMargin = new Thickness(10, 0, 0, 0);
            MaxSliderMargin = new Thickness(277, 0, 0, 0);
            MinSatSliderMargin = new Thickness(0, 0, 10, 0);
            MaxSatSliderMargin = new Thickness(0, 0, 277, 0);
        }

        public void Update(dynamicDevices device)
        {
            DeviceConfig.getSetFloat(ref DeviceConfig.XAxisCurve, "X_Axis_Curve");
            DeviceConfig.sliderMove(DeviceConfig.XAxisCurve, uiDeviceXAxisCurveSlider);
            uiDeviceXAxisCurvePercent.Content = DeviceConfig.ConvertToPercentageString(DeviceConfig.XAxisCurve);

            DeviceConfig.getSetFloat(ref DeviceConfig.XAxisDZ, "X_Axis_DZ");
            DeviceConfig.sliderMove(DeviceConfig.XAxisDZ, uiDeviceXAxisDZSlider);
            uiDeviceXAxisDZPercent.Content = DeviceConfig.ConvertToPercentageString(DeviceConfig.XAxisDZ);

            DeviceConfig.getSetFloat(ref DeviceConfig.XAxisSaturation, "X_Axis_Saturation");
            saturationSliderMove(1f - DeviceConfig.XAxisSaturation, uiDeviceXAxisSaturationSlider);
            uiDeviceXAxisSatPercent.Content = DeviceConfig.ConvertToPercentageString(DeviceConfig.XAxisSaturation);

            DeviceConfig.getSetFloat(ref DeviceConfig.YAxisCurve, "Y_Axis_Curve");
            DeviceConfig.sliderMove(DeviceConfig.YAxisCurve, uiDeviceYAxisCurveSlider);
            uiDeviceYAxisCurvePercent.Content = DeviceConfig.ConvertToPercentageString(DeviceConfig.YAxisCurve);

            DeviceConfig.getSetFloat(ref DeviceConfig.XAxisDZ, "Y_Axis_DZ");
            DeviceConfig.sliderMove(DeviceConfig.YAxisDZ, uiDeviceYAxisDZSlider);
            uiDeviceYAxisDZPercent.Content = DeviceConfig.ConvertToPercentageString(DeviceConfig.YAxisDZ);

            DeviceConfig.getSetFloat(ref DeviceConfig.YAxisSaturation, "Y_Axis_Saturation");
            saturationSliderMove(1f - DeviceConfig.YAxisSaturation, uiDeviceYAxisSaturationSlider);
            uiDeviceYAxisSatPercent.Content = DeviceConfig.ConvertToPercentageString(DeviceConfig.YAxisSaturation);
        }

        private void saturationSliderMove(float axiscurve, FrameworkElement devconf)
        {
            double newPos = MinSatSliderMargin.Right + (axiscurve * (MaxSatSliderMargin.Right - MinSatSliderMargin.Right));

            newPos = Math.Max(MinSatSliderMargin.Right, Math.Min(MaxSatSliderMargin.Right, newPos));

            devconf.Margin = new Thickness(0, 0, newPos, 0);
        }

        private void uiDeviceXYCloseFontEnter(object sender, MouseEventArgs e)
        {
            uiDeviceXYCloseBox.Fill = CloseHoverFill;
            uiDeviceXYCloseFont.Foreground = CloseFontHover;
        }

        private void uiDeviceXYCloseFontLeave(object sender, MouseEventArgs e)
        {
            uiDeviceXYCloseBox.Fill = CloseDefaultFill;
            uiDeviceXYCloseFont.Foreground = CloseFontDefault;
        }

        private void uiDeviceXYCloseFontClose(object sender, MouseButtonEventArgs e)
        {
            DeviceConfig.Instance.xyCurveEditorState(false);
        }

        private void eventXAxisSliderEnter(object sender, MouseEventArgs e)
        {
            uiDeviceXAxisCurveSlider.Fill = HoverColor;
            uiDeviceXAxisCurveSlider.Stroke = HoverColorStroke;
            DeviceConfig.Instance.drawLineForAxisCurve(DeviceConfig.XAxisCurve, false);
        }

        private void eventXAxisSliderLeave(object sender, MouseEventArgs e)
        {
            uiDeviceXAxisCurveSlider.Fill = TglOn;
            uiDeviceXAxisCurveSlider.Stroke = TglOn;
            DeviceConfig.Instance.drawLineForAxisCurve(DeviceConfig.XAxisCurve, true);
        }

        private void eventXAxisSliderClickDown(object sender, MouseButtonEventArgs e)
        {
            mouseStartPosition = e.GetPosition(uiDeviceXAxisCurveSlider);
            isDragging = true;
            uiDeviceXAxisCurveSlider.CaptureMouse();
        }

        private void eventXAxisSliderClickUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            uiDeviceXAxisCurveSlider.ReleaseMouseCapture();
            MainWindow.devicesConfig.Write("X_Axis_Curve", DeviceConfig.XAxisCurve.ToString(), DeviceConfig.inputDevice.Properties.ProductName);

        }

        private void eventXAxisSliderMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(uiDeviceXAxisCurveSlider);
                double deltaX = currentPosition.X - mouseStartPosition.X;

                double newX = Math.Min(Math.Max(uiDeviceXAxisCurveSlider.Margin.Left + deltaX, MinSliderMargin.Left), MaxSliderMargin.Left);
                uiDeviceXAxisCurveSlider.Margin = new Thickness(newX, 0, 0, 0);

                // Calculate normalized value
                DeviceConfig.XAxisCurve = (float)((newX - MinSliderMargin.Left) / (MaxSliderMargin.Left - MinSliderMargin.Left));

                // Ensure slider value stays within 0.0 and 1.0
                DeviceConfig.XAxisCurve = Math.Max(0.0f, Math.Min(1.0f, DeviceConfig.XAxisCurve));

                uiDeviceXAxisCurvePercent.Content = DeviceConfig.ConvertToPercentageString(DeviceConfig.XAxisCurve);
                DeviceConfig.Instance.drawLineForAxisCurve(DeviceConfig.XAxisCurve, true);
                DeviceConfig.Instance.drawLineForAxisCurve(DeviceConfig.XAxisCurve, false);
            }
        }

        private void eventYAxisSliderEnter(object sender, MouseEventArgs e)
        {
            uiDeviceYAxisCurveSlider.Fill = HoverColor;
            uiDeviceYAxisCurveSlider.Stroke = HoverColorStroke;
            DeviceConfig.Instance.drawLineForAxisCurve(DeviceConfig.YAxisCurve, false);
        }

        private void eventYAxisSliderLeave(object sender, MouseEventArgs e)
        {
            uiDeviceYAxisCurveSlider.Fill = TglOn;
            uiDeviceYAxisCurveSlider.Stroke = TglOn;
            DeviceConfig.Instance.drawLineForAxisCurve(DeviceConfig.YAxisCurve, true);
        }

        private void eventYAxisSliderClickDown(object sender, MouseButtonEventArgs e)
        {
            mouseStartPosition = e.GetPosition(uiDeviceYAxisCurveSlider);
            isDragging = true;
            uiDeviceYAxisCurveSlider.CaptureMouse();
        }

        private void eventYAxisSliderClickUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            uiDeviceYAxisCurveSlider.ReleaseMouseCapture();
            MainWindow.devicesConfig.Write("Y_Axis_Curve", DeviceConfig.YAxisCurve.ToString(), DeviceConfig.inputDevice.Properties.ProductName);

        }

        private void eventYAxisSliderMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(uiDeviceYAxisCurveSlider);
                double deltaX = currentPosition.X - mouseStartPosition.X;

                double newX = Math.Min(Math.Max(uiDeviceYAxisCurveSlider.Margin.Left + deltaX, MinSliderMargin.Left), MaxSliderMargin.Left);
                uiDeviceYAxisCurveSlider.Margin = new Thickness(newX, 0, 0, 0);

                // Calculate normalized value
                DeviceConfig.YAxisCurve = (float)((newX - MinSliderMargin.Left) / (MaxSliderMargin.Left - MinSliderMargin.Left));

                // Ensure slider value stays within 0.0 and 1.0
                DeviceConfig.YAxisCurve = Math.Max(0.0f, Math.Min(1.0f, DeviceConfig.YAxisCurve));

                uiDeviceYAxisCurvePercent.Content = DeviceConfig.ConvertToPercentageString(DeviceConfig.YAxisCurve);
                DeviceConfig.Instance.drawLineForAxisCurve(DeviceConfig.YAxisCurve, true);
                DeviceConfig.Instance.drawLineForAxisCurve(DeviceConfig.YAxisCurve, false);
            }
        }

        private void eventXAxisDZSliderEnter(object sender, MouseEventArgs e)
        {
            uiDeviceXAxisDZSlider.Fill = HoverColor;
            uiDeviceXAxisDZSlider.Stroke = HoverColorStroke;
        }

        private void eventXAxisDZSliderLeave(object sender, MouseEventArgs e)
        {
            uiDeviceXAxisDZSlider.Fill = TglOn;
            uiDeviceXAxisDZSlider.Stroke = TglOn;
        }

        private void eventXAxisDZSliderClickDown(object sender, MouseButtonEventArgs e)
        {
            mouseStartPosition = e.GetPosition(uiDeviceXAxisDZSlider);
            isDragging = true;
            uiDeviceXAxisDZSlider.CaptureMouse();
        }

        private void eventXAxisDZSliderClickUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            uiDeviceXAxisDZSlider.ReleaseMouseCapture();
            MainWindow.devicesConfig.Write("X_Axis_DZ", DeviceConfig.XAxisDZ.ToString(), DeviceConfig.inputDevice.Properties.ProductName);

        }

        private void eventXAxisDZSliderMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(uiDeviceXAxisDZSlider);
                double deltaX = currentPosition.X - mouseStartPosition.X;

                double newX = Math.Min(Math.Max(uiDeviceXAxisDZSlider.Margin.Left + deltaX, MinSliderMargin.Left), MaxSliderMargin.Left);
                uiDeviceXAxisDZSlider.Margin = new Thickness(newX, 0, 0, 0);

                // Calculate normalized value
                DeviceConfig.XAxisDZ = (float)((newX - MinSliderMargin.Left) / (MaxSliderMargin.Left - MinSliderMargin.Left));

                // Ensure slider value stays within 0.0 and 1.0
                DeviceConfig.XAxisDZ = Math.Max(0.0f, Math.Min(1.0f, DeviceConfig.XAxisDZ));

                uiDeviceXAxisDZPercent.Content = DeviceConfig.ConvertToPercentageString(DeviceConfig.XAxisDZ);
            }
        }

        private void eventXAxisSatSliderEnter(object sender, MouseEventArgs e)
        {
            uiDeviceXAxisSaturationSlider.Fill = HoverColor;
            uiDeviceXAxisSaturationSlider.Stroke = HoverColorStroke;
        }

        private void eventXAxisSatSliderLeave(object sender, MouseEventArgs e)
        {
            uiDeviceXAxisSaturationSlider.Fill = TglOn;
            uiDeviceXAxisSaturationSlider.Stroke = TglOn;
        }

        private void eventXAxisSatSliderClickDown(object sender, MouseButtonEventArgs e)
        {
            mouseStartPosition = e.GetPosition(uiDeviceXAxisSaturationSlider);
            isDragging = true;
            uiDeviceXAxisSaturationSlider.CaptureMouse();
        }

        private void eventXAxisSatSliderClickUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            uiDeviceXAxisSaturationSlider.ReleaseMouseCapture();
            MainWindow.devicesConfig.Write("X_Axis_Saturation", DeviceConfig.XAxisSaturation.ToString(), DeviceConfig.inputDevice.Properties.ProductName);

        }

        private void eventXAxisSatSliderMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(uiDeviceXAxisSaturationSlider);
                double deltaX = mouseStartPosition.X - currentPosition.X; // Change in X position

                double newX = Math.Min(Math.Max(uiDeviceXAxisSaturationSlider.Margin.Right + deltaX, MinSatSliderMargin.Right), MaxSatSliderMargin.Right);
                uiDeviceXAxisSaturationSlider.Margin = new Thickness(0, 0, newX, 0);

                // Calculate normalized value (1 at full right, 0 at full left)
                DeviceConfig.XAxisSaturation = (float)((newX - MinSatSliderMargin.Right) / (MaxSatSliderMargin.Right - MinSatSliderMargin.Right));

                // Ensure slider value stays within 0.0 and 1.0
                DeviceConfig.XAxisSaturation = Math.Max(0.0f, Math.Min(1.0f, 1f - DeviceConfig.XAxisSaturation));

                uiDeviceXAxisSatPercent.Content =   DeviceConfig.ConvertToPercentageString(DeviceConfig.XAxisSaturation);
            }
        }

        
        // Y Axis DZ and Sat

        private void eventYAxisDZSliderEnter(object sender, MouseEventArgs e)
        {
            uiDeviceYAxisDZSlider.Fill = HoverColor;
            uiDeviceYAxisDZSlider.Stroke = HoverColorStroke;
        }

        private void eventYAxisDZSliderLeave(object sender, MouseEventArgs e)
        {
            uiDeviceYAxisDZSlider.Fill = TglOn;
            uiDeviceYAxisDZSlider.Stroke = TglOn;
        }

        private void eventYAxisDZSliderClickDown(object sender, MouseButtonEventArgs e)
        {
            mouseStartPosition = e.GetPosition(uiDeviceYAxisDZSlider);
            isDragging = true;
            uiDeviceYAxisDZSlider.CaptureMouse();
        }

        private void eventYAxisDZSliderClickUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            uiDeviceYAxisDZSlider.ReleaseMouseCapture();
            MainWindow.devicesConfig.Write("Y_Axis_DZ", DeviceConfig.YAxisDZ.ToString(), DeviceConfig.inputDevice.Properties.ProductName);

        }

        private void eventYAxisDZSliderMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(uiDeviceYAxisDZSlider);
                double deltaX = currentPosition.X - mouseStartPosition.X;

                double newX = Math.Min(Math.Max(uiDeviceYAxisDZSlider.Margin.Left + deltaX, MinSliderMargin.Left), MaxSliderMargin.Left);
                uiDeviceYAxisDZSlider.Margin = new Thickness(newX, 0, 0, 0);

                // Calculate normalized value
                DeviceConfig.YAxisDZ = (float)((newX - MinSliderMargin.Left) / (MaxSliderMargin.Left - MinSliderMargin.Left));

                // Ensure slider value stays within 0.0 and 1.0
                DeviceConfig.YAxisDZ = Math.Max(0.0f, Math.Min(1.0f, DeviceConfig.YAxisDZ));

                uiDeviceYAxisDZPercent.Content = DeviceConfig.ConvertToPercentageString(DeviceConfig.YAxisDZ);
            }
        }

        private void eventYAxisSatSliderEnter(object sender, MouseEventArgs e)
        {
            uiDeviceYAxisSaturationSlider.Fill = HoverColor;
            uiDeviceYAxisSaturationSlider.Stroke = HoverColorStroke;
        }

        private void eventYAxisSatSliderLeave(object sender, MouseEventArgs e)
        {
            uiDeviceYAxisSaturationSlider.Fill = TglOn;
            uiDeviceYAxisSaturationSlider.Stroke = TglOn;
        }

        private void eventYAxisSatSliderClickDown(object sender, MouseButtonEventArgs e)
        {
            mouseStartPosition = e.GetPosition(uiDeviceYAxisSaturationSlider);
            isDragging = true;
            uiDeviceYAxisSaturationSlider.CaptureMouse();
        }

        private void eventYAxisSatSliderClickUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            uiDeviceYAxisSaturationSlider.ReleaseMouseCapture();
            MainWindow.devicesConfig.Write("Y_Axis_Saturation", DeviceConfig.YAxisSaturation.ToString(), DeviceConfig.inputDevice.Properties.ProductName);

        }

        private void eventYAxisSatSliderMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(uiDeviceYAxisSaturationSlider);
                double deltaX = mouseStartPosition.X - currentPosition.X; // Change in X position

                double newX = Math.Min(Math.Max(uiDeviceYAxisSaturationSlider.Margin.Right + deltaX, MinSatSliderMargin.Right), MaxSatSliderMargin.Right);
                uiDeviceYAxisSaturationSlider.Margin = new Thickness(0, 0, newX, 0);

                // Calculate normalized value (1 at full right, 0 at full left)
                DeviceConfig.YAxisSaturation = (float)((newX - MinSatSliderMargin.Right) / (MaxSatSliderMargin.Right - MinSatSliderMargin.Right));

                // Ensure slider value stays within 0.0 and 1.0
                DeviceConfig.YAxisSaturation = Math.Max(0.0f, Math.Min(1.0f, 1f - DeviceConfig.YAxisSaturation));

                uiDeviceYAxisSatPercent.Content = DeviceConfig.ConvertToPercentageString(DeviceConfig.YAxisSaturation);
            }
        }
    }
}

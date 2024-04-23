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
        }

        public void Update(dynamicDevices device)
        {
            DeviceConfig.getSetFloat(ref DeviceConfig.XAxisCurve, "X_Axis_Curve");
            DeviceConfig.sliderMove(DeviceConfig.XAxisCurve, uiDeviceXAxisCurveSlider);
            uiDeviceXAxisCurvePercent.Content = DeviceConfig.ConvertToPercentageString(DeviceConfig.XAxisCurve);
            DeviceConfig.getSetFloat(ref DeviceConfig.YAxisCurve, "Y_Axis_Curve");
            DeviceConfig.sliderMove(DeviceConfig.YAxisCurve, uiDeviceYAxisCurveSlider);
            uiDeviceYAxisCurvePercent.Content = DeviceConfig.ConvertToPercentageString(DeviceConfig.YAxisCurve);
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
    }
}

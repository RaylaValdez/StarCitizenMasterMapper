using GLGraphs.NetGraph;
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
using System;
using GLGraphs.CartesianGraph;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Wpf;
using SharpDX;
using SharpDX.DirectInput;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Reflection.Emit;
using vJoy.Wrapper;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace Star_Shitizen_Master_Mapping
{
    /// <summary>
    /// Interaction logic for DeviceConfig.xaml
    /// </summary>
    public partial class DeviceConfig : UserControl
    {
        public string deviceName = string.Empty;
        public string deviceID { get; set; }
        
        public static Joystick inputDevice { get; set; }
        public bool inpDvAcquired = false;
        public SharpDX.DirectInput.JoystickState inputState = new SharpDX.DirectInput.JoystickState();
        
        public GraphSeries<string> xySeries { get; set; }
        public GraphSeries<string> povCircleSeries { get; set; }
        public GraphSeries<string> povSeries { get; set; }
        public GraphSeries<string> curveLineSeries { get; set; }

        public float zAxisMaxWidth = 0f;

        SolidColorBrush HoverColor = new SolidColorBrush();
        SolidColorBrush HoverColorStroke = new SolidColorBrush();
        SolidColorBrush TglOn = new SolidColorBrush();
        SolidColorBrush TglOn2 = new SolidColorBrush();
        SolidColorBrush TglOff = new SolidColorBrush();
        SolidColorBrush TglOffStroke = new SolidColorBrush();

        ToolTip invertTT = new ToolTip();
        ToolTip deadZoneTT = new ToolTip();
        ToolTip lowerDeadZoneTT = new ToolTip();
        ToolTip saturationTT = new ToolTip();

        public static int XAxisMode = 0;
        public static bool XAxisInvert = false;
        public static float XAxisCurve = 0f;
        public static float XAxisDZ = 0f;
        public static float XAxisLDZ = 0f;
        public static float XAxisSaturation = 1f;

        public static int YAxisMode = 0;
        public static bool YAxisInvert = false;
        public static float YAxisCurve = 0f;
        public static float YAxisDZ = 0f;
        public static float YAxisLDZ = 0f;
        public static float YAxisSaturation = 1f;

        public static int ZAxisMode = 0;
        public static bool ZAxisInvert = false;
        public static float ZAxisCurve = 0f;
        public static float ZAxisDZ = 0f;
        public static float ZAxisLDZ = 0f;
        public static float ZAxisSaturation = 1f;

        public static int RXAxisMode = 0;
        public static bool RXAxisInvert = false;
        public static float RXAxisCurve = 0f;
        public static float RXAxisDZ = 0f;
        public static float RXAxisLDZ = 0f;
        public static float RXAxisSaturation = 1f;

        public static int RYAxisMode = 0;
        public static bool RYAxisInvert = false;
        public static float RYAxisCurve = 0f;
        public static float RYAxisDZ = 0f;
        public static float RYAxisLDZ = 0f;
        public static float RYAxisSaturation = 1f;

        public static int RZAxisMode = 0;
        public static bool RZAxisInvert = false;
        public static float RZAxisCurve = 0f;
        public static float RZAxisDZ = 0f;
        public static float RZAxisLDZ = 0f;
        public static float RZAxisSaturation = 1f;

        public static int SliderAxisMode = 0;
        public static bool SliderInvert = false;
        public static float SliderAxisCurve = 0f;
        public static float SliderAxisDZ = 0f;
        public static float SliderAxisLDZ = 0f;
        public static float SliderAxisSaturation = 1f;

        public static int DialAxisMode = 0;
        public static bool DialInvert = false;
        public static float DialAxisCurve = 0f;
        public static float DialAxisDZ = 0f;
        public static float DialAxisLDZ = 0f;
        public static float DialAxisSaturation = 1f;

        public bool curveEditor = false;

        public static Thickness MinSliderMargin = new Thickness();
        public static Thickness MaxSliderMargin = new Thickness();

        public static Thickness MinDeadZoneSliderMargin = new Thickness();
        public static Thickness MaxDeadZoneSliderMargin = new Thickness();

        public static Thickness MinDZSaturationSliderMargin = new Thickness();
        public static Thickness MaxDZSaturationSliderMargin = new Thickness();

        private Point mouseStartPosition;
        private bool isDragging = false;

        public static DeviceConfig Instance { get; private set; }

        public List<dynamicDeviceButton> buttons = new List<dynamicDeviceButton>();


        public DeviceConfig()
        {
            InitializeComponent();

            zAxisMaxWidth = (float)uiDeviceZAxisBckGrnd.Width;

            DataContext = this;

            xySeries = uiDeviceOutputGraph.Graph.State.AddSeries(SeriesType.Point, deviceName);
            povCircleSeries = uiDeviceOutputGraph.Graph.State.AddSeries(SeriesType.Line, "povCircle");
            povSeries = uiDeviceOutputGraph.Graph.State.AddSeries(SeriesType.Point, deviceName + "_pov");
            curveLineSeries = uiDeviceOutputGraph.Graph.State.AddSeries(SeriesType.Line, "dynamicCurve");

            HoverColor.Color = Color.FromArgb(127, 0, 0, 0);
            HoverColorStroke.Color = Color.FromArgb(255,255,255,255);
            TglOn.Color = Color.FromArgb(255, 58, 125, 177);
            TglOn2.Color = Color.FromArgb(255, 31, 71, 99);
            TglOff.Color = Color.FromArgb(201,10,29,41);
            TglOffStroke.Color = Color.FromArgb(255, 58, 113, 135);

            invertTT.Content = "Invert?";
            deadZoneTT.Content = "Upper Deadzone";
            lowerDeadZoneTT.Content = "Lower Deadzone";
            saturationTT.Content = "Sensitivity (inverse)";

            MinSliderMargin = new Thickness(10,0,0,0);
            MaxSliderMargin = new Thickness(277, 0, 0, 0);

            MinDeadZoneSliderMargin = new Thickness(0, 0, 0, 0);
            MaxDeadZoneSliderMargin = new Thickness(291, 0, 0, 0);

            MinDZSaturationSliderMargin = new Thickness(0,0,0,0);
            MaxDZSaturationSliderMargin = new Thickness(0,0,291,0);

            uiXYCurveWindow.Visibility = Visibility.Hidden;

            Instance = this;

            Thread joyPointerThread = new Thread(() =>
            {
                while (true)
                {
                    //do stuff
                    if (inputDevice != null)
                    {
                        if (inpDvAcquired)
                        {
                            inputDevice.Poll();
                            inputState = inputDevice.GetCurrentState();
                            float adjustedX = (inputState.X - 32768) / 32768f;
                            float adjustedY = (inputState.Y - 32768) / 32768f;
                            float adjustedZ = (inputState.Z) / 65535f;
                            float adjustedXRot = inputState.RotationX / 65535f;
                            float adjustedYRot = inputState.RotationY / 65535f;
                            float adjustedZRot = inputState.RotationZ / 65535f;
                            float adjustedSlider = inputState.Sliders[0] / 65535f;
                            float adjustedDial = inputState.Sliders[1] / 65535f;
                            float adjustedPov = inputState.PointOfViewControllers[0] / 36000f;
                            povPointer(adjustedPov, 0.7f);
                            joyPointer(XAxisInvert ? -adjustedX : adjustedX, YAxisInvert ? -adjustedY : adjustedY);
                            ZAxis(ZAxisInvert ? (1.0f - adjustedZ) : adjustedZ);
                            XRot(RXAxisInvert ? (1.0f - adjustedXRot) : adjustedXRot);
                            YRot(RYAxisInvert ? (1.0f - adjustedYRot) : adjustedYRot);
                            ZRot(RZAxisInvert ? (1.0f - adjustedZRot) : adjustedZRot);
                            ZRot(RZAxisInvert ? (1.0f - adjustedZRot) : adjustedZRot);
                            Slide(SliderInvert ? (1.0f - adjustedSlider) : adjustedSlider);
                            Dial(DialInvert ? (1.0f - adjustedDial) : adjustedDial);


                            for (int i = 0; i < buttons.Count; i++)
                            {
                                buttons[i].updateButtonState(inputState.Buttons[i]);
                            }
                        }
                    }
                    
                    Thread.Sleep(25);
                }
            });
            joyPointerThread.Start();
        }

        public void Update(dynamicDevices device)
        {
            deviceName = device.deviceName;
            deviceID = device.deviceID;
            uiDeviceConfigLabel.Content = deviceName;
            var devicesConfig = MainWindow.devicesConfig;

            var directInput = new DirectInput();
            var tempInputDevice = directInput.GetDevices().FirstOrDefault(device => device.ProductGuid.ToString() == deviceID);

            graphDrawCircle(povCircleSeries, 0f, 0f, 0.7f, 24);
            povCircleSeries.Color = new Color4(85, 85, 85, 255);
            xySeries.Color = new Color4(58, 125, 177, 255);
            povSeries.Color = new Color4(0, 191, 145, 255);
            povSeries.PointShape = SeriesPointShape.Circle;

            if (tempInputDevice != null)
            {
                switch (tempInputDevice.Type)
                {
                    case DeviceType.Joystick:
                    case DeviceType.Gamepad:
                    case DeviceType.FirstPerson:
                    case DeviceType.Flight:
                    case DeviceType.Driving:
                    case DeviceType.Supplemental:
                    {
                        if (inputDevice != null)
                        {
                            inputDevice.Dispose();
                            inpDvAcquired = false;
                        }
                        inputDevice = new Joystick(directInput, tempInputDevice.InstanceGuid);
                        inputDevice.Properties.BufferSize = 128;
                        inputDevice.Acquire();
                        inpDvAcquired = true;
                        break;
                    }
                    case DeviceType.Keyboard:
                    {
                        break;
                    }
                    case DeviceType.Mouse:
                    {
                        break;
                    }
                }
            }
            if (inputDevice != null)
            {
                if (MainWindow.devicesConfig.Read("X_Axis_Invert", inputDevice.Properties.ProductName) == "false")
                {
                    uiDeviceXAxisInvertTgl.Fill = TglOff;
                    uiDeviceXAxisInvertTgl.Stroke = TglOffStroke;
                }
                else
                {
                    uiDeviceXAxisInvertTgl.Fill = TglOn;
                    uiDeviceXAxisInvertTgl.Stroke = TglOn;
                }

                if (MainWindow.devicesConfig.Read("Y_Axis_Invert", inputDevice.Properties.ProductName) == "false")
                {
                    uiDeviceYAxisInvertTgl.Fill = TglOff;
                    uiDeviceYAxisInvertTgl.Stroke = TglOffStroke;
                }
                else
                {
                    uiDeviceYAxisInvertTgl.Fill = TglOn;
                    uiDeviceYAxisInvertTgl.Stroke = TglOn;
                }

                if (MainWindow.devicesConfig.Read("Z_Axis_Invert", inputDevice.Properties.ProductName) == "false")
                {
                    uiDeviceZAxisInvertTgl.Fill = TglOff;
                    uiDeviceZAxisInvertTgl.Stroke = TglOffStroke;
                }
                else
                {
                    uiDeviceZAxisInvertTgl.Fill = TglOn;
                    uiDeviceZAxisInvertTgl.Stroke = TglOn;
                }

                if (MainWindow.devicesConfig.Read("RX_Axis_Invert", inputDevice.Properties.ProductName) == "false")
                {
                    uiDeviceRXAxisInvertTgl.Fill = TglOff;
                    uiDeviceRXAxisInvertTgl.Stroke = TglOffStroke;
                }
                else
                {
                    uiDeviceRXAxisInvertTgl.Fill = TglOn;
                    uiDeviceRXAxisInvertTgl.Stroke = TglOn;
                }

                if (MainWindow.devicesConfig.Read("RY_Axis_Invert", inputDevice.Properties.ProductName) == "false")
                {
                    uiDeviceRYAxisInvertTgl.Fill = TglOff;
                    uiDeviceRYAxisInvertTgl.Stroke = TglOffStroke;
                }
                else
                {
                    uiDeviceRYAxisInvertTgl.Fill = TglOn;
                    uiDeviceRYAxisInvertTgl.Stroke = TglOn;
                }

                if (MainWindow.devicesConfig.Read("RZ_Axis_Invert", inputDevice.Properties.ProductName) == "false")
                {
                    uiDeviceRZAxisInvertTgl.Fill = TglOff;
                    uiDeviceRZAxisInvertTgl.Stroke = TglOffStroke;
                }
                else
                {
                    uiDeviceRZAxisInvertTgl.Fill = TglOn;
                    uiDeviceRZAxisInvertTgl.Stroke = TglOn;
                }

                buttons.Clear();
                uiDeviceButtonsWrapPanel.Children.Clear();
                
                for (var i = 0; i < inputDevice.Capabilities.ButtonCount; i++)
                {
                    var button = new dynamicDeviceButton(i);
                    buttons.Add(button);
                    uiDeviceButtonsWrapPanel.Children.Add(button);
                    
                }




                uiDeviceOutputGraph.Graph.State.Camera.Target.Position = new Vector2(0, 0);
                uiDeviceOutputGraph.Graph.State.Camera.Target.VerticalSize = 2f;
                uiDeviceOutputGraph.Graph.State.Camera.Snap();

                


            }

            getSetInvert(ref XAxisInvert, "X_Axis_Invert");
            getSetInvert(ref YAxisInvert, "Y_Axis_Invert");
            getSetInvert(ref ZAxisInvert, "Z_Axis_Invert");
            getSetInvert(ref RXAxisInvert, "RX_Axis_Invert");
            getSetInvert(ref RYAxisInvert, "RY_Axis_Invert");
            getSetInvert(ref RZAxisInvert, "RZ_Axis_Invert");
            getSetInvert(ref SliderInvert, "Slider_Axis_Invert");
            getSetInvert(ref DialInvert, "D_Axis_Invert");

            getSetFloat(ref ZAxisCurve, "Z_Axis_Curve");
            getSetFloat(ref RXAxisCurve, "RX_Axis_Curve");
            getSetFloat(ref RYAxisCurve, "RY_Axis_Curve");
            getSetFloat(ref RZAxisCurve, "RZ_Axis_Curve");
            getSetFloat(ref SliderAxisCurve, "Slider_Axis_Curve");
            getSetFloat(ref DialAxisCurve, "Dial_Axis_Curve");

            getSetInt(ref ZAxisMode, "Z_Axis_Mode");
            getSetFloat(ref ZAxisDZ, "Z_Axis_DZ");
            getSetFloat(ref ZAxisLDZ, "Z_Axis_LDZ");
            getSetFloat(ref ZAxisSaturation, "Z_Axis_Saturation");


            getSetInt(ref RXAxisMode, "RX_Axis_Mode");
            getSetFloat(ref RXAxisDZ, "RX_Axis_DZ");
            getSetFloat(ref RXAxisLDZ, "RX_Axis_LDZ");
            getSetFloat(ref RXAxisSaturation, "RX_Axis_Saturation");

            getSetInt(ref RYAxisMode, "RY_Axis_Mode");
            getSetFloat(ref RYAxisDZ, "RY_Axis_DZ");
            getSetFloat(ref RYAxisLDZ, "RY_Axis_LDZ");
            getSetFloat(ref RYAxisSaturation, "RY_Axis_Saturation");

            getSetInt(ref RZAxisMode, "RZ_Axis_Mode");
            getSetFloat(ref RZAxisDZ, "RZ_Axis_DZ");
            getSetFloat(ref RZAxisLDZ, "RZ_Axis_LDZ");
            getSetFloat(ref RZAxisSaturation, "RZ_Axis_Saturation");

            getSetInt(ref SliderAxisMode, "Slider_Axis_Mode");
            getSetFloat(ref SliderAxisDZ, "Slider_Axis_DZ");
            getSetFloat(ref SliderAxisLDZ, "Slider_Axis_LDZ");
            getSetFloat(ref SliderAxisSaturation, "Slider_Axis_Saturation");

            getSetInt(ref DialAxisMode, "Dial_Axis_Mode");
            getSetFloat(ref DialAxisDZ, "Dial_Axis_DZ");
            getSetFloat(ref DialAxisLDZ, "Dial_Axis_LDZ");
            getSetFloat(ref DialAxisSaturation, "Dial_Axis_Saturation");


            sliderMove(ZAxisCurve, uiDeviceZAxisCurveSlider);
            sliderMove(RXAxisCurve, uiDeviceRXAxisCurveSlider);
            sliderMove(RYAxisCurve, uiDeviceRYAxisCurveSlider);
            sliderMove(RZAxisCurve, uiDeviceRZAxisCurveSlider);
            sliderMove(SliderAxisCurve, uiDeviceSliderAxisCurveSlider);
            sliderMove(DialAxisCurve, uiDeviceDialAxisCurveSlider);

            deadZoneSliderMove(ZAxisDZ, uiDeviceZAxizDeadZoneSlider);
            deadZoneSliderMove(ZAxisLDZ, uiDeviceZAxizLowerDeadZoneSlider);
            saturationSliderMove(1f - ZAxisSaturation, uiDeviceZAxizDZSaturationSlider);

            deadZoneSliderMove(RXAxisDZ, uiDeviceRXAxisDeadZoneSlider);
            deadZoneSliderMove(RXAxisLDZ, uiDeviceRXAxisLowerDeadZoneSlider);
            saturationSliderMove(1f - RXAxisSaturation, uiDeviceRXAxisDZSaturationSlider);

            deadZoneSliderMove(RYAxisDZ, uiDeviceRYAxisDeadZoneSlider);
            deadZoneSliderMove(RYAxisLDZ, uiDeviceRYAxisLowerDeadZoneSlider);
            saturationSliderMove(1f - RYAxisSaturation, uiDeviceRYAxisDZSaturationSlider);

            deadZoneSliderMove(RZAxisDZ, uiDeviceRZAxisDeadZoneSlider);
            deadZoneSliderMove(RZAxisLDZ, uiDeviceRZAxisLowerDeadZoneSlider);
            saturationSliderMove(1f - RZAxisSaturation, uiDeviceRZAxisDZSaturationSlider);

            deadZoneSliderMove(SliderAxisDZ, uiDeviceSliderDeadZoneSlider);
            deadZoneSliderMove(SliderAxisLDZ, uiDeviceSliderLowerDeadZoneSlider);
            saturationSliderMove(1f - SliderAxisSaturation, uiDeviceSliderDZSaturationSlider);

            deadZoneSliderMove(DialAxisDZ, uiDeviceSliderDeadZoneSlider);
            deadZoneSliderMove(DialAxisLDZ, uiDeviceDialLowerDeadZoneSlider);
            saturationSliderMove(1f - DialAxisSaturation, uiDeviceDialDZSaturationSlider);


            uiDeviceZAxisCurvePercent.Content = ConvertToPercentageString(ZAxisCurve);         
            uiDeviceRXAxisCurvePercent.Content = ConvertToPercentageString(RXAxisCurve);
            uiDeviceRYAxisCurvePercent.Content = ConvertToPercentageString(RYAxisCurve);
            uiDeviceRZAxisCurvePercent.Content = ConvertToPercentageString(RZAxisCurve);
            uiDeviceSliderAxisCurvePercent.Content = ConvertToPercentageString(SliderAxisCurve);
            uiDeviceDialAxisCurvePercent.Content = ConvertToPercentageString(DialAxisCurve);          
            
            // Modes

            if (ZAxisMode == 0)
            {
                uiDeviceZAxizLowerDeadZoneSlider.Visibility = Visibility.Hidden;
                sliderMoveAndSet(0f,"Z_Axis_LDZ",ref ZAxisLDZ, uiDeviceZAxizLowerDeadZoneSlider);
                sliderMoveAndSet(0f, "Z_Axis_DZ", ref ZAxisDZ, uiDeviceZAxizDeadZoneSlider);
            }
            else if (ZAxisMode == 1)
            {
                uiDeviceZAxizLowerDeadZoneSlider.Visibility = Visibility.Visible;
                sliderMoveAndSet(0.515f, "Z_Axis_DZ", ref ZAxisDZ, uiDeviceZAxizDeadZoneSlider);
                sliderMoveAndSet(0.485f, "Z_Axis_LDZ", ref ZAxisLDZ, uiDeviceZAxizLowerDeadZoneSlider);
            }

            if (RXAxisMode == 0)
            {
                uiDeviceRXAxisLowerDeadZoneSlider.Visibility = Visibility.Hidden;
                sliderMoveAndSet(0f, "RX_Axis_LDZ", ref RXAxisLDZ, uiDeviceRXAxisLowerDeadZoneSlider);
                sliderMoveAndSet(0f, "RX_Axis_DZ", ref RXAxisDZ, uiDeviceRXAxisDeadZoneSlider);
            }
            else if (RXAxisMode == 1)
            {
                uiDeviceRXAxisLowerDeadZoneSlider.Visibility = Visibility.Visible;
                sliderMoveAndSet(0.515f, "RX_Axis_DZ",  ref RXAxisDZ,  uiDeviceRXAxisDeadZoneSlider);
                sliderMoveAndSet(0.485f, "RX_Axis_LDZ", ref RXAxisLDZ, uiDeviceRXAxisLowerDeadZoneSlider);
            }

            if (RYAxisMode == 0)
            {
                uiDeviceRYAxisLowerDeadZoneSlider.Visibility = Visibility.Hidden;
                sliderMoveAndSet(0f, "RY_Axis_LDZ", ref RYAxisLDZ, uiDeviceRYAxisLowerDeadZoneSlider);
                sliderMoveAndSet(0f, "RY_Axis_DZ", ref RYAxisDZ, uiDeviceRYAxisDeadZoneSlider);
            }
            else if (RYAxisMode == 1)
            {
                uiDeviceRYAxisLowerDeadZoneSlider.Visibility = Visibility.Visible;
                sliderMoveAndSet(0.515f, "RY_Axis_DZ", ref RYAxisDZ, uiDeviceRYAxisDeadZoneSlider);
                sliderMoveAndSet(0.485f, "RY_Axis_LDZ", ref RYAxisLDZ, uiDeviceRYAxisLowerDeadZoneSlider);
            }

            if (RZAxisMode == 0)
            {
                uiDeviceRZAxisLowerDeadZoneSlider.Visibility = Visibility.Hidden;
                sliderMoveAndSet(0f, "RZ_Axis_LDZ", ref RZAxisLDZ, uiDeviceRZAxisLowerDeadZoneSlider);
                sliderMoveAndSet(0f, "RZ_Axis_DZ", ref RZAxisDZ, uiDeviceRZAxisDeadZoneSlider);
            }
            else if (RZAxisMode == 1)
            {
                uiDeviceRZAxisLowerDeadZoneSlider.Visibility = Visibility.Visible;
                sliderMoveAndSet(0.515f, "RZ_Axis_DZ", ref RZAxisDZ, uiDeviceRZAxisDeadZoneSlider);
                sliderMoveAndSet(0.485f, "RZ_Axis_LDZ", ref RZAxisLDZ, uiDeviceRZAxisLowerDeadZoneSlider);
            }

            if (SliderAxisMode == 0)
            {
                uiDeviceSliderLowerDeadZoneSlider.Visibility = Visibility.Hidden;
                sliderMoveAndSet(0f, "RZ_Axis_LDZ", ref SliderAxisLDZ, uiDeviceSliderLowerDeadZoneSlider);
                sliderMoveAndSet(0f, "RZ_Axis_DZ", ref SliderAxisDZ,   uiDeviceSliderDeadZoneSlider);
            }
            else if (SliderAxisMode == 1)
            {
                uiDeviceSliderLowerDeadZoneSlider.Visibility = Visibility.Visible;
                sliderMoveAndSet(0.515f, "Slider_Axis_DZ", ref  SliderAxisDZ,  uiDeviceSliderDeadZoneSlider);
                sliderMoveAndSet(0.485f, "Slider_Axis_LDZ", ref SliderAxisLDZ, uiDeviceSliderLowerDeadZoneSlider);
            }

            if (DialAxisMode == 0)
            {
                uiDeviceDialLowerDeadZoneSlider.Visibility = Visibility.Hidden;
                sliderMoveAndSet(0f, "Dial_Axis_LDZ", ref DialAxisLDZ, uiDeviceDialLowerDeadZoneSlider);
                sliderMoveAndSet(0f, "Dial_Axis_DZ", ref  DialAxisDZ,  uiDeviceDialDeadZoneSlider);
            }
            else if (DialAxisMode == 1)
            {
                uiDeviceDialLowerDeadZoneSlider.Visibility = Visibility.Visible;
                sliderMoveAndSet(0.515f, "Dial_Axis_DZ", ref  DialAxisDZ,  uiDeviceDialDeadZoneSlider);
                sliderMoveAndSet(0.485f, "Dial_Axis_LDZ", ref DialAxisLDZ, uiDeviceDialLowerDeadZoneSlider);
            }



            uiXYCurveWindow.Update(device);

            

    


            if (device != null)
            {
                if (deviceName.ToString().Contains("Mouse") || deviceName.ToString().Contains("Keyboard"))
                {
                    uiDeviceOutputGraph.Visibility = Visibility.Hidden;
                    uiDeviceAxisScreen.Visibility = Visibility.Hidden;
                    uiDeviceXYCurvesButton.Visibility = Visibility.Hidden;
                    uiDeviceXYCurvesButton1.Visibility = Visibility.Hidden;
                    uiDeviceXYCurvesButton2.Visibility = Visibility.Hidden;
                    uiDeviceXYCurvesButton3.Visibility = Visibility.Hidden;
                    uiDeviceButtonsGrid.Visibility = Visibility.Hidden;
                }
                else
                {
                    uiDeviceOutputGraph.Visibility = Visibility.Visible;
                    uiDeviceAxisScreen.Visibility = Visibility.Visible;
                    uiDeviceXYCurvesButton.Visibility =  Visibility.Visible;
                    uiDeviceXYCurvesButton1.Visibility = Visibility.Visible;
                    uiDeviceXYCurvesButton2.Visibility = Visibility.Visible;
                    uiDeviceXYCurvesButton3.Visibility = Visibility.Visible;
                    uiDeviceButtonsGrid.Visibility = Visibility.Visible;
                }
            }



        }

        private void joyPointer(float x, float y)
        {
            xySeries.Clear();
            xySeries.Add(deviceName, calculateQuadraticY(XAxisCurve,x), calculateQuadraticY(YAxisCurve,y));
        }

        private void povPointer(float povhat, float radius)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(() => povPointer(povhat, radius));
                return;
            }
            if (povhat < 0)
            {
                povSeries.Clear();
            }
            else
            {
                float x = MathF.Sin(povhat * MathF.Tau) * radius;
                float y = MathF.Cos(povhat * MathF.Tau) * radius;
                povSeries.Clear();
                povSeries.Add(deviceName + "_povhat", x, y);
            }
            
        }

        private void graphDrawCircle(GraphSeries<string> series,float cx, float cy, float radius, int segments)
        {
            for (int i = 0; i <= segments; i++)
            {
                float x = MathF.Cos(i / (float)segments * MathF.Tau) * radius;
                float y = MathF.Sin(i / (float)segments * MathF.Tau) * radius;
                series.Add(i.ToString(), x + cx, y + cy);
            }
        }

        private double GetUiWidth(FrameworkElement uiElement)
        {
            if (!Dispatcher.CheckAccess())
            {
                return (double)Dispatcher.Invoke(() => GetUiWidth(uiElement));
            }
            else
            {
                return uiElement.Width;
            }
        }
        
        private void SetUiWidth(FrameworkElement uiElement, double newWidth)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(() => SetUiWidth(uiElement,newWidth));
            }
            else
            {
                uiElement.Width = newWidth;
                
            }
        }

        private void ZAxis(float zAxis)
        {
            var rangedAxis = calculateRange(zAxis, ZAxisLDZ, ZAxisDZ, ZAxisSaturation);
            var curvedAxis = calculateQuadraticY(ZAxisCurve,rangedAxis);
            
            SetUiWidth(uiDeviceZAxisFreGrnd, (double)(GetUiWidth(uiDeviceZAxisBckGrnd) * curvedAxis));
        }

        private void XRot(float xRotation)
        {
            var rangedAxis = calculateRange(xRotation, RXAxisLDZ, RXAxisDZ, RXAxisSaturation);
            var curvedAxis = calculateQuadraticY(RXAxisCurve, rangedAxis);
            SetUiWidth(uiDeviceXRotFreGrnd, (double)(GetUiWidth(uiDeviceXRotBckGrnd) * curvedAxis));
        }

        private void YRot(float yRotation)
        {
            var rangedAxis = calculateRange(yRotation, RYAxisLDZ, RYAxisDZ, RYAxisSaturation);
            var curvedAxis = calculateQuadraticY(RYAxisCurve, rangedAxis);
            SetUiWidth(uiDeviceYRotFreGrnd, (double)(GetUiWidth(uiDeviceYRotBckGrnd) * curvedAxis));
        }

        private void ZRot(float zRotation)
        {
            var rangedAxis = calculateRange(zRotation, RZAxisLDZ, RZAxisDZ, RZAxisSaturation);
            var curvedAxis = calculateQuadraticY(RZAxisCurve, rangedAxis);
            SetUiWidth(uiDeviceZRotFreGrnd, (double)(GetUiWidth(uiDeviceZRotBckGrnd) * curvedAxis));
        }

        private void Slide(float SliderValue)
        {
            var rangedAxis = calculateRange(SliderValue, SliderAxisLDZ, SliderAxisDZ, SliderAxisSaturation);
            var curvedAxis = calculateQuadraticY(SliderAxisCurve, rangedAxis);
            SetUiWidth(uiDeviceSliderFreGrnd, (double)(GetUiWidth(uiDeviceSliderBckGrnd) * curvedAxis));
        }

        private void Dial(float DialValue)
        {
            var rangedAxis = calculateRange(DialValue, DialAxisLDZ, DialAxisDZ, DialAxisSaturation);
            var curvedAxis = calculateQuadraticY(DialAxisCurve, rangedAxis);
            SetUiWidth(uiDeviceDialFreGrnd, (double)(GetUiWidth(uiDeviceDialBckGrnd) * curvedAxis));
        }

        public static void getSetInvert(ref bool inversion, string key)
        {
            if (inputDevice != null)
            {
                var value = MainWindow.devicesConfig.Read(key, inputDevice.Properties.ProductName);
                if (value != null)
                {
                    if (value.ToString() == "true")
                    {
                        inversion = true;
                    }
                    else
                    {
                        inversion = false;
                    }
                }
            }
        }

        public static void getSetFloat(ref float curve, string key)
        {
            if (inputDevice != null)
            {
                var value = MainWindow.devicesConfig.Read(key, inputDevice.Properties.ProductName);
                if (value != null)
                {
                    float.TryParse(value, out curve);
                }
            }
        }

        public static void getSetInt(ref int mode, string key)
        {
            if (inputDevice != null)
            {
                var value = MainWindow.devicesConfig.Read(key, inputDevice.Properties.ProductName);
                if (value != null)
                {
                    int.TryParse(value, out mode);
                }

            }
        }

        public static void sliderMoveAndSet(float newvalue, string key, ref float slider, FrameworkElement devconf)
        {
            if (inputDevice != null)
            {
                double newPos = MinDeadZoneSliderMargin.Left + (newvalue * (MaxDeadZoneSliderMargin.Left - MinDeadZoneSliderMargin.Left));

                newPos = Math.Max(MinDeadZoneSliderMargin.Left, Math.Min(MaxDeadZoneSliderMargin.Left, newPos));

                devconf.Margin = new Thickness(newPos, 0, 0, 0);

                MainWindow.devicesConfig.Write(key, newvalue.ToString(), inputDevice.Properties.ProductName);

                slider = newvalue;
            }
        }

        public void getSetPreciseInput(ref float configValue, string key, string type)
        {
           switch (type)
            {
                case "deadzone":
                {
                    break;
                }
                case "lowerdeadzone":
                {
                    break;
                }
                case "saturation":
                {
                    break;
                }
                case "curve":
                {
                    break;
                }
            }
        }



        // Events

        private void eventZAxisTglEnter(object sender, MouseEventArgs e)
        {
            uiDeviceZAxisInvertTgl.Fill = HoverColor;
            uiDeviceZAxisInvertTgl.Stroke = HoverColorStroke;
            uiDeviceZAxisInvertTgl.ToolTip = invertTT;
            invertTT.Content = "Invert?";

            invertTT.IsOpen = true;
            
        }

        private void eventZAxisTglLeave(object sender, MouseEventArgs e)
        {
            if (!ZAxisInvert)
            {
                uiDeviceZAxisInvertTgl.Fill = TglOff;
                uiDeviceZAxisInvertTgl.Stroke = TglOffStroke;
            }
            else
            {
                uiDeviceZAxisInvertTgl.Fill = TglOn;
                uiDeviceZAxisInvertTgl.Stroke = TglOn;
            }
            
            invertTT.IsOpen = false;
        }

        private void eventZAxisTglClick(object sender, MouseButtonEventArgs e)
        {
            if (MainWindow.devicesConfig.Read("Z_Axis_Invert", inputDevice.Properties.ProductName) == "false")
            {
                MainWindow.devicesConfig.DeleteKey("Z_Axis_Invert", inputDevice.Properties.ProductName);
                MainWindow.devicesConfig.Write("Z_Axis_Invert", "true", inputDevice.Properties.ProductName);
                ZAxisInvert = true;
                uiDeviceZAxisInvertTgl.Fill = TglOn;
                uiDeviceZAxisInvertTgl.Stroke = TglOn;

            }
            else
            {
                MainWindow.devicesConfig.DeleteKey("Z_Axis_Invert", inputDevice.Properties.ProductName);
                MainWindow.devicesConfig.Write("Z_Axis_Invert", "false", inputDevice.Properties.ProductName);
                ZAxisInvert = false;
                uiDeviceZAxisInvertTgl.Fill = TglOff;
                uiDeviceZAxisInvertTgl.Stroke = TglOffStroke;
            }
        }

        private void eventRXAxisTglEnter(object sender, MouseEventArgs e)
        {
            uiDeviceRXAxisInvertTgl.Fill = HoverColor;
            uiDeviceRXAxisInvertTgl.Stroke = HoverColorStroke;
            uiDeviceRXAxisInvertTgl.ToolTip = invertTT;
            invertTT.Content = "Invert?";

            invertTT.IsOpen = true;
        }

        private void eventRXAxisTglLeave(object sender, MouseEventArgs e)
        {
            if (!RXAxisInvert)
            {
                uiDeviceRXAxisInvertTgl.Fill = TglOff;
                uiDeviceRXAxisInvertTgl.Stroke = TglOffStroke;
            }
            else
            {
                uiDeviceRXAxisInvertTgl.Fill = TglOn;
                uiDeviceRXAxisInvertTgl.Stroke = TglOn;
            }

            invertTT.IsOpen = false;
        }

        private void eventRXAxisTglClick(object sender, MouseButtonEventArgs e)
        {
            if (MainWindow.devicesConfig.Read("RX_Axis_Invert", inputDevice.Properties.ProductName) == "false")
            {
                MainWindow.devicesConfig.DeleteKey("RX_Axis_Invert", inputDevice.Properties.ProductName);
                MainWindow.devicesConfig.Write("RX_Axis_Invert", "true", inputDevice.Properties.ProductName);
                RXAxisInvert = true;
                uiDeviceRXAxisInvertTgl.Fill = TglOn;
                uiDeviceRXAxisInvertTgl.Stroke = TglOn;

            }
            else
            {
                MainWindow.devicesConfig.DeleteKey("RX_Axis_Invert", inputDevice.Properties.ProductName);
                MainWindow.devicesConfig.Write("RX_Axis_Invert", "false", inputDevice.Properties.ProductName);
                RXAxisInvert = false;
                uiDeviceRXAxisInvertTgl.Fill = TglOff;
                uiDeviceRXAxisInvertTgl.Stroke = TglOffStroke;
            }
        }

        private void eventRYAxisTglEnter(object sender, MouseEventArgs e)
        {
            uiDeviceRYAxisInvertTgl.Fill = HoverColor;
            uiDeviceRYAxisInvertTgl.Stroke = HoverColorStroke;
            uiDeviceRYAxisInvertTgl.ToolTip = invertTT;
            invertTT.Content = "Invert?";

            invertTT.IsOpen = true;
        }

        private void eventRYAxisTglLeave(object sender, MouseEventArgs e)
        {
            if (!RYAxisInvert)
            {
                uiDeviceRYAxisInvertTgl.Fill = TglOff;
                uiDeviceRYAxisInvertTgl.Stroke = TglOffStroke;
            }
            else
            {
                uiDeviceRYAxisInvertTgl.Fill = TglOn;
                uiDeviceRYAxisInvertTgl.Stroke = TglOn;
            }

            invertTT.IsOpen = false;
        }

        private void eventRYAxisTglClick(object sender, MouseButtonEventArgs e)
        {
            if (MainWindow.devicesConfig.Read("RY_Axis_Invert", inputDevice.Properties.ProductName) == "false")
            {
                MainWindow.devicesConfig.DeleteKey("RY_Axis_Invert", inputDevice.Properties.ProductName);
                MainWindow.devicesConfig.Write("RY_Axis_Invert", "true", inputDevice.Properties.ProductName);
                RYAxisInvert = true;
                uiDeviceRYAxisInvertTgl.Fill = TglOn;
                uiDeviceRYAxisInvertTgl.Stroke = TglOn;

            }
            else
            {
                MainWindow.devicesConfig.DeleteKey("RY_Axis_Invert", inputDevice.Properties.ProductName);
                MainWindow.devicesConfig.Write("RY_Axis_Invert", "false", inputDevice.Properties.ProductName);
                RYAxisInvert = false;
                uiDeviceRYAxisInvertTgl.Fill = TglOff;
                uiDeviceRYAxisInvertTgl.Stroke = TglOffStroke;
            }
        }

        private void eventRZAxisTglEnter(object sender, MouseEventArgs e)
        {
            uiDeviceRZAxisInvertTgl.Fill = HoverColor;
            uiDeviceRZAxisInvertTgl.Stroke = HoverColorStroke;
            uiDeviceRZAxisInvertTgl.ToolTip = invertTT;
            invertTT.Content = "Invert?";

            invertTT.IsOpen = true;
        }

        private void eventRZAxisTglLeave(object sender, MouseEventArgs e)
        {
            if (!RZAxisInvert)
            {
                uiDeviceRZAxisInvertTgl.Fill = TglOff;
                uiDeviceRZAxisInvertTgl.Stroke = TglOffStroke;
            }
            else
            {
                uiDeviceRZAxisInvertTgl.Fill = TglOn;
                uiDeviceRZAxisInvertTgl.Stroke = TglOn;
            }

            invertTT.IsOpen = false;
        }

        private void eventRZAxisTglClick(object sender, MouseButtonEventArgs e)
        {
            if (MainWindow.devicesConfig.Read("RZ_Axis_Invert", inputDevice.Properties.ProductName) == "false")
            {
                MainWindow.devicesConfig.DeleteKey("RZ_Axis_Invert", inputDevice.Properties.ProductName);
                MainWindow.devicesConfig.Write("RZ_Axis_Invert", "true", inputDevice.Properties.ProductName);
                RZAxisInvert = true;
                uiDeviceRZAxisInvertTgl.Fill = TglOn;
                uiDeviceRZAxisInvertTgl.Stroke = TglOn;

            }
            else
            {
                MainWindow.devicesConfig.DeleteKey("RZ_Axis_Invert", inputDevice.Properties.ProductName);
                MainWindow.devicesConfig.Write("RZ_Axis_Invert", "false", inputDevice.Properties.ProductName);
                RZAxisInvert = false;
                uiDeviceRZAxisInvertTgl.Fill = TglOff;
                uiDeviceRZAxisInvertTgl.Stroke = TglOffStroke;
            }
        }

        private void eventSliderTglEnter(object sender, MouseEventArgs e)
        {
            uiDeviceSliderInvertTgl.Fill = HoverColor;
            uiDeviceSliderInvertTgl.Stroke = HoverColorStroke;
            uiDeviceSliderInvertTgl.ToolTip = invertTT;
            invertTT.Content = "Invert?";

            invertTT.IsOpen = true;
        }

        private void eventSliderTglLeave(object sender, MouseEventArgs e)
        {
            if (!SliderInvert)
            {
                uiDeviceSliderInvertTgl.Fill = TglOff;
                uiDeviceSliderInvertTgl.Stroke = TglOffStroke;
            }
            else
            {
                uiDeviceSliderInvertTgl.Fill = TglOn;
                uiDeviceSliderInvertTgl.Stroke = TglOn;
            }

            invertTT.IsOpen = false;
        }

        private void eventSliderTglClick(object sender, MouseButtonEventArgs e)
        {
            if (MainWindow.devicesConfig.Read("Slider_Axis_Invert", inputDevice.Properties.ProductName) == "false")
            {
                MainWindow.devicesConfig.DeleteKey("Slider_Axis_Invert", inputDevice.Properties.ProductName);
                MainWindow.devicesConfig.Write("Slider_Axis_Invert", "true", inputDevice.Properties.ProductName);
                SliderInvert = true;
                uiDeviceSliderInvertTgl.Fill = TglOn;
                uiDeviceSliderInvertTgl.Stroke = TglOn;

            }
            else
            {
                MainWindow.devicesConfig.DeleteKey("Slider_Axis_Invert", inputDevice.Properties.ProductName);
                MainWindow.devicesConfig.Write("Slider_Axis_Invert", "false", inputDevice.Properties.ProductName);
                SliderInvert = false;
                uiDeviceSliderInvertTgl.Fill = TglOff;
                uiDeviceSliderInvertTgl.Stroke = TglOffStroke;
            }
        }

        private void enterDialTglEnter(object sender, MouseEventArgs e)
        {
            uiDeviceDialInvertTgl.Fill = HoverColor;
            uiDeviceDialInvertTgl.Stroke = HoverColorStroke;
            uiDeviceDialInvertTgl.ToolTip = invertTT;
            invertTT.Content = "Invert?";

            invertTT.IsOpen = true;
        }

        private void enterDialTglLeave(object sender, MouseEventArgs e)
        {
            if (!DialInvert)
            {
                uiDeviceDialInvertTgl.Fill = TglOff;
                uiDeviceDialInvertTgl.Stroke = TglOffStroke;
            }
            else
            {
                uiDeviceDialInvertTgl.Fill = TglOn;
                uiDeviceDialInvertTgl.Stroke = TglOn;
            }

            invertTT.IsOpen = false;
        }

        private void enterDialTglClick(object sender, MouseButtonEventArgs e)
        {
            if (MainWindow.devicesConfig.Read("Dial_Axis_Invert", inputDevice.Properties.ProductName) == "false")
            {
                MainWindow.devicesConfig.DeleteKey("Dial_Axis_Invert", inputDevice.Properties.ProductName);
                MainWindow.devicesConfig.Write("Dial_Axis_Invert", "true", inputDevice.Properties.ProductName);
                DialInvert = true;
                uiDeviceDialInvertTgl.Fill = TglOn;
                uiDeviceDialInvertTgl.Stroke = TglOn;

            }
            else
            {
                MainWindow.devicesConfig.DeleteKey("Dial_Axis_Invert", inputDevice.Properties.ProductName);
                MainWindow.devicesConfig.Write("Dial_Axis_Invert", "false", inputDevice.Properties.ProductName);
                DialInvert = false;
                uiDeviceDialInvertTgl.Fill = TglOff;
                uiDeviceDialInvertTgl.Stroke = TglOffStroke;
            }
        }

        private void eventXAxisTglEnter(object sender, MouseEventArgs e)
        {
            uiDeviceXAxisInvertTgl.Fill = HoverColor;
            uiDeviceXAxisInvertTgl.Stroke = HoverColorStroke;
            uiDeviceXAxisInvertTgl.ToolTip = invertTT;
            invertTT.Content = "Invert X?";

            invertTT.IsOpen = true;
        }

        private void eventXAxisTglLeave(object sender, MouseEventArgs e)
        {
            if (!XAxisInvert)
            {
                uiDeviceXAxisInvertTgl.Fill = TglOff;
                uiDeviceXAxisInvertTgl.Stroke = TglOffStroke;
            }
            else
            {
                uiDeviceXAxisInvertTgl.Fill = TglOn;
                uiDeviceXAxisInvertTgl.Stroke = TglOn;
            }

            invertTT.IsOpen = false;
        }

        private void eventXAxisTglClick(object sender, MouseButtonEventArgs e)
        {
            if (MainWindow.devicesConfig.Read("X_Axis_Invert", inputDevice.Properties.ProductName) == "false")
            {
                MainWindow.devicesConfig.DeleteKey("X_Axis_Invert", inputDevice.Properties.ProductName);
                MainWindow.devicesConfig.Write("X_Axis_Invert", "true", inputDevice.Properties.ProductName);
                XAxisInvert = true;
                uiDeviceXAxisInvertTgl.Fill = TglOn;
                uiDeviceXAxisInvertTgl.Stroke = TglOn;

            }
            else
            {
                MainWindow.devicesConfig.DeleteKey("X_Axis_Invert", inputDevice.Properties.ProductName);
                MainWindow.devicesConfig.Write("X_Axis_Invert", "false", inputDevice.Properties.ProductName);
                XAxisInvert = false;
                uiDeviceXAxisInvertTgl.Fill = TglOff;
                uiDeviceXAxisInvertTgl.Stroke = TglOffStroke;
            }
        }

        private void eventYAxisTglEnter(object sender, MouseEventArgs e)
        {
            uiDeviceYAxisInvertTgl.Fill = HoverColor;
            uiDeviceYAxisInvertTgl.Stroke = HoverColorStroke;
            uiDeviceYAxisInvertTgl.ToolTip = invertTT;
            invertTT.Content = "Invert Y?";

            invertTT.IsOpen = true;
        }

        private void eventYAxisTglLeave(object sender, MouseEventArgs e)
        {
            if (!YAxisInvert)
            {
                uiDeviceYAxisInvertTgl.Fill = TglOff;
                uiDeviceYAxisInvertTgl.Stroke = TglOffStroke;
            }
            else
            {
                uiDeviceYAxisInvertTgl.Fill = TglOn;
                uiDeviceYAxisInvertTgl.Stroke = TglOn;
            }

            invertTT.IsOpen = false;
        }

        private void eventYAxisTglClick(object sender, MouseButtonEventArgs e)
        {
            if (MainWindow.devicesConfig.Read("Y_Axis_Invert", inputDevice.Properties.ProductName) == "false")
            {
                MainWindow.devicesConfig.DeleteKey("Y_Axis_Invert", inputDevice.Properties.ProductName);
                MainWindow.devicesConfig.Write("Y_Axis_Invert", "true", inputDevice.Properties.ProductName);
                YAxisInvert = true;
                uiDeviceYAxisInvertTgl.Fill = TglOn;
                uiDeviceYAxisInvertTgl.Stroke = TglOn;

            }
            else
            {
                MainWindow.devicesConfig.DeleteKey("Y_Axis_Invert", inputDevice.Properties.ProductName);
                MainWindow.devicesConfig.Write("Y_Axis_Invert", "false", inputDevice.Properties.ProductName);
                YAxisInvert = false;
                uiDeviceYAxisInvertTgl.Fill = TglOff;
                uiDeviceYAxisInvertTgl.Stroke = TglOffStroke;
            }
        }

        

        private void eventZAxisSliderClickDown(object sender, MouseButtonEventArgs e)
        {
            mouseStartPosition = e.GetPosition(uiDeviceZAxisCurveSlider);
            isDragging = true;
            uiDeviceZAxisCurveSlider.CaptureMouse();
        }

        private void eventZAxisSliderClickUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            uiDeviceZAxisCurveSlider.ReleaseMouseCapture();
            MainWindow.devicesConfig.Write("Z_Axis_Curve", ZAxisCurve.ToString(), inputDevice.Properties.ProductName);
        }

        private void eventZAxisSliderMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(uiDeviceZAxisCurveSlider);
                double deltaX = currentPosition.X - mouseStartPosition.X;

                double newX = Math.Min(Math.Max(uiDeviceZAxisCurveSlider.Margin.Left + deltaX, MinSliderMargin.Left), MaxSliderMargin.Left);
                uiDeviceZAxisCurveSlider.Margin = new Thickness(newX, 0, 0, 0);

                // Calculate normalized value
                ZAxisCurve = (float)((newX - MinSliderMargin.Left) / (MaxSliderMargin.Left - MinSliderMargin.Left));

                // Ensure slider value stays within 0.0 and 1.0
                ZAxisCurve = Math.Max(0.0f, Math.Min(1.0f, ZAxisCurve));

                uiDeviceZAxisCurvePercent.Content = ConvertToPercentageString(ZAxisCurve);
            }
        }

        public static void sliderMove(float axiscurve, FrameworkElement devconf )
        {
            double newPos = MinSliderMargin.Left + (axiscurve * (MaxSliderMargin.Left - MinSliderMargin.Left));

            newPos = Math.Max(MinSliderMargin.Left, Math.Min(MaxSliderMargin.Left, newPos));

            devconf.Margin = new Thickness(newPos,0,0,0);

            
        }

        public static void deadZoneSliderMove(float axiscurve, FrameworkElement devconf)
        {
            double newPos = MinDeadZoneSliderMargin.Left + (axiscurve * (MaxDeadZoneSliderMargin.Left - MinDeadZoneSliderMargin.Left));

            newPos = Math.Max(MinDeadZoneSliderMargin.Left, Math.Min(MaxDeadZoneSliderMargin.Left, newPos));

            devconf.Margin = new Thickness(newPos, 0, 0, 0);
        }

        public static void saturationSliderMove(float axiscurve, FrameworkElement devconf)
        {
            double newPos = MinDZSaturationSliderMargin.Right + (axiscurve * (MaxDZSaturationSliderMargin.Right - MinDZSaturationSliderMargin.Right));

            newPos = Math.Max(MinDZSaturationSliderMargin.Right, Math.Min(MaxDZSaturationSliderMargin.Right, newPos));

            devconf.Margin = new Thickness(0, 0, newPos, 0);
        }

        private void eventZAxisSliderEnter(object sender, MouseEventArgs e)
        {
            uiDeviceZAxisCurveSlider.Fill = HoverColor;
            uiDeviceZAxisCurveSlider.Stroke = HoverColorStroke;
        }

        private void eventZAxisSliderLeave(object sender, MouseEventArgs e)
        {
            uiDeviceZAxisCurveSlider.Fill = TglOn;
            uiDeviceZAxisCurveSlider.Stroke = TglOn;
        }

        public static string ConvertToPercentageString(float value)
        {
            // Convert the float value to a percentage
            float percentage = value * 100;

            // Format the percentage as a string with a % sign
            string percentageString = percentage.ToString("0") + "%";

            return percentageString;
        }

        private void eventRXAxisSliderEnter(object sender, MouseEventArgs e)
        {
            uiDeviceRXAxisCurveSlider.Fill = HoverColor;
            uiDeviceRXAxisCurveSlider.Stroke = HoverColorStroke;
        }

        private void eventRXAxisSliderLeave(object sender, MouseEventArgs e)
        {
            uiDeviceRXAxisCurveSlider.Fill = TglOn;
            uiDeviceRXAxisCurveSlider.Stroke = TglOn;
        }

        private void eventRXAxisSliderClickDown(object sender, MouseButtonEventArgs e)
        {
            mouseStartPosition = e.GetPosition(uiDeviceRXAxisCurveSlider);
            isDragging = true;
            uiDeviceRXAxisCurveSlider.CaptureMouse();
        }

        private void eventRXAxisSliderClickUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            uiDeviceRXAxisCurveSlider.ReleaseMouseCapture();
            MainWindow.devicesConfig.Write("RX_Axis_Curve", RXAxisCurve.ToString(), inputDevice.Properties.ProductName);
        }

        private void eventRXAxisSliderMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(uiDeviceRXAxisCurveSlider);
                double deltaX = currentPosition.X - mouseStartPosition.X;

                double newX = Math.Min(Math.Max(uiDeviceRXAxisCurveSlider.Margin.Left + deltaX, MinSliderMargin.Left), MaxSliderMargin.Left);
                uiDeviceRXAxisCurveSlider.Margin = new Thickness(newX, 0, 0, 0);

                // Calculate normalized value
                RXAxisCurve = (float)((newX - MinSliderMargin.Left) / (MaxSliderMargin.Left - MinSliderMargin.Left));

                // Ensure slider value stays within 0.0 and 1.0
                RXAxisCurve = Math.Max(0.0f, Math.Min(1.0f, RXAxisCurve));

                uiDeviceRXAxisCurvePercent.Content = ConvertToPercentageString(RXAxisCurve);
            }
        }

        private void eventRYAxisSliderEnter(object sender, MouseEventArgs e)
        {
            uiDeviceRYAxisCurveSlider.Fill = HoverColor;
            uiDeviceRYAxisCurveSlider.Stroke = HoverColorStroke;
        }

        private void eventRYAxisSliderLeave(object sender, MouseEventArgs e)
        {
            uiDeviceRYAxisCurveSlider.Fill = TglOn;
            uiDeviceRYAxisCurveSlider.Stroke = TglOn;
        }

        private void eventRYAxisSliderClickDown(object sender, MouseButtonEventArgs e)
        {
            mouseStartPosition = e.GetPosition(uiDeviceRYAxisCurveSlider);
            isDragging = true;
            uiDeviceRYAxisCurveSlider.CaptureMouse();
        }

        private void eventRYAxisSliderClickUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            uiDeviceRYAxisCurveSlider.ReleaseMouseCapture();
            MainWindow.devicesConfig.Write("RY_Axis_Curve", RYAxisCurve.ToString(), inputDevice.Properties.ProductName);
        }

        private void eventRYAxisSliderMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(uiDeviceRYAxisCurveSlider);
                double deltaX = currentPosition.X - mouseStartPosition.X;

                double newX = Math.Min(Math.Max(uiDeviceRYAxisCurveSlider.Margin.Left + deltaX, MinSliderMargin.Left), MaxSliderMargin.Left);
                uiDeviceRYAxisCurveSlider.Margin = new Thickness(newX, 0, 0, 0);

                // Calculate normalized value
                RYAxisCurve = (float)((newX - MinSliderMargin.Left) / (MaxSliderMargin.Left - MinSliderMargin.Left));

                // Ensure slider value stays within 0.0 and 1.0
                RYAxisCurve = Math.Max(0.0f, Math.Min(1.0f, RYAxisCurve));

                uiDeviceRYAxisCurvePercent.Content = ConvertToPercentageString(RYAxisCurve);
            }
        }

        private void eventRZAxisSliderEnter(object sender, MouseEventArgs e)
        {
            uiDeviceRZAxisCurveSlider.Fill = HoverColor;
            uiDeviceRZAxisCurveSlider.Stroke = HoverColorStroke;
        }

        private void eventRZAxisSliderLeave(object sender, MouseEventArgs e)
        {
            uiDeviceRZAxisCurveSlider.Fill = TglOn;
            uiDeviceRZAxisCurveSlider.Stroke = TglOn;
        }

        private void eventRZAxisSliderClickDown(object sender, MouseButtonEventArgs e)
        {
            mouseStartPosition = e.GetPosition(uiDeviceRZAxisCurveSlider);
            isDragging = true;
            uiDeviceRZAxisCurveSlider.CaptureMouse();
        }

        private void eventRZAxisSliderClickUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            uiDeviceRZAxisCurveSlider.ReleaseMouseCapture();
            MainWindow.devicesConfig.Write("RZ_Axis_Curve", RZAxisCurve.ToString(), inputDevice.Properties.ProductName);
        
        }

        private void eventRZAxisSliderMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(uiDeviceRZAxisCurveSlider);
                double deltaX = currentPosition.X - mouseStartPosition.X;

                double newX = Math.Min(Math.Max(uiDeviceRZAxisCurveSlider.Margin.Left + deltaX, MinSliderMargin.Left), MaxSliderMargin.Left);
                uiDeviceRZAxisCurveSlider.Margin = new Thickness(newX, 0, 0, 0);

                // Calculate normalized value
                RZAxisCurve = (float)((newX - MinSliderMargin.Left) / (MaxSliderMargin.Left - MinSliderMargin.Left));

                // Ensure slider value stays within 0.0 and 1.0
                RZAxisCurve = Math.Max(0.0f, Math.Min(1.0f, RZAxisCurve));

                uiDeviceRZAxisCurvePercent.Content = ConvertToPercentageString(RZAxisCurve);
            }
        }

        private void eventSliderAxisSliderEnter(object sender, MouseEventArgs e)
        {
            uiDeviceSliderAxisCurveSlider.Fill = HoverColor;
            uiDeviceSliderAxisCurveSlider.Stroke = HoverColorStroke;
        }

        private void eventSliderAxisSliderLeave(object sender, MouseEventArgs e)
        {
            uiDeviceSliderAxisCurveSlider.Fill = TglOn;
            uiDeviceSliderAxisCurveSlider.Stroke = TglOn;
        }

        private void eventSliderAxisSliderClickDown(object sender, MouseButtonEventArgs e)
        {
            mouseStartPosition = e.GetPosition(uiDeviceSliderAxisCurveSlider);
            isDragging = true;
            uiDeviceSliderAxisCurveSlider.CaptureMouse();
        }

        private void eventSliderAxisSliderClickUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            uiDeviceSliderAxisCurveSlider.ReleaseMouseCapture();
            MainWindow.devicesConfig.Write("Slider_Axis_Curve", SliderAxisCurve.ToString(), inputDevice.Properties.ProductName);

        }

        private void eventSliderAxisSliderMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(uiDeviceSliderAxisCurveSlider);
                double deltaX = currentPosition.X - mouseStartPosition.X;

                double newX = Math.Min(Math.Max(uiDeviceSliderAxisCurveSlider.Margin.Left + deltaX, MinSliderMargin.Left), MaxSliderMargin.Left);
                uiDeviceSliderAxisCurveSlider.Margin = new Thickness(newX, 0, 0, 0);

                // Calculate normalized value
                SliderAxisCurve = (float)((newX - MinSliderMargin.Left) / (MaxSliderMargin.Left - MinSliderMargin.Left));

                // Ensure slider value stays within 0.0 and 1.0
                SliderAxisCurve = Math.Max(0.0f, Math.Min(1.0f, SliderAxisCurve));

                uiDeviceSliderAxisCurvePercent.Content = ConvertToPercentageString(SliderAxisCurve);
            }
        }

        private void eventDialAxisSliderEnter(object sender, MouseEventArgs e)
        {
            uiDeviceDialAxisCurveSlider.Fill = HoverColor;
            uiDeviceDialAxisCurveSlider.Stroke = HoverColorStroke;
        }

        private void eventDialAxisSliderLeave(object sender, MouseEventArgs e)
        {
            uiDeviceDialAxisCurveSlider.Fill = TglOn;
            uiDeviceDialAxisCurveSlider.Stroke = TglOn;
        }

        private void eventDialAxisSliderClickDown(object sender, MouseButtonEventArgs e)
        {
            mouseStartPosition = e.GetPosition(uiDeviceDialAxisCurveSlider);
            isDragging = true;
            uiDeviceDialAxisCurveSlider.CaptureMouse();
        }

        private void eventDialAxisSliderClickUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            uiDeviceDialAxisCurveSlider.ReleaseMouseCapture();
            MainWindow.devicesConfig.Write("Dial_Axis_Curve", DialAxisCurve.ToString(), inputDevice.Properties.ProductName);

        }

        private void eventDialAxisSliderMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(uiDeviceDialAxisCurveSlider);
                double deltaX = currentPosition.X - mouseStartPosition.X;

                double newX = Math.Min(Math.Max(uiDeviceDialAxisCurveSlider.Margin.Left + deltaX, MinSliderMargin.Left), MaxSliderMargin.Left);
                uiDeviceDialAxisCurveSlider.Margin = new Thickness(newX, 0, 0, 0);

                // Calculate normalized value
                DialAxisCurve = (float)((newX - MinSliderMargin.Left) / (MaxSliderMargin.Left - MinSliderMargin.Left));

                // Ensure slider value stays within 0.0 and 1.0
                DialAxisCurve = Math.Max(0.0f, Math.Min(1.0f, DialAxisCurve));

                uiDeviceDialAxisCurvePercent.Content = ConvertToPercentageString(DialAxisCurve);
            }
        }

        private void eventXYCurvesButtonEnter(object sender, MouseEventArgs e)
        {
            uiDeviceXYCurvesButton.Fill = HoverColor;
            uiDeviceXYCurvesButton.Stroke = HoverColorStroke;
            uiDeviceXYCurvesButton1.Fill = HoverColor;
            uiDeviceXYCurvesButton1.Stroke = HoverColorStroke;
            uiDeviceXYCurvesButton2.Fill = HoverColor;
            uiDeviceXYCurvesButton2.Stroke = HoverColorStroke;
            uiDeviceXYCurvesButton3.Fill = HoverColor;
            uiDeviceXYCurvesButton3.Stroke = HoverColorStroke;
        }

        private void eventXYCurvesButtonLeave(object sender, MouseEventArgs e)
        {
            uiDeviceXYCurvesButton.Fill = TglOn2;
            uiDeviceXYCurvesButton.Stroke = TglOffStroke;
            uiDeviceXYCurvesButton1.Fill = TglOn;
            uiDeviceXYCurvesButton1.Stroke = TglOffStroke;
            uiDeviceXYCurvesButton2.Fill = TglOn;
            uiDeviceXYCurvesButton2.Stroke = TglOffStroke;
            uiDeviceXYCurvesButton3.Fill = TglOn;
            uiDeviceXYCurvesButton3.Stroke = TglOffStroke;
        }

        private void eventXYCurvesButtonClick(object sender, MouseButtonEventArgs e)
        {
            xyCurveEditorState(true);
        }

        public void xyCurveEditorState(bool state)
        {
            if (!state)
            {
                uiXYCurveWindow.Visibility = Visibility.Hidden;
                uiXYCurveWindow.IsEnabled = false;
            }
            else
            {
                uiXYCurveWindow.Visibility = Visibility.Visible;
                uiXYCurveWindow.IsEnabled = true;
            }
        }

        private static float Remap(float value, float oldLower, float oldHigher, float newLower, float newHigher)
        {
            return newLower + (value - oldLower) * (newHigher - newLower) / (oldHigher - oldLower);
        }

        public float calculateQuadraticY(float axiscurve, float x)
        {

            return MathF.Pow(x, 1.0f + axiscurve * 2f);
        }

        public float calculateRange(float axisvalue, float lowerDZ, float dz, float saturation)
        {
            if (lowerDZ < 0.01f) 
            {
                // Remaps [0,1] to [deadzone,1], with saturation applied
                axisvalue = MathF.Min(MathF.Max(Remap(axisvalue, dz, saturation, 0f, 1f), 0f), 1f);
            }
            else // Lower deadzone is on
            {
                float midPoint = lowerDZ + ((dz - lowerDZ) / 2);
                if (axisvalue > dz) // If value is greater than deadzone
                {
                    // Remap [deadzone,saturation] to [0,1] and clamp
                    axisvalue = MathF.Min(MathF.Max(Remap(axisvalue, dz, saturation, 0f, 1f), 0f), 1f);
                    // Remap [0,1] to [midpoint, saturation] and clamp
                    axisvalue = MathF.Min(MathF.Max(Remap(axisvalue, 0f, 1f, midPoint, saturation), 0f), 1f);
                }
                else if (axisvalue < lowerDZ) // If value is less than deadzone
                {
                    // Remap [1 - saturation,lowerDZ] to [0,1] and clamp
                    axisvalue = MathF.Min(MathF.Max(Remap(axisvalue, 1f - saturation, lowerDZ, 0f, 1f), 0f), 1f);
                    // Remap [0,1] to [saturation, midpoint] and clamp
                    axisvalue = MathF.Min(MathF.Max(Remap(axisvalue, 0f, 1f, 1f - saturation, midPoint), 0f), 1f);
                }
                else // Between lower deadzone and deadzone, then use midpoint (roughly 0.5)
                {
                    axisvalue = midPoint;
                }
            }
            return axisvalue;
        }



        public void drawLineForAxisCurve(float axiscurve, bool reset)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(() => drawLineForAxisCurve(axiscurve,reset));
            }
            else
            {
                if (reset)
                {
                    // Clear existing data points if reset is true
                    curveLineSeries.Clear();
                    return;
                }

                float x;
                float y;

                for (float i = 0f; i <= 1f; i += 0.1f)
                {
                    x = i;
                    y = calculateQuadraticY(axiscurve, x);  // Use axiscurve in calculation
                    curveLineSeries.Add("dynamicCurve", x, y);
                }
            }
            

            
        }

        private void eventZAxizDeadZoneSliderEnter(object sender, MouseEventArgs e)
        {
            uiDeviceZAxizDeadZoneSlider.ToolTip = deadZoneTT;
            deadZoneTT.IsOpen = true;
        }

        private void eventZAxizDeadZoneSliderLeave(object sender, MouseEventArgs e)
        {
            deadZoneTT.IsOpen = false;
        }

        private void eventZAxizDeadZoneSliderClickDown(object sender, MouseButtonEventArgs e)
        {
            mouseStartPosition = e.GetPosition(uiDeviceZAxizDeadZoneSlider);
            isDragging = true;
            uiDeviceZAxizDeadZoneSlider.CaptureMouse();

            if (e.ClickCount == 2)
            {
                uiDeviceConfigValueWindow.Visibility = Visibility.Visible;
                uiDeviceConfigValueWindow.updateValue((float value) =>
                {
                    ZAxisDZ = value;
                });
            }
        }

        private void eventZAxizDeadZoneSliderClickUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            uiDeviceZAxizDeadZoneSlider.ReleaseMouseCapture();
            MainWindow.devicesConfig.Write("Z_Axis_DZ", ZAxisDZ.ToString(), inputDevice.Properties.ProductName);

        }

        private void eventZAxizDeadZoneSliderMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(uiDeviceZAxizDeadZoneSlider);
                double deltaX = currentPosition.X - mouseStartPosition.X;

                double newX = Math.Min(Math.Max(uiDeviceZAxizDeadZoneSlider.Margin.Left + deltaX, MinDeadZoneSliderMargin.Left), MaxDeadZoneSliderMargin.Left);
                uiDeviceZAxizDeadZoneSlider.Margin = new Thickness(newX, 0, 0, 0);

                // Calculate normalized value
                ZAxisDZ = (float)((newX - MinDeadZoneSliderMargin.Left) / (MaxDeadZoneSliderMargin.Left - MinDeadZoneSliderMargin.Left));

                // Ensure slider value stays within 0.0 and 1.0
                ZAxisDZ = Math.Max(0.0f, Math.Min(1.0f, ZAxisDZ));

                
            }
        }

        private void eventZAxizDZSatSliderEnter(object sender, MouseEventArgs e)
        {
            uiDeviceZAxizDZSaturationSlider.ToolTip = saturationTT;
            saturationTT.IsOpen = true;
        }

        private void eventZAxizDZSatSliderLeave(object sender, MouseEventArgs e)
        {
            saturationTT.IsOpen = false;
        }

        private void eventZAxizDZSatSliderClickDown(object sender, MouseButtonEventArgs e)
        {
            mouseStartPosition = e.GetPosition(uiDeviceZAxizDZSaturationSlider);
            isDragging = true;
            uiDeviceZAxizDZSaturationSlider.CaptureMouse();
        }

        private void eventZAxizDZSatSliderClickUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            uiDeviceZAxizDZSaturationSlider.ReleaseMouseCapture();
            MainWindow.devicesConfig.Write("Z_Axis_Saturation", ZAxisSaturation.ToString(), inputDevice.Properties.ProductName);

        }

        private void eventZAxizDZSatSliderMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(uiDeviceZAxizDZSaturationSlider);
                double deltaX = mouseStartPosition.X - currentPosition.X; // Change in X position

                double newX = Math.Min(Math.Max(uiDeviceZAxizDZSaturationSlider.Margin.Right + deltaX, MinDZSaturationSliderMargin.Right), MaxDZSaturationSliderMargin.Right);
                uiDeviceZAxizDZSaturationSlider.Margin = new Thickness(0, 0, newX, 0);

                // Calculate normalized value (1 at full right, 0 at full left)
                ZAxisSaturation = (float)((newX - MinDZSaturationSliderMargin.Right) / (MaxDZSaturationSliderMargin.Right - MinDZSaturationSliderMargin.Right));

                // Ensure slider value stays within 0.0 and 1.0
                ZAxisSaturation = Math.Max(0.0f, Math.Min(1.0f, 1f - ZAxisSaturation));
            }
        }

        private void eventZAxizLowerDeadZoneSliderEnter(object sender, MouseEventArgs e)
        {
            uiDeviceZAxizLowerDeadZoneSlider.ToolTip = lowerDeadZoneTT;
            lowerDeadZoneTT.IsOpen = true;
        }

        private void eventZAxizLowerDeadZoneSliderLeave(object sender, MouseEventArgs e)
        {
            lowerDeadZoneTT.IsOpen = false;
        }

        private void eventZAxizLowerDeadZoneSliderClickDown(object sender, MouseButtonEventArgs e)
        {
            mouseStartPosition = e.GetPosition(uiDeviceZAxizLowerDeadZoneSlider);
            isDragging = true;
            uiDeviceZAxizLowerDeadZoneSlider.CaptureMouse();
        }

        private void eventZAxizLowerDeadZoneSliderClickUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            uiDeviceZAxizLowerDeadZoneSlider.ReleaseMouseCapture();
            MainWindow.devicesConfig.Write("Z_Axis_LDZ", ZAxisLDZ.ToString(), inputDevice.Properties.ProductName);

        }

        private void eventZAxizLowerDeadZoneSliderMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(uiDeviceZAxizDeadZoneSlider);
                double deltaX = currentPosition.X - mouseStartPosition.X;

                double newX = Math.Min(Math.Max(uiDeviceZAxizDeadZoneSlider.Margin.Left + deltaX, MinDeadZoneSliderMargin.Left), MaxDeadZoneSliderMargin.Left);
                uiDeviceZAxizLowerDeadZoneSlider.Margin = new Thickness(newX, 0, 0, 0);

                // Calculate normalized value
                ZAxisLDZ = (float)((newX - MinDeadZoneSliderMargin.Left) / (MaxDeadZoneSliderMargin.Left - MinDeadZoneSliderMargin.Left));

                // Ensure slider value stays within 0.0 and 1.0
                ZAxisLDZ = Math.Max(0.0f, Math.Min(1.0f, ZAxisLDZ));


            }
        }

        private void eventRXAxizLowerDeadZoneSliderEnter(object sender, MouseEventArgs e)
        {
            uiDeviceRXAxisLowerDeadZoneSlider.ToolTip = lowerDeadZoneTT;
            lowerDeadZoneTT.IsOpen = true;
        }

        private void eventRXAxizLowerDeadZoneSliderLeave(object sender, MouseEventArgs e)
        {
            lowerDeadZoneTT.IsOpen = false;
        }

        private void eventRXAxizLowerDeadZoneSliderClickDown(object sender, MouseButtonEventArgs e)
        {
            mouseStartPosition = e.GetPosition(uiDeviceRXAxisLowerDeadZoneSlider);
            isDragging = true;
            uiDeviceRXAxisLowerDeadZoneSlider.CaptureMouse();
        }

        private void eventRXAxizLowerDeadZoneSliderClickUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            uiDeviceRXAxisLowerDeadZoneSlider.ReleaseMouseCapture();
            MainWindow.devicesConfig.Write("RX_Axis_LDZ", RXAxisLDZ.ToString(), inputDevice.Properties.ProductName);
        }

        private void eventRXAxizLowerDeadZoneSliderMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(uiDeviceRXAxisLowerDeadZoneSlider);
                double deltaX = currentPosition.X - mouseStartPosition.X;

                double newX = Math.Min(Math.Max(uiDeviceRXAxisLowerDeadZoneSlider.Margin.Left + deltaX, MinDeadZoneSliderMargin.Left), MaxDeadZoneSliderMargin.Left);
                uiDeviceRXAxisLowerDeadZoneSlider.Margin = new Thickness(newX, 0, 0, 0);

                // Calculate normalized value
                RXAxisLDZ = (float)((newX - MinDeadZoneSliderMargin.Left) / (MaxDeadZoneSliderMargin.Left - MinDeadZoneSliderMargin.Left));

                // Ensure slider value stays within 0.0 and 1.0
                RXAxisLDZ = Math.Max(0.0f, Math.Min(1.0f, RXAxisLDZ));


            }
        }

        private void eventRXAxizDeadZoneSliderEnter(object sender, MouseEventArgs e)
        {
            uiDeviceRXAxisDeadZoneSlider.ToolTip = deadZoneTT;
            deadZoneTT.IsOpen = true;
        }

        private void eventRXAxizDeadZoneSliderLeave(object sender, MouseEventArgs e)
        {
            deadZoneTT.IsOpen = false;
        }

        private void eventRXAxizDeadZoneSliderClickDown(object sender, MouseButtonEventArgs e)
        {
            mouseStartPosition = e.GetPosition(uiDeviceRXAxisDeadZoneSlider);
            isDragging = true;
            uiDeviceRXAxisDeadZoneSlider.CaptureMouse();
        }

        private void eventRXAxizDeadZoneSliderClickUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            uiDeviceRXAxisDeadZoneSlider.ReleaseMouseCapture();
            MainWindow.devicesConfig.Write("RX_Axis_DZ", RXAxisDZ.ToString(), inputDevice.Properties.ProductName);

        }

        private void eventRXAxizDeadZoneSliderMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(uiDeviceRXAxisDeadZoneSlider);
                double deltaX = currentPosition.X - mouseStartPosition.X;

                double newX = Math.Min(Math.Max(uiDeviceRXAxisDeadZoneSlider.Margin.Left + deltaX, MinDeadZoneSliderMargin.Left), MaxDeadZoneSliderMargin.Left);
                uiDeviceRXAxisDeadZoneSlider.Margin = new Thickness(newX, 0, 0, 0);

                // Calculate normalized value
                RXAxisDZ = (float)((newX - MinDeadZoneSliderMargin.Left) / (MaxDeadZoneSliderMargin.Left - MinDeadZoneSliderMargin.Left));

                // Ensure slider value stays within 0.0 and 1.0
                RXAxisDZ = Math.Max(0.0f, Math.Min(1.0f, RXAxisDZ));


            }
        }

        private void eventRXAxizDZSatSliderEnter(object sender, MouseEventArgs e)
        {
            uiDeviceRXAxisDZSaturationSlider.ToolTip = saturationTT;
            saturationTT.IsOpen = true;
        }

        private void eventRXAxizDZSatSliderLeave(object sender, MouseEventArgs e)
        {
            saturationTT.IsOpen = false;
        }

        private void eventRXAxizDZSatSliderClickDown(object sender, MouseButtonEventArgs e)
        {
            mouseStartPosition = e.GetPosition(uiDeviceRXAxisDZSaturationSlider);
            isDragging = true;
            uiDeviceRXAxisDZSaturationSlider.CaptureMouse();
        }

        private void eventRXAxizDZSatSliderClickUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            uiDeviceRXAxisDZSaturationSlider.ReleaseMouseCapture();
            MainWindow.devicesConfig.Write("RX_Axis_Saturation", RXAxisSaturation.ToString(), inputDevice.Properties.ProductName);

        }

        private void eventRXAxizDZSatSliderMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(uiDeviceRXAxisDZSaturationSlider);
                double deltaX = mouseStartPosition.X - currentPosition.X; // Change in X position

                double newX = Math.Min(Math.Max(uiDeviceRXAxisDZSaturationSlider.Margin.Right + deltaX, MinDZSaturationSliderMargin.Right), MaxDZSaturationSliderMargin.Right);
                uiDeviceRXAxisDZSaturationSlider.Margin = new Thickness(0, 0, newX, 0);

                // Calculate normalized value (1 at full right, 0 at full left)
                RXAxisSaturation = (float)((newX - MinDZSaturationSliderMargin.Right) / (MaxDZSaturationSliderMargin.Right - MinDZSaturationSliderMargin.Right));

                // Ensure slider value stays within 0.0 and 1.0
                RXAxisSaturation = Math.Max(0.0f, Math.Min(1.0f, 1f - RXAxisSaturation));
            }
        }

        private void eventRYAxisLowerDeadZoneSliderEnter(object sender, MouseEventArgs e)
        {
            uiDeviceRYAxisLowerDeadZoneSlider.ToolTip = lowerDeadZoneTT;
            lowerDeadZoneTT.IsOpen = true;
        }

        private void eventRYAxisLowerDeadZoneSliderLeave(object sender, MouseEventArgs e)
        {
            lowerDeadZoneTT.IsOpen = false;
        }

        private void eventRYAxisLowerDeadZoneSliderClickDown(object sender, MouseButtonEventArgs e)
        {
            mouseStartPosition = e.GetPosition(uiDeviceRYAxisLowerDeadZoneSlider);
            isDragging = true;
            uiDeviceRYAxisLowerDeadZoneSlider.CaptureMouse();
        }

        private void eventRYAxisLowerDeadZoneSliderClickUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            uiDeviceRYAxisLowerDeadZoneSlider.ReleaseMouseCapture();
            MainWindow.devicesConfig.Write("RY_Axis_LDZ", RXAxisLDZ.ToString(), inputDevice.Properties.ProductName);

        }

        private void eventRYAxisLowerDeadZoneSliderMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(uiDeviceRYAxisLowerDeadZoneSlider);
                double deltaX = currentPosition.X - mouseStartPosition.X;

                double newX = Math.Min(Math.Max(uiDeviceRYAxisLowerDeadZoneSlider.Margin.Left + deltaX, MinDeadZoneSliderMargin.Left), MaxDeadZoneSliderMargin.Left);
                uiDeviceRYAxisLowerDeadZoneSlider.Margin = new Thickness(newX, 0, 0, 0);

                // Calculate normalized value
                RYAxisLDZ = (float)((newX - MinDeadZoneSliderMargin.Left) / (MaxDeadZoneSliderMargin.Left - MinDeadZoneSliderMargin.Left));

                // Ensure slider value stays within 0.0 and 1.0
                RYAxisLDZ = Math.Max(0.0f, Math.Min(1.0f, RYAxisLDZ));


            }
        }

        private void eventRYAxisDeadZoneSliderEnter(object sender, MouseEventArgs e)
        {
            uiDeviceRYAxisDeadZoneSlider.ToolTip = deadZoneTT;
            deadZoneTT.IsOpen = true;
        }

        private void eventRYAxisDeadZoneSliderLeave(object sender, MouseEventArgs e)
        {
            deadZoneTT.IsOpen = false;
        }

        private void eventRYAxisDeadZoneSliderClickDown(object sender, MouseButtonEventArgs e)
        {
            mouseStartPosition = e.GetPosition(uiDeviceRYAxisDeadZoneSlider);
            isDragging = true;
            uiDeviceRYAxisDeadZoneSlider.CaptureMouse();
        }

        private void eventRYAxisDeadZoneSliderClickUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            uiDeviceRYAxisDeadZoneSlider.ReleaseMouseCapture();
            MainWindow.devicesConfig.Write("RY_Axis_DZ", RYAxisDZ.ToString(), inputDevice.Properties.ProductName);

        }

        private void eventRYAxisDeadZoneSliderMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(uiDeviceRYAxisDeadZoneSlider);
                double deltaX = currentPosition.X - mouseStartPosition.X;

                double newX = Math.Min(Math.Max(uiDeviceRYAxisDeadZoneSlider.Margin.Left + deltaX, MinDeadZoneSliderMargin.Left), MaxDeadZoneSliderMargin.Left);
                uiDeviceRYAxisDeadZoneSlider.Margin = new Thickness(newX, 0, 0, 0);

                // Calculate normalized value
                RYAxisDZ = (float)((newX - MinDeadZoneSliderMargin.Left) / (MaxDeadZoneSliderMargin.Left - MinDeadZoneSliderMargin.Left));

                // Ensure slider value stays within 0.0 and 1.0
                RYAxisDZ = Math.Max(0.0f, Math.Min(1.0f, RYAxisDZ));


            }
        }


        private void eventRYAxisDZSatSliderEnter(object sender, MouseEventArgs e)
        {
            uiDeviceRYAxisDZSaturationSlider.ToolTip = saturationTT;
            saturationTT.IsOpen = true;
        }

        private void eventRYAxisDZSatSliderLeave(object sender, MouseEventArgs e)
        {
            saturationTT.IsOpen = false;
        }

        private void eventRYAxisDZSatSliderClickDown(object sender, MouseButtonEventArgs e)
        {
            mouseStartPosition = e.GetPosition(uiDeviceRYAxisDZSaturationSlider);
            isDragging = true;
            uiDeviceRYAxisDZSaturationSlider.CaptureMouse();
        }

        private void eventRYAxisDZSatSliderClickUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            uiDeviceRYAxisDZSaturationSlider.ReleaseMouseCapture();
            MainWindow.devicesConfig.Write("RY_Axis_Saturation", RYAxisSaturation.ToString(), inputDevice.Properties.ProductName);

        }

        private void eventRYAxisDZSatSliderMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(uiDeviceRYAxisDZSaturationSlider);
                double deltaX = mouseStartPosition.X - currentPosition.X; // Change in X position

                double newX = Math.Min(Math.Max(uiDeviceRYAxisDZSaturationSlider.Margin.Right + deltaX, MinDZSaturationSliderMargin.Right), MaxDZSaturationSliderMargin.Right);
                uiDeviceRYAxisDZSaturationSlider.Margin = new Thickness(0, 0, newX, 0);

                // Calculate normalized value (1 at full right, 0 at full left)
                RYAxisSaturation = (float)((newX - MinDZSaturationSliderMargin.Right) / (MaxDZSaturationSliderMargin.Right - MinDZSaturationSliderMargin.Right));

                // Ensure slider value stays within 0.0 and 1.0
                RYAxisSaturation = Math.Max(0.0f, Math.Min(1.0f, 1f - RYAxisSaturation));
            }
        }


        // RZ 


        private void eventRZAxisLowerDeadZoneSliderEnter(object sender, MouseEventArgs e)
        {
            uiDeviceRZAxisLowerDeadZoneSlider.ToolTip = lowerDeadZoneTT;
            lowerDeadZoneTT.IsOpen = true;
        }

        private void eventRZAxisLowerDeadZoneSliderLeave(object sender, MouseEventArgs e)
        {
            lowerDeadZoneTT.IsOpen = false;
        }

        private void eventRZAxisLowerDeadZoneSliderClickDown(object sender, MouseButtonEventArgs e)
        {
            mouseStartPosition = e.GetPosition(uiDeviceRZAxisLowerDeadZoneSlider);
            isDragging = true;
            uiDeviceRZAxisLowerDeadZoneSlider.CaptureMouse();
        }

        private void eventRZAxisLowerDeadZoneSliderClickUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            uiDeviceRZAxisLowerDeadZoneSlider.ReleaseMouseCapture();
            MainWindow.devicesConfig.Write("RZ_Axis_LDZ", RZAxisLDZ.ToString(), inputDevice.Properties.ProductName);

        }

        private void eventRZAxisLowerDeadZoneSliderMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(uiDeviceRZAxisLowerDeadZoneSlider);
                double deltaX = currentPosition.X - mouseStartPosition.X;

                double newX = Math.Min(Math.Max(uiDeviceRZAxisLowerDeadZoneSlider.Margin.Left + deltaX, MinDeadZoneSliderMargin.Left), MaxDeadZoneSliderMargin.Left);
                uiDeviceRZAxisLowerDeadZoneSlider.Margin = new Thickness(newX, 0, 0, 0);

                // Calculate normalized value
                RZAxisLDZ = (float)((newX - MinDeadZoneSliderMargin.Left) / (MaxDeadZoneSliderMargin.Left - MinDeadZoneSliderMargin.Left));

                // Ensure slider value stays within 0.0 and 1.0
                RZAxisLDZ = Math.Max(0.0f, Math.Min(1.0f, RZAxisLDZ));


            }
        }

        private void eventRZAxisDeadZoneSliderEnter(object sender, MouseEventArgs e)
        {
            uiDeviceRZAxisDeadZoneSlider.ToolTip = deadZoneTT;
            deadZoneTT.IsOpen = true;
        }

        private void eventRZAxisDeadZoneSliderLeave(object sender, MouseEventArgs e)
        {
            deadZoneTT.IsOpen = false;
        }

        private void eventRZAxisDeadZoneSliderClickDown(object sender, MouseButtonEventArgs e)
        {
            mouseStartPosition = e.GetPosition(uiDeviceRZAxisDeadZoneSlider);
            isDragging = true;
            uiDeviceRZAxisDeadZoneSlider.CaptureMouse();
        }

        private void eventRZAxisDeadZoneSliderClickUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            uiDeviceRZAxisDeadZoneSlider.ReleaseMouseCapture();
            MainWindow.devicesConfig.Write("RZ_Axis_DZ", RZAxisDZ.ToString(), inputDevice.Properties.ProductName);

        }

        private void eventRZAxisDeadZoneSliderMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(uiDeviceRZAxisDeadZoneSlider);
                double deltaX = currentPosition.X - mouseStartPosition.X;

                double newX = Math.Min(Math.Max(uiDeviceRZAxisDeadZoneSlider.Margin.Left + deltaX, MinDeadZoneSliderMargin.Left), MaxDeadZoneSliderMargin.Left);
                uiDeviceRZAxisDeadZoneSlider.Margin = new Thickness(newX, 0, 0, 0);

                // Calculate normalized value
                RZAxisDZ = (float)((newX - MinDeadZoneSliderMargin.Left) / (MaxDeadZoneSliderMargin.Left - MinDeadZoneSliderMargin.Left));

                // Ensure slider value stays within 0.0 and 1.0
                RZAxisDZ = Math.Max(0.0f, Math.Min(1.0f, RZAxisDZ));


            }
        }


        private void eventRZAxisDZSatSliderEnter(object sender, MouseEventArgs e)
        {
            uiDeviceRZAxisDZSaturationSlider.ToolTip = saturationTT;
            saturationTT.IsOpen = true;
        }

        private void eventRZAxisDZSatSliderLeave(object sender, MouseEventArgs e)
        {
            saturationTT.IsOpen = false;
        }

        private void eventRZAxisDZSatSliderClickDown(object sender, MouseButtonEventArgs e)
        {
            mouseStartPosition = e.GetPosition(uiDeviceRZAxisDZSaturationSlider);
            isDragging = true;
            uiDeviceRZAxisDZSaturationSlider.CaptureMouse();
        }

        private void eventRZAxisDZSatSliderClickUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            uiDeviceRZAxisDZSaturationSlider.ReleaseMouseCapture();
            MainWindow.devicesConfig.Write("RZ_Axis_Saturation", RYAxisSaturation.ToString(), inputDevice.Properties.ProductName);

        }

        private void eventRZAxisDZSatSliderMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(uiDeviceRZAxisDZSaturationSlider);
                double deltaX = mouseStartPosition.X - currentPosition.X; // Change in X position

                double newX = Math.Min(Math.Max(uiDeviceRZAxisDZSaturationSlider.Margin.Right + deltaX, MinDZSaturationSliderMargin.Right), MaxDZSaturationSliderMargin.Right);
                uiDeviceRZAxisDZSaturationSlider.Margin = new Thickness(0, 0, newX, 0);

                // Calculate normalized value (1 at full right, 0 at full left)
                RZAxisSaturation = (float)((newX - MinDZSaturationSliderMargin.Right) / (MaxDZSaturationSliderMargin.Right - MinDZSaturationSliderMargin.Right));

                // Ensure slider value stays within 0.0 and 1.0
                RZAxisSaturation = Math.Max(0.0f, Math.Min(1.0f, 1f - RZAxisSaturation));
            }
        }


        // Slider

        private void eventSliderLowerDeadZoneSliderEnter(object sender, MouseEventArgs e)
        {
            uiDeviceSliderLowerDeadZoneSlider.ToolTip = lowerDeadZoneTT;
            lowerDeadZoneTT.IsOpen = true;
        }

        private void eventSliderLowerDeadZoneSliderLeave(object sender, MouseEventArgs e)
        {
            lowerDeadZoneTT.IsOpen = false;
        }

        private void eventSliderLowerDeadZoneSliderClickDown(object sender, MouseButtonEventArgs e)
        {
            mouseStartPosition = e.GetPosition(uiDeviceSliderLowerDeadZoneSlider);
            isDragging = true;
            uiDeviceSliderLowerDeadZoneSlider.CaptureMouse();
        }

        private void eventSliderLowerDeadZoneSliderClickUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            uiDeviceSliderLowerDeadZoneSlider.ReleaseMouseCapture();
            MainWindow.devicesConfig.Write("Slider_Axis_LDZ", SliderAxisLDZ.ToString(), inputDevice.Properties.ProductName);

        }

        private void eventSliderLowerDeadZoneSliderMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(uiDeviceSliderLowerDeadZoneSlider);
                double deltaX = currentPosition.X - mouseStartPosition.X;

                double newX = Math.Min(Math.Max(uiDeviceSliderLowerDeadZoneSlider.Margin.Left + deltaX, MinDeadZoneSliderMargin.Left), MaxDeadZoneSliderMargin.Left);
                uiDeviceSliderLowerDeadZoneSlider.Margin = new Thickness(newX, 0, 0, 0);

                // Calculate normalized value
                SliderAxisLDZ = (float)((newX - MinDeadZoneSliderMargin.Left) / (MaxDeadZoneSliderMargin.Left - MinDeadZoneSliderMargin.Left));

                // Ensure slider value stays within 0.0 and 1.0
                SliderAxisLDZ = Math.Max(0.0f, Math.Min(1.0f, SliderAxisLDZ));


            }
        }

        private void eventSliderDeadZoneSliderEnter(object sender, MouseEventArgs e)
        {
            uiDeviceSliderDeadZoneSlider.ToolTip = deadZoneTT;
            deadZoneTT.IsOpen = true;
        }

        private void eventSliderDeadZoneSliderLeave(object sender, MouseEventArgs e)
        {
            deadZoneTT.IsOpen = false;
        }

        private void eventSliderDeadZoneSliderClickDown(object sender, MouseButtonEventArgs e)
        {
            mouseStartPosition = e.GetPosition(uiDeviceSliderDeadZoneSlider);
            isDragging = true;
            uiDeviceSliderDeadZoneSlider.CaptureMouse();
        }

        private void eventSliderDeadZoneSliderClickUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            uiDeviceSliderDeadZoneSlider.ReleaseMouseCapture();
            MainWindow.devicesConfig.Write("Slider_Axis_DZ", SliderAxisDZ.ToString(), inputDevice.Properties.ProductName);

        }

        private void eventSliderDeadZoneSliderMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(uiDeviceSliderDeadZoneSlider);
                double deltaX = currentPosition.X - mouseStartPosition.X;

                double newX = Math.Min(Math.Max(uiDeviceSliderDeadZoneSlider.Margin.Left + deltaX, MinDeadZoneSliderMargin.Left), MaxDeadZoneSliderMargin.Left);
                uiDeviceSliderDeadZoneSlider.Margin = new Thickness(newX, 0, 0, 0);

                // Calculate normalized value
                SliderAxisDZ = (float)((newX - MinDeadZoneSliderMargin.Left) / (MaxDeadZoneSliderMargin.Left - MinDeadZoneSliderMargin.Left));

                // Ensure slider value stays within 0.0 and 1.0
                SliderAxisDZ = Math.Max(0.0f, Math.Min(1.0f, SliderAxisDZ));


            }
        }

        private void eventSliderDZSatSliderEnter(object sender, MouseEventArgs e)
        {
            uiDeviceSliderDZSaturationSlider.ToolTip = saturationTT;
            saturationTT.IsOpen = true;
        }

        private void eventSliderDZSatSliderLeave(object sender, MouseEventArgs e)
        {
            saturationTT.IsOpen = false;
        }

        private void eventSliderDZSatSliderClickDown(object sender, MouseButtonEventArgs e)
        {
            mouseStartPosition = e.GetPosition(uiDeviceSliderDZSaturationSlider);
            isDragging = true;
            uiDeviceSliderDZSaturationSlider.CaptureMouse();
        }

        private void eventSliderDZSatSliderClickUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            uiDeviceSliderDZSaturationSlider.ReleaseMouseCapture();
            MainWindow.devicesConfig.Write("Slider_Axis_Saturation", SliderAxisSaturation.ToString(), inputDevice.Properties.ProductName);

        }

        private void eventSliderDZSatSliderMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(uiDeviceSliderDZSaturationSlider);
                double deltaX = mouseStartPosition.X - currentPosition.X; // Change in X position

                double newX = Math.Min(Math.Max(uiDeviceSliderDZSaturationSlider.Margin.Right + deltaX, MinDZSaturationSliderMargin.Right), MaxDZSaturationSliderMargin.Right);
                uiDeviceSliderDZSaturationSlider.Margin = new Thickness(0, 0, newX, 0);

                // Calculate normalized value (1 at full right, 0 at full left)
                SliderAxisSaturation = (float)((newX - MinDZSaturationSliderMargin.Right) / (MaxDZSaturationSliderMargin.Right - MinDZSaturationSliderMargin.Right));

                // Ensure slider value stays within 0.0 and 1.0
                SliderAxisSaturation = Math.Max(0.0f, Math.Min(1.0f, 1f - SliderAxisSaturation));
            }
        }

        // Dial

        private void eventDialLowerDeadZoneSliderEnter(object sender, MouseEventArgs e)
        {
            uiDeviceDialLowerDeadZoneSlider.ToolTip = lowerDeadZoneTT;
            lowerDeadZoneTT.IsOpen = true;
        }

        private void eventDialLowerDeadZoneSliderLeave(object sender, MouseEventArgs e)
        {
            lowerDeadZoneTT.IsOpen = false;
        }

        private void eventDialLowerDeadZoneSliderClickDown(object sender, MouseButtonEventArgs e)
        {
            mouseStartPosition = e.GetPosition(uiDeviceDialLowerDeadZoneSlider);
            isDragging = true;
            uiDeviceDialLowerDeadZoneSlider.CaptureMouse();
        }

        private void eventDialLowerDeadZoneSliderClickUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            uiDeviceDialLowerDeadZoneSlider.ReleaseMouseCapture();
            MainWindow.devicesConfig.Write("Dial_Axis_LDZ", DialAxisLDZ.ToString(), inputDevice.Properties.ProductName);

        }

        private void eventDialLowerDeadZoneSliderMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(uiDeviceDialLowerDeadZoneSlider);
                double deltaX = currentPosition.X - mouseStartPosition.X;

                double newX = Math.Min(Math.Max(uiDeviceDialLowerDeadZoneSlider.Margin.Left + deltaX, MinDeadZoneSliderMargin.Left), MaxDeadZoneSliderMargin.Left);
                uiDeviceDialLowerDeadZoneSlider.Margin = new Thickness(newX, 0, 0, 0);

                // Calculate normalized value
                DialAxisLDZ = (float)((newX - MinDeadZoneSliderMargin.Left) / (MaxDeadZoneSliderMargin.Left - MinDeadZoneSliderMargin.Left));

                // Ensure slider value stays within 0.0 and 1.0
                DialAxisLDZ = Math.Max(0.0f, Math.Min(1.0f, DialAxisLDZ));


            }
        }

        private void eventDialDeadZoneSliderEnter(object sender, MouseEventArgs e)
        {
            uiDeviceDialDeadZoneSlider.ToolTip = deadZoneTT;
            deadZoneTT.IsOpen = true;
        }

        private void eventDialDeadZoneSliderLeave(object sender, MouseEventArgs e)
        {
            deadZoneTT.IsOpen = false;
        }

        private void eventDialDeadZoneSliderClickDown(object sender, MouseButtonEventArgs e)
        {
            mouseStartPosition = e.GetPosition(uiDeviceDialDeadZoneSlider);
            isDragging = true;
            uiDeviceDialDeadZoneSlider.CaptureMouse();
        }

        private void eventDialDeadZoneSliderClickUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            uiDeviceDialDeadZoneSlider.ReleaseMouseCapture();
            MainWindow.devicesConfig.Write("Dial_Axis_DZ", DialAxisDZ.ToString(), inputDevice.Properties.ProductName);

        }

        private void eventDialDeadZoneSliderMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(uiDeviceDialDeadZoneSlider);
                double deltaX = currentPosition.X - mouseStartPosition.X;

                double newX = Math.Min(Math.Max(uiDeviceDialDeadZoneSlider.Margin.Left + deltaX, MinDeadZoneSliderMargin.Left), MaxDeadZoneSliderMargin.Left);
                uiDeviceDialDeadZoneSlider.Margin = new Thickness(newX, 0, 0, 0);

                // Calculate normalized value
                DialAxisDZ = (float)((newX - MinDeadZoneSliderMargin.Left) / (MaxDeadZoneSliderMargin.Left - MinDeadZoneSliderMargin.Left));

                // Ensure slider value stays within 0.0 and 1.0
                DialAxisDZ = Math.Max(0.0f, Math.Min(1.0f, DialAxisDZ));


            }
        }

        private void eventDialDZSatSliderEnter(object sender, MouseEventArgs e)
        {
            uiDeviceDialDZSaturationSlider.ToolTip = saturationTT;
            saturationTT.IsOpen = true;
        }

        private void eventDialDZSatSliderLeave(object sender, MouseEventArgs e)
        {
            saturationTT.IsOpen = false;
        }

        private void eventDialDZSatSliderClickDown(object sender, MouseButtonEventArgs e)
        {
            mouseStartPosition = e.GetPosition(uiDeviceDialDZSaturationSlider);
            isDragging = true;
            uiDeviceDialDZSaturationSlider.CaptureMouse();
        }

        private void eventDialDZSatSliderClickUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            uiDeviceDialDZSaturationSlider.ReleaseMouseCapture();
            MainWindow.devicesConfig.Write("Dial_Axis_Saturation", DialAxisSaturation.ToString(), inputDevice.Properties.ProductName);

        }

        private void eventDialDZSatSliderMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(uiDeviceDialDZSaturationSlider);
                double deltaX = mouseStartPosition.X - currentPosition.X; // Change in X position

                double newX = Math.Min(Math.Max(uiDeviceDialDZSaturationSlider.Margin.Right + deltaX, MinDZSaturationSliderMargin.Right), MaxDZSaturationSliderMargin.Right);
                uiDeviceDialDZSaturationSlider.Margin = new Thickness(0, 0, newX, 0);

                // Calculate normalized value (1 at full right, 0 at full left)
                DialAxisSaturation = (float)((newX - MinDZSaturationSliderMargin.Right) / (MaxDZSaturationSliderMargin.Right - MinDZSaturationSliderMargin.Right));

                // Ensure slider value stays within 0.0 and 1.0
                DialAxisSaturation = Math.Max(0.0f, Math.Min(1.0f, 1f - DialAxisSaturation));
            }
        }

        // Deadzone Modes
        private void eventZAxisDeadZoneSliderRClick(object sender, MouseButtonEventArgs e)
        {
            if (ZAxisMode == 1)
            {
                ZAxisMode = 0;
                MainWindow.devicesConfig.Write("Z_Axis_Mode", "0", inputDevice.Properties.ProductName);
                uiDeviceZAxizLowerDeadZoneSlider.Visibility = Visibility.Hidden;
                sliderMoveAndSet(0f, "Z_Axis_LDZ", ref ZAxisLDZ, uiDeviceZAxizLowerDeadZoneSlider);
                sliderMoveAndSet(0f, "Z_Axis_DZ", ref ZAxisDZ, uiDeviceZAxizDeadZoneSlider);
            }
            else if (ZAxisMode == 0)
            {
                ZAxisMode = 1;
                MainWindow.devicesConfig.Write("Z_Axis_Mode", "1", inputDevice.Properties.ProductName);
                uiDeviceZAxizLowerDeadZoneSlider.Visibility = Visibility.Visible;
                sliderMoveAndSet(0.515f, "Z_Axis_DZ", ref ZAxisDZ, uiDeviceZAxizDeadZoneSlider);
                sliderMoveAndSet(0.485f, "Z_Axis_LDZ", ref ZAxisLDZ, uiDeviceZAxizLowerDeadZoneSlider);
            }
        }

        private void eventRXAxisDeadZoneSliderRClick(object sender, MouseButtonEventArgs e)
        {
            if (RXAxisMode == 1)
            {
                RXAxisMode = 0;
                MainWindow.devicesConfig.Write("RX_Axis_Mode", "0", inputDevice.Properties.ProductName);
                uiDeviceRXAxisLowerDeadZoneSlider.Visibility = Visibility.Hidden;
                sliderMoveAndSet(0f, "RX_Axis_LDZ", ref RXAxisLDZ, uiDeviceRXAxisLowerDeadZoneSlider);
                sliderMoveAndSet(0f, "RX_Axis_DZ", ref RXAxisDZ, uiDeviceRXAxisDeadZoneSlider);
            }
            else if (RXAxisMode == 0)
            {
                RXAxisMode = 1;
                MainWindow.devicesConfig.Write("RX_Axis_Mode", "1", inputDevice.Properties.ProductName);
                uiDeviceRXAxisLowerDeadZoneSlider.Visibility = Visibility.Visible;
                sliderMoveAndSet(0.515f, "RX_Axis_DZ", ref RXAxisDZ, uiDeviceRXAxisDeadZoneSlider);
                sliderMoveAndSet(0.485f, "RX_Axis_LDZ", ref RXAxisLDZ, uiDeviceRXAxisLowerDeadZoneSlider);
            }
        }

        private void eventRYAxisDeadZoneSliderRClickUp(object sender, MouseButtonEventArgs e)
        {
            if (RYAxisMode == 1)
            {
                RYAxisMode = 0;
                MainWindow.devicesConfig.Write("RY_Axis_Mode", "0", inputDevice.Properties.ProductName);
                uiDeviceRYAxisLowerDeadZoneSlider.Visibility = Visibility.Hidden;
                sliderMoveAndSet(0f, "RY_Axis_LDZ", ref RYAxisLDZ, uiDeviceRYAxisLowerDeadZoneSlider);
                sliderMoveAndSet(0f, "RY_Axis_DZ", ref RYAxisDZ, uiDeviceRYAxisDeadZoneSlider);
            }
            else if (RYAxisMode == 0)
            {
                RYAxisMode = 1;
                MainWindow.devicesConfig.Write("RY_Axis_Mode", "1", inputDevice.Properties.ProductName);
                uiDeviceRYAxisLowerDeadZoneSlider.Visibility = Visibility.Visible;
                sliderMoveAndSet(0.515f, "RY_Axis_DZ", ref RYAxisDZ, uiDeviceRYAxisDeadZoneSlider);
                sliderMoveAndSet(0.485f, "RY_Axis_LDZ", ref RYAxisLDZ, uiDeviceRYAxisLowerDeadZoneSlider);
            }
        }

        private void eventRZAxisDeadZoneSliderRClick(object sender, MouseButtonEventArgs e)
        {
            if (RZAxisMode == 1)
            {
                RZAxisMode = 0;
                MainWindow.devicesConfig.Write("RZ_Axis_Mode", "0", inputDevice.Properties.ProductName);
                uiDeviceRZAxisLowerDeadZoneSlider.Visibility = Visibility.Hidden;
                sliderMoveAndSet(0f, "RZ_Axis_LDZ", ref RZAxisLDZ, uiDeviceRZAxisLowerDeadZoneSlider);
                sliderMoveAndSet(0f, "RZ_Axis_DZ", ref RZAxisDZ, uiDeviceRZAxisDeadZoneSlider);
            }
            else if (RZAxisMode == 0)
            {
                RZAxisMode = 1;
                MainWindow.devicesConfig.Write("RZ_Axis_Mode", "1", inputDevice.Properties.ProductName);
                uiDeviceRZAxisLowerDeadZoneSlider.Visibility = Visibility.Visible;
                sliderMoveAndSet(0.515f, "RZ_Axis_DZ", ref RZAxisDZ, uiDeviceRZAxisDeadZoneSlider);
                sliderMoveAndSet(0.485f, "RZ_Axis_LDZ", ref RZAxisLDZ, uiDeviceRZAxisLowerDeadZoneSlider);
            }
        }

        private void eventSliderDeadZoneSliderRClick(object sender, MouseButtonEventArgs e)
        {
            if (SliderAxisMode == 1)
            {
                SliderAxisMode = 0;
                MainWindow.devicesConfig.Write("Slider_Axis_Mode", "0", inputDevice.Properties.ProductName);
                uiDeviceSliderLowerDeadZoneSlider.Visibility = Visibility.Hidden;
                sliderMoveAndSet(0f, "Slider_Axis_LDZ", ref SliderAxisLDZ, uiDeviceSliderLowerDeadZoneSlider);
                sliderMoveAndSet(0f, "Slider_Axis_DZ", ref SliderAxisDZ, uiDeviceSliderDeadZoneSlider);
            }
            else if (SliderAxisMode == 0)
            {
                SliderAxisMode = 1;
                MainWindow.devicesConfig.Write("Slider_Axis_Mode", "1", inputDevice.Properties.ProductName);
                uiDeviceSliderLowerDeadZoneSlider.Visibility = Visibility.Visible;
                sliderMoveAndSet(0.515f, "Slider_Axis_DZ", ref SliderAxisDZ, uiDeviceSliderDeadZoneSlider);
                sliderMoveAndSet(0.485f, "Slider_Axis_LDZ", ref SliderAxisLDZ, uiDeviceSliderLowerDeadZoneSlider);
            }
        }

        private void eventDialDeadZoneSliderRClick(object sender, MouseButtonEventArgs e)
        {
            if (DialAxisMode == 1)
            {
                DialAxisMode = 0;
                MainWindow.devicesConfig.Write("Dial_Axis_Mode", "0", inputDevice.Properties.ProductName);
                uiDeviceDialLowerDeadZoneSlider.Visibility = Visibility.Hidden;
                sliderMoveAndSet(0f, "Dial_Axis_LDZ", ref DialAxisLDZ, uiDeviceDialLowerDeadZoneSlider);
                sliderMoveAndSet(0f, "Dial_Axis_DZ", ref  DialAxisDZ,  uiDeviceDialDeadZoneSlider);
            }
            else if (DialAxisMode == 0)
            {
                DialAxisMode = 1;
                MainWindow.devicesConfig.Write("Dial_Axis_Mode", "1", inputDevice.Properties.ProductName);
                uiDeviceDialLowerDeadZoneSlider.Visibility = Visibility.Visible;
                sliderMoveAndSet(0.515f, "Dial_Axis_DZ", ref  DialAxisDZ,  uiDeviceDialDeadZoneSlider);
                sliderMoveAndSet(0.485f, "Dial_Axis_LDZ", ref DialAxisLDZ, uiDeviceDialLowerDeadZoneSlider);
            }
        }
    }

}

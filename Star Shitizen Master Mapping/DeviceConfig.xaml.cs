﻿using GLGraphs.NetGraph;
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

        public static bool XAxisInvert = false;
        public static bool YAxisInvert = false;
        public static bool ZAxisInvert = false;
        public static bool RXAxisInvert = false;
        public static bool RYAxisInvert = false;
        public static bool RZAxisInvert = false;
        public static bool SliderInvert = false;
        public static bool DialInvert = false;
        public static float XAxisCurve = 0f;
        public static float YAxisCurve = 0f;
        public static float ZAxisCurve = 0f;
        public static float RXAxisCurve = 0f;
        public static float RYAxisCurve = 0f;
        public static float RZAxisCurve = 0f;
        public static float SliderAxisCurve = 0f;
        public static float DialAxisCurve = 0f;
        public static float XAxisDZ = 0f;
        public static float XAxisLDZ = 0f;
        public static float XAxisSaturation = 0f;
        public static float YAxisDZ = 0f;
        public static float YAxisLDZ = 0f;
        public static float YAxisSaturation = 0f;
        public static float ZAxisDZ = 0f;
        public static float ZAxisLDZ = 0f;
        public static float ZAxisSaturation = 0f;
        public static float RXAxisDZ = 0f;
        public static float RXAxisLDZ = 0f;
        public static float RXAxisSaturation = 0f;
        public static float RYAxisDZ = 0f;
        public static float RYAxisLDZ = 0f;
        public static float RYAxisSaturation = 0f;
        public static float RZAxisDZ = 0f;
        public static float RZAxisLDZ = 0f;
        public static float RZAxisSaturation = 0f;
        public static float SliderAxisDZ = 0f;
        public static float SliderAxisLDZ = 0f;
        public static float SliderAxisSaturation = 0f;
        public static float DialAxisDZ = 0f;
        public static float DialAxisLDZ = 0f;
        public static float DialAxisSaturation = 0f;

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
            getSetFloat(ref ZAxisDZ, "Z_Axis_DZ");
            getSetFloat(ref ZAxisLDZ, "Z_Axis_LDZ");
            getSetFloat(ref ZAxisSaturation, "Z_Axis_Saturation");
            getSetFloat(ref RXAxisDZ, "RX_Axis_DZ");
            getSetFloat(ref RXAxisLDZ, "RX_Axis_LDZ");
            getSetFloat(ref RXAxisSaturation, "RX_Axis_Saturation");


            sliderMove(ZAxisCurve, uiDeviceZAxisCurveSlider);
            sliderMove(RXAxisCurve, uiDeviceRXAxisCurveSlider);
            sliderMove(RYAxisCurve, uiDeviceRYAxisCurveSlider);
            sliderMove(RZAxisCurve, uiDeviceRZAxisCurveSlider);
            sliderMove(SliderAxisCurve, uiDeviceSliderAxisCurveSlider);
            sliderMove(DialAxisCurve, uiDeviceDialAxisCurveSlider);

            deadZoneSliderMove(ZAxisDZ, uiDeviceZAxizDeadZoneSlider);
            deadZoneSliderMove(ZAxisLDZ, uiDeviceZAxizLowerDeadZoneSlider);
            saturationSliderMove(ZAxisSaturation, uiDeviceZAxizDZSaturationSlider);

            deadZoneSliderMove(RXAxisDZ, uiDeviceRXAxisDeadZoneSlider);
            deadZoneSliderMove(RXAxisLDZ, uiDeviceRXAxisDeadZoneSlider);
            saturationSliderMove(RXAxisSaturation, uiDeviceRXAxisDeadZoneSlider);

            deadZoneSliderMove(RYAxisDZ, uiDeviceRYAxisDeadZoneSlider);
            deadZoneSliderMove(RYAxisLDZ, uiDeviceRYAxisDeadZoneSlider);
            saturationSliderMove(RYAxisSaturation, uiDeviceRYAxisDeadZoneSlider);


            uiDeviceZAxisCurvePercent.Content = ConvertToPercentageString(ZAxisCurve);         
            uiDeviceRXAxisCurvePercent.Content = ConvertToPercentageString(RXAxisCurve);
            uiDeviceRYAxisCurvePercent.Content = ConvertToPercentageString(RYAxisCurve);
            uiDeviceRZAxisCurvePercent.Content = ConvertToPercentageString(RZAxisCurve);
            uiDeviceSliderAxisCurvePercent.Content = ConvertToPercentageString(SliderAxisCurve);
            uiDeviceDialAxisCurvePercent.Content = ConvertToPercentageString(DialAxisCurve);          
            

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
            var curvedAxis = calculateQuadraticY(ZAxisCurve,zAxis);
            SetUiWidth(uiDeviceZAxisFreGrnd, (double)(GetUiWidth(uiDeviceZAxisBckGrnd) * curvedAxis));
        }

        private void XRot(float xRotation)
        {
            var curvedAxis = calculateQuadraticY(RXAxisCurve, xRotation);
            SetUiWidth(uiDeviceXRotFreGrnd, (double)(GetUiWidth(uiDeviceXRotBckGrnd) * curvedAxis));
        }

        private void YRot(float yRotation)
        {
            var curvedAxis = calculateQuadraticY(RYAxisCurve, yRotation);
            SetUiWidth(uiDeviceYRotFreGrnd, (double)(GetUiWidth(uiDeviceYRotBckGrnd) * curvedAxis));
        }

        private void ZRot(float zRotation)
        {
            var curvedAxis = calculateQuadraticY(RZAxisCurve, zRotation);
            SetUiWidth(uiDeviceZRotFreGrnd, (double)(GetUiWidth(uiDeviceZRotBckGrnd) * curvedAxis));
        }

        private void Slide(float SliderValue)
        {
            var curvedAxis = calculateQuadraticY(SliderAxisCurve, SliderValue);
            SetUiWidth(uiDeviceSliderFreGrnd, (double)(GetUiWidth(uiDeviceSliderBckGrnd) * curvedAxis));
        }

        private void Dial(float DialValue)
        {
            var curvedAxis = calculateQuadraticY(DialAxisCurve, DialValue);
            SetUiWidth(uiDeviceDialFreGrnd, (double)(GetUiWidth(uiDeviceDialBckGrnd) * curvedAxis));
        }

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

        public float calculateQuadraticY(float axiscurve, float x)
        {

            return MathF.Pow(x, 1.0f + axiscurve * 2f);
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
                ZAxisSaturation = Math.Max(0.0f, Math.Min(1.0f, ZAxisSaturation));
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
                RXAxisSaturation = Math.Max(0.0f, Math.Min(1.0f, RXAxisSaturation));
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
                RYAxisSaturation = Math.Max(0.0f, Math.Min(1.0f, RYAxisSaturation));
            }
        }
    }

}

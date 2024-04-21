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

namespace Star_Shitizen_Master_Mapping
{
    /// <summary>
    /// Interaction logic for DeviceConfig.xaml
    /// </summary>
    public partial class DeviceConfig : UserControl
    {
        public string deviceName = string.Empty;
        public string deviceID { get; set; }
        
        public Joystick inputDevice { get; set; }
        public bool inpDvAcquired = false;
        public SharpDX.DirectInput.JoystickState inputState = new SharpDX.DirectInput.JoystickState();
        
        public GraphSeries<string> xySeries { get; set; }
        public GraphSeries<string> povCircleSeries { get; set; }
        public GraphSeries<string> povSeries { get; set; }

        public float zAxisMaxWidth = 0f;

        SolidColorBrush TglOn = new SolidColorBrush();
        SolidColorBrush TglOff = new SolidColorBrush();
        SolidColorBrush TglOffStroke = new SolidColorBrush();

        ToolTip invertTT = new ToolTip();

        public bool XAxisInvert = false;
        public bool YAxisInvert = false;
        public bool ZAxisInvert = false;
        public bool RXAxisInvert = false;
        public bool RYAxisInvert = false;
        public bool RZAxisInvert = false;
        public bool SliderInvert = false;
        public bool DialInvert = false;


        public DeviceConfig()
        {
            InitializeComponent();

            zAxisMaxWidth = (float)uiDeviceZAxisBckGrnd.Width;

            DataContext = this;

            xySeries = uiDeviceOutputGraph.Graph.State.AddSeries(SeriesType.Point, deviceName);
            povCircleSeries = uiDeviceOutputGraph.Graph.State.AddSeries(SeriesType.Line, "povCircle");
            povSeries = uiDeviceOutputGraph.Graph.State.AddSeries(SeriesType.Point, deviceName + "_pov");

            TglOn.Color = Color.FromArgb(255, 58, 125, 177);
            TglOff.Color = Color.FromArgb(201,10,29,41);
            TglOffStroke.Color = Color.FromArgb(255, 58, 113, 135);

            invertTT.Content = "Invert?";

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
                            povPointer(adjustedPov, 0.75f);
                            joyPointer(XAxisInvert ? -adjustedX : adjustedX, YAxisInvert ? -adjustedY : adjustedY);
                            ZAxis(ZAxisInvert ? (1.0f - adjustedZ) : adjustedZ);
                            XRot(RXAxisInvert ? (1.0f - adjustedXRot) : adjustedXRot);
                            YRot(RYAxisInvert ? (1.0f - adjustedYRot) : adjustedYRot);
                            ZRot(RZAxisInvert ? (1.0f - adjustedZRot) : adjustedZRot);
                            ZRot(RZAxisInvert ? (1.0f - adjustedZRot) : adjustedZRot);
                            Slide(SliderInvert ? (1.0f - adjustedSlider) : adjustedSlider);
                            Dial(DialInvert ? (1.0f - adjustedDial) : adjustedDial);

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

            graphDrawCircle(povCircleSeries, 0f, 0f, 0.75f, 24);
            povCircleSeries.Color = new Color4(85, 85, 85, 255);
            xySeries.Color = new Color4(58, 125, 177, 255);
            povSeries.Color = new Color4(31, 71, 99, 255);
            povSeries.PointShape = SeriesPointShape.Circle;

            getSetInvert(ref XAxisInvert, "X_Axis_Invert");
            getSetInvert(ref YAxisInvert, "Y_Axis_Invert");
            getSetInvert(ref ZAxisInvert, "Z_Axis_Invert");
            getSetInvert(ref RXAxisInvert, "RX_Axis_Invert");
            getSetInvert(ref RYAxisInvert, "RY_Axis_Invert");
            getSetInvert(ref RZAxisInvert, "RZ_Axis_Invert");
            getSetInvert(ref SliderInvert, "Slider_Axis_Invert");
            getSetInvert(ref DialInvert, "D_Axis_Invert");




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
                    uiDevicesRXAxisInvertTgl.Fill = TglOff;
                    uiDevicesRXAxisInvertTgl.Stroke = TglOffStroke;
                }
                else
                {
                    uiDevicesRXAxisInvertTgl.Fill = TglOn;
                    uiDevicesRXAxisInvertTgl.Stroke = TglOn;
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


                uiDeviceOutputGraph.Graph.State.Camera.Target.Position = new Vector2(0, 0);
                uiDeviceOutputGraph.Graph.State.Camera.Target.VerticalSize = 2f;
                uiDeviceOutputGraph.Graph.State.Camera.Snap();

                


            }


            if (device != null)
            {
                if (deviceName.ToString().Contains("Mouse") || deviceName.ToString().Contains("Keyboard"))
                {
                    uiDeviceOutputGraph.Visibility = Visibility.Hidden;
                    uiDeviceAxisScreen.Visibility = Visibility.Hidden;
                }
                else
                {
                    uiDeviceOutputGraph.Visibility = Visibility.Visible;
                    uiDeviceAxisScreen.Visibility = Visibility.Visible;
                }
            }



        }

        private void joyPointer(float x, float y)
        {
            xySeries.Clear();
            xySeries.Add(deviceName, x, y);
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
            SetUiWidth(uiDeviceZAxisFreGrnd, (double)(GetUiWidth(uiDeviceZAxisBckGrnd) * zAxis));
        }

        private void XRot(float xRotation)
        {
            SetUiWidth(uiDeviceXRotFreGrnd, (double)(GetUiWidth(uiDeviceXRotBckGrnd) * xRotation));
        }

        private void YRot(float yRotation)
        {
            SetUiWidth(uiDeviceYRotFreGrnd, (double)(GetUiWidth(uiDeviceYRotBckGrnd) * yRotation));
        }

        private void ZRot(float zRotation)
        {
            SetUiWidth(uiDeviceZRotFreGrnd, (double)(GetUiWidth(uiDeviceZRotBckGrnd) * zRotation));
        }

        private void Slide(float SliderValue)
        {
            SetUiWidth(uiDeviceSliderFreGrnd, (double)(GetUiWidth(uiDeviceSliderBckGrnd) * SliderValue));
        }

        private void Dial(float DialValue)
        {
            SetUiWidth(uiDeviceDialFreGrnd, (double)(GetUiWidth(uiDeviceDialBckGrnd) * DialValue));
        }

        private void eventZAxisTglEnter(object sender, MouseEventArgs e)
        {
            uiDeviceZAxisInvertTgl.Fill = TglOn;
            uiDeviceZAxisInvertTgl.Stroke = TglOn;
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
            
            invertTT.IsOpen = false;
        }

        private void eventZAxisTglClick(object sender, MouseButtonEventArgs e)
        {
            if (MainWindow.devicesConfig.Read("Z_Axis_Invert", inputDevice.Properties.ProductName) == "false")
            {
                MainWindow.devicesConfig.DeleteKey("Z_Axis_Invert", inputDevice.Properties.ProductName);
                MainWindow.devicesConfig.Write("Z_Axis_Invert", "true", inputDevice.Properties.ProductName);
                ZAxisInvert = true;

            }
            else
            {
                MainWindow.devicesConfig.DeleteKey("Z_Axis_Invert", inputDevice.Properties.ProductName);
                MainWindow.devicesConfig.Write("Z_Axis_Invert", "false", inputDevice.Properties.ProductName);
                ZAxisInvert = false;
            }
        }

        private void eventRXAxisTglEnter(object sender, MouseEventArgs e)
        {
            uiDevicesRXAxisInvertTgl.Fill = TglOn;
            uiDevicesRXAxisInvertTgl.Stroke = TglOn;
            uiDevicesRXAxisInvertTgl.ToolTip = invertTT;
            invertTT.Content = "Invert?";

            invertTT.IsOpen = true;
        }

        private void eventRXAxisTglLeave(object sender, MouseEventArgs e)
        {
            if (!RXAxisInvert)
            {
                uiDevicesRXAxisInvertTgl.Fill = TglOff;
                uiDevicesRXAxisInvertTgl.Stroke = TglOffStroke;
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

            }
            else
            {
                MainWindow.devicesConfig.DeleteKey("RX_Axis_Invert", inputDevice.Properties.ProductName);
                MainWindow.devicesConfig.Write("RX_Axis_Invert", "false", inputDevice.Properties.ProductName);
                RXAxisInvert = false;
            }
        }

        private void eventRYAxisTglEnter(object sender, MouseEventArgs e)
        {
            uiDeviceRYAxisInvertTgl.Fill = TglOn;
            uiDeviceRYAxisInvertTgl.Stroke = TglOn;
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

            invertTT.IsOpen = false;
        }

        private void eventRYAxisTglClick(object sender, MouseButtonEventArgs e)
        {
            if (MainWindow.devicesConfig.Read("RY_Axis_Invert", inputDevice.Properties.ProductName) == "false")
            {
                MainWindow.devicesConfig.DeleteKey("RY_Axis_Invert", inputDevice.Properties.ProductName);
                MainWindow.devicesConfig.Write("RY_Axis_Invert", "true", inputDevice.Properties.ProductName);
                RYAxisInvert = true;

            }
            else
            {
                MainWindow.devicesConfig.DeleteKey("RY_Axis_Invert", inputDevice.Properties.ProductName);
                MainWindow.devicesConfig.Write("RY_Axis_Invert", "false", inputDevice.Properties.ProductName);
                RYAxisInvert = false;
            }
        }

        private void eventRZAxisTglEnter(object sender, MouseEventArgs e)
        {
            uiDeviceRZAxisInvertTgl.Fill = TglOn;
            uiDeviceRZAxisInvertTgl.Stroke = TglOn;
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

            invertTT.IsOpen = false;
        }

        private void eventRZAxisTglClick(object sender, MouseButtonEventArgs e)
        {
            if (MainWindow.devicesConfig.Read("RZ_Axis_Invert", inputDevice.Properties.ProductName) == "false")
            {
                MainWindow.devicesConfig.DeleteKey("RZ_Axis_Invert", inputDevice.Properties.ProductName);
                MainWindow.devicesConfig.Write("RZ_Axis_Invert", "true", inputDevice.Properties.ProductName);
                RZAxisInvert = true;

            }
            else
            {
                MainWindow.devicesConfig.DeleteKey("RZ_Axis_Invert", inputDevice.Properties.ProductName);
                MainWindow.devicesConfig.Write("RZ_Axis_Invert", "false", inputDevice.Properties.ProductName);
                RZAxisInvert = false;
            }
        }

        private void eventSliderTglEnter(object sender, MouseEventArgs e)
        {
            uiDeviceSliderInvertTgl.Fill = TglOn;
            uiDeviceSliderInvertTgl.Stroke = TglOn;
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

            invertTT.IsOpen = false;
        }

        private void eventSliderTglClick(object sender, MouseButtonEventArgs e)
        {
            if (MainWindow.devicesConfig.Read("Slider_Axis_Invert", inputDevice.Properties.ProductName) == "false")
            {
                MainWindow.devicesConfig.DeleteKey("Slider_Axis_Invert", inputDevice.Properties.ProductName);
                MainWindow.devicesConfig.Write("Slider_Axis_Invert", "true", inputDevice.Properties.ProductName);
                RZAxisInvert = true;

            }
            else
            {
                MainWindow.devicesConfig.DeleteKey("Slider_Axis_Invert", inputDevice.Properties.ProductName);
                MainWindow.devicesConfig.Write("Slider_Axis_Invert", "false", inputDevice.Properties.ProductName);
                RZAxisInvert = false;
            }
        }

        private void enterDialTglEnter(object sender, MouseEventArgs e)
        {
            uiDeviceDialInvertTgl.Fill = TglOn;
            uiDeviceDialInvertTgl.Stroke = TglOn;
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

            invertTT.IsOpen = false;
        }

        private void enterDialTglClick(object sender, MouseButtonEventArgs e)
        {
            if (MainWindow.devicesConfig.Read("Dial_Axis_Invert", inputDevice.Properties.ProductName) == "false")
            {
                MainWindow.devicesConfig.DeleteKey("Dial_Axis_Invert", inputDevice.Properties.ProductName);
                MainWindow.devicesConfig.Write("Dial_Axis_Invert", "true", inputDevice.Properties.ProductName);
                DialInvert = true;

            }
            else
            {
                MainWindow.devicesConfig.DeleteKey("Dial_Axis_Invert", inputDevice.Properties.ProductName);
                MainWindow.devicesConfig.Write("Dial_Axis_Invert", "false", inputDevice.Properties.ProductName);
                DialInvert = false;
            }
        }

        private void eventXAxisTglEnter(object sender, MouseEventArgs e)
        {
            uiDeviceXAxisInvertTgl.Fill = TglOn;
            uiDeviceXAxisInvertTgl.Stroke = TglOn;
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

            invertTT.IsOpen = false;
        }

        private void eventXAxisTglClick(object sender, MouseButtonEventArgs e)
        {
            if (MainWindow.devicesConfig.Read("X_Axis_Invert", inputDevice.Properties.ProductName) == "false")
            {
                MainWindow.devicesConfig.DeleteKey("X_Axis_Invert", inputDevice.Properties.ProductName);
                MainWindow.devicesConfig.Write("X_Axis_Invert", "true", inputDevice.Properties.ProductName);
                XAxisInvert = true;

            }
            else
            {
                MainWindow.devicesConfig.DeleteKey("X_Axis_Invert", inputDevice.Properties.ProductName);
                MainWindow.devicesConfig.Write("X_Axis_Invert", "false", inputDevice.Properties.ProductName);
                XAxisInvert = false;
            }
        }

        private void eventYAxisTglEnter(object sender, MouseEventArgs e)
        {
            uiDeviceYAxisInvertTgl.Fill = TglOn;
            uiDeviceYAxisInvertTgl.Stroke = TglOn;
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

            invertTT.IsOpen = false;
        }

        private void eventYAxisTglClick(object sender, MouseButtonEventArgs e)
        {
            if (MainWindow.devicesConfig.Read("Y_Axis_Invert", inputDevice.Properties.ProductName) == "false")
            {
                MainWindow.devicesConfig.DeleteKey("Y_Axis_Invert", inputDevice.Properties.ProductName);
                MainWindow.devicesConfig.Write("Y_Axis_Invert", "true", inputDevice.Properties.ProductName);
                YAxisInvert = true;

            }
            else
            {
                MainWindow.devicesConfig.DeleteKey("Y_Axis_Invert", inputDevice.Properties.ProductName);
                MainWindow.devicesConfig.Write("Y_Axis_Invert", "false", inputDevice.Properties.ProductName);
                YAxisInvert = false;
            }
        }

        private void getSetInvert(ref bool inversion, string key)
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
    }

}

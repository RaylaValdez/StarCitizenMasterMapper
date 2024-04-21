using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GLGraphs.CartesianGraph;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Wpf;

namespace Star_Shitizen_Master_Mapping
{
    class DeviceCartesianGraph : UserControl
    {
        private GLWpfControl _control;
        private CartesianGraphState<string> _state;

        /// The actual graph this control wraps.
        public CartesianGraph<string> Graph { get; set; }

        /// Event fired before the graph is updated & rendered.
        public event Action<TimeSpan> Render;

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            var settings = new GLWpfControlSettings();
            _control = new GLWpfControl();
            _control.Ready += OnReady;
            Content = _control;
            _control.Start(settings);
        }

        private void OnReady()
        {
            var graphSettings = CartesianGraphSettings.Default;
            Graph = new CartesianGraph<string>(graphSettings);
            _state = Graph.State;
            _control.Render += OnRender;
            Graph.State.XGridSpacing.Automatic = false;
            Graph.State.YGridSpacing.Automatic = false;
            Graph.State.XGridSpacing.Minor = 0.1f;
            Graph.State.YGridSpacing.Minor = 0.1f;
            Graph.State.XGridSpacing.Major = 0.5f;
            Graph.State.YGridSpacing.Major = 0.5f;
            ResetView();
        }


        public void ResetView()
        {
            if (Graph == null)
            {
                return;
            }
            _state.Camera.Target.Position = Vector2.Zero;
            //_state.Camera.Target.VerticalSize = CartesianGraphState<string>.DefaultCameraZoom;
            _state.IsCameraAutoControlled = false;
        }

        private void OnRender(TimeSpan deltaTime)
        {
            if (Graph == null)
            {
                return;
            }
            Render?.Invoke(deltaTime);
            var size = _control.RenderSize;
            Graph.State.ViewportHeight = (float)size.Height;
            Graph.State.ViewportHeight = (float)size.Width;
            var delta = (float)deltaTime.TotalSeconds;
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Viewport(0, 0, (int)size.Width, (int)size.Height);
            if (Graph != null)
            {
                var aspect = (float)(size.Width / size.Height);
                Graph.State.Camera.Target.AspectRatio = aspect;
                Graph.State.Camera.Current.AspectRatio = aspect;
                Graph.State.Update(delta);
                Graph.Render();
            }
        }
    }
}
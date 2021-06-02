using System;
using System.Collections.Generic;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Mathematics;
using Fluint.Layer.Tasks;
using Fluint.Layer.UI;
using Fluint.Layer.Windowing;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Fluint.Engine.GL46.Windowing
{
    public class GLWindowProvider : GameWindow, IWindowProvider
    {
        public GLWindowProvider(ModulePacket packet) :
            base(GameWindowSettings.Default, new NativeWindowSettings()
            {
                Size = new OpenTK.Mathematics.Vector2i(1600, 900), APIVersion = new Version(4, 6)
            })
        {
            _packet = packet;
            _taskManager = packet.CreateScoped<ITaskManager>();
            FrameQueue = new Queue<Action>();
        }

        private readonly ModulePacket _packet;
        private readonly ITaskManager _taskManager;

        public IWindow Client { get; private set; }
        
        public string WindowTitle 
        {
            get => Title;
            set => FrameQueue.Enqueue(() => Title = value);
        }
        public bool WindowVSync
        {
            get => VSync == VSyncMode.On;
            set => FrameQueue.Enqueue(() => VSync = (VSyncMode)Convert.ToInt32(value));
        }
        public Vector2i WindowSize
        {
            get => OpenTKHelper.Vector2i(Size);
            set => Size = OpenTKHelper.Vector2i(value);
        }
        public Vector2i WindowLocation 
        { 
            get => OpenTKHelper.Vector2i(Location);
            set => Location = OpenTKHelper.Vector2i(value);
        }

        public Queue<Action> FrameQueue { get; }
        public object NativeKeyboardObject { get => KeyboardState; }
        public object NativeMouseObject { get => MouseState; }

        public void Start()
        {
            Client.OnStart();
            Run();
        }

        protected override void OnLoad()
        {
            _taskManager.Invoke(TaskSchedule.WindowReady, new TaskArgs(Client));

            Client.OnLoad();

            base.OnLoad();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            _taskManager.Invoke(TaskSchedule.WindowUpdate, new TaskArgs(Client));
            while (FrameQueue.Count > 0)
            {
                FrameQueue.Dequeue().Invoke();
            }

            Client.OnUpdate(args.Time);

            base.OnUpdateFrame(args);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.ClearColor(OpenTKHelper.Color4(Color.DarkSlateGray));
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

            _taskManager.Invoke(TaskSchedule.WindowRender, new TaskArgs(Client));
            Client.OnRender(args.Time);

            SwapBuffers();

            base.OnRenderFrame(args);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            _taskManager.Invoke(TaskSchedule.WindowMouseScroll, new TaskArgs(e.OffsetY));
            Client.OnMouseWheelMoved(OpenTKHelper.Vector2(e.Offset));

            base.OnMouseWheel(e);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, e.Width, e.Height);

            _taskManager.Invoke(TaskSchedule.WindowResize, new TaskArgs(Client));
            Client.OnResize(e.Width, e.Height);

            base.OnResize(e);
        }

        protected override void OnTextInput(TextInputEventArgs e)
        {
            _taskManager.Invoke(TaskSchedule.WindowEnterText, new TaskArgs(new object[] { e.Unicode }, Client));
            Client.OnTextReceived(e.Unicode, e.AsString);

            base.OnTextInput(e);
        }

        public void Adopt<client>() where client : IWindow
        {
            var instanceManager = _packet.GetSingleton<IGuiInstanceManager>();

            Client = (IWindow)_packet.FetchAndCreateInstance(typeof(client));
            Client.SetProvider(this);

            instanceManager.Adopt(Client);
        }
    }
}

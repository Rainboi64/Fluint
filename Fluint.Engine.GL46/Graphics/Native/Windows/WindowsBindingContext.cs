#define Platform_Windows
#define DEBUG
// Greatly Influenced by : https://github.com/varon/GLWpfControl/blob/master/src/GLWpfControl/DXGLContext.cs
// Greatly Influenced by : https://github.com/opentk/GLControl/blob/master/OpenTK.WinForms/GLControl.cs
using System;
using Fluint.Layer.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Fluint.Layer;
using System.Threading;
using System.Threading.Tasks;
using Fluint.Engine.GL46.Graphics.Native.GLFW;

namespace Fluint.Engine.GL46.Graphics.Native.Windows
{
    #if Platform_Windows
    public class WindowsBindingContext : IBindingContext
    {
        public WindowsBindingContext()
        {

        }

        private int _width;
        private int _height;
        private IntPtr _handle;
        private IGLFWGraphicsContext _internalContext;
        private NativeWindow _nativeWindow;

        public unsafe void InitializeContext(BindingContextSettings settings)
        {


            // init the mfing native window.

            var nws = NativeWindowSettings.Default;
            nws.StartFocused = false;
            nws.StartVisible = false;
            nws.NumberOfSamples = 0;
            nws.APIVersion = new Version(4, 6);
            nws.Flags = ContextFlags.Default;
            nws.Profile = ContextProfile.Core;
            nws.API = ContextAPI.OpenGL;
            nws.AutoLoadBindings = true;
            nws.WindowBorder = WindowBorder.Hidden;
            nws.WindowState = WindowState.Normal;

            _nativeWindow = new NativeWindow(nws);
 
            // retrieve window handle/info
            _handle = settings.ContextHandle;
            _internalContext = _nativeWindow.Context;

            // Set the style of the control to ensure corrent re-parenting behavoir

            const int CS_VREDRAW = 0x1;
            const int CS_HREDRAW = 0x2;
            const int CS_OWNDC = 0x20;

            IntPtr style = (IntPtr)(long)(CS_VREDRAW | CS_HREDRAW | CS_OWNDC);

            Win32.SetWindowLongPtr(_handle, Win32.WindowLongs.GWL_STYLE, style);

            // Get the the actual (windows shit) window pointer
            IntPtr hWnd = GLFWNative.glfwGetWin32Window(_nativeWindow.WindowPtr);

            // Set the parent of the window to be the control.
            Win32.SetParent(hWnd, _handle);

            // Yeah, am a CHAD, I use WINAPI and unsafe functions and am not scared at all.

            // Change the real HWND's window styles to be "WS_CHILD | WS_DISABLED" (i.e.,
            // a child of some container, with no input support), and turn off *all* the
            // other style bits (most of the rest of them could cause trouble).  In
            // particular, this turns off stuff like WS_BORDER and WS_CAPTION and WS_POPUP
            // and so on, any of which GLFW might have turned on for us.
            IntPtr childStyle = (IntPtr)(long)(Win32.WindowStyles.WS_CHILD
                );
            Win32.SetWindowLongPtr(hWnd, Win32.WindowLongs.GWL_STYLE, childStyle);

            Resize(settings.Width, settings.Height);

            Win32.SendMessage(hWnd, Win32.WM_SYSCOMMAND, Win32.SC_MAXIMIZE, 0);
            MakeCurrent();
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

#if DEBUG
            new Debug();
#endif
        }

        public void Resize(int width, int height)
        {
            _width = width;
            _height = height;
            _nativeWindow.ClientRectangle = new OpenTK.Mathematics.Box2i(0, 0, width, height);
            GL.Viewport(0, 0, _width, _height);
        }

        public void PreRender()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }
        
        public void PostRender()
        {
            //GL.Flush();
        }

        public void MakeCurrent()
        {

            _internalContext?.MakeCurrent();
        }

        public void SwapBuffers()
        {
            _internalContext?.SwapBuffers();
        }

        public void Dispose()
        {
            _nativeWindow.Dispose();
        }

        public void Test()
        {
            MakeCurrent();

            PreRender();


            PostRender(); 

            SwapBuffers();
        }
    }
#endif
}

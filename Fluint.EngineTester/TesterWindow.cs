using System;
using System.Collections.Generic;
using Fluint.Layer.Mathematics;
using OpenTK.Windowing.Desktop;
using Fluint.Layer.Graphics;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using Fluint.Engine.GL46.Graphics;

namespace Fluint.EngineTester
{
    public class TesterWindow : GameWindow
    {

        private readonly IShader _shader;
        private readonly IVertexLayout<PositionColorVertex> _vertexLayout;
        private readonly IRenderer3D<PositionColorVertex> _renderer3D;
 
        public TesterWindow(ServiceProvider serviceProvider) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            _shader = serviceProvider.GetService<IShader>();
            _renderer3D = serviceProvider.GetService<IRenderer3D<PositionColorVertex>>();
            _vertexLayout = serviceProvider.GetService<IVertexLayout<PositionColorVertex>>();

            _shader.LoadSource(File.ReadAllText("shader.vert"), File.ReadAllText("shader.frag"));
        }

        PositionColorVertex[] vertices =
        {
            new PositionColorVertex(new Vector3(0.5f,  0.5f, 0.0f), new Vector4(0,0,1,1)),  // top right
            new PositionColorVertex(new Vector3(0.5f, -0.5f, 0.0f), new Vector4(0,0,1,1)),  // bottom right
            new PositionColorVertex(new Vector3(-0.5f, -0.5f, 0.0f), new Vector4(0,0,1,1)),  // bottom left
            new PositionColorVertex(new Vector3(-0.5f,  0.5f, 0.0f), new Vector4(0,0,1,1))  // top left
        };

        uint[] indices =
        {  // note that we start from 0!
            0, 1, 3,   // first triangle
            1, 2, 3    // second triangle
        };


        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            _renderer3D.Flush();
            Context.SwapBuffers();

            base.OnRenderFrame(args);
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            VSync = VSyncMode.Off;
            GL.Viewport(0, 0, Size.X, Size.Y);

            base.OnResize(e);
        }
        protected override void OnLoad()
        {
            Debug debug = new Debug();


            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);


            _renderer3D.Begin(_vertexLayout, _shader);
            _renderer3D.Submit(new Layer.Graphics.Renderable3D<PositionColorVertex>(vertices, indices, Matrix.Identity));

            base.OnLoad();
        }

        protected override void OnUnload()
        {
            base.OnUnload();
            _renderer3D.End();
        }
    }
}

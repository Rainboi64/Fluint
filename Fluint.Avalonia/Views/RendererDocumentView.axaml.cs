using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Fluint.Avalonia.Controls;
using Fluint.Avalonia.ViewModels;
using Fluint.Layer.Graphics;
using Fluint.Layer.Mathematics;

namespace Fluint.Avalonia.Views
{
    public class RendererDocumentView : UserControl
    {
        private RendererContext _rendererContext;
        private readonly IShader _shader;
        private readonly IVertexLayout<PositionColorVertex> _vertexLayout;
        private readonly IRenderer3D<PositionColorVertex> _renderer3D;



        PositionColorVertex[] vertices =
        {
            new PositionColorVertex(new Vector3(0.5f,  0.5f, 0.0f), new Vector4(1,1,1,1)),  // top right
            new PositionColorVertex(new Vector3(0.5f, -0.5f, 0.0f), new Vector4(1,1,1,1)),  // bottom right
            new PositionColorVertex(new Vector3(-0.5f, -0.5f, 0.0f), new Vector4(1,1,1,1)),  // bottom left
            new PositionColorVertex(new Vector3(-0.5f,  0.5f, 0.0f), new Vector4(1,1,1,1))  // top left
        };


        uint[] indices =
        {  // note that we start from 0!
            0, 1, 3,   // first triangle
            1, 2, 3    // second triangle
        };

        public RendererDocumentView(IBindingContext bindingContext, IShader shader, IVertexLayout<PositionColorVertex> vertexLayout, IRenderer3D<PositionColorVertex> renderer3D)
        {
            this.InitializeComponent();

            _shader = shader;
            _vertexLayout = vertexLayout;
            _renderer3D = renderer3D;

            _rendererContext = this.FindControl<RendererContext>("renderer");
            _rendererContext.Load(bindingContext);

            _rendererContext.Ready += _rendererContext_Ready;
            _rendererContext.RenderCallback += _rendererContext_RenderCallback;
        }

        public RendererDocumentView() { }

        private void _rendererContext_Ready()
        {
            _shader.LoadSource(File.ReadAllText("shader.vert"), File.ReadAllText("shader.frag"));
            _renderer3D.Begin(_vertexLayout, _shader);
            _renderer3D.Submit(new Renderable3D<PositionColorVertex>(vertices, indices, Matrix.Identity));

        }

        private void _rendererContext_RenderCallback(TimeSpan obj)
        {
            _renderer3D.Flush();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

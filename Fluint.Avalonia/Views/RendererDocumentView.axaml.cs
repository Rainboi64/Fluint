using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Fluint.Avalonia.Controls;
using Fluint.Avalonia.ViewModels;
using Fluint.Layer.DependencyInjection;
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
        private readonly ModulePacket _packet;

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

        public RendererDocumentView(ModulePacket packet, IBindingContext bindingContext, IShader shader, IVertexLayout<PositionColorVertex> vertexLayout, IRenderer3D<PositionColorVertex> renderer3D)
        {
            this.InitializeComponent();


            _rendererContext = this.FindControl<RendererContext>("renderer");
            _rendererContext.Load(bindingContext);


        }

        public RendererDocumentView() { }

        private void _rendererContext_Ready()
        {

        }

        private void _rendererContext_RenderCallback(TimeSpan obj)
        {
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

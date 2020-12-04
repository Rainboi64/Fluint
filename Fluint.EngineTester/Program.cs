using System;
using Fluint.Layer;
using Fluint.Layer.Graphics;
using Fluint.Engine.GL46.Graphics;
using Microsoft.Extensions.DependencyInjection;

namespace Fluint.EngineTester
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection sc = new ServiceCollection();
            sc.AddScoped<IShader, Shader>();
            sc.AddScoped<IVertexLayout<PositionColorVertex>, VertexArrayObject<PositionColorVertex>>();
            sc.AddScoped<IRenderer3D<PositionColorVertex>, Basic3DRenderer<PositionColorVertex>>();
            
            using TesterWindow window = new TesterWindow(sc.BuildServiceProvider());
            window.Run();
        }
    }
}

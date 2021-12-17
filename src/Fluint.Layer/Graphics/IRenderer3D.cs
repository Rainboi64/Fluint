//
// IRenderer3D.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//


using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Graphics
{
    [Initialization(InitializationMethod.Scoped)]
    public interface IRenderer3D<VertexType> : IModule where VertexType : struct
    {
        Matrix ViewMatrix { get; set; }
        Matrix ProjectionMatrix { get; set; }

        /// <summary>
        /// Initilizes The Renderer.
        /// </summary>
        void Load();

        /// <summary>
        /// Initializes The renderer. (gets it ready to start receiving data)
        /// </summary>
        void Begin(IVertexLayout<VertexType> layout, IShader shader);

        /// <summary>
        /// Pass in the renderables to be rendered.
        /// </summary>
        /// <param name="renderable3D">the actual renderer</param>
        void Submit(IRenderable3D<VertexType> renderable3D);

        /// <summary>
        /// Renders all the renderables.
        /// </summary>
        void Flush();

        /// <summary>
        /// De-Initializes the renderer.
        /// </summary>
        void End();
    }
}
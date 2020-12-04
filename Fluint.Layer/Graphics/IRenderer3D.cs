//
// IRenderer3D.cs
//
// Copyright (C) 2020 Yaman Alhalabi
//


namespace Fluint.Layer.Graphics
{
    [Initialization(InitializationMethod.Scoped)]
    public interface IRenderer3D<VertexType> : IModule where VertexType : struct
    {
        /// <summary>
        /// Initializes The renderer. (gets it ready to start receiving data)
        /// </summary>
        void Begin(IVertexLayout<VertexType> layout, IShader shader);

        /// <summary>
        /// Pass in the renderables to be rendered.
        /// </summary>
        /// <param name="renderable3D">the actual renderer</param>
        void Submit(Renderable3D<VertexType> renderable3D);

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
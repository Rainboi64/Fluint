//
// IVertexLayout.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.Graphics
{
    [Initialization(InitializationMethod.Scoped)]
    public interface IVertexLayout<VertexType> : IModule, IDisposable where VertexType : struct
    {
        /// <summary>
        /// Gets the size of the vertex.
        /// </summary>
        int VertexSize { get; }

        /// <summary>
        /// Calculates the vertex size.
        /// </summary>
        void Calculate();

        /// <summary>
        /// To be called before enabling the layout.
        /// </summary>
        void Load();
        
        /// <summary>
        /// To enable the layout.
        /// </summary>
        void Enable();

        /// <summary>
        /// To disable the layout.
        /// </summary>
        void Disable();
    }
}

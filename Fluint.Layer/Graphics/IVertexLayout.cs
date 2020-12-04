using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.Graphics
{
    public interface IVertexLayout<VertexType> where VertexType : struct
    {
        /// <summary>
        /// Gets the size of the vertex.
        /// </summary>
        int VertexSize { get; }
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

using Fluint.Layer.Graphics;
using Fluint.Layer.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.Engine
{
    /// <summary>
    /// I might add a caching or apply property to save on some resources.
    /// I probably should really.
    /// really really should.
    /// </summary>
    /// <typeparam name="VertexType"></typeparam>
    public interface IMesh : IRenderComponent, ISceneObject, IModule
    {
        IEnumerable<PositionNormalUVTIDVertex> VertexArray { get; set; }
        IEnumerable<uint> IndexArray { get; set; }
    }
}

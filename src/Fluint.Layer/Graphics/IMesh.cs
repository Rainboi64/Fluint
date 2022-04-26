//
// IMesh.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System.Collections.Generic;
using Fluint.Layer.Graphics.API;

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
        IEnumerable<PositionNormalUvtidVertex> VertexArray
        {
            get;
            set;
        }

        IEnumerable<uint> IndexArray
        {
            get;
            set;
        }
    }
}
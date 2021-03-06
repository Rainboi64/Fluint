using Fluint.Layer.Graphics;
using Fluint.Layer.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.Engine
{
    [Initialization(InitializationMethod.Scoped)]
    public interface ISceneObject : IModule
    {
        IRenderComponent RenderComponent { get; set; }
        Vector3 Position { get; set; }
        Quaternion Rotation { get; set; }
        Vector3 Scale { get; set; }
        Matrix ModelMatrix { get; set; }

        void Render();
    }
}

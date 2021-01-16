using Fluint.Layer.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.Engine
{
    public interface ISceneObject
    {
        Vector3 Position { get; set; }
        Quaternion Rotation { get; set; }
        Vector3 Scale { get; set; }
    }
}

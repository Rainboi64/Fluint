//
// ISceneObject.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Graphics
{
    [Initialization(InitializationMethod.Scoped)]
    public interface ISceneObject : IModule
    {
        IRenderComponent RenderComponent
        {
            get;
            set;
        }

        Vector3 Position
        {
            get;
            set;
        }

        Quaternion Rotation
        {
            get;
            set;
        }

        Vector3 Scale
        {
            get;
            set;
        }

        Matrix ModelMatrix
        {
            get;
            set;
        }

        void Render();
    }
}
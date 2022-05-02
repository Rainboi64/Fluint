﻿//
// ICamera.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Graphics.API;
using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Graphics
{
    [Initialization(InitializationMethod.Scoped)]
    public interface ICamera : IModule
    {
        Vector3 Translation
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

        Matrix ViewMatrix
        {
            get;
            set;
        }

        ProjectionMode ProjectionMode
        {
            get;
            set;
        }

        Viewport Viewport
        {
            get;
            set;
        }

        IFramebuffer Framebuffer
        {
            get;
        }

        void Submit(IScene scene);
        void Render();
    }
}
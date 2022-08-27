//
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
        public float Zoom
        {
            get;
            set;
        }

        Vector3 Position
        {
            get;
            set;
        }

        ViewportF Viewport
        {
            get;
            set;
        }

        Vector3 Front
        {
            get;
        }

        Vector3 Up
        {
            get;
        }

        Vector3 Right
        {
            get;
        }

        float Pitch
        {
            get;
            set;
        }

        float Yaw
        {
            get;
            set;
        }

        float Fov
        {
            get;
            set;
        }

        ProjectionMode ProjectionMode
        {
            get;
            set;
        }

        Matrix GetViewMatrix();
        Matrix GetProjectionMatrix();
    }
}
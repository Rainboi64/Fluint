//
// Camera.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using Fluint.Layer.Graphics;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Mathematics;

namespace Fluint.Graphics.Base
{
    public class Camera : ICamera
    {
        private const float PiOver4 = MathF.PI / 4.0f;
        private const float PiOver2 = MathF.PI / 2.0f;

        private float _pitch;
        private float _yaw = -PiOver2;
        private float _fov = PiOver2;

        public Vector3 Position
        {
            get;
            set;
        }

        public float AspectRatio
        {
            private get;
            set;
        }

        public Vector3 Front
        {
            get;
            private set;
        } = -Vector3.UnitZ;

        public Vector3 Up
        {
            get;
            private set;
        } = Vector3.UnitY;

        public Vector3 Right
        {
            get;
            private set;
        } = Vector3.UnitX;

        public float Pitch
        {
            get => MathUtil.RadiansToDegrees(_pitch);
            set
            {
                var angle = MathUtil.Clamp(value, -89f, 89f);
                _pitch = MathUtil.DegreesToRadians(angle);
                UpdateVectors();
            }
        }

        public float Yaw
        {
            get => MathUtil.RadiansToDegrees(_yaw);
            set
            {
                _yaw = MathUtil.DegreesToRadians(value);
                UpdateVectors();
            }
        }

        public float Fov
        {
            get => MathUtil.RadiansToDegrees(_fov);
            set
            {
                var angle = MathUtil.Clamp(value, 1f, 45f);
                _fov = MathUtil.DegreesToRadians(angle);
            }
        }

        public ProjectionMode ProjectionMode
        {
            get;
            set;
        }

        public Matrix GetViewMatrix()
        {
            return Matrix.LookAtRH(Position, Position + Front, Up);
        }

        public Matrix GetProjectionMatrix()
        {
            if (ProjectionMode == ProjectionMode.Orthogonal)
            {
                return Matrix.OrthoRH(750, 750, 0.01f, 100f);
            }

            return Matrix.PerspectiveFovRH(_fov, AspectRatio, 0.01f, 100f);
        }

        private void UpdateVectors()
        {
            var x = (float)Math.Cos(_pitch) * (float)Math.Cos(_yaw);
            var y = (float)Math.Sin(_pitch);
            var z = (float)Math.Cos(_pitch) * (float)Math.Sin(_yaw);

            Front = new Vector3(x, y, z);
            Vector3.Normalize(Front);

            Right = Vector3.Normalize(Vector3.Cross(Front, Vector3.UnitY));
            Up = Vector3.Normalize(Vector3.Cross(Right, Front));
        }
    }
}
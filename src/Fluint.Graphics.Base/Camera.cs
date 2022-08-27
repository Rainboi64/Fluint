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
        private float _fov = PiOver2;

        private float _pitch;
        private float _yaw = -PiOver2;

        public float Zoom
        {
            get;
            set;
        }

        public Vector3 Position
        {
            get;
            set;
        }

        public ViewportF Viewport
        {
            get;
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
            Zoom = Math.Max(Zoom, Viewport.MinDepth);
            if (ProjectionMode == ProjectionMode.Orthogonal)
            {
                var x = Viewport.Width / 2.0f * Zoom;
                var y = Viewport.Height / 2.0f * Zoom;

                return Matrix.OrthoOffCenterRH(-x, x, -y, y, Viewport.MinDepth, Viewport.MaxDepth);
            }

            return Matrix.PerspectiveFovRH(_fov, Viewport.AspectRatio, Viewport.MinDepth, Viewport.MaxDepth);
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
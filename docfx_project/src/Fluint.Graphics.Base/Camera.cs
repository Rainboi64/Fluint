//
// Camera.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Graphics;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Mathematics;

namespace Fluint.Graphics.Base
{
    public class Camera : ICamera
    {
        private readonly ModulePacket _packet;

        private readonly IRenderer3D<PositionNormalUvtidVertex> _renderer3D;
        private readonly IShader _shader;
        private readonly IVertexLayout<PositionNormalUvtidVertex> _vertexLayout;
        private IFramebuffer _framebuffer;
        private Matrix _projectionMatrix = Matrix.Identity;
        private Quaternion _rotation = Quaternion.Identity;
        private Vector3 _scale = new Vector3(1);
        private Vector3 _translation = new Vector3(0);
        private Matrix _viewMatrix = Matrix.Identity;
        private Viewport _viewport;

        // initialization my man 😎
        public Camera(ModulePacket packet)
        {
            _packet = packet;
            _vertexLayout = _packet.CreateScoped<IVertexLayout<PositionNormalUvtidVertex>>();
            _renderer3D = _packet.CreateScoped<IRenderer3D<PositionNormalUvtidVertex>>();
            _shader = _packet.CreateScoped<IShader>();
            _framebuffer = _packet.CreateScoped<IFramebuffer>();
            _shader.LoadSource("", "");
        }

        public Vector3 Translation
        {
            get => _translation;
            set
            {
                _translation = value;
                _viewMatrix.TranslationVector = value;
            }
        }

        public Quaternion Rotation
        {
            get => _rotation;
            set
            {
                _viewMatrix = Matrix.Scaling(_scale) * Matrix.RotationQuaternion(value) *
                              Matrix.Translation(_translation);
            }
        }

        public Vector3 Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                _viewMatrix.ScaleVector = value;
            }
        }

        public Matrix ViewMatrix
        {
            get => _viewMatrix;
            set
            {
                _viewMatrix = value;
                _viewMatrix.Decompose(out _scale, out _rotation, out _translation);
            }
        }

        public Viewport Viewport
        {
            get => _viewport;
            set
            {
                _viewport = value;
                switch (ProjectionMode)
                {
                    case ProjectionMode.Orthogonal:
                        _projectionMatrix = Matrix.OrthoRH(_viewport.Width, _viewport.Height, _viewport.MinDepth,
                            _viewport.MaxDepth);
                        break;
                    case ProjectionMode.Prespective:
                        _projectionMatrix = Matrix.PerspectiveRH(_viewport.Width, _viewport.Height, _viewport.MinDepth,
                            _viewport.MaxDepth);
                        break;
                }
            }
        }

        public ProjectionMode ProjectionMode
        {
            get;
            set;
        } = ProjectionMode.Prespective;

        public IFramebuffer Framebuffer => throw new NotImplementedException();

        public void Submit(IScene scene)
        {
            _framebuffer.Create(new Vector2i(_viewport.Width, _viewport.Height));

            _renderer3D.Begin(_vertexLayout, _shader);

            var length = scene.Count;
            for (var i = 0; i < length; i++)
            {
                var rendererComponent = scene[i].RenderComponent.Load();
                _renderer3D.Submit(rendererComponent);
            }
        }

        public void Render()
        {
            _renderer3D.ViewMatrix = _viewMatrix;
            _renderer3D.ProjectionMatrix = _projectionMatrix;

            _framebuffer.Bind();
            _renderer3D.Flush();
            _framebuffer.Unbind();
        }
    }
}
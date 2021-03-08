using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Engine;
using Fluint.Layer.Graphics;
using Fluint.Layer.Mathematics;

namespace Fluint.Engine.GL46
{
    public class Camera : ICamera
    {
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
               _viewMatrix = Matrix.Scaling(_scale) * Matrix.RotationQuaternion(value) * Matrix.Translation(_translation);
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
                        _projectionMatrix = Matrix.OrthoRH(_viewport.Width, _viewport.Height, _viewport.MinDepth, _viewport.MaxDepth);
                        break;
                    case ProjectionMode.Prespective:
                        _projectionMatrix = Matrix.PerspectiveRH(_viewport.Width, _viewport.Height, _viewport.MinDepth, _viewport.MaxDepth);
                        break;
                }
            }
        }

        public ProjectionMode ProjectionMode { get; set; } = ProjectionMode.Prespective;

        private Matrix _viewMatrix = Matrix.Identity;
        private Matrix _projectionMatrix = Matrix.Identity;
        private Vector3 _translation = new Vector3(0);
        private Quaternion _rotation = Quaternion.Identity;
        private Vector3 _scale = new Vector3(1);
        private Viewport _viewport;

        private readonly ModulePacket _packet;

        private readonly IRenderer3D<PositionNormalUVTIDVertex> _renderer3D;
        private readonly IShader _shader;
        private readonly IVertexLayout<PositionNormalUVTIDVertex> _vertexLayout;

        // initialization my man 😎
        public Camera(ModulePacket packet)
        {
            _packet = packet;
            _vertexLayout = _packet.New<IVertexLayout<PositionNormalUVTIDVertex>>();
            _renderer3D = _packet.New<IRenderer3D<PositionNormalUVTIDVertex>>();
            _shader = _packet.New<IShader>();
        }

        public void Submit(IScene scene)
        {
            _renderer3D.Begin(_vertexLayout, _shader);

            var length = scene.Count;
            for ( int i = 0; i < length; i++)
            {
                var rendererComponent = scene[i].RenderComponent.Load();
                _renderer3D.Submit(rendererComponent);
            }
        }

        public void Render()
        {
            _renderer3D.ViewMatrix = _viewMatrix;
            _renderer3D.ProjectionMatrix = _projectionMatrix;

            _renderer3D.Flush();
        }
    }
}

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Engine;
using Fluint.Layer.Graphics;
using Fluint.Layer.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Engine.GL46
{
    public class Camera : ICamera
    {
        public Vector3 Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Quaternion Rotation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Vector3 Scale { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private ModulePacket _packet;
        private Matrix _modelMatrix = Matrix.Identity;
        private IRenderer3D<PositionNormalUVTIDVertex> renderer3D;
        private IShader shader;

        public void Render(IScene scene)
        {
            shader.SetViewMatrix(_modelMatrix);
            renderer3D.Begin(_packet.New<IVertexLayout<PositionNormalUVTIDVertex>>(), shader);
            foreach (var item in scene)
            {
                var rendererComponent = item.RenderComponent.Load();
                rendererComponent.ModelMatrix = item.ModelMatrix;
                renderer3D.Submit(rendererComponent);
            }
            renderer3D.Flush();
        }
    }
}

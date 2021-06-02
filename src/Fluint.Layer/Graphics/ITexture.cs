using System;
using System.Collections.Generic;
using System.Text;
using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Graphics
{
    [Initialization(InitializationMethod.Scoped)]
    public interface ITexture : IModule, IDisposable
    {
        public void Bind();
        public void Unbind();
        public int Width { get; }
        public int Height { get; }
        public Color[] Pixels { get; }
        public int ConvertIndex(int x, int y);
        public void Upload();
        public void LoadFromFile(string fileName);
    }
}

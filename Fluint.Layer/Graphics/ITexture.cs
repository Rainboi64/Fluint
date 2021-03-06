using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.Graphics
{
    [Initialization(InitializationMethod.Scoped)]
    public interface ITexture : IModule
    {
        public void Bind();
        public void Unbind();
        public void LoadFromFile(string fileName);
    }
}

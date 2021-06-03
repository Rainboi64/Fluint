//
// ShaderPacket.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System.Collections;
using System.Collections.Generic;

namespace Fluint.Layer.Graphics
{
    public class ShaderPacket : IReadOnlyList<ShaderObject>
    {
        public ShaderPacket(IEnumerable<ShaderObject> value, string tag)
        {
            Tag = tag;
            _shaderObjects = (ShaderObject[])value;
        }

        public ShaderPacket() { }

        public string Tag = "";

        public ShaderObject[] _shaderObjects;

        public int Count => _shaderObjects.Length;

        public ShaderObject this[int index] => _shaderObjects[index];

        public IEnumerator<ShaderObject> GetEnumerator()
        {
            return (IEnumerator<ShaderObject>)_shaderObjects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _shaderObjects.GetEnumerator();
        }
    }
}

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
        public ShaderObject[] ShaderObjects;

        public string Tag = "";

        public ShaderPacket(IEnumerable<ShaderObject> value, string tag)
        {
            Tag = tag;
            ShaderObjects = (ShaderObject[])value;
        }

        public ShaderPacket()
        {
        }

        public int Count => ShaderObjects.Length;

        public ShaderObject this[int index] => ShaderObjects[index];

        public IEnumerator<ShaderObject> GetEnumerator()
        {
            return (IEnumerator<ShaderObject>)ShaderObjects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ShaderObjects.GetEnumerator();
        }
    }
}
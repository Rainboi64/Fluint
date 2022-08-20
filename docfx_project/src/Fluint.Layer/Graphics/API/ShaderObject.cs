//
// ShaderObject.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.Graphics.API
{
    public struct ShaderObject
    {
        public ShaderObject(ShaderObjectType type, object value, string tag)
        {
            Type = type;
            Value = value;
            Tag = tag;
        }

        public string Tag;
        public ShaderObjectType Type;
        public object Value;
    }
}
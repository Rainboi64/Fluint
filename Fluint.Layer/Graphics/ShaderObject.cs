namespace Fluint.Layer.Graphics
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

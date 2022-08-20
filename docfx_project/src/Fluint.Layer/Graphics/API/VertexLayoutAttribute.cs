//
// VertexLayoutAttribute.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.Graphics.API
{
    public class VertexLayoutAttribute
    {
        public VertexLayoutAttribute(string name, VertexLayoutAttributeType attributeType, int componentsCount)
        {
            Name = name;
            AttributeType = attributeType;
            ComponentsCount = componentsCount;
        }

        public string Name
        {
            get;
            set;
        }

        public VertexLayoutAttributeType AttributeType
        {
            get;
            set;
        }

        public int ComponentsCount
        {
            get;
            set;
        }
    }
}
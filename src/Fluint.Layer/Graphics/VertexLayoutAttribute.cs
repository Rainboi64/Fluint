//
// VertexLayoutAttribute.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.Graphics
{
    public class VertexLayoutAttribute
    {
        public string Name { get; set; }
        public VertexLayoutAttributeType AttributeType { get; set; }
        public int ComponentsCount { get; set; }

        public VertexLayoutAttribute(string name, VertexLayoutAttributeType attributeType, int componentsCount)
        {
            Name = name;
            AttributeType = attributeType;
            ComponentsCount = componentsCount;
        }
    }
}

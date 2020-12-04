using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.Graphics
{
    public class VertexLayoutAttribute
    {
        public VertexLayoutAttributeType AttributeType { get; set; }
        public int ComponentsCount { get; set; }

        public VertexLayoutAttribute(VertexLayoutAttributeType attributeType, int componentsCount)
        {
            AttributeType = attributeType;
            ComponentsCount = componentsCount;
        }
    }
}

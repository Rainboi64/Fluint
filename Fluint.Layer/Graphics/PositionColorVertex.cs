using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Graphics
{
    /// <summary>
    /// A data structure to be loaded into buffers, contains position, and color.
    /// </summary>
    public struct PositionColorVertex 
    {
        public static VertexLayoutAttribute[] VertexLayout = new VertexLayoutAttribute[]
        {
            new VertexLayoutAttribute(VertexLayoutAttributeType.Float, 3),
            new VertexLayoutAttribute(VertexLayoutAttributeType.Float, 4)
        };

        public Vector3 Position { get; set; }
        public Vector4 Color { get; set; }

        public PositionColorVertex(Vector3 position, Vector4 color) 
        {
            Position = position;
            Color = color;
        }
    }
}

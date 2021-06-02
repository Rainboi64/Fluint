using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Graphics
{
    /// <summary>
    /// A data structure to be loaded into buffers, contains position.
    /// </summary>
    public struct PositionVertex
    {
        public Vector3 Position { get; set; }

        public PositionVertex(Vector3 position) 
        {
            Position = position;
        }
    }
}

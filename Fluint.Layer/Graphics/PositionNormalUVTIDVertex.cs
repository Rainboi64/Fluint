using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Graphics
{
    /// <summary>
    /// A data structure to be loaded into buffers, contains position, normal, UV, TextureID.
    /// </summary>
    public struct PositionNormalUVTIDVertex
    {
        public readonly Vector3 Position { get; }
        public readonly Vector3 Normal { get; }
        public readonly Vector2 UV { get; }
        public readonly uint TID { get; }

        public PositionNormalUVTIDVertex(Vector3 position, Vector3 normal, Vector2 uv, uint tid) 
        {
            Position = position;
            Normal = normal;
            UV = uv;
            TID = tid;
        }
    }
}

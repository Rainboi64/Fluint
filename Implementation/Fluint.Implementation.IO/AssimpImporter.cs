using Fluint.Layer.Engine;
using Fluint.Layer.IO;
using Assimp;
using System.Collections.Generic;
using Fluint.Layer.Graphics;
using Fluint.Layer.Mathematics;
using Fluint.Layer.DependencyInjection;

namespace Fluint.Implementation.IO
{
    public class AssimpImporter : IImporter
    {
        private readonly ModulePacket _packet;
        public AssimpImporter(ModulePacket packet)
        {
            _packet = packet;
        }

        public string[] FileExtenstions => new[] { "obj" };
        public unsafe IMesh[] Import(string fileName)
        {
            var newMeshes = new List<IMesh>();
            var context = new AssimpContext();
            var scene = context.ImportFile(fileName);
            foreach (var mesh in scene.Meshes)
            {
                var newMesh = _packet.New<IMesh>();
                var indices = new List<uint>();
                var vertex = new List<PositionNormalUVTIDVertex>();
                foreach(var face in mesh.Faces)
                {
                    indices.AddRange((IEnumerable<uint>)face.Indices);
                }

                for (var i = 0; i < mesh.VertexCount; i++)
                {
                    var vertice = mesh.Vertices[i];
                    var normal = mesh.Normals[i];
                    vertex.Add(new PositionNormalUVTIDVertex(*(Vector3*)&vertice, *(Vector3*)&normal, new Vector2(0), 0));
                }
                newMesh.VertexArray = vertex;
                newMesh.IndexArray = indices;
                newMesh.ModelMatrix = Matrix.Identity;
                // TODO: copy material stuff.
                // newMesh.Material = 

                newMeshes.Add(newMesh);
            }
            return newMeshes.ToArray();
        }
    }
}

//
// AssimpImporter.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System.Collections.Generic;
using Assimp;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Graphics;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.IO;
using Fluint.Layer.Mathematics;

namespace Fluint.IO.Base
{
    public class AssimpImporter : IImporter
    {
        private readonly ModulePacket _packet;

        public AssimpImporter(ModulePacket packet)
        {
            _packet = packet;
        }

        public string[] FileExtenstions => new[] { "obj", "stl" };

        public unsafe IMesh[] Import(string fileName)
        {
            var newMeshes = new List<IMesh>();
            var context = new AssimpContext();
            var scene = context.ImportFile(fileName);
            foreach (var mesh in scene.Meshes)
            {
                var newMesh = _packet.CreateScoped<IMesh>();
                var indices = new List<uint>();
                var vertex = new List<PositionNormalUvtidVertex>();
                foreach (var face in mesh.Faces)
                {
                    indices.AddRange(face.Indices as IEnumerable<uint>);
                }

                for (var i = 0; i < mesh.VertexCount; i++)
                {
                    var vertice = mesh.Vertices[i];
                    var normal = mesh.Normals[i];
                    vertex.Add(
                        new PositionNormalUvtidVertex(*(Vector3*)&vertice, *(Vector3*)&normal, new Vector2(0), 0));
                }

                // newMesh.VertexArray = vertex;
                // newMesh.IndexArray = indices;
                // newMesh.Model = Matrix.Identity;
                // TODO: copy material stuff.
                // newMesh.Material = 

                newMeshes.Add(newMesh);
            }

            return newMeshes.ToArray();
        }
    }
}
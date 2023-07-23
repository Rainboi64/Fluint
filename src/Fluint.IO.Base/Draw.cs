// 
// Draw.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;
using System.Collections.Generic;
using System.Linq;
using Assimp;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Graphics.Debug;
using Fluint.Layer.Mathematics;
using Fluint.Layer.SDK;

namespace Fluint.IO.Base;

public class Draw : ILambda
{
    private readonly ModulePacket _packet;

    public Draw(ModulePacket packet)
    {
        _packet = packet;
    }

    public string Command => "draw";

    public LambdaObject Run(string[] args)
    {
        var server = _packet.GetSingleton<IDebugServer>();
        var context = new AssimpContext();
        var scene = context.ImportFile(args.FirstOrDefault());
        var random = new Random();
        foreach (var mesh in scene.Meshes)
        {
            var indices = new List<int>();
            var vertices = new List<PositionColorVertex>();
            foreach (var face in mesh.Faces)
            {
                indices.AddRange(face.Indices);
            }

            foreach (var index in indices)
            {
                var vertex = mesh.Vertices[index];
                vertices.Add(new PositionColorVertex(new Vector3(vertex.X, vertex.Y, vertex.Z),
                    (Vector4)random.NextColor()));
            }

            server.DrawVertices(vertices);
        }

        return LambdaObject.Success;
    }
}
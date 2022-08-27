// 
// MeshFactory.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using System.Collections.Generic;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Graphics;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Mathematics;

namespace Fluint.Graphics.Base;

public class MeshFactory : IMeshFactory
{
    private readonly ModulePacket _packet;

    public MeshFactory(ModulePacket packet)
    {
        _packet = packet;
    }

    public IMesh CreateMesh()
    {
        throw new NotImplementedException();
    }

    public IMesh CreateUnitBox()
    {
        var cubeColor = new Vector4(112 / 256f, 53 / 256f, 63 / 256f, 1.0f);
        var cubeVertices = new List<PositionColorVertex> {
            new(new Vector3(-1.0f, -1.0f, -1.0f), cubeColor), // Front 
            new(new Vector3(-1.0f, 1.0f, -1.0f), cubeColor),
            new(new Vector3(1.0f, 1.0f, -1.0f), cubeColor),
            new(new Vector3(-1.0f, -1.0f, -1.0f), cubeColor),
            new(new Vector3(1.0f, 1.0f, -1.0f), cubeColor),
            new(new Vector3(1.0f, -1.0f, -1.0f), cubeColor),

            new(new Vector3(-1.0f, -1.0f, 1.0f), cubeColor), // BACK 
            new(new Vector3(1.0f, 1.0f, 1.0f), cubeColor),
            new(new Vector3(-1.0f, 1.0f, 1.0f), cubeColor),
            new(new Vector3(-1.0f, -1.0f, 1.0f), cubeColor),
            new(new Vector3(1.0f, -1.0f, 1.0f), cubeColor),
            new(new Vector3(1.0f, 1.0f, 1.0f), cubeColor),

            new(new Vector3(-1.0f, 1.0f, -1.0f), cubeColor), // Top 
            new(new Vector3(-1.0f, 1.0f, 1.0f), cubeColor),
            new(new Vector3(1.0f, 1.0f, 1.0f), cubeColor),
            new(new Vector3(-1.0f, 1.0f, -1.0f), cubeColor),
            new(new Vector3(1.0f, 1.0f, 1.0f), cubeColor),
            new(new Vector3(1.0f, 1.0f, -1.0f), cubeColor),

            new(new Vector3(-1.0f, -1.0f, -1.0f), cubeColor), // Bottom 
            new(new Vector3(1.0f, -1.0f, 1.0f), cubeColor),
            new(new Vector3(-1.0f, -1.0f, 1.0f), cubeColor),
            new(new Vector3(-1.0f, -1.0f, -1.0f), cubeColor),
            new(new Vector3(1.0f, -1.0f, -1.0f), cubeColor),
            new(new Vector3(1.0f, -1.0f, 1.0f), cubeColor),

            new(new Vector3(-1.0f, -1.0f, -1.0f), cubeColor), // Left 
            new(new Vector3(-1.0f, -1.0f, 1.0f), cubeColor),
            new(new Vector3(-1.0f, 1.0f, 1.0f), cubeColor),
            new(new Vector3(-1.0f, -1.0f, -1.0f), cubeColor),
            new(new Vector3(-1.0f, 1.0f, 1.0f), cubeColor),
            new(new Vector3(-1.0f, 1.0f, -1.0f), cubeColor),

            new(new Vector3(1.0f, -1.0f, -1.0f), cubeColor), // Right 
            new(new Vector3(1.0f, 1.0f, 1.0f), cubeColor),
            new(new Vector3(1.0f, -1.0f, 1.0f), cubeColor),
            new(new Vector3(1.0f, -1.0f, -1.0f), cubeColor),
            new(new Vector3(1.0f, 1.0f, -1.0f), cubeColor),
            new(new Vector3(1.0f, 1.0f, 1.0f), cubeColor)
        };

        var factory = _packet.CreateScoped<IGraphicsFactory>();
        var buffer = factory.CreateVertexBuffer(cubeVertices.ToArray());

        return new Mesh(buffer);
    }

    public IMesh CreateUnitSphere()
    {
        throw new NotImplementedException();
    }
}
//
// GizmoCameraCube.cs
//
// Copyright (C) 2023 Yaman Alhalabi

using Fluint.Layer.Editor.Gizmos;
using Fluint.Layer.Graphics;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Mathematics;

namespace Fluint.Editor.Base.Gizmos;

public class GizmoCameraCube : IGizmoCameraCube
{
    public PositionWidthColorVertex[] GetVertices()
    {
        // Define the vertices of the cube
        var vertices = new[]
        {
            new Vector3(-1, -1, -1), // 0
            new Vector3( 1, -1, -1), // 1
            new Vector3( 1,  1, -1), // 2
            new Vector3(-1,  1, -1), // 3
            new Vector3(-1, -1,  1), // 4
            new Vector3( 1, -1,  1), // 5
            new Vector3( 1,  1,  1), // 6
            new Vector3(-1,  1,  1)  // 7
        };

        // Define the edges of the cube by their vertices indices
        var edges = new [,]
        {
            { 0, 1 }, { 1, 2 }, { 2, 3 }, { 3, 0 }, // Bottom face
            { 4, 5 }, { 5, 6 }, { 6, 7 }, { 7, 4 }, // Top face
            { 0, 4 }, { 1, 5 }, { 2, 6 }, { 3, 7 }  // Vertical edges
        };

        // Create the array to hold the vertices of the edges
        var edgeVertices = new PositionWidthColorVertex[edges.Length * 2];

        // Populate the edge vertices array
        for (var i = 0; i < edges.GetLength(0); i++)
        {
            edgeVertices[2 * i] = new PositionWidthColorVertex(vertices[edges[i, 0]], 1, (Vector4) Color.Red);
            edgeVertices[2 * i + 1] =  new PositionWidthColorVertex(vertices[edges[i, 1]], 1, (Vector4) Color.Red);
        }

        return edgeVertices;
    }

    public ICamera Camera
    {
        get;
        set;
    }
}

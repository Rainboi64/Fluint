// 
// GizmoSphere.cs
// 
// Copyright (C) 2023 Yaman Alhalabi

using System;
using Fluint.Layer.Editor.Gizmos;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Mathematics;

namespace Fluint.Editor.Base.Gizmos;

// Current Implementation is not working :(
public class GizmoSphere : IGizmoSphere
{
    // https://gamedev.stackexchange.com/questions/188654/creating-sphere-triangles-from-vertices
    public PositionWidthColorVertex[] GetVertices()
    {
        var width = 3;
        var numVertices = LatitudeLines * (LongitudeLines + 1) + 2;
        var numTriangles = LongitudeLines * LatitudeLines * 2;

        var vertices = new PositionWidthColorVertex[numTriangles * 3];
        var positions = new Vector3[numVertices];

        // Poles
        positions[0] = new Vector3(0, Radius, 0);
        positions[numVertices - 1] = new Vector3(0, -Radius, 0);

        // +1.0f because there's a gap between the poles and the first parallel.
        var latitudeSpacing = 1.0f / (LatitudeLines + 1.0f);
        var longitudeSpacing = 1.0f / LongitudeLines;
        var v = 1;

        for (var latitude = 0; latitude < LatitudeLines; latitude++)
        {
            for (var longitude = 0; longitude <= LongitudeLines; longitude++)
            {
                var theta = (float)(longitude * longitudeSpacing * 2.0f * Math.PI);
                var phi = (float)((1.0f - (latitude + 1) * latitudeSpacing - 0.5f) * Math.PI);
                var c = MathF.Cos(phi);

                positions[v] = Center + new Vector3(
                    c * MathF.Cos(theta),
                    MathF.Sin(phi),
                    c * MathF.Sin(theta)) * Radius;

                v++;
            }
        }

        v = 0;
        for (var i = 0; i < LongitudeLines; i++)
        {
            vertices[v++] = new PositionWidthColorVertex(positions[0], width, (Vector4)Color);
            vertices[v++] = new PositionWidthColorVertex(positions[i + 2], width, (Vector4)Color);
            vertices[v++] = new PositionWidthColorVertex(positions[i + 1], width, (Vector4)Color);
        }

        var rowLength = LongitudeLines + 1;
        for (var latitude = 0; latitude < LatitudeLines - 1; latitude++)
        {
            // Plus one for the pole.
            var rowStart = latitude * rowLength + 1;
            for (var longitude = 0; longitude < LongitudeLines; longitude++)
            {
                var firstCorner = rowStart + longitude;
                // First triangle of quad: Top-Left, Bottom-Left, Bottom-Right
                vertices[v++] = new PositionWidthColorVertex(positions[firstCorner], width, (Vector4)Color);
                vertices[v++] = new PositionWidthColorVertex(positions[firstCorner + rowLength + 1], width, (Vector4)Color);
                vertices[v++] = new PositionWidthColorVertex(positions[firstCorner + rowLength], width, (Vector4)Color);
                // Second triangle of quad: Top-Left, Bottom-Right, Top-Right
                vertices[v++] = new PositionWidthColorVertex(positions[firstCorner], width, (Vector4)Color);
                vertices[v++] = new PositionWidthColorVertex(positions[firstCorner + 1], width, (Vector4)Color);
                vertices[v++] = new PositionWidthColorVertex(positions[firstCorner + rowLength + 1], width, (Vector4)Color);
            }
        }

        var pole = positions.Length - 1;
        var bottomRow = (LatitudeLines - 1) * rowLength + 1;
        for (var i = 0; i < LongitudeLines; i++)
        {
            vertices[v++] = new PositionWidthColorVertex(positions[pole], width, (Vector4)Color);
            vertices[v++] = new PositionWidthColorVertex(positions[bottomRow + i], width, (Vector4)Color);
            vertices[v++] = new PositionWidthColorVertex(positions[bottomRow + i + 1], width, (Vector4)Color);
        }

        return vertices;
    }

    public Vector3 Center
    {
        get;
        set;
    }

    public float Radius
    {
        get;
        set;
    }

    public int LongitudeLines
    {
        get;
        set;
    }

    public int LatitudeLines
    {
        get;
        set;
    }

    public Color Color
    {
        get;
        set;
    }
}
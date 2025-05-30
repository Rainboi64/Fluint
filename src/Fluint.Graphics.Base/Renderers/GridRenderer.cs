using System;
using System.Collections.Generic;
using System.Linq;
using Fluint.Layer.Editor.Viewport;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Graphics.Renderers;
using Fluint.Layer.Mathematics;

namespace Fluint.Graphics.Base.Renderers;

public class GridRenderer : LineRenderer, IGridRenderer
{
    private const float LineWidth = 0.5f;
    public Grid Grid
    {
        get;
        set;
    }

    public GridRenderer(IGraphicsFactory graphicsFactory) : base(graphicsFactory, "Grid")
    {

    }

    public new void Start()
    {
        GenerateGrid();
        base.Start();
        CommandList.ClearDepthStencil(null, 1, 0);

    }

    public new void PreRender()
    {
        if (Grid.NeedsUpdate)
        {
            GenerateGrid();
            Grid.NeedsUpdate = false;
        }

        base.PreRender();
    }

    private void GenerateGrid()
    {
        var vertices = new List<PositionWidthColorVertex>();
        var k = 0;

        for (float x = -Grid.Size.X; x <= Grid.Size.X; x += Grid.Offsets.X)
        {
            var color = new Vector4(0.4f);
            if (x % 10 == 0)
            {
                color = new Vector4(0.8f);
            }

            if (x == 0)
            {
                color = new Vector4(1f, 0.4f, 0.4f, 1);
            }

            vertices.Add(new PositionWidthColorVertex(new Vector3(x, 0, -Grid.Size.Y), LineWidth, color));
            vertices.Add(new PositionWidthColorVertex(new Vector3(x, 0, Grid.Size.Y), LineWidth, color));
        }

        for (float y = -Grid.Size.Y; y <= Grid.Size.Y; y += Grid.Offsets.Y)
        {
            var color = new Vector4(0.4f);
            if (y % 10 == 0)
            {
                color = new Vector4(0.8f);
            }

            if (y == 0)
            {
                color = new Vector4(0.4f, 0.4f, 1f, 1);
            }

            vertices.Add(new PositionWidthColorVertex(new Vector3(-Grid.Size.X, 0, y), LineWidth, color));
            vertices.Add(new PositionWidthColorVertex(new Vector3(Grid.Size.X, 0, y), LineWidth, color));
        }

        Array.Copy(vertices.ToArray(), BufferObject.Vertex, vertices.Count);
    }
}

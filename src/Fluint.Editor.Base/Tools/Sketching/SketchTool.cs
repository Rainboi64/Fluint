//
// SketchTool.cs
//
// Copyright (C) 2021 Yaman Alhalabi

using System;
using System.Collections.Generic;
using System.Linq;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Editor;
using Fluint.Layer.Editor.Gizmos;
using Fluint.Layer.Editor.Tools;
using Fluint.Layer.Editor.Tools.Sketching;
using Fluint.Layer.Editor.Tools.Sketching.Shapes;
using Fluint.Layer.Editor.Viewport;
using Fluint.Layer.EntityComponentSystem;
using Fluint.Layer.Graphics;
using Fluint.Layer.Input;
using Fluint.Layer.Mathematics;

namespace Fluint.Editor.Base.Tools.Sketching;

[Tool("Sketch Tool", "./assets/tools/sketch_tool_48px.png")]
public class SketchTool : ITool
{
    private readonly Dictionary<ISketch, IGizmo[]> _gizmos = new();
    private readonly ModulePacket _packet;
    private readonly IGizmoProvider _provider;
    private readonly ILogger _logger;
    private readonly ISketchSystem _system;
    private readonly IWorld _world;

    private SketchMode _mode = SketchMode.Draw;

    private bool _active;
    private (ISketch Sketch, INode Node) _activeSketch = (null, null);
    
    private Vector3 _intersectionPoint = Vector3.Zero;
    private readonly List<(ISketch, int)> _selectedVertices = new();

    private Vector3 _dragStart = Vector3.Zero;
    private int _defaultSegments = 16;
    private float _defaultDepth = 0;

    public SketchTool(ModulePacket packet)
    {
        _packet = packet;
        _logger = packet.GetSingleton<ILogger>();
        _provider = packet.GetSingleton<IGizmoProvider>();
        _world = packet.GetSingleton<IWorld>();
        _system = _world.GetSystem<ISketchSystem, ISketch>();
    }

    public void Begin(IViewportContext context)
    {
        context.OnTick += ViewportOnOnTick;
    }

    private Ray GetPickRay(IViewportContext context)
    {
        // Inverting the mouse data coming from the glfw window.
        var mouse = new Vector2(
            context.InputManager.MouseLocation.X - context.Location.X,
            context.InputManager.MouseLocation.Y - context.Location.Y);

        var ray = Ray.GetPickRay(
            (int)mouse.X,
            context.Viewport.Height - (int)mouse.Y,
            context.Camera.Viewport,
            context.Camera.GetViewMatrix(),
            context.Camera.GetProjectionMatrix());

        return ray;
    }

    private static Vector3 IntersectViewPlane(Ray ray, ICamera camera)
    {
        var ground = new Plane(Vector3.Zero,
            // Or Vector3.UP for using the XY Grid
            Vector3.Up
            // camera.Front
        );
        Collision.RayIntersectsPlane(ref ray, ref ground, out Vector3 intersect);
        return intersect;
    }

    private void ViewportOnOnTick(object sender, EventArgs e)
    {
        if (sender is not IViewportContext { Focused: true } context)
        {
            return;
        }

        if (context.BindingsManager.GetState("MOVE_CAMERA") == InputState.Repeat)
        {
            return;
        }

        if (context.BindingsManager.GetState("GRID_SIZE_INCREASE") == InputState.Press)
        {
            context.Grid.Offsets = new Vector2(context.Grid.Offsets.X + 2, context.Grid.Offsets.Y + 2);
        }

        if (context.BindingsManager.GetState("SKETCH_ENABLE") == InputState.Press)
        {
            _active = !_active;
            _activeSketch = (null, null);
        }

        if (!_active)
        {
            return;
        }

        DrawGizmos();

        // Start Dragging
        if (context.InputManager.StartDragging(MouseButton.Button1))
        {
            // Mouse pick ray
            var ray = GetPickRay(context);
            var point = IntersectViewPlane(ray, context.Camera);
            _dragStart = point;

            if (Pick(ray))
            {
                return;
            }
        }
        
        // Dragging
        else if (context.InputManager.IsDragging(MouseButton.Button1))
        {
            Dragging(context);
        }

        // Stop Dragging
        if (context.InputManager.StopDragging(MouseButton.Button1))
        {
            _activeSketch = (null, null);
            _selectedVertices.Clear();
            _intersectionPoint = Vector3.Zero;
        }

        // Toggle Mode
        if (context.InputManager.IsMouseButtonPressed(MouseButton.Button3))
        {
            _mode = _mode == SketchMode.Extend ? SketchMode.Draw : SketchMode.Extend;
        }
    }

    private bool Pick(Ray ray)
    {
        // var rayLine = _packet.CreateScoped<IGizmoLine>();
        // rayLine.Start = ray.Position - ray.Direction * 10;
        // rayLine.End = ray.Position + ray.Direction * 10;
        // rayLine.Color = Color.Red;
        // rayLine.Width = 1;
        // _provider.Gizmos.Add(rayLine);
        
        // Test for selection
        if (!_system.Pick(ray, out IEnumerable<ISketch> items))
        {
            return false;
        }

        var sketches = items as ISketch[] ?? items.ToArray();
        _logger.Information("Picked {0} sketches.", sketches.Length);
            
        foreach (var sketch in sketches)
        {
            for (var i = 0; i < sketch.Vertices.Length; i += 2)
            {
                var cur = sketch.Vertices[i];
                var next = sketch.Vertices[i + 1];

                if (Collision.RayIsCloseToSegment(ray, cur, next, 0.1f, out var intersection))
                {
                    _intersectionPoint = intersection;
                   _selectedVertices.Add((sketch, i));
                    _selectedVertices.Add((sketch, i + 1));
                }
            }
        }

        return true;
    }

    private void Dragging(IViewportContext context)
    {
        var ray = GetPickRay(context);
        var point = IntersectViewPlane(ray, context.Camera);
        
        var snappedStart = Snap(_dragStart, context.Grid);
        var snappedEnd = Snap(point, context.Grid);

        var radiusVector = snappedEnd - snappedStart;
        if (radiusVector.Length() < 0.1f)
        {
            if (_activeSketch.Sketch is null)
            {
                return;
            }

            _system.Purge(_activeSketch.Sketch);
            _activeSketch.Sketch = null;

            return;
        } 
        
        if (_selectedVertices.Count > 0)
        {
            foreach (var (sketch, index) in _selectedVertices)
            {
                var previous = index == 0 ? sketch.Vertices.Length - 1 : index - 1;
                var next = index == sketch.Vertices.Length - 1 ? 0 : index + 1;
                
                sketch.Vertices[previous] = snappedEnd;
                sketch.Vertices[index] = snappedEnd;
                sketch.Vertices[next] = snappedEnd;

                sketch.UpdateBoundingBox();
            }
        }
        else if (_activeSketch.Sketch is null)
        {
            CreatePolygon(snappedStart, snappedEnd);
        }
        else
        {
            UpdatePolygon(context, snappedEnd);
        }
    }

    private void UpdatePolygon(IViewportContext context, Vector3 snappedPoint)
    {
        var polygon = (IPolygon)_activeSketch.Sketch;
        polygon!.Corner = snappedPoint;
        
        if (context.InputManager.MouseScrollDelta.IsZero)
        {
            return;
        }

        switch (_mode)
        {
            case SketchMode.Draw:
                var newSegments = _defaultSegments + (int)context.InputManager.MouseScrollDelta.Y;
                if (newSegments < 0)
                {
                    break;
                }

                _defaultSegments = newSegments;
                polygon.Segments = newSegments;
                
                _activeSketch.Node.Name = PolygonNameGenerator.GenerateNodeName(_defaultSegments);
                break;
            case SketchMode.Extend:
                var newDepth = context.InputManager.MouseScrollDelta.Y;

                _defaultDepth = newDepth;
                polygon.Depth = newDepth; 
                break;
        }
    }

    private void DrawGizmos()
    {
        foreach (var sketch in _system.Sketches)
        {
            switch (sketch)
            {
                case IPolygon circle when !_gizmos.ContainsKey(sketch):
                {
                    var line = _packet.CreateScoped<IGizmoLine>();
                    line.Color = Color.DarkRed;
                    line.Start = circle.Center;
                    line.End = circle.Corner;
                    line.Width = 1;

                    var grabberCenter = _packet.CreateScoped<IGizmoCube>();
                    grabberCenter.Box =
                        new OrientedBoundingBox(circle.Center + Vector3.Left, circle.Center + Vector3.Right);
                    grabberCenter.Color = Color.Red;

                    var grabberCorner = _packet.CreateScoped<IGizmoCube>();
                    grabberCorner.Box =
                        new OrientedBoundingBox(circle.Corner + Vector3.Left, circle.Corner + Vector3.Right);
                    grabberCorner.Color = Color.Red;

                    _provider.Gizmos.Add(line);
                    _provider.Gizmos.Add(grabberCenter);
                    _provider.Gizmos.Add(grabberCorner);
                    _gizmos[sketch] = new IGizmo[] { line, grabberCenter, grabberCorner };

                    break;
                }

                case IPolygon circle:
                {
                    var gizmos = _gizmos[sketch];

                    if (gizmos[0] is IGizmoLine gizmoLine)
                    {
                        gizmoLine.Start = circle.Center;
                        gizmoLine.End = circle.Segments >= 3 ? circle.Corner : circle.Center;
                    }

                    if (gizmos[1] is IGizmoCube gizmoCube)
                    {
                        gizmoCube.Box =
                            new OrientedBoundingBox(circle.Center - Vector3.One / 10.0f,
                                circle.Center + Vector3.One / 10.0f);
                    }

                    if (gizmos[2] is IGizmoCube gizmoCube1)
                    {
                        gizmoCube1.Box =
                            new OrientedBoundingBox(circle.Corner - Vector3.One / 10.0f,
                                circle.Corner + Vector3.One / 10.0f);
                    }

                    break;
                }
                case ISpline:
                    break;
            }
        }
    }

    private static Vector3 Snap(Vector3 point, Grid grid)
    {
        return new Vector3(
            grid.Offsets.X * MathF.Round(point.X / grid.Offsets.X),
            grid.Offsets.Y * MathF.Round(point.Y / grid.Offsets.Y),
            MathF.Round(point.Z));
    }

    private void CreatePolygon(Vector3 start, Vector3 end)
    {
        var polygon = _packet.CreateScoped<IPolygon>();
        polygon.Segments = _defaultSegments;
        polygon.Center = start;
        polygon.Corner = end;
        polygon.Depth = _defaultDepth;

        var node = _packet.CreateScoped<INode>();
        node.Name = PolygonNameGenerator.GenerateNodeName(_defaultSegments);

        _activeSketch = (polygon, node);

        var sketchEntity = new Entity();
        sketchEntity.AddComponent(node);
        sketchEntity.AddComponent(polygon);

        _world.AddComponent(node);
        _world.AddComponent<ISketch>(polygon);

        _logger.Information("[Sketch Tool] Creating a {0}", node.Name);
    }
}

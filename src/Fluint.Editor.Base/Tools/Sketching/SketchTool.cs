// 
// SketchTool.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using System.Collections.Generic;
using Fluint.Layer.DependencyInjection;
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

    private readonly ISketchSystem _system;
    private readonly IWorld _world;
    private bool _active;
    private (ShapeType Type, ISketch Sketch) _activeSketch = (ShapeType.None, null);

    private int _segments = 16;

    public SketchTool(ModulePacket packet)
    {
        _packet = packet;
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
            context.Grid.Offsets = new Vector2i(context.Grid.Offsets.X + 2, context.Grid.Offsets.Y + 2);
        }

        if (context.BindingsManager.GetState("SKETCH_ENABLE") == InputState.Press)
        {
            _active = !_active;
            _activeSketch = (ShapeType.None, null);
        }

        if (!_active)
        {
            return;
        }

        DrawGizmos();

        if (!context.InputManager.WasMouseButtonPressed(MouseButton.Button1) &&
            context.InputManager.IsMouseButtonPressed(MouseButton.Button1))
        {
            var ray = GetPickRay(context);
            var point = IntersectViewPlane(ray, context.Camera);
            var snapped = Snap(point, context.Grid);

            CreatePolygon(snapped);
        }
        else if (context.InputManager.WasMouseButtonPressed(MouseButton.Button1) &&
                 context.InputManager.IsMouseButtonPressed(MouseButton.Button1))
        {
            var ray = GetPickRay(context);
            var point = IntersectViewPlane(ray, context.Camera);
            var snappedPoint = Snap(point, context.Grid);

            switch (_activeSketch.Type)
            {
                case ShapeType.Polygon:
                    ((IPolygon)_activeSketch.Sketch)!.Corner = snappedPoint;
                    if (!context.InputManager.MouseScrollDelta.IsZero)
                    {
                        _segments = ((IPolygon)_activeSketch.Sketch)!.Segments +=
                            (int)context.InputManager.MouseScrollDelta.Y;
                    }

                    break;

                case ShapeType.Spline:
                    break;
                case ShapeType.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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

    private void CreatePolygon(Vector3 point)
    {
        var circle = _world.CreateComponent<IPolygon, ISketch>();
        circle.Segments = _segments;
        circle.Center = point;
        circle.Corner = point + Vector3.Right;

        _activeSketch = (ShapeType.Polygon, circle);
        _system.Register(circle);
    }
}
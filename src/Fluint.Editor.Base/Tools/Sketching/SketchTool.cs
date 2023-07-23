// 
// SketchTool.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Editor.Gizmos;
using Fluint.Layer.Editor.Tools;
using Fluint.Layer.Editor.Tools.Sketching;
using Fluint.Layer.Editor.Viewport;
using Fluint.Layer.EntityComponentSystem;
using Fluint.Layer.Graphics;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Graphics.Debug;
using Fluint.Layer.Input;
using Fluint.Layer.Mathematics;

namespace Fluint.Editor.Base.Tools.Sketching;

[Tool("Sketch Tool", "./assets/tools/sketch_tool_48px.png")]
public class SketchTool : ITool
{
    private readonly ModulePacket _packet;
    private readonly IGizmoProvider _provider;
    private readonly Random _random = new();

    private readonly IDebugServer _server;
    private readonly ISketchSystem _system;
    private readonly IWorld _world;

    private bool _active;
    private ISketch _activeSketch;
    private bool _initialDrag;
    private int[] _selectedVertex = Array.Empty<int>();

    public SketchTool(ModulePacket packet)
    {
        _packet = packet;
        _provider = packet.GetSingleton<IGizmoProvider>();
        _server = packet.GetSingleton<IDebugServer>();
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

    private Vector3 IntersectViewPlane(Ray ray, ICamera camera)
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


        if (context.BindingsManager.GetState("GRID_SIZE_INCREASE") == InputState.Press)
        {
            context.Grid.Offsets = new Vector2i(context.Grid.Offsets.X + 2, context.Grid.Offsets.Y + 2);
        }

        if (context.BindingsManager.GetState("SKETCH_ENABLE") == InputState.Press)
        {
            _active = !_active;
            _activeSketch = null;
        }

        if (!_active)
        {
            return;
        }

        if (!context.InputManager.WasMouseButtonPressed(MouseButton.Button1) &&
            context.InputManager.IsMouseButtonPressed(MouseButton.Button1))
        {
            var ray = GetPickRay(context);
            var point = IntersectViewPlane(ray, context.Camera);
            var polygon = _packet.CreateScoped<IGizmoCylinder>();
            polygon.Height = _random.Next(5, 20);
            polygon.IsCone = _random.Next(1, 3) == 1;
            polygon.Center = point;
            polygon.Radius = _random.Next(5, 15);
            polygon.BaseSegments = _random.Next(3, 64);
            polygon.Color = _random.NextColor();

            _provider.Gizmos.Add(polygon);
        }

        if (_activeSketch is not null && !_initialDrag)
        {
            if (!context.InputManager.WasMouseButtonPressed(MouseButton.Button1) &&
                context.InputManager.IsMouseButtonPressed(MouseButton.Button1))
            {
                var ray = GetPickRay(context);
                _selectedVertex = _activeSketch.PickMultipleVertex(ray, 1);
            }

            if (context.InputManager.IsMouseButtonPressed(MouseButton.Button1) && _selectedVertex.Length > 0)
            {
                var ray = GetPickRay(context);
                var point = IntersectViewPlane(ray, context.Camera);

                foreach (var idx in _selectedVertex)
                {
                    var vertex = _activeSketch!.Vertex[idx];
                    vertex.Position = point;
                    _activeSketch.Vertex[idx] = vertex;
                }
            }

            return;
        }


        if (_initialDrag && context.InputManager.WasMouseButtonPressed(MouseButton.Button1) &&
            context.InputManager.IsMouseButtonPressed(MouseButton.Button1))
        {
            var ray = GetPickRay(context);
            var point = IntersectViewPlane(ray, context.Camera);

            _activeSketch!.Vertex = GenerateCube(_activeSketch.Vertex[0].Position, point, context.Grid.Offsets);
            _activeSketch!.Update();
        }

        if (_initialDrag && context.InputManager.WasMouseButtonPressed(MouseButton.Button1) &&
            !context.InputManager.IsMouseButtonPressed(MouseButton.Button1))
        {
            _initialDrag = false;
        }
    }

    private PositionColorVertex[] GenerateCube(Vector3 start, Vector3 end, Vector2i offsets)
    {
        // start = new Vector3(
        //     MathUtil.RoundToClosest(start.X, offsets.X),
        //     MathUtil.RoundToClosest(start.Y, 1),
        //     MathUtil.RoundToClosest(start.Z, offsets.Y));
        //
        // end = new Vector3(
        //     MathUtil.RoundToClosest(end.X, offsets.X),
        //     MathUtil.RoundToClosest(end.Y, 1),
        //     MathUtil.RoundToClosest(end.Z, offsets.Y));

        var temp = new PositionColorVertex[]
        {
            new(new Vector3(start.X, start.Y, start.Z), Vector4.UnitY),
            new(new Vector3(end.X, start.Y, start.Z), Vector4.UnitY),

            new(new Vector3(end.X, start.Y, start.Z), Vector4.UnitY),
            new(new Vector3(end.X, start.Y, end.Z), Vector4.UnitY),

            new(new Vector3(end.X, start.Y, end.Z), Vector4.UnitY),
            new(new Vector3(start.X, start.Y, end.Z), Vector4.UnitY),

            new(new Vector3(start.X, start.Y, end.Z), Vector4.UnitY),
            new(new Vector3(start.X, start.Y, start.Z), Vector4.UnitY)
        };
        return temp;
    }

    private void CreateSketch(Vector3 point)
    {
        _activeSketch = _world.CreateComponent<ISketch>();
        _activeSketch!.Vertex = new[]
        {
            new PositionColorVertex(point, Vector4.UnitX)
        };
        _activeSketch.Update();
        _system.Register(_activeSketch);
    }
}
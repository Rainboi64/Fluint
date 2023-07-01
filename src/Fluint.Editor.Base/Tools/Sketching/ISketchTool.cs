// 
// SketchTool.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Editor.Tools;
using Fluint.Layer.Editor.Tools.Sketching;
using Fluint.Layer.Editor.Viewport;
using Fluint.Layer.EntityComponentSystem;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Input;
using Fluint.Layer.Mathematics;

namespace Fluint.Editor.Base.Tools.Sketching;

[Tool("Sketch Tool", "./assets/tools/sketch_tool_48px.png")]
public class SketchTool : ITool
{
    private readonly ISketchSystem _system;

    private readonly IWorld _world;

    public SketchTool(ModulePacket packet)
    {
        _world = packet.GetSingleton<IWorld>();
        _system = _world.GetSystem<ISketchSystem, ISketch>();
    }

    public void Begin(IViewportContext context)
    {
        context.OnTick += ViewportOnOnTick;
    }

    private Vector3 GetPickRay(IViewportContext context)
    {
        var screen = new Vector2(
            context.InputManager.MouseLocation.X - context.Location.X,
            context.InputManager.MouseLocation.Y - context.Location.Y);

        var ray = Ray.GetPickRay(
            (int)screen.X,
            context.Viewport.Height - (int)screen.Y,
            context.Viewport,
            context.Camera.GetViewMatrix() * context.Camera.GetProjectionMatrix());

        var ground = new Plane(context.Camera.Position, context.Camera.Front);
        Collision.RayIntersectsPlane(ref ray, ref ground, out Vector3 intersect);
        return intersect;
    }

    private void ViewportOnOnTick(object sender, EventArgs e)
    {
        if (sender is not IViewportContext { Focused: true } context)
        {
            return;
        }


        if (context.InputManager.WasMouseButtonPressed(MouseButton.Button1) ||
            !context.InputManager.IsMouseButtonPressed(MouseButton.Button1))
        {
            return;
        }

        var point = GetPickRay(context);

        var sketch = _world.CreateComponent<ISketch>();
        sketch.Vertex = new[] {
            new PositionColorVertex(point, Vector4.UnitX),
            new PositionColorVertex(point * 2, Vector4.UnitY)
        };
        sketch.Update();

        _system.Register(sketch);
    }
}
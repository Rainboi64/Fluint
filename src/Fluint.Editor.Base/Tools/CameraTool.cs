// 
// CameraTool.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Editor.Tools;
using Fluint.Layer.Editor.Viewport;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Input;
using Fluint.Layer.Mathematics;

namespace Fluint.Editor.Base.Tools;

public class CameraTool : ITool
{
    private readonly ModulePacket _packet;

    public CameraTool(ModulePacket packet)
    {
        _packet = packet;
    }

    public void Begin(IViewportContext context)
    {
        var state = new CameraToolState(_packet.CreateScoped<IMouseCapture>());
        state.MouseCapture.Begin(context.Window);
        context.AddState(state);
        context.OnTick += ViewportOnTick;
    }

    private void ViewportOnTick(object sender, TickEventArgs e)
    {
        if (sender is not IViewportContext { Focused: true } context)
        {
            return;
        }

        var state = context.GetState<CameraToolState>();

        state.MouseCapture.Update();
        const float camBoost = 0.1f;

        if (context.BindingsManager.Get("MOVE_CAMERA") != InputState.Repeat)
        {
            return;
        }

        if (context.InputManager.IsMouseButtonPressed(MouseButton.Button1))
        {
            state.MouseCapture.Capture();
            context.Camera.Pitch = state.MouseCapture.Y * camBoost;
            context.Camera.Yaw = state.MouseCapture.X * camBoost;
        }

        context.Camera.Zoom -= context.InputManager.MouseScrollDelta.Y / 100f;

        if (context.InputManager.IsKeyPressed(Key.K))
        {
            context.Camera.ProjectionMode = context.Camera.ProjectionMode == ProjectionMode.Orthogonal
                ? ProjectionMode.Prespective
                : ProjectionMode.Orthogonal;
        }

        var boost = context.InputManager.IsKeyPressed(Key.LeftShift) ? 20f : 10f;
        var movementVec = Vector3.Zero;

        if (context.InputManager.IsKeyPressed(Key.W))
        {
            if (context.Camera.ProjectionMode == ProjectionMode.Orthogonal)
            {
                movementVec -= context.Camera.Up;
            }
            else
            {
                movementVec += context.Camera.Front;
            }
        }

        if (context.InputManager.IsKeyPressed(Key.S))
        {
            if (context.Camera.ProjectionMode == ProjectionMode.Orthogonal)
            {
                movementVec += context.Camera.Up;
            }
            else
            {
                movementVec += context.Camera.Front;
            }
        }

        if (context.InputManager.IsKeyPressed(Key.A))
        {
            movementVec -= context.Camera.Right;
        }

        if (context.InputManager.IsKeyPressed(Key.D))
        {
            movementVec += context.Camera.Right;
        }

        context.Camera.Position += Vector3.Normalize(movementVec) * boost * (float)e.Time;
    }
}
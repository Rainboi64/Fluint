// GameWindow.cs
// 
// Copyright (C) 2023 Yaman Alhalabi

using Fluint.Layer.Graphics;
using Fluint.Layer.Graphics.Renderers;
using Fluint.Layer.Mathematics;
using Fluint.Layer.UI;
using Fluint.Layer.Windowing;
using ImGuiNET;

namespace Game;

public class GameWindow : IPuppet
{
    private readonly ICanvas _canvas;
    private readonly ICanvasRenderer _canvasRenderer;
    
    private IWindow _possessor = null!;

    public GameWindow(ICanvas canvas, ICanvasRenderer canvasRenderer)
    {
        _canvas = canvas;
        _canvasRenderer = canvasRenderer;
    }

    public void SetPossessed(IWindow possessor)
    {
        _possessor = possessor;
    }

    public void OnLoad()
    {
        _canvas.InitializeCanvas(_possessor.Size.X, _possessor.Size.Y);

        _canvasRenderer.Canvas = _canvas;
    }

    public void PreRender(double delay)
    {
        _canvas.InitializeCanvas(_possessor.Size.X, _possessor.Size.Y);

        _canvas.Clear(Color.Red);
        _canvas.DrawCircle(new Vector2i(30, 30), 15, Color.Aqua);
        _canvasRenderer.Update();
        
    }

    public void PostRender(double delay)
    {
        ImGui.Begin("Main Canvas", ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoNav | ImGuiWindowFlags.NoTitleBar);
        ImGui.Image(new IntPtr(_canvasRenderer.Texture.Handle), _possessor.Size.ToNumericsVector2());
        ImGui.End();
    }

    public void OnResize(int width, int height)
    {
        _canvas.InitializeCanvas(width, height);
    }
}
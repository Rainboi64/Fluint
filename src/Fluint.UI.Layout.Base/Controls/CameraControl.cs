// 
// CameraControl.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Fluint.Layer.Configuration;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Editor;
using Fluint.Layer.Graphics;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Input;
using Fluint.Layer.Localization;
using Fluint.Layer.Mathematics;
using Fluint.Layer.StateManagement;
using Fluint.Layer.UI;
using Fluint.Layer.Windowing;

namespace Fluint.UI.Layout.Base.Controls;

public class CameraControl : Control
{
    private static readonly Vector4 cubeColor = new(112 / 256f, 53 / 256f, 63 / 256f, 1.0f);
    private readonly ICamera _camera;

    private readonly ICommandList _commandList;
    private readonly IContainer _container;
    private readonly IVertexBuffer _cubeBuffer;

    private readonly IPipeline _cubePipeline;

    private readonly IPipeline _gridPipeline;

    private readonly IPipeline _hudPipeline;

    private readonly IConstantBuffer _inputBuffer;

    private readonly ITextLabel _label;
    private readonly IMouseCapture _mouseCapture;

    private readonly IStateManager _stateManager;
    private readonly ISwapChain _swapChain;

    private readonly ToolState _toolState;

    private readonly IViewport _viewport;

    private readonly List<PositionColorVertex> vertices1Colored = new() {
        new PositionColorVertex(new Vector3(-1.0f, -1.0f, -1.0f), cubeColor), // Front 
        new PositionColorVertex(new Vector3(-1.0f, 1.0f, -1.0f), cubeColor),
        new PositionColorVertex(new Vector3(1.0f, 1.0f, -1.0f), cubeColor),
        new PositionColorVertex(new Vector3(-1.0f, -1.0f, -1.0f), cubeColor),
        new PositionColorVertex(new Vector3(1.0f, 1.0f, -1.0f), cubeColor),
        new PositionColorVertex(new Vector3(1.0f, -1.0f, -1.0f), cubeColor),

        new PositionColorVertex(new Vector3(-1.0f, -1.0f, 1.0f), cubeColor), // BACK 
        new PositionColorVertex(new Vector3(1.0f, 1.0f, 1.0f), cubeColor),
        new PositionColorVertex(new Vector3(-1.0f, 1.0f, 1.0f), cubeColor),
        new PositionColorVertex(new Vector3(-1.0f, -1.0f, 1.0f), cubeColor),
        new PositionColorVertex(new Vector3(1.0f, -1.0f, 1.0f), cubeColor),
        new PositionColorVertex(new Vector3(1.0f, 1.0f, 1.0f), cubeColor),

        new PositionColorVertex(new Vector3(-1.0f, 1.0f, -1.0f), cubeColor), // Top 
        new PositionColorVertex(new Vector3(-1.0f, 1.0f, 1.0f), cubeColor),
        new PositionColorVertex(new Vector3(1.0f, 1.0f, 1.0f), cubeColor),
        new PositionColorVertex(new Vector3(-1.0f, 1.0f, -1.0f), cubeColor),
        new PositionColorVertex(new Vector3(1.0f, 1.0f, 1.0f), cubeColor),
        new PositionColorVertex(new Vector3(1.0f, 1.0f, -1.0f), cubeColor),

        new PositionColorVertex(new Vector3(-1.0f, -1.0f, -1.0f), cubeColor), // Bottom 
        new PositionColorVertex(new Vector3(1.0f, -1.0f, 1.0f), cubeColor),
        new PositionColorVertex(new Vector3(-1.0f, -1.0f, 1.0f), cubeColor),
        new PositionColorVertex(new Vector3(-1.0f, -1.0f, -1.0f), cubeColor),
        new PositionColorVertex(new Vector3(1.0f, -1.0f, -1.0f), cubeColor),
        new PositionColorVertex(new Vector3(1.0f, -1.0f, 1.0f), cubeColor),

        new PositionColorVertex(new Vector3(-1.0f, -1.0f, -1.0f), cubeColor), // Left 
        new PositionColorVertex(new Vector3(-1.0f, -1.0f, 1.0f), cubeColor),
        new PositionColorVertex(new Vector3(-1.0f, 1.0f, 1.0f), cubeColor),
        new PositionColorVertex(new Vector3(-1.0f, -1.0f, -1.0f), cubeColor),
        new PositionColorVertex(new Vector3(-1.0f, 1.0f, 1.0f), cubeColor),
        new PositionColorVertex(new Vector3(-1.0f, 1.0f, -1.0f), cubeColor),

        new PositionColorVertex(new Vector3(1.0f, -1.0f, -1.0f), cubeColor), // Right 
        new PositionColorVertex(new Vector3(1.0f, 1.0f, 1.0f), cubeColor),
        new PositionColorVertex(new Vector3(1.0f, -1.0f, 1.0f), cubeColor),
        new PositionColorVertex(new Vector3(1.0f, -1.0f, -1.0f), cubeColor),
        new PositionColorVertex(new Vector3(1.0f, 1.0f, -1.0f), cubeColor),
        new PositionColorVertex(new Vector3(1.0f, 1.0f, 1.0f), cubeColor)
    };

    private IVertexBuffer _gridBuffer;
    private IVertexBuffer _hudBuffer;
    private IInputManager _inputManager;

    private float _length = 1.0f;
    private IWindow _window;
    private Vector3 end = Vector3.One;

    private Vector3 start = Vector3.One;

    private List<PositionColorVertex> verticesColored = new() {
        new PositionColorVertex(new Vector3(-1.0f, -1.0f, -1.0f), cubeColor), // Front 
        new PositionColorVertex(new Vector3(-1.0f, 1.0f, -1.0f), cubeColor),
        new PositionColorVertex(new Vector3(1.0f, 1.0f, -1.0f), cubeColor),
        new PositionColorVertex(new Vector3(-1.0f, -1.0f, -1.0f), cubeColor),
        new PositionColorVertex(new Vector3(1.0f, 1.0f, -1.0f), cubeColor),
        new PositionColorVertex(new Vector3(1.0f, -1.0f, -1.0f), cubeColor),

        new PositionColorVertex(new Vector3(-1.0f, -1.0f, 1.0f), cubeColor), // BACK 
        new PositionColorVertex(new Vector3(1.0f, 1.0f, 1.0f), cubeColor),
        new PositionColorVertex(new Vector3(-1.0f, 1.0f, 1.0f), cubeColor),
        new PositionColorVertex(new Vector3(-1.0f, -1.0f, 1.0f), cubeColor),
        new PositionColorVertex(new Vector3(1.0f, -1.0f, 1.0f), cubeColor),
        new PositionColorVertex(new Vector3(1.0f, 1.0f, 1.0f), cubeColor),

        new PositionColorVertex(new Vector3(-1.0f, 1.0f, -1.0f), cubeColor), // Top 
        new PositionColorVertex(new Vector3(-1.0f, 1.0f, 1.0f), cubeColor),
        new PositionColorVertex(new Vector3(1.0f, 1.0f, 1.0f), cubeColor),
        new PositionColorVertex(new Vector3(-1.0f, 1.0f, -1.0f), cubeColor),
        new PositionColorVertex(new Vector3(1.0f, 1.0f, 1.0f), cubeColor),
        new PositionColorVertex(new Vector3(1.0f, 1.0f, -1.0f), cubeColor),

        new PositionColorVertex(new Vector3(-1.0f, -1.0f, -1.0f), cubeColor), // Bottom 
        new PositionColorVertex(new Vector3(1.0f, -1.0f, 1.0f), cubeColor),
        new PositionColorVertex(new Vector3(-1.0f, -1.0f, 1.0f), cubeColor),
        new PositionColorVertex(new Vector3(-1.0f, -1.0f, -1.0f), cubeColor),
        new PositionColorVertex(new Vector3(1.0f, -1.0f, -1.0f), cubeColor),
        new PositionColorVertex(new Vector3(1.0f, -1.0f, 1.0f), cubeColor),

        new PositionColorVertex(new Vector3(-1.0f, -1.0f, -1.0f), cubeColor), // Left 
        new PositionColorVertex(new Vector3(-1.0f, -1.0f, 1.0f), cubeColor),
        new PositionColorVertex(new Vector3(-1.0f, 1.0f, 1.0f), cubeColor),
        new PositionColorVertex(new Vector3(-1.0f, -1.0f, -1.0f), cubeColor),
        new PositionColorVertex(new Vector3(-1.0f, 1.0f, 1.0f), cubeColor),
        new PositionColorVertex(new Vector3(-1.0f, 1.0f, -1.0f), cubeColor),

        new PositionColorVertex(new Vector3(1.0f, -1.0f, -1.0f), cubeColor), // Right 
        new PositionColorVertex(new Vector3(1.0f, 1.0f, 1.0f), cubeColor),
        new PositionColorVertex(new Vector3(1.0f, -1.0f, 1.0f), cubeColor),
        new PositionColorVertex(new Vector3(1.0f, -1.0f, -1.0f), cubeColor),
        new PositionColorVertex(new Vector3(1.0f, 1.0f, -1.0f), cubeColor),
        new PositionColorVertex(new Vector3(1.0f, 1.0f, 1.0f), cubeColor)
    };

    public CameraControl(ModulePacket packet)
    {
        _stateManager = packet.GetSingleton<IStateManager>();
        _toolState = _stateManager.GetState<ToolState>();

        var configurationManager = packet.GetSingleton<IConfigurationManager>();
        var theme = configurationManager.Get<ThemeConfiguration>();

        var white = (Vector4)Color.Wheat;
        var red = (Vector4)Color.Red;
        var green = (Vector4)Color.Green;


        const int gridSize = 1024;
        var gridLines = new List<PositionColorVertex>();
        for (var y = -gridSize; y <= gridSize; y++)
        {
            var color = new Vector4(0.2f);
            if (y % 10 == 0)
            {
                color = new Vector4(0.4f);
            }

            if (y == 0)
            {
                color = new Vector4(0.8f, 0.2f, 0.2f, 1);
            }

            gridLines.Add(new PositionColorVertex(new Vector3(y, 0, -gridSize), color));
            gridLines.Add(new PositionColorVertex(new Vector3(y, 0, gridSize), color));
        }

        for (var x = -gridSize; x <= gridSize; x++)
        {
            var color = new Vector4(0.2f);
            if (x % 10 == 0)
            {
                color = new Vector4(0.4f);
            }

            if (x == 0)
            {
                color = new Vector4(0.2f, 0.2f, 0.8f, 1);
            }

            gridLines.Add(new PositionColorVertex(new Vector3(-gridSize, 0, x), color));
            gridLines.Add(new PositionColorVertex(new Vector3(gridSize, 0, x), color));
        }

        var localizationManager = packet.GetSingleton<ILocalizationManager>();
        var graphicsFactory = packet.CreateScoped<IGraphicsFactory>();

        _mouseCapture = packet.CreateScoped<IMouseCapture>();

        _camera = packet.CreateScoped<ICamera>();
        _camera.Fov = 90;

        _camera.Viewport = new ViewportF(0, 0, 500, 500);
        _camera.ProjectionMode = ProjectionMode.Orthogonal;
        _camera.Position = new Vector3(0, 0, 0);
        _camera.Zoom = 0.01f;
        _camera.Pitch = 90;

        var inputBuffer = new InputBuffer {
            ModelMatrix = Matrix.Scaling(1f), ViewMatrix = _camera.GetViewMatrix(),
            ProjectionMatrix = _camera.GetProjectionMatrix()
        };
        _inputBuffer = graphicsFactory.CreateConstantBuffer(inputBuffer);

        _container = packet.CreateScoped<IContainer>();
        _swapChain =
            graphicsFactory.CreateSwapchain(new SwapChainDescriptor(750, 750, false, false, SwapEffect.Discard));

        var vertexShader = graphicsFactory.CreateShaderFromFile(ShaderStage.Vertex, "./base/shaders/grid.vert",
            VertexType.PositionColor, Enumerable.Empty<(string, string)>());
        var pixelShader = graphicsFactory.CreateShaderFromFile(ShaderStage.Pixel, "./base/shaders/grid.frag",
            VertexType.PositionColor, Enumerable.Empty<(string, string)>());

        _gridBuffer = graphicsFactory.CreateVertexBuffer(verticesColored.ToArray());
        _hudBuffer = graphicsFactory.CreateVertexBuffer(verticesColored.ToArray());

        vertexShader = graphicsFactory.CreateShaderFromFile(ShaderStage.Vertex, "./base/shaders/simple.vert",
            VertexType.PositionColor, Enumerable.Empty<(string, string)>());
        pixelShader = graphicsFactory.CreateShaderFromFile(ShaderStage.Pixel, "./base/shaders/simple.frag",
            VertexType.PositionColor, Enumerable.Empty<(string, string)>());

        _gridPipeline = graphicsFactory.CreatePipeline(vertexShader, pixelShader, vertexShader.InputLayout, null, null,
            null, new Viewport(0, 0, 750, 750), PrimitiveTopology.TriangleList);
        _cubePipeline = graphicsFactory.CreatePipeline(vertexShader, pixelShader, vertexShader.InputLayout, null, null,
            null, new Viewport(0, 0, 750, 750), PrimitiveTopology.Lines);
        _hudPipeline = graphicsFactory.CreatePipeline(vertexShader, pixelShader, vertexShader.InputLayout, null, null,
            null, new Viewport(0, 0, 750, 750), PrimitiveTopology.Lines);

        _cubeBuffer = graphicsFactory.CreateVertexBuffer(gridLines.ToArray());

        _commandList = graphicsFactory.CreateCommandList();
        _viewport = packet.CreateScoped<IViewport>();
        _viewport.SwapChain = _swapChain;

        _container.Title = localizationManager.Fetch("camera");

        _container.ScrollBar = false;

        _label = packet.CreateScoped<ITextLabel>();

        _container.Add("Viewport", _viewport);
        Children.Add(_container);
    }


    public List<PositionColorVertex> GenerateCube(Vector3 Verts, Vector3 Trans, Color4 color)
    {
        var moder = new Vector3((int)Math.Round(Verts.X), (int)Math.Round(Verts.Y), (int)Math.Round(Verts.Z));
        var trans = new Vector3((int)Math.Round(Trans.X), (int)Math.Round(Trans.Y), (int)Math.Round(Trans.Z));

        moder -= trans;

        moder += Vector3.Up * _length;

        return vertices1Colored.Select(verts => new PositionColorVertex(trans + verts.Position * moder, color))
            .ToList();
    }

    private void InitializeCommandList()
    {
        _commandList.Clear();

        _commandList.ClearRenderTarget(_swapChain.TextureView, new Color4(0, 0, 0, 0));
        _commandList.ClearDepthStencil(_swapChain.DepthStencilView, 1.0f, 1);

        _commandList.Begin("Grid", _cubePipeline);
        _commandList.SetVertexBuffer(_cubeBuffer);
        _commandList.SetConstantBuffer(_inputBuffer, BufferScope.VertexShader);
        _commandList.Draw(16384 * 8);

        _commandList.Begin("Cube", _gridPipeline);
        _commandList.SetVertexBuffer(_gridBuffer);
        _commandList.SetConstantBuffer(_inputBuffer, BufferScope.VertexShader);
        _commandList.Draw(16384 * 8);

        _commandList.Begin("HUD", _cubePipeline);
        _commandList.SetVertexBuffer(_hudBuffer);
        _commandList.SetConstantBuffer(_inputBuffer, BufferScope.VertexShader);
        _commandList.Draw(16384 * 8);

        _commandList.End();
    }

    public override void Begin(string name, IWindow window)
    {
        _window = window;
        _inputManager = window.InputManager;
        _mouseCapture.Begin(window);

        if (_stateManager.GetState<WorldState>(window).HudBuffer is null)
        {
            _stateManager.GetState<WorldState>(window).WorldVerticesBuffer = _gridBuffer;
            _stateManager.GetState<WorldState>(window).HudBuffer = _hudBuffer;
            _stateManager.GetState<WorldState>(window).WorldVertices = verticesColored;
        }
        else
        {
            _gridBuffer = _stateManager.GetState<WorldState>(window).WorldVerticesBuffer;
            verticesColored = _stateManager.GetState<WorldState>(window).WorldVertices;
            _hudBuffer = _stateManager.GetState<WorldState>(window).HudBuffer;
        }


        InitializeCommandList();
        base.Begin(name, window);
    }

    private bool GetPickWithGround(out Vector3 hitpoint)
    {
        var x = _inputManager.MouseLocation.X - _container.Location.X;
        var y = _container.Size.Y - (_inputManager.MouseLocation.Y - _container.Location.Y);

        var pickRay = Ray.GetPickRay((int)x, (int)y, _camera.Viewport,
            _camera.GetViewMatrix() * _camera.GetProjectionMatrix());
        var plane = new Plane(Vector3.Zero, Vector3.Down);

        return plane.Intersects(ref pickRay, out hitpoint);
    }

    public override void Tick()
    {
        _mouseCapture.Update();
        if (_container.IsFocused)
        {
            var camBoost = 0.1f;

            if (_inputManager.IsKeyPressed(Key.K))
            {
                _camera.ProjectionMode = _camera.ProjectionMode == ProjectionMode.Orthogonal
                    ? ProjectionMode.Prespective
                    : ProjectionMode.Orthogonal;
            }

            if (_toolState.ActiveTool is ISketchTool)
            {
                if (_inputManager.IsMouseButtonPressed(MouseButton.Button2))
                {
                    _mouseCapture.Capture();
                    _camera.Yaw = _mouseCapture.X * camBoost;
                    _camera.Pitch = _mouseCapture.Y * camBoost;
                }

                if (_inputManager.IsMouseButtonPressed(MouseButton.Button1))
                {
                    if (_inputManager.WasMouseButtonPressed(MouseButton.Button1))
                    {
                        if (GetPickWithGround(out var e))
                        {
                            end = e;
                            _hudBuffer.Initialize(GenerateCube(e, start, Color.Red).ToArray());
                        }
                    }
                    else
                    {
                        if (GetPickWithGround(out var st))
                        {
                            start = st;
                        }
                    }
                }
            }


            if (_inputManager.IsKeyPressed(Key.Space))
            {
                verticesColored.AddRange(GenerateCube(end, start, Color.Gray));
                _gridBuffer.Initialize(verticesColored.ToArray());
            }

            var boost = _inputManager.IsKeyPressed(Key.LeftShift) ? 20f : 10f;
            var movementVec = Vector3.Zero;

            if (_inputManager.IsKeyPressed(Key.LeftShift))
            {
                _length += _inputManager.MouseScrollDelta.Y;
            }
            else
            {
                _camera.Zoom -= _inputManager.MouseScrollDelta.Y / 100f;
            }


            if (_inputManager.IsKeyPressed(Key.W))
            {
                movementVec += _camera.Front;
            }

            if (_inputManager.IsKeyPressed(Key.S))
            {
                movementVec -= _camera.Front;
            }

            if (_inputManager.IsKeyPressed(Key.A))
            {
                movementVec -= _camera.Right;
            }

            if (_inputManager.IsKeyPressed(Key.D))
            {
                movementVec += _camera.Right;
            }

            _camera.Position += Vector3.Normalize(movementVec) * boost * (float)_window.FrameTime;

            _inputBuffer.UpdateBuffer(new InputBuffer {
                ModelMatrix = Matrix.Scaling(1f), ViewMatrix = _camera.GetViewMatrix(),
                ProjectionMatrix = _camera.GetProjectionMatrix()
            });
        }

        _swapChain.Present();
        {
            _commandList.Submit();
        }
        _swapChain.Disconnect();

        _label.Text =
            $"Camera Position: ({_camera.Position.ToString()}), Camera Pitch: ({_camera.Pitch}), Camera Yaw: ({_camera.Yaw})";

        base.Tick();

        if (_viewport.Size != _container.Size)
        {
            _viewport.Size = _container.Size;
            _camera.Viewport =
                _cubePipeline.Viewport =
                    _gridPipeline.Viewport = new Viewport(0, 0, _container.Size.X, _container.Size.Y, 0.01f, 100000f);
            InitializeCommandList();
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct InputBuffer
    {
        public Matrix ModelMatrix;
        public Matrix ViewMatrix;
        public Matrix ProjectionMatrix;
    }
}
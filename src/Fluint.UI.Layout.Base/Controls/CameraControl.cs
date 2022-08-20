// 
// CameraControl.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Graphics;
using Fluint.Layer.Graphics.API;
using Fluint.Layer.Input;
using Fluint.Layer.Localization;
using Fluint.Layer.Mathematics;
using Fluint.Layer.UI;
using Fluint.Layer.Windowing;
using IContainer = Fluint.Layer.UI.IContainer;

namespace Fluint.UI.Layout.Base.Controls;

public class CameraControl : Control
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct InputBuffer 
    {
        public Matrix ModelMatrix;
        public Matrix ViewMatrix;
        public Matrix ProjectionMatrix;
    }

    private readonly ICommandList _commandList;
    private readonly ISwapChain _swapChain;
    private readonly IPipeline _pipeline;
    private readonly IVertexBuffer _buffer;
    private readonly IViewport _viewport;
    private readonly IContainer _container;
    private readonly ICamera _camera;
    private readonly IConstantBuffer _inputBuffer;
    private readonly ITextLabel _label;
    private readonly IMouseCapture _mouseCapture;
    private IInputManager _inputManager;
    private IWindow _window;

    public CameraControl(ModulePacket packet)
    {
        var verticesColored = new List<PositionColorVertex>
        {
            new (new Vector3(-1f,  1f, 0.0f), new Vector4(  1.0f, 0.0f, 0.0f, 1.0f)),
            new (new Vector3( 1f,  1f, 0.0f), new Vector4( 0.0f, 1.0f, 0.0f, 1.0f)),
            new (new Vector3(-1f, -1f, 0.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f)),
            
            new (new Vector3(-1f, -1f, 0.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f)),
            new (new Vector3(1f,  1f, 0.0f), new Vector4( 0.0f, 1.0f, 0.0f, 1.0f)),
            new (new Vector3(1f, -1f, 0.0f), new Vector4(1.0f, 1.0f, 1.0f, 1.0f)),
        };
        
        var localizationManager = packet.GetSingleton<ILocalizationManager>();
        var graphicsFactory = packet.CreateScoped<IGraphicsFactory>();

        _mouseCapture = packet.CreateScoped<IMouseCapture>();
        
        _camera = packet.CreateScoped<ICamera>();
        _camera.Fov = 90;
        _camera.AspectRatio = 1;
        _camera.ProjectionMode = ProjectionMode.Prespective;
        _camera.Position = new Vector3(1.5f, -1, 0.5f);;

        var inputBuffer = new InputBuffer() { ModelMatrix = Matrix.Scaling(10f), ViewMatrix = _camera.GetViewMatrix(), ProjectionMatrix = _camera.GetProjectionMatrix() };
        _inputBuffer = graphicsFactory.CreateConstantBuffer(inputBuffer);
            
        _container = packet.CreateScoped<IContainer>();
        _swapChain = graphicsFactory.CreateSwapchain(new SwapChainDescriptor(750, 750, false, false, SwapEffect.Discard));

        var vertexShader = graphicsFactory.CreateShaderFromFile(ShaderStage.Vertex, "./base/shaders/vertex", VertexType.PositionColor,  Enumerable.Empty<(string, string)>());
        var pixelShader = graphicsFactory.CreateShaderFromFile(ShaderStage.Pixel, "./base/shaders/fragment", VertexType.PositionColor,  Enumerable.Empty<(string, string)>());
        
        _pipeline = graphicsFactory.CreatePipeline(vertexShader, pixelShader, vertexShader.InputLayout, null, null, null, new Viewport(0, 0, 750, 750), PrimitiveTopology.TriangleList);
        _buffer = graphicsFactory.CreateVertexBuffer(verticesColored.ToArray());
        
        _commandList = graphicsFactory.CreateCommandList();
        _viewport = packet.CreateScoped<IViewport>();
        _viewport.SwapChain = _swapChain;
        
        _container.Title = localizationManager.Fetch("camera");

        _label = packet.CreateScoped<ITextLabel>();
        
        _container.Add("label", _label);
        _container.Add("Viewport", _viewport);
        Children.Add(_container);
    }

    private void InitializeCommandList()
    {
        _commandList.Clear();
        
        _commandList.ClearRenderTarget(_swapChain.TextureView, new Color4(0, 0, 0, 0));
        _commandList.ClearDepthStencil(_swapChain.DepthStencilView, 1.0f, 1);
                    
        _commandList.Begin("Initial", _pipeline);
        _commandList.SetVertexBuffer(_buffer);
        _commandList.SetConstantBuffer(_inputBuffer, BufferScope.VertexShader);
        _commandList.Draw(6);
        _commandList.End();
    }
    
    public override void Begin(string name, IWindow window)
    {
        _window = window;
        _inputManager = window.InputManager;
        _mouseCapture.Begin(window);
        
        InitializeCommandList();
        base.Begin(name, window);
    }

    public override void Tick()
    {
        _mouseCapture.Update();
        if (_container.IsFocused)
        {
            var camBoost = 0.1f;
            if (_inputManager.IsMouseButtonPressed(MouseButton.Button1))
            {
                _mouseCapture.Capture();
                _camera.Yaw =  _mouseCapture.X * camBoost;
                _camera.Pitch =  _mouseCapture.Y * camBoost;
            }

            float boost = _inputManager.IsKeyPressed(Key.LeftShift) ? 20f : 10f;
            var movementVec = Vector3.Zero;
        
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
            
            _inputBuffer.UpdateBuffer(new InputBuffer() { ModelMatrix =  Matrix.Scaling(10f), ViewMatrix = _camera.GetViewMatrix(), ProjectionMatrix = _camera.GetProjectionMatrix() });
        }

        _swapChain.Present();
        {
            _commandList.Submit();
        }
        _swapChain.Disconnect();

        _label.Text = $"Camera Position: ({_camera.Position.ToString()}), Camera Pitch: ({_camera.Pitch}), Camera Yaw: ({_camera.Yaw})";
        
        base.Tick();

        if (_viewport.Size != _container.Size)
        {
            _viewport.Size = _container.Size;
            _pipeline.Viewport = new Viewport(0, 0, _container.Size.X, _container.Size.Y, 0.01f, 100000f);
            _camera.AspectRatio = (float)_viewport.Size.X / _viewport.Size.Y;
            InitializeCommandList();
        }
    }
}

//
// MainWindow.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Engine;
using Fluint.Layer.Graphics;
using Fluint.Layer.Input;
using Fluint.Layer.Mathematics;
using Fluint.Layer.UI;
using Fluint.Layer.Windowing;
using ImGuiNET;

namespace Fluint.Implementation.UI
{
    public class MainWindow : IWindow
    {
        private readonly ModulePacket _packet;
        public MainWindow(ModulePacket packet)
        {
            _packet = packet;
            Controls = new List<IGuiComponent>();
            _ghosts = new List<IGhost>();
        }

        private readonly List<IGhost> _ghosts;

        public string Title { get => _provider.WindowTitle; set => _provider.WindowTitle = value; }
        public bool VSync { get => _provider.WindowVSync; set => _provider.WindowVSync = value; }
        public Vector2i Size { get => _provider.WindowSize; set => _provider.WindowSize = value; }
        public Vector2i Location { get => _provider.WindowLocation; set => _provider.WindowLocation = value; }
        public IInputManager InputManager { get; private set; }
        public ICollection<IGuiComponent> Controls { get; }

        private IWindowProvider _provider;

        private float _time = 0;
        private ITextureView _textureView;
        private ICanvas _canvas;

        private ITextureView _cameraView;
        public void OnLoad()
        {
            foreach (var ghost in _ghosts)
            {
                ghost.OnLoad();
            }

            _textureView = _packet.CreateScoped<ITextureView>();
            _cameraView = _packet.CreateScoped<ITextureView>();

            _canvas = _packet.CreateScoped<ICanvas>();
           
            _canvas.InitializeCanvas(512, 512);

            _textureView.Begin("Canvas View");
            _cameraView.Begin("Camera View");

            _textureView.Texture = _canvas.CreateBoundTexture();
            
            Controls.Add(_textureView);
            Controls.Add(_cameraView);
            //TODO: Window stuff!
        }

        public void OnStart()
        {
            foreach (var ghost in _ghosts)
            {
                ghost.OnStart();
            }

            //TODO: Window stuff!
        }

        public void OnRender(double delay)
        {
            foreach (var ghost in _ghosts)
            {
                ghost.OnRender(delay);
            }

            //TODO: Window stuff!
        }

        public void OnUpdate(double delay)
        {
            _time += (float)delay;
            foreach (var ghost in _ghosts)
            {
                ghost.OnUpdate(delay);
            }

            foreach (var control in Controls)
            {
                control.Tick();
            }
            
            _canvas.Clear();

            _canvas.DrawCircle(new Vector2i(256 + (int)(24f * Math.Cos(_time)), 256), (int)Math.Abs(128f * Math.Sin(_time)), Color.Red);

            _textureView.Texture.Bind();
            _textureView.Texture.Upload();
            _textureView.Texture.Unbind();

            //TODO: Window stuff!
        }

        public void OnTextReceived(int unicode, string data)
        {
            foreach (var ghost in _ghosts)
            {
                ghost.OnTextReceived(unicode, data);
            }

            //TODO: Window stuff!
        }

        public void OnResize(int width, int height)
        {
            foreach (var ghost in _ghosts)
            {
                ghost.OnResize(width, height);
            }

            //TODO: Window stuff!
        }
        public void OnMouseWheelMoved(Vector2 offset)
        {
            foreach (var ghost in _ghosts)
            {
                ghost.OnMouseWheelMoved(offset);
            }

            //TODO: Window stuff!
        }

        public void SetProvider(in IWindowProvider provider)
        {
            _provider = provider;

            InputManager = _packet.CreateScoped<IInputManager>();
            InputManager.Load(provider);
        }
        public void Enqueue(Action action)
        {
            _provider.FrameQueue.Enqueue(action);
        }

        public void AdoptGhost<Ghost>() where Ghost : IGhost
        {
            var ghost = (IGhost)_packet.FetchAndCreateInstance(typeof(Ghost));
            ghost.SetPossessed(this);
            _ghosts.Add(ghost);
        }
    }
}

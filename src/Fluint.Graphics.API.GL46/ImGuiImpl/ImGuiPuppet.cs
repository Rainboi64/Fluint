//
// ImGuiPuppet.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Fluint.Layer.Configuration;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Input;
using Fluint.Layer.UI;
using Fluint.Layer.Windowing;
using ImGuiNET;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using MouseButton = Fluint.Layer.Input.MouseButton;
using Vector2 = System.Numerics.Vector2;

namespace Fluint.Graphics.API.GL46.ImGuiImpl
{
    public class ImGuiPuppet : IPuppet
    {
        private readonly IConfigurationManager _configurationManager;

        private readonly Array _keys = Enum.GetValues(typeof(Keys));
        private readonly ILogger _logger;

        private readonly List<char> _pressedChars = new();

        private ImGuiTexture _fontTexture;
        private bool _frameBegun;
        private int _indexBuffer;
        private int _indexBufferSize;

        private IWindow _possessedWindow;


        private Vector2 _scaleFactor = Vector2.One;
        private ImGuiShader _shader;

        private int _vertexArray;
        private int _vertexBuffer;
        private int _vertexBufferSize;
        private int _windowHeight;

        private int _windowWidth;

        public ImGuiPuppet(ILogger logger, IConfigurationManager configurationManager)
        {
            _logger = logger;
            _configurationManager = configurationManager;
        }

        public void SetPossessed(in IWindow possessor)
        {
            _possessedWindow = possessor;
        }

        public void OnLoad()
        {
            var config = _configurationManager.Get<UIConfiguration>();

            _windowWidth = _possessedWindow.Size.X;
            _windowHeight = _possessedWindow.Size.Y;

            var context = ImGui.CreateContext();

            ImGui.SetCurrentContext(context);


            var io = ImGui.GetIO();

            io.BackendFlags |= ImGuiBackendFlags.RendererHasVtxOffset;
            io.ConfigFlags |= ImGuiConfigFlags.DockingEnable;
            io.ConfigWindowsMoveFromTitleBarOnly = true;

            unsafe
            {
                var builder =
                    new ImFontGlyphRangesBuilderPtr(ImGuiNative.ImFontGlyphRangesBuilder_ImFontGlyphRangesBuilder());
                builder.AddRanges(io.Fonts.GetGlyphRangesDefault());

                builder.AddText(
                    "Α α Β β Γ γ Δ δ Ε ε Ζ ζ Η η Θ θ Ι ι Κ κ Λ λ Μ μ Ν ν Ξ ξ Ο ο Π π Ρ ρ Σ σ ς Τ τ Υ υ Φ φ Χ χ Ψ ψ Ω ω");

                builder.BuildRanges(out var ranges);

                foreach (var font in config.Fonts)
                {
                    if (!File.Exists(font.FontPath))
                    {
                        _logger.Error("[{0}] Configuration Error font file doesn't exist ({1}, {2})", "ImGuiPuppet",
                            font.FontPath, font.FontSize);
                    }

                    io.Fonts.AddFontFromFileTTF(font.FontPath, font.FontSize, null, ranges.Data);
                }

                io.Fonts.Build();
            }

            CreateDeviceResources();
            SetKeyMappings();

            SetPerFrameImGuiData(1f / 60f);

            ImGui.NewFrame();
            _frameBegun = true;
        }

        public void OnResize(int width, int height)
        {
            _windowWidth = width;
            _windowHeight = height;
        }

        /// <summary>
        /// Renders the ImGui draw list data.
        /// This method requires a <see cref="GraphicsDevice"/> because it may create new DeviceBuffers if the size of vertex
        /// or index data has increased beyond the capacity of the existing buffers.
        /// A <see cref="GL46CommandList"/> is needed to submit drawing and resource update commands.
        /// </summary>
        public void OnRender(double delay)
        {
            if (_frameBegun)
            {
                _frameBegun = false;
                ImGui.Render();
                RenderImDrawData(ImGui.GetDrawData());
            }
        }

        /// <summary>
        /// Updates ImGui input and IO configuration state.
        /// </summary>
        public void OnUpdate(double delay)
        {
            if (_frameBegun)
            {
                ImGui.Render();
            }

            SetPerFrameImGuiData((float)delay);
            UpdateImGuiInput(_possessedWindow.InputManager);

            _frameBegun = true;

            ImGui.NewFrame();
        }

        public void OnMouseWheelMoved(Layer.Mathematics.Vector2 offset)
        {
            var io = ImGui.GetIO();

            io.MouseWheelH = offset.X;
            io.MouseWheel = offset.Y;
        }

        public void OnTextReceived(int unicode, string data)
        {
            _pressedChars.Add((char)unicode);
        }

        public void DestroyDeviceObjects()
        {
            Dispose();
        }

        public void CreateDeviceResources()
        {
            Util.CreateVertexArray("ImGui", out _vertexArray);

            _vertexBufferSize = 10000;
            _indexBufferSize = 2000;

            Util.CreateVertexBuffer("ImGui", out _vertexBuffer);
            Util.CreateElementBuffer("ImGui", out _indexBuffer);
            GL.NamedBufferData(_vertexBuffer, _vertexBufferSize, IntPtr.Zero, BufferUsageHint.DynamicDraw);
            GL.NamedBufferData(_indexBuffer, _indexBufferSize, IntPtr.Zero, BufferUsageHint.DynamicDraw);

            RecreateFontDeviceTexture();

            var VertexSource = @"#version 450 core

uniform mat4 projection_matrix;

layout(location = 0) in vec2 in_position;
layout(location = 1) in vec2 in_texCoord;
layout(location = 2) in vec4 in_color;

out vec4 color;
out vec2 texCoord;

void main()
{
    gl_Position = projection_matrix * vec4(in_position, 0, 1);
    color = in_color;
    texCoord = in_texCoord;
}";
            var FragmentSource = @"#version 450 core

uniform sampler2D in_fontTexture;

in vec4 color;
in vec2 texCoord;

out vec4 outputColor;

void main()
{
    outputColor = color * texture(in_fontTexture, texCoord);
}";
            _shader = new ImGuiShader("ImGui", VertexSource, FragmentSource);

            GL.VertexArrayVertexBuffer(_vertexArray, 0, _vertexBuffer, IntPtr.Zero, Unsafe.SizeOf<ImDrawVert>());
            GL.VertexArrayElementBuffer(_vertexArray, _indexBuffer);

            GL.EnableVertexArrayAttrib(_vertexArray, 0);
            GL.VertexArrayAttribBinding(_vertexArray, 0, 0);
            GL.VertexArrayAttribFormat(_vertexArray, 0, 2, VertexAttribType.Float, false, 0);

            GL.EnableVertexArrayAttrib(_vertexArray, 1);
            GL.VertexArrayAttribBinding(_vertexArray, 1, 0);
            GL.VertexArrayAttribFormat(_vertexArray, 1, 2, VertexAttribType.Float, false, 8);

            GL.EnableVertexArrayAttrib(_vertexArray, 2);
            GL.VertexArrayAttribBinding(_vertexArray, 2, 0);
            GL.VertexArrayAttribFormat(_vertexArray, 2, 4, VertexAttribType.UnsignedByte, true, 16);
        }

        public void RecreateFontDeviceTexture()
        {
            var io = ImGui.GetIO();

            io.Fonts.GetTexDataAsRGBA32(out IntPtr pixels, out var width, out var height, out var bytesPerPixel);
            _fontTexture = new ImGuiTexture("ImGui Text Atlas", width, height, pixels);
            _fontTexture.SetMagFilter(TextureMagFilter.Linear);
            _fontTexture.SetMinFilter(TextureMinFilter.Linear);

            io.Fonts.SetTexID((IntPtr)_fontTexture.GlTexture);

            io.Fonts.ClearTexData();
        }

        /// <summary>
        /// Sets per-frame data based on the associated window.
        /// This is called by Render(float).
        /// </summary>
        private void SetPerFrameImGuiData(float deltaSeconds)
        {
            var io = ImGui.GetIO();

            io.DisplaySize = new Vector2(
                _windowWidth / _scaleFactor.X,
                _windowHeight / _scaleFactor.Y);

            io.DisplayFramebufferScale = _scaleFactor;
            io.DeltaTime = deltaSeconds; // DeltaTime is in seconds.
        }

        private void UpdateImGuiInput(IInputManager inputManager)
        {
            var io = ImGui.GetIO();

            io.MouseDown[0] = inputManager.IsMouseButtonPressed(MouseButton.Left);
            io.MouseDown[1] = inputManager.IsMouseButtonPressed(MouseButton.Right);
            io.MouseDown[2] = inputManager.IsMouseButtonPressed(MouseButton.Middle);

            io.MousePos = new Vector2(inputManager.MouseLocation.X, inputManager.MouseLocation.Y);

            for (var i = 0; i < _keys.Length; i++)
            {
                if ((Keys)i == Keys.Unknown)
                {
                    continue;
                }

                io.KeysDown[i] = inputManager.IsKeyPressed((Key)i);
            }

            foreach (var c in _pressedChars)
            {
                io.AddInputCharacter(c);
            }

            _pressedChars.Clear();

            io.KeyCtrl = inputManager.IsKeyPressed(Key.LeftControl) || inputManager.IsKeyPressed(Key.RightControl);
            io.KeyAlt = inputManager.IsKeyPressed(Key.LeftAlt) || inputManager.IsKeyPressed(Key.RightAlt);
            io.KeyShift = inputManager.IsKeyPressed(Key.LeftShift) || inputManager.IsKeyPressed(Key.RightShift);
            io.KeySuper = inputManager.IsKeyPressed(Key.LeftSuper) || inputManager.IsKeyPressed(Key.RightSuper);
        }

        private static void SetKeyMappings()
        {
            var io = ImGui.GetIO();
            io.KeyMap[(int)ImGuiKey.Tab] = (int)Keys.Tab;
            io.KeyMap[(int)ImGuiKey.LeftArrow] = (int)Keys.Left;
            io.KeyMap[(int)ImGuiKey.RightArrow] = (int)Keys.Right;
            io.KeyMap[(int)ImGuiKey.UpArrow] = (int)Keys.Up;
            io.KeyMap[(int)ImGuiKey.DownArrow] = (int)Keys.Down;
            io.KeyMap[(int)ImGuiKey.PageUp] = (int)Keys.PageUp;
            io.KeyMap[(int)ImGuiKey.PageDown] = (int)Keys.PageDown;
            io.KeyMap[(int)ImGuiKey.Home] = (int)Keys.Home;
            io.KeyMap[(int)ImGuiKey.End] = (int)Keys.End;
            io.KeyMap[(int)ImGuiKey.Delete] = (int)Keys.Delete;
            io.KeyMap[(int)ImGuiKey.Backspace] = (int)Keys.Backspace;
            io.KeyMap[(int)ImGuiKey.Enter] = (int)Keys.Enter;
            io.KeyMap[(int)ImGuiKey.Escape] = (int)Keys.Escape;
            io.KeyMap[(int)ImGuiKey.A] = (int)Keys.A;
            io.KeyMap[(int)ImGuiKey.C] = (int)Keys.C;
            io.KeyMap[(int)ImGuiKey.V] = (int)Keys.V;
            io.KeyMap[(int)ImGuiKey.X] = (int)Keys.X;
            io.KeyMap[(int)ImGuiKey.Y] = (int)Keys.Y;
            io.KeyMap[(int)ImGuiKey.Z] = (int)Keys.Z;
        }

        private void RenderImDrawData(ImDrawDataPtr draw_data)
        {
            if (draw_data.CmdListsCount == 0)
            {
                return;
            }

            for (var i = 0; i < draw_data.CmdListsCount; i++)
            {
                var cmd_list = draw_data.CmdListsRange[i];

                var vertexSize = cmd_list.VtxBuffer.Size * Unsafe.SizeOf<ImDrawVert>();
                if (vertexSize > _vertexBufferSize)
                {
                    var newSize = (int)Math.Max(_vertexBufferSize * 1.5f, vertexSize);
                    GL.NamedBufferData(_vertexBuffer, newSize, IntPtr.Zero, BufferUsageHint.DynamicDraw);
                    _vertexBufferSize = newSize;

                    _logger.Verbose("[{0}] Resized dear imgui vertex buffer to new size {1}", "ImGuiPuppet",
                        _vertexBufferSize);
                }

                var indexSize = cmd_list.IdxBuffer.Size * sizeof(ushort);
                if (indexSize > _indexBufferSize)
                {
                    var newSize = (int)Math.Max(_indexBufferSize * 1.5f, indexSize);
                    GL.NamedBufferData(_indexBuffer, newSize, IntPtr.Zero, BufferUsageHint.DynamicDraw);
                    _indexBufferSize = newSize;

                    _logger.Verbose("[{0}] Resized dear imgui index buffer to new size {1}", "ImGuiPuppet",
                        _indexBufferSize);
                }
            }

            // Setup orthographic projection matrix into our constant buffer
            var io = ImGui.GetIO();
            var mvp = Matrix4.CreateOrthographicOffCenter(
                0.0f,
                io.DisplaySize.X,
                io.DisplaySize.Y,
                0.0f,
                -1.0f,
                1.0f);

            _shader.UseShader();
            GL.UniformMatrix4(_shader.GetUniformLocation("projection_matrix"), false, ref mvp);
            GL.Uniform1(_shader.GetUniformLocation("in_fontTexture"), 0);

            GL.BindVertexArray(_vertexArray);

            draw_data.ScaleClipRects(io.DisplayFramebufferScale);

            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.ScissorTest);
            GL.BlendEquation(BlendEquationMode.FuncAdd);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Disable(EnableCap.CullFace);
            GL.Disable(EnableCap.DepthTest);

            // Render command lists
            for (var n = 0; n < draw_data.CmdListsCount; n++)
            {
                var cmd_list = draw_data.CmdListsRange[n];

                GL.NamedBufferSubData(_vertexBuffer, IntPtr.Zero, cmd_list.VtxBuffer.Size * Unsafe.SizeOf<ImDrawVert>(),
                    cmd_list.VtxBuffer.Data);
                GL.NamedBufferSubData(_indexBuffer, IntPtr.Zero, cmd_list.IdxBuffer.Size * sizeof(ushort),
                    cmd_list.IdxBuffer.Data);

                var vtx_offset = 0;
                var idx_offset = 0;

                for (var cmd_i = 0; cmd_i < cmd_list.CmdBuffer.Size; cmd_i++)
                {
                    var pcmd = cmd_list.CmdBuffer[cmd_i];
                    if (pcmd.UserCallback != IntPtr.Zero)
                    {
                        throw new NotImplementedException();
                    }
                    else
                    {
                        GL.ActiveTexture(TextureUnit.Texture0);
                        GL.BindTexture(TextureTarget.Texture2D, (int)pcmd.TextureId);

                        // We do _windowHeight - (int)clip.W instead of (int)clip.Y because gl has flipped Y when it comes to these coordinates
                        var clip = pcmd.ClipRect;
                        GL.Scissor((int)clip.X, _windowHeight - (int)clip.W, (int)(clip.Z - clip.X),
                            (int)(clip.W - clip.Y));

                        if ((io.BackendFlags & ImGuiBackendFlags.RendererHasVtxOffset) != 0)
                        {
                            GL.DrawElementsBaseVertex(PrimitiveType.Triangles, (int)pcmd.ElemCount,
                                DrawElementsType.UnsignedShort, (IntPtr)(idx_offset * sizeof(ushort)), vtx_offset);
                        }
                        else
                        {
                            GL.DrawElements(BeginMode.Triangles, (int)pcmd.ElemCount, DrawElementsType.UnsignedShort,
                                (int)pcmd.IdxOffset * sizeof(ushort));
                        }
                    }

                    idx_offset += (int)pcmd.ElemCount;
                }

                vtx_offset += cmd_list.VtxBuffer.Size;
            }

            GL.BindVertexArray(0);
            GL.UseProgram(0);

            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.ScissorTest);
        }

        /// <summary>
        /// Frees all graphics resources used by the renderer.
        /// </summary>
        public void Dispose()
        {
            _fontTexture.Dispose();
            _shader.Dispose();
        }
    }
}
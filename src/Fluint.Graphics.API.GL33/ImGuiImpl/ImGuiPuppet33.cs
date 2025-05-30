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

namespace Fluint.Graphics.API.GL33.ImGuiImpl;

public class ImGuiPuppet33 : IPuppet
{
    private readonly IConfigurationManager _configurationManager;

    private readonly Array _keys = Enum.GetValues(typeof(Keys));
    private readonly ILogger _logger;

    private readonly List<char> _pressedChars = new();


    private  Vector2 _scaleFactor = Vector2.One;

    private ImGuiTexture33 _fontTexture33;
    private bool _frameBegun;
    private int _indexBuffer;
    private int _indexBufferSize;

    private IWindow _possessedWindow;
    private ImGuiShader33 _shader33;

    private int _vertexArray;
    private int _vertexBuffer;
    private int _vertexBufferSize;
    private int _windowHeight;

    private int _windowWidth;

    public ImGuiPuppet33(ILogger logger, IConfigurationManager configurationManager)
    {
        _logger = logger;
        _configurationManager = configurationManager;
    }

    public void SetPossessed(IWindow possessor)
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
                    _logger.Warning("[{0}] Configuration Error font file doesn't exist ({1}, {2})", "ImGuiPuppet33",
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
    ///     Renders the ImGui draw list data.
    ///     This method requires a <see cref="GraphicsDevice" /> because it may create new DeviceBuffers if the size of vertex
    ///     or index data has increased beyond the capacity of the existing buffers.
    ///     A <see cref="GL46CommandList" /> is needed to submit drawing and resource update commands.
    /// </summary>
    public void PreRender(double delay)
    {
        if (!_frameBegun)
        {
            return;
        }

        _frameBegun = false;
        ImGui.Render();
        RenderImDrawData(ImGui.GetDrawData());
    }

    /// <summary>
    ///     Updates ImGui input and IO configuration state.
    /// </summary>
    public void PreUpdate(double delay)
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

    public void RecreateFontDeviceTexture()
    {
        var io = ImGui.GetIO();

        io.Fonts.GetTexDataAsRGBA32(out IntPtr pixels, out var width, out var height, out var bytesPerPixel);
        _fontTexture33 = new ImGuiTexture33("ImGui Text Atlas", width, height, pixels);
        _fontTexture33.SetMagFilter(TextureMagFilter.Linear);
        _fontTexture33.SetMinFilter(TextureMinFilter.Linear);

        io.Fonts.SetTexID((IntPtr)_fontTexture33.GlTexture);

        io.Fonts.ClearTexData();
    }

    /// <summary>
    ///     Sets per-frame data based on the associated window.
    ///     This is called by Render(float).
    /// </summary>
    private void SetPerFrameImGuiData(float deltaSeconds)
    {
        var io = ImGui.GetIO();

        _scaleFactor = new Vector2(_possessedWindow.ScaleRate.X, _possessedWindow.ScaleRate.Y);
        
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

    public void CreateDeviceResources()
    {
        Util.CreateVertexArray("ImGui", out _vertexArray);

        _vertexBufferSize = 10000;
        _indexBufferSize = 2000;

        Util.CreateVertexBuffer("ImGui", out _vertexBuffer);
        Util.CreateElementBuffer("ImGui", out _indexBuffer);
    
        RecreateFontDeviceTexture();

        var VertexSource = @"#version 330 core

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
        var FragmentSource = @"#version 330 core

uniform sampler2D in_fontTexture;

in vec4 color;
in vec2 texCoord;

out vec4 outputColor;

void main()
{
    outputColor = color * texture(in_fontTexture, texCoord);
}";
        _shader33 = new ImGuiShader33("ImGui", VertexSource, FragmentSource);

        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBuffer);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _indexBuffer);

        GL.EnableVertexAttribArray(0);
        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, Unsafe.SizeOf<ImDrawVert>(), 0);

        GL.EnableVertexAttribArray(1);
        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, Unsafe.SizeOf<ImDrawVert>(), 8);

        GL.EnableVertexAttribArray(2);
        GL.VertexAttribPointer(2, 4, VertexAttribPointerType.UnsignedByte, true, Unsafe.SizeOf<ImDrawVert>(), 16);
    }

    private void RenderImDrawData(ImDrawDataPtr drawData)
    {
    if (drawData.CmdListsCount == 0)
    {
        return;
    }

    for (var i = 0; i < drawData.CmdListsCount; i++)
    {
        var cmdList = drawData.CmdListsRange[i];

        var vertexSize = cmdList.VtxBuffer.Size * Unsafe.SizeOf<ImDrawVert>();
        if (vertexSize > _vertexBufferSize)
        {
            var newSize = (int)Math.Max(_vertexBufferSize * 1.5f, vertexSize);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, newSize, IntPtr.Zero, BufferUsageHint.DynamicDraw);
            _vertexBufferSize = newSize;

            _logger.Verbose("[{0}] Resized dear imgui vertex buffer to new size {1}", "ImGuiPuppet",
                _vertexBufferSize);
        }

        var indexSize = cmdList.IdxBuffer.Size * sizeof(ushort);
        if (indexSize > _indexBufferSize)
        {
            var newSize = (int)Math.Max(_indexBufferSize * 1.5f, indexSize);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _indexBuffer);
            GL.BufferData(BufferTarget.ElementArrayBuffer, newSize, IntPtr.Zero, BufferUsageHint.DynamicDraw);
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

    _shader33.UseShader();
    GL.UniformMatrix4(_shader33.GetUniformLocation("projection_matrix"), false, ref mvp);
    GL.Uniform1(_shader33.GetUniformLocation("in_fontTexture"), 0);

    GL.BindVertexArray(_vertexArray);

    drawData.ScaleClipRects(io.DisplayFramebufferScale);

    GL.Enable(EnableCap.Blend);
    GL.Enable(EnableCap.ScissorTest);
    GL.BlendEquation(BlendEquationMode.FuncAdd);
    GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
    GL.Disable(EnableCap.CullFace);
    GL.Disable(EnableCap.DepthTest);

    // Render command lists
    for (var n = 0; n < drawData.CmdListsCount; n++)
    {
        var cmdList = drawData.CmdListsRange[n];

        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBuffer);
        GL.BufferData(BufferTarget.ArrayBuffer, cmdList.VtxBuffer.Size * Unsafe.SizeOf<ImDrawVert>(),
            cmdList.VtxBuffer.Data, BufferUsageHint.DynamicDraw);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _indexBuffer);
        GL.BufferData(BufferTarget.ElementArrayBuffer, cmdList.IdxBuffer.Size * sizeof(ushort),
            cmdList.IdxBuffer.Data, BufferUsageHint.DynamicDraw);

        var vtx_offset = 0;
        var idx_offset = 0;

        for (var cmd_i = 0; cmd_i < cmdList.CmdBuffer.Size; cmd_i++)
        {
            var pcmd = cmdList.CmdBuffer[cmd_i];
            if (pcmd.UserCallback != IntPtr.Zero)
            {
                throw new NotImplementedException();
            }

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

            idx_offset += (int)pcmd.ElemCount;
        }

        vtx_offset += cmdList.VtxBuffer.Size;
    }

    GL.BindVertexArray(0);
    GL.UseProgram(0);

    GL.Disable(EnableCap.Blend);
    GL.Disable(EnableCap.ScissorTest);
}

    
    /// <summary>
    ///     Frees all graphics resources used by the renderer.
    /// </summary>
    public void Dispose()
    {
        _fontTexture33.Dispose();
        _shader33.Dispose();
    }
}
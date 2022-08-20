// 
// G46InputLayout.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System.Collections.Generic;
using Fluint.Graphics.API.GLCommon;
using Fluint.Layer.Graphics.API;
using OpenTK.Graphics.OpenGL4;

namespace Fluint.Graphics.API.GL46;

public class GL46InputLayout : IInputLayout
{
    private readonly IEnumerable<GLVertexAttribute> _attributes;
    private readonly int _handle;

    public GL46InputLayout(IEnumerable<GLVertexAttribute> attributes)
    {
        _attributes = attributes;

        GL.CreateVertexArrays(1, out _handle);
        foreach (var attribute in _attributes)
        {
            GL.EnableVertexArrayAttrib(_handle, attribute.Index);
            GL.VertexArrayAttribFormat(_handle, attribute.Index, attribute.Components, attribute.Type, false,
                attribute.Offset);
            GL.VertexArrayAttribBinding(_handle, attribute.Index, 0);
        }
    }

    public void Dispose()
    {
        GL.DeleteVertexArray(_handle);
    }

    public static implicit operator int(GL46InputLayout inputLayout)
    {
        return inputLayout._handle;
    }
}
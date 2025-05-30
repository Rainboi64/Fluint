// 
// G46InputLayout.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using System.Collections.Generic;
using System.Linq;
using Fluint.Graphics.API.GLCommon;
using Fluint.Layer.Graphics.API;
using OpenTK.Graphics.OpenGL4;

namespace Fluint.Graphics.API.GL33;

public class GL33InputLayout : IInputLayout
{
    private readonly int _handle;
    private readonly List<GLVertexAttribute> _glVertexAttributes;

    public GL33InputLayout(IEnumerable<GLVertexAttribute> attributes)
    {
        GL.GenVertexArrays(1, out _handle);

        _glVertexAttributes = attributes.ToList();
    }

    public void Enable(int stride)
    {
        GL.BindVertexArray(_handle);
        foreach (var attribute in _glVertexAttributes)
        {
            GL.EnableVertexAttribArray(attribute.Index);
            GL.VertexAttribPointer(attribute.Index, attribute.Components, (VertexAttribPointerType)attribute.Type, false, stride, attribute.Offset);
            var x = GL.GetError();

            if (x != ErrorCode.NoError)
            {
                throw new Exception();
            }
        }
    }

    public void Dispose()
    {
        GL.DeleteVertexArray(_handle);
    }

    public static implicit operator int(GL33InputLayout inputLayout)
    {
        return inputLayout._handle;
    }
}
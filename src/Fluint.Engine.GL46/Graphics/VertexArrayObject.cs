//
// VertexArrayObject.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Fluint.Layer.Graphics;
using Fluint.Layer.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace Fluint.Engine.GL46.Graphics
{
    public class VertexArrayObject<TVertexType> : IVertexLayout<TVertexType> where TVertexType : struct
    {
        private List<VertexLayoutAttribute> _attributes;

        public int Handle
        {
            get;
            private set;
        }

        public int VertexSize
        {
            get;
            private set;
        }

        public void Load()
        {
            Bind();
            GL.EnableVertexAttribArray(0);
        }

        public void Enable()
        {
            Bind();
            for (int i = 0; i < _attributes.Count; i++)
            {
                var attribute = _attributes[i];
                var type = GetVertexPointerType(attribute.AttributeType);
                var offset = Marshal.OffsetOf<TVertexType>(attribute.Name);

                GL.VertexAttribPointer(i, attribute.ComponentsCount, type,
                    false, VertexSize, offset);
            }

            GL.EnableVertexAttribArray(0);
        }

        public void Disable()
        {
            Bind();
            for (int i = 0; i < _attributes.Count; i++)
            {
                GL.DisableVertexAttribArray(i);
            }

            Unbind();
        }

        public void Calculate()
        {
            //Handle = GL.GenVertexArray();
            _attributes = new List<VertexLayoutAttribute>();
            Handle = GL.GenVertexArray();

            var type = typeof(TVertexType);
            var properties = type.GetFields();

            foreach (var property in properties)
            {
                //TODO: Finish these..
                var @switch = new Dictionary<Type, Action> {
                    { typeof(int), () => Add(property.Name, VertexLayoutAttributeType.Int, 1) },
                    { typeof(uint), () => Add(property.Name, VertexLayoutAttributeType.UnsignedInt, 1) },
                    { typeof(short), () => Add(property.Name, VertexLayoutAttributeType.Short, 1) },
                    { typeof(ushort), () => Add(property.Name, VertexLayoutAttributeType.UnsignedShort, 1) },
                    { typeof(float), () => Add(property.Name, VertexLayoutAttributeType.Float, 1) },
                    { typeof(double), () => Add(property.Name, VertexLayoutAttributeType.Double, 1) },
                    { typeof(Vector2), () => Add(property.Name, VertexLayoutAttributeType.Float, 2) },
                    { typeof(Vector3), () => Add(property.Name, VertexLayoutAttributeType.Float, 3) },
                    { typeof(Vector4), () => Add(property.Name, VertexLayoutAttributeType.Float, 4) },
                };
                @switch[property.FieldType]();
            }

            VertexSize = Marshal.SizeOf(type);
        }

        public void Dispose()
        {
            GL.DeleteVertexArray(Handle);
            GC.SuppressFinalize(this);
        }

        public void Bind()
        {
            GL.BindVertexArray(Handle);
        }

        public void Unbind()
        {
            GL.BindVertexArray(0);
        }

        private void Add(string name, VertexLayoutAttributeType type, int components)
        {
            _attributes.Add(new VertexLayoutAttribute(name, type, components));
        }

        private static VertexAttribPointerType GetVertexPointerType(VertexLayoutAttributeType type)
        {
            return type switch {
                VertexLayoutAttributeType.Byte => VertexAttribPointerType.Byte,
                VertexLayoutAttributeType.Int => VertexAttribPointerType.Int,
                VertexLayoutAttributeType.Short => VertexAttribPointerType.Short,
                VertexLayoutAttributeType.Float => VertexAttribPointerType.Float,
                VertexLayoutAttributeType.Double => VertexAttribPointerType.Double,

                VertexLayoutAttributeType.UnsignedByte => VertexAttribPointerType.UnsignedByte,
                VertexLayoutAttributeType.UnsignedInt => VertexAttribPointerType.UnsignedInt,
                VertexLayoutAttributeType.UnsignedShort => VertexAttribPointerType.UnsignedShort,

                _ => throw new NotImplementedException($"Unsupported AttributeType: {type}."),
            };
        }
    }
}
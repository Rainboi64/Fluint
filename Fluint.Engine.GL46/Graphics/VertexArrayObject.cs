// 
// VertexArrayObject.cs
//
// Copyright (C) 2020 Yaman Alhalabi
//

using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections;
using System.Collections.Generic;
using Fluint.Layer.Mathematics;
using Fluint.Layer.Graphics;
using System.Runtime.InteropServices;

namespace Fluint.Engine.GL46.Graphics
{
    public class VertexArrayObject<VertexType> : IVertexLayout<VertexType> where VertexType : struct
    {
        private List<VertexLayoutAttribute> _attributes;
        public int VertexSize { get; private set; }
        public int Handle { get; private set; }

        public void Bind()
        {
            GL.BindVertexArray(Handle);
        }

        public void Unbind()
        {
            GL.BindVertexArray(0);
        }

        public void Load()
        {
            Bind();
            GL.EnableVertexAttribArray(0);
        }

        private void Add(string name, VertexLayoutAttributeType type, int components)
        {
            _attributes.Add(new VertexLayoutAttribute(name, type, components));
        }

        public void Enable()
        {
            Bind();
            for (int i = 0; i < _attributes.Count; i++)
            {
                var attribute = _attributes[i];
                var type = GetVertexPointerType(attribute.AttributeType);
                var offset = Marshal.OffsetOf<VertexType>(attribute.Name);

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

        private static VertexAttribPointerType GetVertexPointerType(VertexLayoutAttributeType type)
        {
            return type switch
            {
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

        public void Calculate()
        {
            //Handle = GL.GenVertexArray();
            _attributes = new List<VertexLayoutAttribute>();
            Handle = GL.GenVertexArray();

            var type = typeof(VertexType);
            var properties = type.GetFields();

            foreach (var property in properties)
            {
                //TODO: Finish these..
                var @switch = new Dictionary<Type, Action>
                {
                    { typeof(int),     () => Add(property.Name, VertexLayoutAttributeType.Int, 1)                  },
                    { typeof(uint),    () => Add(property.Name, VertexLayoutAttributeType.UnsignedInt, 1)          },
                    { typeof(short),   () => Add(property.Name, VertexLayoutAttributeType.Short, 1)                },
                    { typeof(ushort),  () => Add(property.Name, VertexLayoutAttributeType.UnsignedShort, 1)        },
                    { typeof(float),   () => Add(property.Name, VertexLayoutAttributeType.Float, 1)                },
                    { typeof(double),  () => Add(property.Name, VertexLayoutAttributeType.Double, 1)               },
                    { typeof(Vector2), () => Add(property.Name, VertexLayoutAttributeType.Float, 2)                },
                    { typeof(Vector3), () => Add(property.Name, VertexLayoutAttributeType.Float, 3)                },
                    { typeof(Vector4), () => Add(property.Name, VertexLayoutAttributeType.Float, 4)                },
                };
                @switch[property.FieldType]();
            }
            VertexSize = Marshal.SizeOf(type);
        }
    }
}
